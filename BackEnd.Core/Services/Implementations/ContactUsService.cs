using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BackEnd.Core.DTOs.ContactUs;
using BackEnd.Core.Security;
using BackEnd.Core.Services.Interfaces;
using BackEnd.DataLayer.Entities.Site;
using BackEnd.DataLayer.Repository;

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

        #region MyRegion

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
                    Description = msg.Description.SanitizeText()
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

        #region Dispose

        public void Dispose()
        {
        contactRepository?.Dispose();

    }

    #endregion

}
}
