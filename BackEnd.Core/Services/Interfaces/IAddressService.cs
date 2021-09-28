using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BackEnd.Core.DTOs.Address;
using BackEnd.DataLayer.Entities.Site;

namespace BackEnd.Core.Services.Interfaces
{
    public interface IAddressService: IDisposable
    {
        Task<List<ContactAddress>> GetAllActiveAddress();
    }
}
