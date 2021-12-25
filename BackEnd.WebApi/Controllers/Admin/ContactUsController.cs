using BackEnd.Core.DTOs.ContactUs;
using BackEnd.Core.Services.Interfaces;
using BackEnd.Core.utilities.Common;
using BackEnd.WebApi.Controllers.Site;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BackEnd.WebApi.Controllers.Admin
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

        #region Paging And Filtering


        [HttpGet("get-all-contactUsList")]
        public async Task<IActionResult> GetAllFilterContactUs([FromQuery] FilterContactUsDTO filter)
       {
            var contactUsList = await contactUsService.GetContactUsFilterPagingAsync(filter);
            return JsonResponseStatus.Success(contactUsList);
        }

        #endregion

        #region Delete
        [HttpPost("delete-contactus")]
        public async Task<IActionResult> DeleteContactUs([FromBody] ContactUsDTO contactUsDTO, [FromQuery] FilterContactUsDTO filter)
        {
            if (contactUsDTO == null) return JsonResponseStatus.NotFound();
            if (!ModelState.IsValid) return JsonResponseStatus.ModelError();
            if (!await contactUsService.RemoveContactUsAsync(contactUsDTO)) return JsonResponseStatus.ServerError();

            var returnContactUss = await contactUsService.GetContactUsFilterPagingAsync(filter);
            return JsonResponseStatus.Success(returnContactUss);

        }
        #endregion

        #region Update Status
        [HttpPost("update-contactus")]
        public async Task<IActionResult> UpdateContactUs([FromBody] ContactUsDTO contactUsDTO, [FromQuery] FilterContactUsDTO filter)
        {
            if (contactUsDTO == null) return JsonResponseStatus.NotFound();
            if (!ModelState.IsValid) return JsonResponseStatus.ModelError();
            if (!await contactUsService.ChangeContactUsStatusAsync(contactUsDTO)) return JsonResponseStatus.ServerError();

            var returnContactUss = await contactUsService.GetContactUsFilterPagingAsync(filter);
            return JsonResponseStatus.Success(returnContactUss);

        }
        #endregion
    }
}
