using BoscoAFH.Common;
using BoscoAFH.Entities.Data;
using BoscoAFH.MasterInfrastructure.Interfaces;
using BoscoAFH.MasterInfrastructure.Models.Input;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoscoAFH.MasterInfrastructure.Repositorys
{
    /// <summary>
    /// Repository class to manage user roles and retrieve related data.
    /// </summary>
    public class RoleRepository(BoscoAFHDbContext context, ILogger<RoleRepository> logger) : IRoleRepository
    {
        #region Properties

        /// <summary>
        /// The database context.
        /// </summary>
        private readonly BoscoAFHDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<RoleRepository> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        /// <summary>
        /// The current user service.
        /// </summary>
        #endregion Properties

        #region User Roles Retrieval

        /// <summary>
        /// Retrieves all user roles asynchronously.
        /// </summary>
        /// <returns>A list of user roles.</returns>
        public async Task<List<RoleDTO>> GetRolesAsync()
        {
            try
            {
                return await _context.Roles.AsNoTracking()
        .Select(r => new RoleDTO
        {
            RoleId = r.RoleId,
            RoleName = r.RoleName,
            IsActive = r.IsActive,
            IsDeleted = r.IsDeleted,
            IsReferenced = _context.Users.Any(u => u.RoleId == r.RoleId),
        }).OrderBy(x => x.RoleName)
        .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, SerilogErrorMessages.GetFailed);
                return new List<RoleDTO>(); // Return an empty list in case of an error
            }
        }

        
        #endregion User Roles Retrieval
    }
}
