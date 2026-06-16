using BoscoAFH.CommonService;
using BoscoAFH.MasterInfrastructure.Models.Input;
using BoscoAFH.MasterService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace BoscoAFH.Master.Controllers
{
    [ApiController]
    [ApiExplorerSettings(GroupName = SwaggerModuleDoc.BoscoAFHMaster)]
    [Authorize]
    public class UserController(IUserService _userService) : BaseController
    {
        #region Methods

        /// <summary>
        /// Retrieve all users.
        /// </summary>
        /// <remarks>
        /// This endpoint fetches a list of all users available in the system.
        /// </remarks>
        /// <returns>A list of users. <see cref="UserDTO"/></returns>
        /// <response code="200">Returns the list of users.</response>
        /// <response code="204">No Users found.</response>
        /// <response code="500">If an error occurs while fetching users.</response>
        [HttpGet]
        [ActionName(APIActionName.API_User.getUserAsync)]
        public async Task<IActionResult> GetUsersAsync()
        {
            return ApiResultArgs(await _userService.GetUsersAsync(), APIHttpType.HttpGet);
        }

        /// <summary>
        /// Retrieve a user by their ID.
        /// </summary>
        /// <remarks>
        /// This endpoint fetches details of a specific user based on the provided ID.
        /// </remarks>
        /// <param name="id">The unique identifier of the user.</param>
        /// <returns>Details of the user.</returns>
        /// <response code="200">Returns the user details.</response>
        /// <response code="204">No User found.</response>
        /// <response code="500">If an error occurs while fetching the user.</response>
        [HttpGet]
        [ActionName(APIActionName.API_User.getUserbyIdAsync)]
        public async Task<IActionResult> GetUsersbyIdAsync([Required] long id)
        {
            return ApiResultArgs(await _userService.GetUsersbyIdAsync(id), APIHttpType.HttpGet);
        }

        /// <summary>
        /// Delete a user by their ID.
        /// </summary>
        /// <remarks>
        /// This endpoint soft-deletes a user based on the provided ID.
        /// </remarks>
        /// <param name="id">The unique identifier of the user.</param>
        /// <returns>A confirmation message.</returns>
        /// <response code="205">If the user is successfully deleted.</response>
        /// <response code="204">If the user is not found.</response>
        /// <response code="500">If an error occurs while deleting the user.</response>
        [HttpDelete]
        [ActionName(APIActionName.API_User.deleteUserAsync)]
        public async Task<IActionResult> DeleteUserAsync([Required] long id)
        {
            return ApiResultArgs(await _userService.DeleteUsersAsync(id), APIHttpType.HttpDelete);
        }

        /// <summary>
        /// Create a new user.
        /// </summary>
        /// <remarks>
        /// This endpoint allows the creation of a new user in the system.
        /// </remarks>
        /// <param name="input">The user details to create.</param>
        /// <returns>The details of the created user.</returns>
        /// <response code="201">If the user is successfully created.</response>
        /// <response code="202">If the user is updated successfully.</response>
        /// <response code="203">If save new or update failed.</response>
        /// <response code="204">If the input is invalid.</response>
        /// <response code="409">If username or email already exists.</response>
        /// <response code="500">If an error occurs while creating the user.</response>
        [HttpPost]
        [ActionName(APIActionName.API_User.saveUserAsync)]
        public async Task<IActionResult> SaveUserAsync([Required] UserDTO input)
        {
            return ApiResultArgs(await _userService.SaveUsersAsync(input), APIHttpType.HttpPost);
        }

        /// <summary>
        /// Update an existing user.
        /// </summary>
        /// <param name="input">The user details to update.</param>
        /// <returns>A confirmation message.</returns>
        [HttpPut]
        [ActionName(APIActionName.API_User.updateUserAsync)]
        public async Task<IActionResult> UpdateUserAsync([Required] UserDTO input)
        {
            return ApiResultArgs(await _userService.SaveUsersAsync(input), APIHttpType.HttpPut);
        }

        #endregion Methods
    }
}
