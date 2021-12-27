using BackEnd.Core.DTOs.Appointment;
using BackEnd.Core.Services.Interfaces;
using BackEnd.Core.utilities.Common;
using BackEnd.WebApi.Controllers.Site;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BackEnd.WebApi.Controllers.Admin
{

    public class AppointmentController : PanelBaseController
    {

        #region constructor
        private readonly IAppointmentService appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            this.appointmentService = appointmentService;
        }
        #endregion

        #region Paging And Filtering
        [HttpGet("get-filter-appointments")]
        public async Task<IActionResult> GetFilterAppointment([FromQuery] FilterAppointmentDTO filter)
        {
            var appointments = await appointmentService.GetAppointmentsFilterPagingAsync(filter);
            return JsonResponseStatus.Success(appointments);
        }


        [HttpGet("get-all-appointments")]
        public async Task<IActionResult> GetAllFilterAppointment([FromQuery] FilterAppointmentDTO filter)
        {
            var appointments = await appointmentService.GetAllAppointmentsFilterPagingAsync(filter);
            return JsonResponseStatus.Success(appointments);
        }

        #endregion

        #region Delete
        [HttpPost("delete-appointment")]
        public async Task<IActionResult> DeleteAppointment([FromBody] AppointmentDTO appointmentDTO, [FromQuery] FilterAppointmentDTO filter)
        {
            if (appointmentDTO == null) return JsonResponseStatus.NotFound();
            if (!ModelState.IsValid) return JsonResponseStatus.ModelError();
            if (!await appointmentService.DeleteAppointment(appointmentDTO)) return JsonResponseStatus.ServerError();

            var returnAppointments = await appointmentService.GetAppointmentsFilterPagingAsync(filter);
            return JsonResponseStatus.Success(returnAppointments);

        }
        #endregion

        #region Update Note and Status
        [HttpPost("update-appointment")]
        public async Task<IActionResult> UpdateAppointment([FromBody] AppointmentDTO appointmentDTO, [FromQuery] FilterAppointmentDTO filter)
        {
            if (appointmentDTO == null) return JsonResponseStatus.NotFound();
            if (!ModelState.IsValid) return JsonResponseStatus.ModelError();
            if (!await appointmentService.UpdateAppointment(appointmentDTO)) return JsonResponseStatus.ServerError();

            var returnAppointments = await appointmentService.GetAppointmentsFilterPagingAsync(filter);
            return JsonResponseStatus.Success(returnAppointments);

        }
        #endregion
    }
}
