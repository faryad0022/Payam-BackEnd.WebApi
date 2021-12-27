using BackEnd.Core.DTOs.Blog;
using BackEnd.Core.DTOs.Images;
using BackEnd.Core.Services.Interfaces;
using BackEnd.Core.utilities.Common;
using BackEnd.Core.utilities.Extensions.FileExtensions;
using BackEnd.Core.ViewModels.Blog;
using BackEnd.WebApi.Controllers.Site;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.WebApi.Controllers.Admin
{
    public class BlogContentsController : PanelBaseController
    {
        #region Constructor
        private readonly IBlogGroupService blogGroupService;
        private readonly IBlogContentService blogContentService;
        private readonly IImageGalleryService imageGalleryService;

        public BlogContentsController(IBlogGroupService blogGroupService, IBlogContentService blogContentService, IImageGalleryService imageGalleryService)
        {
            this.blogGroupService = blogGroupService;
            this.blogContentService = blogContentService;
            this.imageGalleryService = imageGalleryService;
        }
        #endregion

        #region Get Blog Group and blog
        [HttpGet("get-bloggroups")]
        public async Task<IActionResult> GetBlogGroups()
        {
            var blogGroups = await blogGroupService.GetAllActiveBlogGroupsAsync();
            if (blogGroups == null) return JsonResponseStatus.NotFound();
            return JsonResponseStatus.Success(blogGroups);

        }


        [HttpGet("get-filter-blogs")]
        public async Task<IActionResult> GetFilterBlogs([FromQuery] FilterBlogDTO filter)
        {
            var blogs = await blogContentService.GetFilterBlogs(filter);
            return JsonResponseStatus.Success(blogs);
        }
        #endregion

        #region Add Blog
        [HttpPost("add-blog")]
        public async Task<IActionResult> AddBlog([FromBody] BlogContentDTO blogContentDTO, [FromQuery] FilterBlogDTO filter)
        {
            if (!ModelState.IsValid) return JsonResponseStatus.ModelError();
            if (!await blogContentService.CheckUniqueTitleAsync(blogContentDTO.Title)) return JsonResponseStatus.Duplicate();

            if (string.IsNullOrEmpty(blogContentDTO.Base64Image)) return JsonResponseStatus.ModelError();
            var imageFile = ImageUploaderExtensions.Base64ToImage(blogContentDTO.Base64Image);
            var imageName = Guid.NewGuid().ToString("N") + ".jpeg";
            imageFile.AddImageToServer(imageName, PathTools.BlogImageServerPath);
            blogContentDTO.ImageName = imageName;


            if (!await blogContentService.AddBlogAsync(blogContentDTO))
            {
                ImageUploaderExtensions.DeleteImageFromServer(imageName, PathTools.BlogImageServerPath);
                return JsonResponseStatus.ServerError();
            }
            var blogs = await blogContentService.GetFilterBlogs(filter);
            return JsonResponseStatus.Success(blogs);
        }
        #endregion

        #region Delete Blog Pic
        [HttpPost("delete-blog-image")]
        public IActionResult DeleteBlogPic([FromBody] ImageGalleryDTO image)
        {
            if (image == null) return JsonResponseStatus.NotFound();
            try
            {
                ImageUploaderExtensions.DeleteImageFromServer(image.ImageName, PathTools.BlogContentImageServerPath);
                return JsonResponseStatus.Success();
            }
            catch
            {
                return JsonResponseStatus.ServerError();
            }



        }
        #endregion
        #region Add Blog Pic
        [HttpPost("upload-blog-image")]
        public async Task<IActionResult> AddBlogPic(IFormFile UploadFiles)
        {
            var bytes = await UploadFiles.GetBytes();
            var imageBase64 = Convert.ToBase64String(bytes);

            if (string.IsNullOrEmpty(imageBase64)) return JsonResponseStatus.ModelError();
            var imageFile = ImageUploaderExtensions.Base64ToImage(imageBase64);
            var imageName = Guid.NewGuid().ToString("N") + ".jpeg";
            imageFile.AddImageToServer(imageName, PathTools.BlogContentImageServerPath);

            var httpurl = string.Format("{0}://{1}{2}", Request.Scheme, Request.Host.Value, Request.PathBase.Value);
            var returnUrl = httpurl + "/images/blogcontent/origin/";
            Response.Headers.Add("ejUrl", returnUrl);
            Response.Headers.Add("ejUrlName", imageName);

            return JsonResponseStatus.Success();
        }
        #endregion
        #region Change Status
        [HttpPost("change-blog-status")]
        public async Task<IActionResult> ChangeBlogStatus([FromBody] VmReturnBlog vm, [FromQuery] FilterBlogDTO filter)
        {
            // Using VmReturnBlog Because We Dont Need Required Property Like Tags Base64Image ....
            var blog = await blogContentService.GetBlogByIdAsync(vm.Id);
            if (blog == null) return JsonResponseStatus.NotFound();
            if (!ModelState.IsValid) return JsonResponseStatus.ModelError();
            if (!await blogContentService.ChangeBlogStatusAsync(blog.Id)) return JsonResponseStatus.ServerError();
            var blogs = await blogContentService.GetFilterBlogs(filter);
            return JsonResponseStatus.Success(blogs);
        }
        #endregion


        #region Edit
        [HttpPost("edit-blog")]
        public async Task<IActionResult> EditBlog([FromBody] VmReturnBlog vmReturnBlog, [FromQuery] FilterBlogDTO filter)
        {
            var blog = await blogContentService.GetBlogByIdAsync(vmReturnBlog.Id);
            var blogContent = new BlogContentDTO
            {
                Id = vmReturnBlog.Id,
                Title = vmReturnBlog.Title,
                Tags = vmReturnBlog.Tags,
                Text = vmReturnBlog.Text,
                BlogGroupId = vmReturnBlog.BlogGroupId,
                UserId = vmReturnBlog.UserId,
                UserName = vmReturnBlog.UserName,
                BlogGroupName = vmReturnBlog.BlogGroupName
            };

            //اگر تصویر اصلی تغییر کرد
            if (!string.IsNullOrEmpty(vmReturnBlog.Base64Image))
            {
                ImageUploaderExtensions.DeleteImageFromServer(vmReturnBlog.ImageName, PathTools.BlogImageServerPath);

                var imageFile = ImageUploaderExtensions.Base64ToImage(vmReturnBlog.Base64Image);
                var imageName = Guid.NewGuid().ToString("N") + ".jpeg";
                imageFile.AddImageToServer(imageName, PathTools.BlogImageServerPath);
                vmReturnBlog.ImageName = imageName;
                var image = new ImageGalleryDTO
                {
                    Base64Image = vmReturnBlog.Base64Image,
                    ImageName = imageName,
                    Title = vmReturnBlog.Title
                };
                if (!await imageGalleryService.UploadImageToGalleryAsync(image)) return JsonResponseStatus.ServerError();
                blogContent.ImageName = imageName;
            }



            blogContent.ImageName = vmReturnBlog.ImageName;


            if (await blogContentService.CheckUniqueTitleAsync(blogContent.Title) && blog.Title != vmReturnBlog.Title) return JsonResponseStatus.Duplicate();
            if (!await blogContentService.EditBlogAsync(blogContent)) return JsonResponseStatus.ServerError();
            var blogs = await blogContentService.GetFilterBlogs(filter);
            return JsonResponseStatus.Success(blogs);

        }
        #endregion
        #region Delete Blog
        [HttpPost("delete-blog")]
        public async Task<IActionResult> DeleteBlog([FromBody] VmReturnBlog vm, [FromQuery] FilterBlogDTO filter)
        {
            // Using VmReturnBlog Because We Dont Need Required Property Like Tags Base64Image ....
            var blog = await blogContentService.GetBlogByIdAsync(vm.Id);
            if (blog == null) return JsonResponseStatus.NotFound();
            if (!ModelState.IsValid) return JsonResponseStatus.ModelError();

            try
            {
                ImageUploaderExtensions.DeleteImageFromServer(blog.ImageName, PathTools.BlogImageServerPath);

            }
            catch
            {

                return JsonResponseStatus.ServerError();
            }

            if (!await blogContentService.DeleteBlogAsync(blog.Id)) return JsonResponseStatus.ServerError();
            var blogs = await blogContentService.GetFilterBlogs(filter);
            return JsonResponseStatus.Success(blogs);
        }
        #endregion
    }
}
