using BackEnd.Core.DTOs.Blog;
using BackEnd.Core.Services.Interfaces;
using BackEnd.Core.utilities.Common;
using BackEnd.Core.ViewModels.Blog;
using BackEnd.WebApi.Controllers.Site;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BackEnd.WebApi.Controllers.Admin
{
    public class BlogGroupController : PanelBaseController
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
        public async Task<IActionResult> AddBlogGroup([FromBody] BlogGroupDTO blogGroupDTO, [FromQuery] FilterBlogGroupDTO filter)
        {
            if (!ModelState.IsValid) return JsonResponseStatus.ModelError();
            if (await blogGroupService.CheckUniqueTitleAsync(blogGroupDTO.Title)) return JsonResponseStatus.Duplicate();
            if (!await blogGroupService.AddBlogGroup(blogGroupDTO)) return JsonResponseStatus.ServerError();
            var blogGroups = await blogGroupService.GetFilterBlogGourps(filter);
            return JsonResponseStatus.Success(blogGroups);
        }
        #endregion

        #region Edit BlogGroup
        [HttpPost("edit-bloggroup")]
        public async Task<IActionResult> EditBlogGroup([FromBody] BlogGroupDTO blogGroupDTO, [FromQuery] FilterBlogGroupDTO filter)
        {
            if (blogGroupDTO == null) return JsonResponseStatus.NotFound();

            if (!ModelState.IsValid) return JsonResponseStatus.ModelError();
            var blogGroup = await blogGroupService.GetBlogGroupByIdAsync(blogGroupDTO.Id);

            if (await blogGroupService.CheckUniqueTitleAsync(blogGroupDTO.Title) && blogGroup.Title != blogGroupDTO.Title) return JsonResponseStatus.Duplicate();
            if (!await blogGroupService.EditBlogGroup(blogGroupDTO)) return JsonResponseStatus.ServerError();
            var blogGroups = await blogGroupService.GetFilterBlogGourps(filter);
            return JsonResponseStatus.Success(blogGroups);
        }
        #endregion


        #region Remove BlogGroup
        [HttpPost("remove-bloggroup")]
        public async Task<IActionResult> RemoveBlogGroup([FromBody] BlogGroupDTO blogGroupDTO, [FromQuery] FilterBlogGroupDTO filter)
        {
            if (!ModelState.IsValid) return JsonResponseStatus.ModelError();
            if (!await blogGroupService.RemoveBlogGroup(blogGroupDTO)) return JsonResponseStatus.ServerError();
            var blogGroups = await blogGroupService.GetFilterBlogGourps(filter);
            return JsonResponseStatus.Success(blogGroups);
        }
        #endregion
    }
}
