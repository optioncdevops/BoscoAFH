using BoscoAFH.Common;
using BoscoAFH.MasterInfrastructure.Models.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoscoAFH.MasterService.Interfaces
{
    /// <summary>
    /// The IRoleService interface defines the methods for managing roles in the system.
    /// </summary>
    public interface IRoleService
    {
        /// <summary>
        /// Retrieves all roles from the system.
        /// </summary>
        /// <returns>A ResultArgs object containing the result of the operation.</returns>
        Task<ResultArgs> GetRolesAsync();

        /// <summary>
        /// Retrieves a specific role from the system by its ID.
        /// </summary>
        /// <param name="id">The ID of the role to retrieve.</param>
        /// <returns>A ResultArgs object containing the result of the operation.</returns>
        Task<ResultArgs> GetRolesbyIdAsync(long id);

        /// <summary>
        /// Saves a new role to the system.
        /// </summary>
        /// <param name="role">The RoleDTO object containing the details of the role to save.</param>
        /// <returns>A ResultArgs object containing the result of the operation.</returns>
        Task<ResultArgs> SaveRolesAsync(RoleDTO role);

        /// <summary>
        /// Deletes a role from the system by its ID.
        /// </summary>
        /// <param name="id">The ID of the role to delete.</param>
        /// <returns>A ResultArgs object containing the result of the operation.</returns>
        Task<ResultArgs> DeleteRolesAsync(long id);
         
    }
}
