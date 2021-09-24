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

    public class SliderController : SiteBaseController
    {
        #region Constructor
        private ISliderService sliderService;
        public SliderController(ISliderService sliderService)
        {
            this.sliderService = sliderService;
        }
        #endregion

        #region Get Active Sliders
        [HttpGet("GetActiveSliders")]
        public async Task<IActionResult> GetActiveSliders()
        {
            var slider = await sliderService.GetAllActiveSliders();
            return JsonResponseStatus.Success(slider);
        }
        #endregion
    }
}
