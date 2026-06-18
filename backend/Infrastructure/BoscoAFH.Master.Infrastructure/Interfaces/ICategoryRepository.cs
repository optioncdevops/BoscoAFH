using BoscoAFH.MasterInfrastructure.Models.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoscoAFH.MasterInfrastructure.Interfaces
{
    /// <summary>
    /// The interface for the user roles repository, providing methods to retrieve user roles and related modules.
    /// </summary>
    public interface ICategoryRepository
    {
        /// <summary>
        /// Retrieves all user role asynchronously.
        /// </summary>
        /// <returns>A list of user roles.</returns>
        Task<dynamic> GetCategoryAsync();
        Task<List<CategoryDTO>> GetCategoryListAsync();

    }
}
