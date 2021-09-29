using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Dom.Events;
using BackEnd.Core.DTOs.ContactUs;
using BackEnd.DataLayer.Entities.Site;

namespace BackEnd.Core.Services.Interfaces
{
    public interface IContactUsService: IDisposable
    {
        Task<ContactUsDTO.ContactUsResult> SendMessageAsync(ContactUsDTO msg);
    }
}
