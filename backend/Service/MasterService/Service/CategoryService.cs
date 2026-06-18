using AutoMapper;
using BoscoAFH.Common;
using BoscoAFH.Entities.Models;
using BoscoAFH.MasterInfrastructure.Interfaces;
using BoscoAFH.MasterInfrastructure.Repositorys;
using BoscoAFH.MasterService.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoscoAFH.MasterService.Service
{

    /// <summary>
    /// Service class for managing roles and permissions.
    /// </summary>
    public class CategoryService(ICategoryRepository _category, IMapper _mapper, ILogger<RoleService> _logger) : ICategoryService
    {
        public async Task<ResultArgs> GetCategoryAsync()
        {
            try
            {
                var data = await _category.GetCategoryAsync();
                return data != null ?
                    new ResultArgs { ResultData = data }
                     : new ResultArgs { StatusCode = ErrorCodes.NoRecordFound, StatusMessage = ErrorMessages.NoRecordFound };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, SerilogErrorMessages.GetFailed);
                return new ResultArgs { StatusCode = ErrorCodes.InternalServerError, StatusMessage = ErrorMessages.InternalServerError };
            }
        }
        public async Task<ResultArgs> GetCategoryListAsync()
        {
            try
            {
                var data = await _category.GetCategoryListAsync();
                return data != null ?
                    new ResultArgs { ResultData = data }
                     : new ResultArgs { StatusCode = ErrorCodes.NoRecordFound, StatusMessage = ErrorMessages.NoRecordFound };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, SerilogErrorMessages.GetFailed);
                return new ResultArgs { StatusCode = ErrorCodes.InternalServerError, StatusMessage = ErrorMessages.InternalServerError };
            }
        }

    }
}
