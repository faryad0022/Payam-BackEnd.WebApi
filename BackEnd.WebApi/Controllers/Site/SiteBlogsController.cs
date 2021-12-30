using BackEnd.Core.DTOs.Blog;
using BackEnd.Core.Services.Interfaces;
using BackEnd.Core.utilities.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.WebApi.Controllers.Site
{

    public class SiteBlogsController : SiteBaseController
    {
        #region Constructor
        private readonly IBlogGroupService blogGroupService;
        private readonly IBlogContentService blogContentService;
        public SiteBlogsController(IBlogGroupService blogGroupService, IBlogContentService blogContentService)
        {
            this.blogContentService = blogContentService;
            this.blogGroupService = blogGroupService;
        }
        #endregion

        #region Get BlogGroups
        [HttpGet("get-bloggroups")]
        public async Task<IActionResult> GetBlogGroups()
        {
            var blogGroupsDTO = await blogGroupService.GetActiveBlogGroups();
            return JsonResponseStatus.Success(blogGroupsDTO);
        }
        #endregion

        #region Get Blogs
        [HttpGet("get-blogs")]
        public async Task<IActionResult> GetBlogs([FromQuery] FilterSiteBlogDTO filter)
        {
            var blogs = await blogContentService.GetSiteFilterBlogs(filter);
            return JsonResponseStatus.Success(blogs);
        }

        [HttpGet("get-blog/{blogId}")]
        public async Task<IActionResult> GetBlog(long blogId)
        {
            var blog = await blogContentService.GetBlogByIdAsync(blogId);
            if (blog == null) return JsonResponseStatus.NotFound();
            var blogContent = new SiteBlogDTO
            {
                Id = blogId,
                Title = blog.Title,
                Tags = blog.Tags,
                Text = blog.Text,
                BlogGroupId = blog.BlogGroupId,
                BlogGroupName = blog.BlogGroupName
            };
            return JsonResponseStatus.Success(blogContent);
        }
        #endregion

        #region Get Latest Blogs
        [HttpGet("get-latest-blogs")]
        public async Task<IActionResult> GetLatestBlogs()
        {
            var blogs = await blogContentService.GetLatestBlogs();
            return JsonResponseStatus.Success(blogs);
        }
        #endregion

    }
}
