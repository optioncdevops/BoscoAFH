using BoscoAFH.MasterInfrastructure.Models.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoscoAFH.MasterInfrastructure.Interfaces
{
    /// <summary>
    /// Interface for authentication repository.
    /// </summary>
    public interface IAuthenticateRepository
    {
        /// <summary>
        /// Authenticates a user based on the provided credentials.
        /// </summary>
        /// <param name="user">The user credentials to authenticate.</param>
        /// <returns>A task containing the user details if authentication is successful.</returns>
        Task<UserDetailResult> AuthenticateUserAsync(UserCredential user);

    }
}
