using BoscoAFH.Common;
using BoscoAFH.MasterInfrastructure.Interfaces;
using BoscoAFH.MasterInfrastructure.Models.Input;
using BoscoAFH.MasterService.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoscoAFH.MasterService.Service
{
    /// <summary>
    /// Service class for handling authentication-related tasks.
    /// </summary>
    public class AuthenticateService(IAuthenticateRepository _repository, ILogger<AuthenticateService> logger) : IAuthenticateService
    {


      

        /// <summary>
        /// Authenticates a user asynchronously using the provided user credentials.
        /// </summary>
        /// <param name="user">User credentials to authenticate.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the authentication result.</returns>
        public async Task<ResultArgs> AuthenticateUserAsync(UserCredential user)
        {
            var resultArgs = new ResultArgs();
            try
            { 
                string pwd =  EncryptionHelper.HashPassword(user.Password);

                UserDetailResult objResult = await _repository.AuthenticateUserAsync(user);
                if (objResult.UserDetails != null)
                {
                   
                    if (EncryptionHelper.VerifyPassword(objResult.UserDetails.Password, user.Password))
                    {
                        if (objResult.UserDetails.IsActive.HasValue && objResult.UserDetails.IsActive.Value)
                        {
                            resultArgs.StatusCode = ErrorCodes.Success;
                            resultArgs.StatusMessage = ErrorMessages.Success;
                            resultArgs.ResultData = objResult;
                        }
                        else
                        {
                            resultArgs.StatusCode = ErrorCodes.CustomMessage;
                            resultArgs.StatusMessage = ErrorMessages.InActiveUser;
                        }
                    }
                    else
                    {
                        resultArgs.StatusCode = ErrorCodes.CustomMessage;
                        resultArgs.StatusMessage = ErrorMessages.PasswordIncorrect;
                    }
                }
                else
                {
                    if (objResult.StatusCode == -91)
                    {
                        resultArgs.StatusCode = ErrorCodes.CustomMessage;
                        resultArgs.StatusMessage = ErrorMessages.InCompleteUser;
                    }
                    else
                    {
                        resultArgs.StatusCode = ErrorCodes.NoRecordFound;
                        resultArgs.StatusMessage = ErrorMessages.UserNameNotExists;
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.StatusCode = ErrorCodes.InternalServerError;
                resultArgs.StatusMessage = ErrorMessages.InternalServerError;
            }
            return resultArgs;
        }
    }
}
