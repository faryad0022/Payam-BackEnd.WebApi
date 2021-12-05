using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BackEnd.Core.DTOs.Images;
using BackEnd.Core.Services.Interfaces;
using BackEnd.Core.utilities.Common;

namespace BackEnd.WebApi.Controllers.Site
{

    public class ImagesController : SiteBaseController
    {
        #region constructor

        private readonly IImageGalleryService imageGalleryService;

        public ImagesController(IImageGalleryService imageGalleryService)
        {
            this.imageGalleryService = imageGalleryService;
        }

        #endregion

        #region Paging And Filtering
        [HttpGet("get-filter-images")]
        public async Task<IActionResult> GetImages([FromQuery]FilterImageDTO filter)
        {
            var images = await imageGalleryService.FilterImagesAsync(filter);
            
            return JsonResponseStatus.Success(images);
        }

        #endregion

    }
}
