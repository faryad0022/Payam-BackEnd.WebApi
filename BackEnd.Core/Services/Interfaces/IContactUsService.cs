using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BackEnd.Core.DTOs.ContactUs;
using BackEnd.DataLayer.Entities.Site;

namespace BackEnd.Core.Services.Interfaces
{
    public interface IContactUsService: IDisposable
    {
        Task<ContactUsDTO.ContactUsResult> SendMessageAsync(ContactUsDTO msg);

        Task<ContactUs> GetContactUsByIdAsync(long Id);
        Task<FilterContactUsDTO> GetContactUsFilterPagingAsync(FilterContactUsDTO filter);


        Task<bool> RemoveContactUsAsync(ContactUsDTO contactUsDTO);

        Task<bool> ChangeContactUsStatusAsync(ContactUsDTO contactUsDTO);

    }
}
