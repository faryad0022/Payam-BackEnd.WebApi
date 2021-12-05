using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Core.DTOs.Images;
using BackEnd.Core.Services.Interfaces;
using BackEnd.Core.utilities.Common;
using BackEnd.Core.utilities.Extensions.FileExtensions;
using BackEnd.WebApi.Controllers.Site;

namespace BackEnd.WebApi.Controllers.Admin
{
    public class GalleryController : SiteBaseController
    {
        private readonly IImageGalleryService imageGalleryService;

        public GalleryController(IImageGalleryService imageGalleryService)
        {
            this.imageGalleryService = imageGalleryService;
        }

        #region Paging And Filtering
        [HttpGet("get-filter-images")]
        public async Task<IActionResult> GetImages([FromQuery] FilterImageDTO filter)
        {
            var images = await imageGalleryService.FilterImagesAsync(filter);

            return JsonResponseStatus.Success(images);
        }

        #endregion

        [HttpPost("add-new-image")]
        public async Task<IActionResult> AddImageToGallery([FromBody]ImageGalleryDTO image)
        {
            if (image == null) return JsonResponseStatus.NotFound();
            if (string.IsNullOrEmpty(image.Base64Image)) return JsonResponseStatus.ModelError();
            var imageFile = ImageUploaderExtensions.Base64ToImage(image.Base64Image);
            var imageName = Guid.NewGuid().ToString("N")+ ".jpeg";
            imageFile.AddImageToServer(imageName,PathTools.GalleryServerPath);
            image.ImageName = imageName;
            if(!await imageGalleryService.UploadImageToGalleryAsync(image)) return  JsonResponseStatus.ServerError();
            return JsonResponseStatus.Success(image);
        }
    }
}
