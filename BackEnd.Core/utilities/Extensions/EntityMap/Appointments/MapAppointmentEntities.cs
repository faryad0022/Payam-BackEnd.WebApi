using BackEnd.Core.DTOs.Appointment;
using BackEnd.DataLayer.Entities.Site;
using System.Collections.Generic;

namespace BackEnd.Core.utilities.Extensions.EntityMap.Appointments
{
    public static class MapAppointmentEntities
    {
        public static List<AppointmentDTO> MapToAppointmentDTO(this List<Appointment> source)
        {

            var returnAppointmentsDTO = new List<AppointmentDTO>();
            foreach (var item in source)
            {
                var vm = new AppointmentDTO
                {
                    Id = item.Id,
                    Telephone = item.Telephone,
                    CellPhone = item.CellPhone,
                    IsDelete = item.IsDelete,
                    Name = item.Name,
                    Email = item.Email,
                    Status = item.Status,
                    Note = item.Note,
                    CreateDate = item.CreateDate,
                    LastUpdateDate = item.LastUpdateDate

                };
                returnAppointmentsDTO.Add(vm);
            }
            return returnAppointmentsDTO;
        }
    }
}