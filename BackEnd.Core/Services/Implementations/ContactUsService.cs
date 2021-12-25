using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Core.DTOs.ContactUs;
using BackEnd.Core.DTOs.Paging;
using BackEnd.Core.Security;
using BackEnd.Core.Services.Interfaces;
using BackEnd.Core.utilities.Extensions.Paging;
using BackEnd.DataLayer.Entities.Site;
using BackEnd.DataLayer.Repository;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Core.Services.Implementations
{
    public class ContactUsService : IContactUsService
    {
        #region Constructor

        private IGenericRepository<ContactUs> contactRepository;

        public ContactUsService(IGenericRepository<ContactUs> contactRepository)
        {
            this.contactRepository = contactRepository;
        }

        #endregion

        #region User Send Message To Admin

        public async Task<ContactUsDTO.ContactUsResult> SendMessageAsync(ContactUsDTO msg)
        {
            try
            {
                var message = new ContactUs
                {
                    Email = msg.Email.SanitizeText(),
                    Name = msg.Name.SanitizeText(),
                    Telephone = msg.Telephone.SanitizeText(),
                    Title = msg.Title.SanitizeText(),
                    Description = msg.Description.SanitizeText(),
                    Status = ContactUsDTO.ContactUsStatus.NotSeen.ToString()
                };
                await contactRepository.AddEntity(message);
                await contactRepository.SaveChanges();
                return ContactUsDTO.ContactUsResult.Success;
            }
            catch
            {
                return ContactUsDTO.ContactUsResult.ServerError;

            }
        }


        #endregion

        #region Get

        public async Task<ContactUs> GetContactUsByIdAsync(long Id)
        {
            return await contactRepository.GetEntityById(Id);
        }
        public async Task<FilterContactUsDTO> GetContactUsFilterPagingAsync(FilterContactUsDTO filter)
        {
            var contactUsQuery = contactRepository.GetEntitiesQuery().OrderByDescending(b => b.CreateDate).AsQueryable();

            if (!filter.showRemoved)
            {
                contactUsQuery = contactUsQuery.Where(b => !b.IsDelete);

            }
            if (!string.IsNullOrEmpty(filter.SearchKey))
            {
                contactUsQuery = contactUsQuery.Where(b => b.Status == filter.SearchKey);
            }

    

            var count = (int)Math.Ceiling(contactUsQuery.Count() / (double)filter.TakeEntity); // بدست آوردن تعداد صفحات
            var pager = Pager.Build(count, filter.PageId, filter.TakeEntity);
            var contactUsList = await contactUsQuery.Paging(pager).ToListAsync();
            var returnContactUsss = new List<ContactUsDTO>();
            foreach (var item in contactUsList)
            {
                var vm = new ContactUsDTO
                {
                    Id = item.Id,
                    Name = item.Name,
                    Status = item.Status,
                    Telephone = item.Telephone,
                    IsDelete = item.IsDelete,
                    Description = item.Description,
                    Email = item.Email,
                    Title = item.Title,
                    CreateDate = item.CreateDate
                };
                returnContactUsss.Add(vm);
            }

            return filter.SetContactUsList(returnContactUsss).SetPaging(pager);
        }

        #endregion



        #region Delete
        public async Task<bool> RemoveContactUsAsync(ContactUsDTO contactUsDTO)
        {
            var oldContactUs = await GetContactUsByIdAsync(contactUsDTO.Id);

            oldContactUs.IsDelete = !oldContactUs.IsDelete;
            oldContactUs.Status = ContactUsDTO.ContactUsStatus.Deleted.ToString();



            try
            {
                contactRepository.UpdateEntity(oldContactUs);
                await contactRepository.SaveChanges();
                return true;
            }
            catch
            {

                return false;
            }
        }
        #endregion


        #region Change ContactUs Status
        public async Task<bool> ChangeContactUsStatusAsync(ContactUsDTO contactUsDTO)
        {
            var oldContactUs = await GetContactUsByIdAsync(contactUsDTO.Id);

            oldContactUs.Status = contactUsDTO.Status;



            try
            {
                contactRepository.UpdateEntity(oldContactUs);
                await contactRepository.SaveChanges();
                return true;
            }
            catch
            {

                return false;
            }
        }
        #endregion

        #region Dispose

        public void Dispose()
        {
        contactRepository?.Dispose();

    }

    #endregion

}
}
