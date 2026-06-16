using AutoMapper;
using BoscoAFH.Common;
using BoscoAFH.Entities.Models;
using BoscoAFH.MasterInfrastructure.Interfaces;
using BoscoAFH.MasterInfrastructure.Models.Input;
using BoscoAFH.MasterService.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data;

namespace BoscoAFH.MasterService.Service
{
    /// <summary>
    /// Service class for managing roles and permissions.
    /// </summary>
    public class RoleService(IDBRepository<Role> _dbRepository, IRoleRepository _roleRepository, IMapper _mapper, ILogger<RoleService> _logger) : IRoleService
    {
       

        #region Get Methods

        /// <summary>
        /// Retrieves all roles.
        /// </summary>
        public async Task<ResultArgs> GetRolesAsync()
        {
            try
            {

                var data = await _roleRepository.GetRolesAsync();

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
        /// Retrieves a role by its ID.
        /// </summary>
        public async Task<ResultArgs> GetRolesbyIdAsync(long id)
        {
            try
            {
                ResultArgs resultArgs = new ResultArgs();

                Role? role = await _dbRepository.GetByIdAsync(id);
              
                if (role != null)
                {
                    RoleDTO mappedRole = new RoleDTO()
                    {
                        RoleId = role?.RoleId ?? 0,
                        RoleName = role?.RoleName ?? string.Empty,
                        Description = role?.Description ?? string.Empty,
                        IsActive = role?.IsActive ?? false
                    };
                    resultArgs.ResultData = mappedRole;
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
        /// Saves or updates a role.
        /// </summary>
        public async Task<ResultArgs> SaveRolesAsync(RoleDTO dto)
        {
            try
            {
                var existingRoleWithName = (await _dbRepository.GetAllAsync()).FirstOrDefault(x => x.RoleName == dto.RoleName && x.RoleId != dto.RoleId);
                if (existingRoleWithName != null)
                {
                    return new ResultArgs { StatusCode = ErrorCodes.Conflict, StatusMessage = ErrorMessages.Exist };
                }

                if (dto.RoleId == 0)
                {
                    var roleEntity = _mapper.Map<Role>(dto);
                    int rowsAffected = await _dbRepository.SaveAsync(roleEntity);
                    return new ResultArgs { ResultData = rowsAffected, StatusCode = rowsAffected > 0 ? ErrorCodes.Created : ErrorCodes.Failed, StatusMessage = rowsAffected > 0 ? ErrorMessages.SaveSuccess : ErrorMessages.SaveFailed };
                }

                var existingRole = await _dbRepository.GetByIdAsync(dto.RoleId);
                if (existingRole == null)
                {
                    return new ResultArgs { StatusCode = ErrorCodes.NoRecordFound, StatusMessage = ErrorMessages.NoRecordFound };
                }

                _mapper.Map(dto, existingRole);
                int updatedRows = await _dbRepository.UpdateAsync(existingRole);
                return new ResultArgs { ResultData = updatedRows, StatusCode = updatedRows > 0 ? ErrorCodes.Updated : ErrorCodes.Failed, StatusMessage = updatedRows > 0 ? ErrorMessages.UpdateSuccess : ErrorMessages.SaveFailed };
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
        /// Deletes a role by marking it as deleted.
        /// </summary>
        public async Task<ResultArgs> DeleteRolesAsync(long id)
        {
            try
            {
                var role = await _dbRepository.GetByIdAsync(id);
                if (role == null)
                {
                    return new ResultArgs { StatusCode = ErrorCodes.NoRecordFound, StatusMessage = ErrorMessages.NoRecordFound };
                }

                role.IsDeleted = true;
                int rowsAffected = await _dbRepository.UpdateAsync(role);
                return new ResultArgs { StatusCode = rowsAffected > 0 ? ErrorCodes.Success : ErrorCodes.Failed, StatusMessage = rowsAffected > 0 ? ErrorMessages.DeleteSuccess : ErrorMessages.DeleteFailed };
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
