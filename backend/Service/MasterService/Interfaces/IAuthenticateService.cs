using BoscoAFH.Common;
using BoscoAFH.MasterInfrastructure.Models.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoscoAFH.MasterService.Interfaces
{
    /// <summary>
    /// Interface for authentication services
    /// </summary>
    public interface IAuthenticateService
    {
        /// <summary>
        /// Authenticates a user asynchronously
        /// </summary>
        /// <param name="user">User credentials</param>
        /// <returns>Result of the authentication</returns>
        Task<ResultArgs> AuthenticateUserAsync(UserCredential user);
    }
}
