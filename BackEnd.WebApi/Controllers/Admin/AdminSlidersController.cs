using BackEnd.Core.DTOs.Sliders;
using BackEnd.Core.Services.Interfaces;
using BackEnd.Core.utilities.Common;
using BackEnd.Core.utilities.Extensions.FileExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.WebApi.Controllers.Admin
{
    public class AdminSlidersController : PanelBaseController
    {
        #region Constructor
        private readonly ISliderService sliderService;
        public AdminSlidersController(ISliderService sliderService)
        {
            this.sliderService = sliderService;
        }
        #endregion

        #region Get Slider By Paging
        [HttpGet("get-filter-sliders")]
        public async Task<IActionResult> GetSliders([FromQuery] FilterSliderDTO filter)
        {
            var images = await sliderService.GetAllSlidersFilterPagingAsync(filter);

            return JsonResponseStatus.Success(images);
        }
        #endregion

        #region Add Slider
        [HttpPost("add-new-slider")]
        public async Task<IActionResult> AddImageToGallery([FromBody] SliderDTO slider, [FromQuery] FilterSliderDTO filter)
        {
            if (slider == null) return JsonResponseStatus.NotFound();
            if (string.IsNullOrEmpty(slider.Base64Image)) return JsonResponseStatus.ModelError();
            var imageFile = ImageUploaderExtensions.Base64ToImage(slider.Base64Image);
            var imageName = Guid.NewGuid().ToString("N") + ".jpeg";
            imageFile.AddImageToServer(imageName, PathTools.SliderServerPath);
            slider.ImageName = imageName;
            if (await sliderService.AddSliderAsync(slider) == SliderDTO.SliderResult.ServerError) return JsonResponseStatus.ServerError();
            var sliders = await sliderService.GetAllSlidersFilterPagingAsync(filter);
            return JsonResponseStatus.Success(sliders);
        }

        #endregion

        #region Update
        [HttpPost("update-slider")]
        public async Task<IActionResult> UpdateSliderFromGallery([FromBody] SliderDTO sliderDTO, [FromQuery] FilterSliderDTO filter)
        {
            if (!ModelState.IsValid) return JsonResponseStatus.ModelError();
            var res = await sliderService.UpdateSliderAsync(sliderDTO);
            switch (res)
            {
                case SliderDTO.SliderResult.NotFound:
                    return JsonResponseStatus.NotFound();
                case SliderDTO.SliderResult.ServerError:
                    return JsonResponseStatus.ServerError();
            }
            var sliders = await sliderService.GetAllSlidersFilterPagingAsync(filter);
            return JsonResponseStatus.Success(sliders);

        }

        #endregion
    }
}
