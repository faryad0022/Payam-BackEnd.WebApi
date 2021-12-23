using BackEnd.Core.DTOs.Appointment;
using BackEnd.Core.DTOs.Paging;
using BackEnd.Core.Services.Interfaces;
using BackEnd.Core.utilities.Extensions.Paging;
using BackEnd.DataLayer.Entities.Site;
using BackEnd.DataLayer.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Core.Services.Implementations
{
    public class AppointmentService : IAppointmentService
    {
        #region constructor

        private IGenericRepository<Appointment> appointmentRepository;

        public AppointmentService(IGenericRepository<Appointment> appointmentRepository)
        {
            this.appointmentRepository = appointmentRepository;
        }
        #endregion


        #region Get Not Deleted
        public async Task<FilterAppointmentDTO> GetAppointmentsFilterPagingAsync(FilterAppointmentDTO filter)
        {
            var appointmentQuery = appointmentRepository.GetEntitiesQuery().Where(s=> !s.IsDelete).OrderByDescending(s => s.CreateDate).AsQueryable();//فیلتر نزولی بر اساس تاریخ
            if (!string.IsNullOrEmpty(filter.SearchKey))
            {
                appointmentQuery = appointmentQuery.Where(s => s.Status.Contains(filter.SearchKey));
            }

            var count = (int)Math.Ceiling(appointmentQuery.Count() / (double)filter.TakeEntity);// تعداد صفحات
            var pager = Pager.Build(count, filter.PageId, filter.TakeEntity);
            var appoints = await appointmentQuery.Paging(pager).ToListAsync();

            var returnAppointments = new List<AppointmentDTO>();
            foreach (var item in appoints)
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
                returnAppointments.Add(vm);
            }
            return filter.SetAppointments(returnAppointments).SetPaging(pager);
        }
        #endregion


        #region Get All
        public async Task<FilterAppointmentDTO> GetAllAppointmentsFilterPagingAsync(FilterAppointmentDTO filter)
        {
            var appointmentQuery = appointmentRepository.GetEntitiesQuery().OrderByDescending(s => s.LastUpdateDate).AsQueryable();//فیلتر نزولی بر اساس تاریخ
            if (!string.IsNullOrEmpty(filter.SearchKey))
            {
                appointmentQuery = appointmentQuery.Where(s => s.Email.Contains(filter.SearchKey) ||
                                                               s.CellPhone.Contains(filter.SearchKey) ||
                                                               s.Telephone.Contains(filter.SearchKey) ||
                                                               s.Name.Contains(filter.SearchKey));
            }

            var count = (int)Math.Ceiling(appointmentQuery.Count() / (double)filter.TakeEntity);// تعداد صفحات
            var pager = Pager.Build(count, filter.PageId, filter.TakeEntity);
            var appoints = await appointmentQuery.Paging(pager).ToListAsync();

            var returnAppointments = new List<AppointmentDTO>();
            foreach (var item in appoints)
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
                returnAppointments.Add(vm);
            }
            return filter.SetAppointments(returnAppointments).SetPaging(pager);
        }
        #endregion

        #region Add


        #endregion

        #region Delete
        public async Task<bool> DeleteAppointment(AppointmentDTO appointmentDTO)
        {

            try
            {
                var appointment = await appointmentRepository.GetEntityById(appointmentDTO.Id);
                appointment.IsDelete = appointmentDTO.IsDelete;
                appointmentRepository.UpdateEntity(appointment);
                await appointmentRepository.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        #endregion

        #region Update (Include Note,Status)

        public async Task<bool> UpdateAppointment(AppointmentDTO appointmentDTO)
        {
            try
            {
                var appointment = await appointmentRepository.GetEntityById(appointmentDTO.Id);
                appointment.Note = appointmentDTO.Note;
                appointment.Status = appointmentDTO.Status;


                appointmentRepository.UpdateEntity(appointment);
                await appointmentRepository.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        #endregion

        #region Dispose
        public void Dispose()
        {
            appointmentRepository?.Dispose();
        }

        #endregion
    }
}
