using BackEnd.Core.DTOs.About;
using BackEnd.Core.DTOs.Images;
using BackEnd.Core.Services.Interfaces;
using BackEnd.Core.utilities.Common;
using BackEnd.Core.utilities.Extensions.FileExtensions;
using BackEnd.WebApi.Controllers.Site;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.WebApi.Controllers.Admin
{
    public class AboutController : SiteBaseController
    {
        #region Constructor
        private readonly IAboutService aboutService;
        public AboutController(IAboutService aboutService)
        {
            this.aboutService = aboutService;
        }
        #endregion

        #region Get
        [HttpGet("get-about")]
        public async Task<IActionResult> GetFilterLogos()
        {
            var about = await aboutService.GetAboutAsync();

            return JsonResponseStatus.Success(about);
        }
        #endregion

        #region Add
        [HttpPost("add-about")]
        public async Task<IActionResult> AddNewAbout([FromBody] AboutDTO about)
        {

            if (!ModelState.IsValid) return JsonResponseStatus.ModelError();

            var res = await aboutService.AddAboutAsync(about);
            switch (res)
            {
                case AboutDTO.AboutResult.Exist:
                    return JsonResponseStatus.Exist();
                case AboutDTO.AboutResult.ServerError:
                    return JsonResponseStatus.ServerError();

            }
            var returnData = await aboutService.GetAboutAsync();
            return JsonResponseStatus.Success(returnData);
        }
        #endregion


        #region Edit
        [HttpPost("edit-about")]
        public async Task<IActionResult> EditAbout([FromBody] AboutDTO about)
        {

            if (!ModelState.IsValid) return JsonResponseStatus.ModelError();

            var res = await aboutService.UpdateAboutAsync(about);
            switch (res)
            {
                case AboutDTO.AboutResult.NotFound:
                    return JsonResponseStatus.NotFound();
                case AboutDTO.AboutResult.ServerError:
                    return JsonResponseStatus.ServerError();

            }
            var returnData = await aboutService.GetAboutAsync();
            return JsonResponseStatus.Success(returnData);
        }
        #endregion


        #region Delete Blog Pic
        [HttpPost("delete-about-image")]
        public IActionResult DeleteBlogPic([FromBody] ImageGalleryDTO image)
        {
            if (image == null) return JsonResponseStatus.NotFound();
            try
            {
                ImageUploaderExtensions.DeleteImageFromServer(image.ImageName, PathTools.AboutImageServerPath);
                return JsonResponseStatus.Success();
            }
            catch
            {
                return JsonResponseStatus.ServerError();
            }



        }
        #endregion
        #region Add Blog Pic
        [HttpPost("upload-about-image")]
        public async Task<IActionResult> AddAboutPic(IFormFile UploadFiles)
        {
            var bytes = await UploadFiles.GetBytes();
            var imageBase64 = Convert.ToBase64String(bytes);

            if (string.IsNullOrEmpty(imageBase64)) return JsonResponseStatus.ModelError();
            var imageFile = ImageUploaderExtensions.Base64ToImage(imageBase64);
            var imageName = Guid.NewGuid().ToString("N") + ".jpeg";
            imageFile.AddImageToServer(imageName, PathTools.AboutImageServerPath);

            var httpurl = string.Format("{0}://{1}{2}", Request.Scheme, Request.Host.Value, Request.PathBase.Value);
            var returnUrl = httpurl + "/images/about/origin/";
            Response.Headers.Add("ejUrl", returnUrl);
            Response.Headers.Add("ejUrlName", imageName);

            return JsonResponseStatus.Success();
        }
        #endregion
    }
}
