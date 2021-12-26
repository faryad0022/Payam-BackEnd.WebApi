using BackEnd.Core.DTOs.AdminLogo;
using BackEnd.Core.DTOs.Logo;
using BackEnd.Core.Services.Interfaces;
using BackEnd.Core.utilities.Common;
using BackEnd.Core.utilities.Extensions.FileExtensions;
using BackEnd.WebApi.Controllers.Site;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BackEnd.WebApi.Controllers.Admin
{

    public class LogoController : SiteBaseController
    {
        #region Constructor
        private readonly ILogoService logoService;
        public LogoController(ILogoService logoService)
        {
            this.logoService = logoService;
        }
        #endregion

        #region Get
        [HttpGet("get-filter-logos")]
        public async Task<IActionResult> GetFilterLogos([FromQuery] FilterLogoDTO filter)
        {
            var logos = await logoService.GetLogoFilterPagingAsync(filter);

            return JsonResponseStatus.Success(logos);
        }
        #endregion

        #region Add
        [HttpPost("add-new-logo")]
        public async Task<IActionResult> AddNewLogo([FromBody] LogoDTO logo, [FromQuery] FilterLogoDTO filter)
        {
            if (string.IsNullOrEmpty(logo.Base64Image)) return JsonResponseStatus.ModelError();

            if (!ModelState.IsValid) return JsonResponseStatus.ModelError();
            var imageFile = ImageUploaderExtensions.Base64ToImage(logo.Base64Image);
            var imageName = Guid.NewGuid().ToString("N") + ".jpeg";
            logo.ImageName = imageName;
            var res = await logoService.AddLogoAsync(logo);
            switch (res)
            {
                case LogoDTO.LogoResult.MustHaveOneActiveLogo:
                    return JsonResponseStatus.MustHaveOneItem(new { message = "یک لوگو باید فعال باشد, لطفا از لیست لوگو را فعال کنید, بعد از اضافه شدن لوگو, لوگو قبلی غیر فعال میشود" });

                case LogoDTO.LogoResult.NotFount:
                    return JsonResponseStatus.NotFound(new { message = "لوگو جهت بارگذادی یافت  نشد" });
                case LogoDTO.LogoResult.ActiveLogoNotFound:
                    return JsonResponseStatus.Error(new { message = "خطا غیر فعالسازی لوگو قبلی" });
                case LogoDTO.LogoResult.ServerError:
                    return JsonResponseStatus.Error(new { message = "خطا از سمت سرور" });
                   
            }
            imageFile.AddImageToServer(imageName, PathTools.LogoServerPath);
            var logos = await logoService.GetLogoFilterPagingAsync(filter);
            return JsonResponseStatus.Success(logos);
        }
        #endregion

        #region Active Logo
        [HttpPost("active-logo")]
        public async Task<IActionResult> ActiveLogo([FromBody] LogoDTO logo, [FromQuery] FilterLogoDTO filter)
        {

            if (!ModelState.IsValid) return JsonResponseStatus.ModelError();

            var res = await logoService.ActiveLogoAsync(logo);
            switch (res)
            {
                case LogoDTO.LogoResult.NotFount:
                    return JsonResponseStatus.NotFound(new { message = "لوگو جهت فعال سیازی یافت  نضد" });
                case LogoDTO.LogoResult.ActiveLogoNotFound:
                    return JsonResponseStatus.Error(new { message = "خطا غیر فعالسازی لوگو قبلی" });
                case LogoDTO.LogoResult.ServerError:
                    return JsonResponseStatus.Error(new { message = "خطا از سمت سرور" });

            }
            var logos = await logoService.GetLogoFilterPagingAsync(filter);
            return JsonResponseStatus.Success(logos);
        }
        #endregion
    }
}
