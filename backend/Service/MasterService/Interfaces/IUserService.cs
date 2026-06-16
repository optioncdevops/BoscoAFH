using BoscoAFH.Common;
using BoscoAFH.MasterInfrastructure.Models.Input;
using System.Threading.Tasks;

namespace BoscoAFH.MasterService.Interfaces
{
    /// <summary>
    /// The IUserService interface defines the methods for managing users in the system.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Retrieves all users from the system.
        /// </summary>
        /// <returns>A ResultArgs object containing the result of the operation.</returns>
        Task<ResultArgs> GetUsersAsync();

        /// <summary>
        /// Retrieves a specific user from the system by its ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>A ResultArgs object containing the result of the operation.</returns>
        Task<ResultArgs> GetUsersbyIdAsync(long id);

        /// <summary>
        /// Saves or updates a user to the system.
        /// </summary>
        /// <param name="user">The UserDTO object containing the details of the user to save.</param>
        /// <returns>A ResultArgs object containing the result of the operation.</returns>
        Task<ResultArgs> SaveUsersAsync(UserDTO user);

        /// <summary>
        /// Deletes a user from the system by its ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>A ResultArgs object containing the result of the operation.</returns>
        Task<ResultArgs> DeleteUsersAsync(long id);
    }
}
