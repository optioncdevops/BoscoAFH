using AutoMapper;
using BoscoAFH.Common;
using BoscoAFH.Entities.Models;
using BoscoAFH.MasterInfrastructure.Interfaces;
using BoscoAFH.MasterInfrastructure.Models.Input;
using BoscoAFH.MasterService.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoscoAFH.MasterService.Service
{
    /// <summary>
    /// Service class for managing users.
    /// </summary>
    public class UserService(IDBRepository<User> dbRepository, IMapper mapper, ILogger<UserService> logger) : IUserService
    {
        #region Properties

        private readonly IDBRepository<User> _dbRepository = dbRepository ?? throw new ArgumentNullException(nameof(dbRepository));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        private readonly ILogger<UserService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        #endregion

        #region Get Methods

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        public async Task<ResultArgs> GetUsersAsync()
        {
            try
            {
                var data = (await _dbRepository.GetAllAsync(
                    user => new UserDTO
                    {
                        UserId = user.UserId,
                        UserName = user.UserName.Trim(),
                        FullName = user.FullName.Trim(),
                        Email = user.Email.Trim(),
                        MobileNo = user.MobileNo != null ? user.MobileNo.Trim() : null,
                        IsActive = user.IsActive,
                        IsDeleted = user.IsDeleted
                    }))
                    .AsQueryable()
                    .AsNoTracking()
                    .OrderBy(a => a.UserName)
                    .ToList();

                return data != null ?
                    new ResultArgs { StatusCode = ErrorCodes.Success, StatusMessage = ErrorMessages.Success, ResultData = data }
                    : new ResultArgs { StatusCode = ErrorCodes.NoRecordFound, StatusMessage = ErrorMessages.NoRecordFound };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, SerilogErrorMessages.GetFailed);
                return new ResultArgs { StatusCode = ErrorCodes.InternalServerError, StatusMessage = ErrorMessages.InternalServerError };
            }
        }

        /// <summary>
        /// Retrieves a user by their ID.
        /// </summary>
        public async Task<ResultArgs> GetUsersbyIdAsync(long id)
        {
            try
            {
                ResultArgs resultArgs = new ResultArgs();
                User? user = await _dbRepository.GetByIdAsync(id);

                if (user != null)
                {
                    UserDTO mappedUser = new UserDTO()
                    {
                        UserId = user.UserId,
                        UserName = user.UserName,
                        FullName = user.FullName,
                        Email = user.Email,
                        MobileNo = user.MobileNo,
                        IsActive = user.IsActive,
                        IsDeleted = user.IsDeleted
                    };
                    resultArgs.ResultData = mappedUser;
                }
                else
                {
                    resultArgs.StatusCode = ErrorCodes.NoRecordFound;
                    resultArgs.StatusMessage = ErrorMessages.NoRecordFound;
                }
                return resultArgs;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, SerilogErrorMessages.GetFailed);
                return new ResultArgs { StatusCode = ErrorCodes.InternalServerError, StatusMessage = ErrorMessages.InternalServerError };
            }
        }

        #endregion

        #region Save & Update Methods

        /// <summary>
        /// Saves or updates a user.
        /// </summary>
        public async Task<ResultArgs> SaveUsersAsync(UserDTO dto)
        {
            try
            {
                // Check if username already exists
                var existingUserWithName = (await _dbRepository.GetAllAsync())
                    .FirstOrDefault(x => x.UserName.Equals(dto.UserName, StringComparison.OrdinalIgnoreCase) && x.UserId != dto.UserId);
                if (existingUserWithName != null)
                {
                    return new ResultArgs { StatusCode = ErrorCodes.Conflict, StatusMessage = "Username already exists." };
                }

                // Check if email already exists
                var existingUserWithEmail = (await _dbRepository.GetAllAsync())
                    .FirstOrDefault(x => x.Email.Equals(dto.Email, StringComparison.OrdinalIgnoreCase) && x.UserId != dto.UserId);
                if (existingUserWithEmail != null)
                {
                    return new ResultArgs { StatusCode = ErrorCodes.Conflict, StatusMessage = "Email already exists." };
                }

                if (dto.UserId == 0)
                {
                    if (string.IsNullOrEmpty(dto.Password))
                    {
                        return new ResultArgs { StatusCode = ErrorCodes.Failed, StatusMessage = "Password is required for new users." };
                    }

                    var userEntity = _mapper.Map<User>(dto);
                    userEntity.PasswordHash = EncryptionHelper.HashPassword(dto.Password);
                    userEntity.CreatedAt = DateTime.UtcNow;

                    int rowsAffected = await _dbRepository.SaveAsync(userEntity);
                    return new ResultArgs
                    {
                        ResultData = rowsAffected,
                        StatusCode = rowsAffected > 0 ? ErrorCodes.Created : ErrorCodes.Failed,
                        StatusMessage = rowsAffected > 0 ? ErrorMessages.SaveSuccess : ErrorMessages.SaveFailed
                    };
                }

                var existingUser = await _dbRepository.GetByIdAsync(dto.UserId);
                if (existingUser == null)
                {
                    return new ResultArgs { StatusCode = ErrorCodes.NoRecordFound, StatusMessage = ErrorMessages.NoRecordFound };
                }

                // Preserve original password hash if a new password wasn't provided
                string originalPasswordHash = existingUser.PasswordHash;
                DateTime originalCreatedAt = existingUser.CreatedAt;

                _mapper.Map(dto, existingUser);

                if (!string.IsNullOrEmpty(dto.Password))
                {
                    existingUser.PasswordHash = EncryptionHelper.HashPassword(dto.Password);
                }
                else
                {
                    existingUser.PasswordHash = originalPasswordHash;
                }

                existingUser.CreatedAt = originalCreatedAt;

                int updatedRows = await _dbRepository.UpdateAsync(existingUser);
                return new ResultArgs
                {
                    ResultData = updatedRows,
                    StatusCode = updatedRows > 0 ? ErrorCodes.Updated : ErrorCodes.Failed,
                    StatusMessage = updatedRows > 0 ? ErrorMessages.UpdateSuccess : ErrorMessages.SaveFailed
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, SerilogErrorMessages.GetFailed);
                return new ResultArgs { StatusCode = ErrorCodes.InternalServerError, StatusMessage = ErrorMessages.InternalServerError };
            }
        }

        #endregion

        #region Delete Methods

        /// <summary>
        /// Deletes a user by marking it as deleted.
        /// </summary>
        public async Task<ResultArgs> DeleteUsersAsync(long id)
        {
            try
            {
                var user = await _dbRepository.GetByIdAsync(id);
                if (user == null)
                {
                    return new ResultArgs { StatusCode = ErrorCodes.NoRecordFound, StatusMessage = ErrorMessages.NoRecordFound };
                }

                user.IsDeleted = true;
                int rowsAffected = await _dbRepository.UpdateAsync(user);
                return new ResultArgs
                {
                    StatusCode = rowsAffected > 0 ? ErrorCodes.Success : ErrorCodes.Failed,
                    StatusMessage = rowsAffected > 0 ? ErrorMessages.DeleteSuccess : ErrorMessages.DeleteFailed
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, SerilogErrorMessages.DeleteFailed);
                return new ResultArgs { StatusCode = ErrorCodes.InternalServerError, StatusMessage = ErrorMessages.InternalServerError };
            }
        }

        #endregion
    }
}
