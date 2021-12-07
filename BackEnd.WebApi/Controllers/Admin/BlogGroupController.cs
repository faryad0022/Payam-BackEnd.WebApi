using BackEnd.Core.DTOs.Blog;
using BackEnd.Core.Services.Interfaces;
using BackEnd.Core.utilities.Common;
using BackEnd.Core.ViewModels.Blog;
using BackEnd.WebApi.Controllers.Site;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BackEnd.WebApi.Controllers.Admin
{
    public class BlogGroupController : SiteBaseController
    {
        #region Constructor
        private readonly IBlogGroupService blogGroupService;
        public BlogGroupController(IBlogGroupService blogGroupService)
        {
            this.blogGroupService = blogGroupService;
        }
        #endregion

        #region Get
        [HttpGet("filter-blogGroups")]
        public async Task<IActionResult> GetFilterBlogGroups([FromQuery] FilterBlogGroupDTO filter)
        {
            var blogGroups = await blogGroupService.GetFilterBlogGourps(filter);
            return JsonResponseStatus.Success(blogGroups);
        }
        #endregion

        #region Add BlogGroup
        [HttpPost("add-new-bloggroup")]
        public async Task<IActionResult> AddBlogGroup([FromBody] BlogGroupDTO blogGroupDTO)
        {
            if (!ModelState.IsValid) return JsonResponseStatus.ModelError();
            if (!await blogGroupService.AddBlogGroup(blogGroupDTO)) return JsonResponseStatus.ServerError();
            if (!await blogGroupService.CheckUniqueTitleAsync(blogGroupDTO.Title)) return JsonResponseStatus.Duplicate();
            var returnBlogGroup = new VmReturnBlogGroup
            {
                Title = blogGroupDTO.Title,
                Description = blogGroupDTO.Description
            };
            return JsonResponseStatus.Success(returnBlogGroup);
        }
        #endregion
    }
}
