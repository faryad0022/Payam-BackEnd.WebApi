using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Core.Services.Interfaces;
using BackEnd.Core.utilities.Common;

namespace BackEnd.WebApi.Controllers.Site
{

    public class SocialsController : SiteBaseController
    {
        #region Constructor

        private readonly ISocialService socialService;

        public SocialsController(ISocialService socialService)
        {
            this.socialService = socialService;
        }

        #endregion
        [HttpGet("get-active-socials")]
        public async Task<IActionResult> GetAllActiveSocials()
        {
            var socials = await socialService.GetAllActiveSocialsAsync();
            return JsonResponseStatus.Success(socials);
        }
    }
}
