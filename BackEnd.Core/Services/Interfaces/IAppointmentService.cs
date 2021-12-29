using BackEnd.Core.DTOs.Appointment;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Core.Services.Interfaces
{
    public interface IAppointmentService : IDisposable
    {
        Task<FilterAppointmentDTO> GetAppointmentsFilterPagingAsync(FilterAppointmentDTO filter);
        Task<FilterAppointmentDTO> GetAllAppointmentsFilterPagingAsync(FilterAppointmentDTO filter);

        Task<bool> DeleteAppointment(AppointmentDTO appointmentDTO);
        Task<bool> UpdateAppointment(AppointmentDTO appointmentDTO);
        Task<AppointmentDTO.AppointmentResult> AddAppointment(AppointmentDTO appointmentDTO);


    }
}
