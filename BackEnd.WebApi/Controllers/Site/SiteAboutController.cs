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

    public class SiteAboutController : SiteBaseController
    {
        #region Constructor
        private readonly IAboutService aboutService;
        public SiteAboutController(IAboutService aboutService)
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
    }
}
