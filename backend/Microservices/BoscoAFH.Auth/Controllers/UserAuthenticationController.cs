using BoscoAFH.CommonService;
using BoscoAFH.MasterInfrastructure.Models.Input;
using BoscoAFH.MasterService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BoscoAFH.Auth.Controllers
{
    [ApiController]
    [ApiExplorerSettings(GroupName = SwaggerModuleDoc.BoscoAFHAuth)]
    [AllowAnonymous]
    public class UserAuthenticationController(IAuthenticateService userAuthenticate, IJwtTokenGenerator jwtToken, ILogger<UserAuthenticationController> logger) : BaseController
    {
        #region Private Members

        private readonly IAuthenticateService _userAuthenticate = userAuthenticate ?? throw new ArgumentNullException(nameof(userAuthenticate), "Authentication Service cannot be null");
        private readonly ILogger<UserAuthenticationController> _logger = logger ?? throw new ArgumentNullException(nameof(_logger), "Logger Service cannot be null");
        private readonly IJwtTokenGenerator _jwtToken = jwtToken ?? throw new ArgumentNullException(nameof(jwtToken), "JWTTokenGenerator Service cannot be null");

        #endregion Private Members

        /// <summary>
        /// To login a user
        /// </summary>
        /// <param name="user">User credentials</param>
        /// <returns>users data to set in session <see cref="UserDetailResult" /></returns>
        /// <response code="200">Logged in successfully</response>
        /// <response code="204">No user found.</response>
        /// <response code="102">for warnings like invalid password and inactive account</response>
        [HttpPost]
        [ActionName(APIActionName.API_Login.userAuthenticateAsync)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status102Processing)]
        public async Task<IActionResult> UserAuthenticateAsync([FromBody, Required] UserCredential user)
        {
            ResultArgs objResult = null;
            try
            {
                if (user == null)
                {
                    return BadRequest();
                }
                objResult = await _userAuthenticate.AuthenticateUserAsync(user);

                if (objResult.StatusCode == ErrorCodes.Success)
                {
                    UserDetailResult userDetail = (UserDetailResult)objResult.ResultData;
                    if (userDetail != null
                        )
                    {
                        userDetail.UserDetails.Password = "";
                        userDetail.UserDetails.Token = _jwtToken.GenerateToken(userDetail.UserDetails);
                    }
                    objResult.ResultData = userDetail;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, SerilogErrorMessages.AuthFailed, user.UserName);
                objResult.StatusCode = ErrorCodes.InternalServerError;
                objResult.StatusMessage = ErrorMessages.InternalServerError;
            }
            return ApiResultArgs(objResult, APIHttpType.HttpPost);
        }

    }
}
