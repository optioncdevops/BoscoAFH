using BoscoAFH.CommonService;
using BoscoAFH.MasterInfrastructure.Models.Input;
using BoscoAFH.MasterService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BoscoAFH.Master.Controllers
{
   

    [ApiController]
    [ApiExplorerSettings(GroupName = SwaggerModuleDoc.BoscoAFHMaster)]
    [Authorize]
    public class RoleController(IRoleService _roleRepository) : BaseController
    {

        #region Methods

        /// <summary>
        /// Retrieve all roles.
        /// </summary>
        /// <remarks>
        /// This endpoint fetches a list of all roles available in the system.
        /// </remarks>
        /// <returns>A list of roles. <see cref="RoleDTO"/></returns>
        /// <response code="200">Returns the list of roles.</response>
        /// <response code="204">No Roles found.</response>
        /// <response code="500">If an error occurs while fetching roles.</response>
        [HttpGet]
        [ActionName(APIActionName.API_Role.getRoleAsync)]
        public async Task<IActionResult> GetRolesAsync()
        {
            return ApiResultArgs(await _roleRepository.GetRolesAsync(), APIHttpType.HttpGet);
        }

        /// <summary>
        /// Retrieve a role by its ID.
        /// </summary>
        /// <remarks>
        /// This endpoint fetches details of a specific role based on the provided ID.
        /// </remarks>
        /// <param name="id">The unique identifier of the role.</param>
        /// <returns>Details of the role.</returns>
        /// <response code="200">Returns the role details.</response>
        /// <response code="204">No Role found.</response>
        /// <response code="500">If an error occurs while fetching the role.</response>
        [HttpGet]
        [ActionName(APIActionName.API_Role.getRolesbyIdAsync)]
        public async Task<IActionResult> GetRolesbyIdAsync([Required] long id)
        {
            return ApiResultArgs(await _roleRepository.GetRolesbyIdAsync(id), APIHttpType.HttpGet);
        }

         

        /// <summary>
        /// Delete a role by its ID.
        /// </summary>
        /// <remarks>
        /// This endpoint deletes a role based on the provided ID.
        /// </remarks>
        /// <param name="id">The unique identifier of the role.</param>
        /// <returns>A confirmation message.</returns>
        /// <response code="205">If the role is successfully deleted.</response>
        /// <response code="204">If the role is not found.</response>
        /// <response code="500">If an error occurs while deleting the role.</response>
        [HttpDelete]
        [ActionName(APIActionName.API_Role.deleteRoleAsync)]
        public async Task<IActionResult> DeleteRoleAsync([Required] long id)
        {
            return ApiResultArgs(await _roleRepository.DeleteRolesAsync(id), APIHttpType.HttpDelete);
        }

        /// <summary>
        /// Create a new role.
        /// </summary>
        /// <remarks>
        /// This endpoint allows the creation of a new role in the system.
        /// </remarks>
        /// <param name="input">The role details to create.</param>
        /// <returns>The details of the created role.</returns>
        /// <response code="201">If the role is successfully created.</response>
        /// <response code="202">If the role is updated  successfully.</response>
        /// <response code="203">If save new or update failed.</response>
        /// <response code="204">If the input is invalid.</response>
        /// <response code="409">If Role name already exists.</response>
        /// <response code="500">If an error occurs while creating the role.</response>
        [HttpPost]
        [ActionName(APIActionName.API_Role.saveRoleAsync)]
        public async Task<IActionResult> SaveRoleAsync([Required] RoleDTO input)
        {
            return ApiResultArgs(await _roleRepository.SaveRolesAsync(input), APIHttpType.HttpPost);
        }

        /// <summary>
        ///  Update the user role
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [ActionName(APIActionName.API_Role.updateRoleAsync)]
        public async Task<IActionResult> UpdateRoleAsync([Required] RoleDTO input)
        {
            return ApiResultArgs(await _roleRepository.SaveRolesAsync(input), APIHttpType.HttpPut);
        }
         
        #endregion Methods

    }
}
