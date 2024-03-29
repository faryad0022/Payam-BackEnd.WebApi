﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using BackEnd.Core.DTOs.Images;
using BackEnd.Core.Services.Interfaces;
using BackEnd.Core.utilities.Common;
using BackEnd.Core.utilities.Extensions.FileExtensions;

namespace BackEnd.WebApi.Controllers.Admin
{
    public class GalleryController : PanelBaseController
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


        #region Add
        [HttpPost("add-new-image")]
        public async Task<IActionResult> AddImageToGallery([FromBody]ImageGalleryDTO image, [FromQuery] FilterImageDTO filter)
        {
            if (image == null) return JsonResponseStatus.NotFound();
            if (string.IsNullOrEmpty(image.Base64Image)) return JsonResponseStatus.ModelError();
            var imageFile = ImageUploaderExtensions.Base64ToImage(image.Base64Image);
            var imageName = Guid.NewGuid().ToString("N")+ ".jpeg";
            imageFile.AddImageToServer(imageName,PathTools.GalleryServerPath);
            image.ImageName = imageName;
            if(!await imageGalleryService.UploadImageToGalleryAsync(image)) return  JsonResponseStatus.ServerError();
            var gallery = await imageGalleryService.FilterImagesAsync(filter);
            return JsonResponseStatus.Success(gallery);
        }

        #endregion

        #region Delete
        [HttpPost("delete-image")]
        public async Task<IActionResult> DeleteImageFromGallery([FromBody] ImageGalleryDTO image, [FromQuery] FilterImageDTO filter)
        {
            if (image == null) return JsonResponseStatus.NotFound();
            if (!await imageGalleryService.ImageExistById(image.Id)) return JsonResponseStatus.NotFound();
            if (!await imageGalleryService.DeleteImage(image)) return JsonResponseStatus.ServerError();
            try
            {
                ImageUploaderExtensions.DeleteImageFromServer(image.ImageName, PathTools.GalleryServerPath);
                var images = await imageGalleryService.FilterImagesAsync(filter);

                return JsonResponseStatus.Success(images);
            }
            catch
            {
                return JsonResponseStatus.ServerError();
            }
        }

        #endregion
    }
}
