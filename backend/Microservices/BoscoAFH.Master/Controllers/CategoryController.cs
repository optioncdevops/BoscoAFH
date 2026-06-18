using BoscoAFH.CommonService;
using BoscoAFH.MasterInfrastructure.Models.Input;
using BoscoAFH.MasterService.Interfaces;

namespace BoscoAFH.Master.Controllers
{



    [ApiController]
    [ApiExplorerSettings(GroupName = SwaggerModuleDoc.BoscoAFHMaster)]
    [Authorize]
    public class CategoryController(ICategoryService _category) : BaseController
    {

        #region Methods

        /// <summary>
        /// Retrieve all Category.
        /// </summary>
        /// <remarks>
        /// This endpoint fetches a list of all Category available in the system.
        /// </remarks>
        /// <returns>A list of Category. </returns>
        /// <response code="200">Returns the list of Category.</response>
        /// <response code="204">No Category found.</response>
        /// <response code="500">If an error occurs while fetching Category.</response>
        [HttpGet]
        [ActionName(APIActionName.API_Category.getCategoryAsync)]
        public async Task<IActionResult> GetCategoryAsync()
        {
            return ApiResultArgs(await _category.GetCategoryAsync(), APIHttpType.HttpGet);
        }
        [HttpGet]
        [ActionName(APIActionName.API_Category.getCategoryListAsync)]
        public async Task<IActionResult> GetCategoryListAsync()
        {
            return ApiResultArgs(await _category.GetCategoryListAsync(), APIHttpType.HttpGet);
        }
        #endregion Methods
    }

}
