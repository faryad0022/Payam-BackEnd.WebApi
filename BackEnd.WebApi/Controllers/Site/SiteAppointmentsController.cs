using BackEnd.Core.DTOs.Appointment;
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
    public class SiteAppointmentsController : SiteBaseController
    {
        #region Constructor
        private readonly IAppointmentService appointmentService;
        public SiteAppointmentsController(IAppointmentService appointmentService)
        {
            this.appointmentService = appointmentService;
        }
        #endregion

        #region Add
        [HttpPost("add-user-appointment-request")]
        public async Task<IActionResult> AddAppointment(AppointmentDTO appointmentDTO)
        {
            if (!ModelState.IsValid) return JsonResponseStatus.ModelError(new { message = "لطفا تمام فیلد هارا پر کنید"});
            var res = await appointmentService.AddAppointment(appointmentDTO);
            switch (res)
            {
                case AppointmentDTO.AppointmentResult.Suuccess:
                    return JsonResponseStatus.Success(new { message = "درخواست شما ارسال شد. همکاران ما با شما تماس خواهند گرفت" });
                case AppointmentDTO.AppointmentResult.ServerError:
                    return JsonResponseStatus.ServerError(new { message = "لطفا در زمان دیگر درخواست خودرا ارسال کنید"});
            }
            return JsonResponseStatus.Success(new { message = "درخواست شما ارسال شد. همکاران ما با شما تماس خواهند گرفت"});
        }
        #endregion

    }
}
