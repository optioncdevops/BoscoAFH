using BoscoAFH.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoscoAFH.MasterService.Interfaces
{

    /// <summary>
    /// The IRoleService interface defines the methods for managing roles in the system.
    /// </summary>
    public interface ICategoryService
    {
        /// <summary>
        /// Retrieves all roles from the system.
        /// </summary>
        /// <returns>A ResultArgs object containing the result of the operation.</returns>
        Task<ResultArgs> GetCategoryAsync();
        Task<ResultArgs> GetCategoryListAsync();
    }
}
