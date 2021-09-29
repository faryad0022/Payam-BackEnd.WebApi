using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BackEnd.Core.DTOs.ContactUs;
using BackEnd.Core.Services.Interfaces;
using BackEnd.Core.utilities.Common;

namespace BackEnd.WebApi.Controllers.Site
{

    public class ContactUsController : SiteBaseController
    {
        #region Constructor

        private readonly IContactUsService contactUsService;

        public ContactUsController(IContactUsService contactUsService)
        {
            this.contactUsService = contactUsService;
        }

        #endregion

        [HttpPost("send-message")]
        public async Task<IActionResult> SendMessage(ContactUsDTO msg)
        {
            if (!ModelState.IsValid) return JsonResponseStatus.ModelError("خطا در اعتبار سنجی داده ها");
            var res = await contactUsService.SendMessageAsync(msg);
            switch (res)
            {
                case ContactUsDTO.ContactUsResult.ServerError:
                    return JsonResponseStatus.ServerError("خطا از سمت سرور");
            }
            return JsonResponseStatus.Success("پیغام شما با موفقیت ارسال شد");
        }
    }
}
