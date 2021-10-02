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
        Task<List<ContactAddress>> GetAllActiveAddressAsync();
        Task<List<ContactAddress>> GetAllAddressAsync();
        Task<bool> AddNewAddressAsync(AddressDTO address);
        Task<bool> ChangeAddressStateAsync(ContactAddress address);
        Task<ContactAddress> GetAddressByIdAsync(long Id);
        Task<bool> EditAddressAsync(AddressDTO newAddress, ContactAddress oldAddress);
    }
}
