using BackEnd.Core.DTOs.Paging;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Core.DTOs.Appointment
{
    public class FilterAppointmentDTO: BasePaging
    {
        public string SearchKey { get; set; }

        public List<AppointmentDTO> Appointments { get; set; }

        public FilterAppointmentDTO SetPaging(BasePaging paging)
        {
            this.PageId = paging.PageId;
            this.PageCount = paging.PageCount;
            this.ActivePage = paging.ActivePage;
            this.StartPage = paging.StartPage;
            this.EndPage = paging.EndPage;
            this.TakeEntity = paging.TakeEntity;
            this.SkipEntity = paging.SkipEntity;
            return this;
        }
        public FilterAppointmentDTO SetAppointments(List<AppointmentDTO> appointments)
        {
            this.Appointments = appointments;
            return this;
        }
    }
}
