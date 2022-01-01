using BackEnd.Core.DTOs.Appointment;
using BackEnd.Core.DTOs.Paging;
using BackEnd.Core.Security;
using BackEnd.Core.Services.Interfaces;
using BackEnd.Core.utilities.Convertors;
using BackEnd.Core.utilities.Extensions.Paging;
using BackEnd.DataLayer.Entities.PhoneBook;
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

        private readonly IGenericRepository<Appointment> appointmentRepository;
        private readonly IGenericRepository<ContactList> contactRepository;
        private readonly IMailSender mailSender;
        private readonly IViewRenderService viewRender;


        public AppointmentService(
            IGenericRepository<Appointment> appointmentRepository,
            IGenericRepository<ContactList> contactRepository,
            IMailSender mailSender,
            IViewRenderService viewRender
            )
        {
            this.contactRepository = contactRepository;
            this.appointmentRepository = appointmentRepository;
            this.mailSender = mailSender;
            this.viewRender = viewRender;
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
        public async Task<AppointmentDTO.AppointmentResult> AddAppointment(AppointmentDTO appointmentDTO)
        {
            var appointment = new Appointment
            {
                CellPhone = appointmentDTO.CellPhone.SanitizeText(),
                Email = appointmentDTO.Email.SanitizeText(),
                Name = appointmentDTO.Name.SanitizeText(),
                Telephone = appointmentDTO.Telephone.SanitizeText(),
                Status = appointmentDTO.Status.SanitizeText()

            };
            var contactList = new ContactList
            {
                CellPhone = appointmentDTO.CellPhone.SanitizeText(),
                Email = appointmentDTO.Email.SanitizeText(),
                IsSelected = false,
                Name = appointmentDTO.Name.SanitizeText(),
                Telephone = appointmentDTO.Telephone.SanitizeText()
            };

            try
            {
                await contactRepository.AddEntity(contactList);
                await appointmentRepository.AddEntity(appointment);
                await appointmentRepository.SaveChanges();

                var body = await viewRender.RenderToStringAsync("Email/NewRequest", contactList);
                mailSender.Send("mahancomputer49@gmail.com", "درخواست مشاوره جدید", body);
                mailSender.Send("payamabolhassani52@gmail.com", "درخواست مشاوره جدید", body);
                return AppointmentDTO.AppointmentResult.Suuccess;
            }
            catch (Exception)
            {

                return AppointmentDTO.AppointmentResult.ServerError;
            }
        }


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
            contactRepository?.Dispose();
            appointmentRepository?.Dispose();
        }

        #endregion
    }
}
