using BackEnd.Core.DTOs.Social;
using BackEnd.Core.Services.Interfaces;
using BackEnd.Core.utilities.Common;
using BackEnd.WebApi.Controllers.Site;
using BackEnd.WebApi.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.WebApi.Controllers.Admin
{
    public class SocialController : PanelBaseController
    {
        #region Constructor

        private readonly ISocialService socialService;

        public SocialController(ISocialService socialService)
        {
            this.socialService = socialService;
        }

        #endregion


        #region Get
        [HttpGet("get-socials")]
        public async Task<IActionResult> GetFilterSocial([FromQuery] FilterSocialDTO filter)
        {
            var socials = await socialService.GetSocialFilterPagingAsync(filter);
            return JsonResponseStatus.Success(socials);
        }
        #endregion


        #region Add 
        [HttpPost("add-new-social")]
        public async Task<IActionResult> AddSocial([FromBody] SocialDTO socialDTO, [FromQuery] FilterSocialDTO filter)
        {
            if (!ModelState.IsValid) return JsonResponseStatus.ModelError();
            if (!await socialService.AddSocialAsync(socialDTO)) return JsonResponseStatus.ServerError();
            var socials = await socialService.GetSocialFilterPagingAsync(filter);
            return JsonResponseStatus.Success(socials);
        }
        #endregion

        #region Edit 
        [HttpPost("edit-social")]
        public async Task<IActionResult> EditSocial([FromBody] SocialDTO socialDTO, [FromQuery] FilterSocialDTO filter)
        {
            if (socialDTO == null) return JsonResponseStatus.NotFound();

            if (!ModelState.IsValid) return JsonResponseStatus.ModelError();
            var social = await socialService.GetSocialByIdAsync(socialDTO.Id);

            if (!await socialService.EditSocialAsync(socialDTO)) return JsonResponseStatus.ServerError();
            var socials = await socialService.GetSocialFilterPagingAsync(filter);
            return JsonResponseStatus.Success(socials);
        }
        #endregion


        #region Remove 
        [HttpPost("remove-social")]
        public async Task<IActionResult> RemoveBlogGroup([FromBody] SocialDTO socialDTO, [FromQuery] FilterSocialDTO filter)
        {
            if (!ModelState.IsValid) return JsonResponseStatus.ModelError();
            if (!await socialService.RemoveSocialAsync(socialDTO)) return JsonResponseStatus.ServerError();
            var socials = await socialService.GetSocialFilterPagingAsync(filter);
            return JsonResponseStatus.Success(socials);
        }
        #endregion
    }
}
