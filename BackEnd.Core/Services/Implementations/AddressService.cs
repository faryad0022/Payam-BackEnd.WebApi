using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackEnd.Core.DTOs.Address;
using BackEnd.Core.Services.Interfaces;
using BackEnd.DataLayer.Entities.Site;
using BackEnd.DataLayer.Repository;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Core.Services.Implementations
{
    public class AddressService : IAddressService
    {
        #region Constructor

        private IGenericRepository<ContactAddress> addressRepository;

        public AddressService(IGenericRepository<ContactAddress> addressRepository)
        {
            this.addressRepository = addressRepository;
        }

        #endregion

        #region Get

        public async Task<List<ContactAddress>> GetAllActiveAddressAsync()
        {
            return  await addressRepository.GetEntitiesQuery().Where(a => !a.IsDelete).ToListAsync();
        }
        public async Task<List<ContactAddress>> GetAllAddressAsync()
        {
            return await addressRepository.GetEntitiesQuery().ToListAsync();
        }

        public async Task<ContactAddress> GetAddressByIdAsync(long Id)
        {
            return await addressRepository.GetEntityById(Id);
        }

        #endregion

        #region Add

        public async Task<bool> AddNewAddressAsync(AddressDTO addr)
        {
            var address = new ContactAddress
            {
                Address = addr.Address,
                City = addr.City,
                Telephone = addr.Telephone,
                CellPhone = addr.CellPhone,
                WorkHour = addr.WorkHour,
                
            };
            try
            {
                await addressRepository.AddEntity(address);
                await addressRepository.SaveChanges();
                return true;

            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region delete

        public async Task<bool> ChangeAddressStateAsync(ContactAddress address)
        {
            try
            {
                address.IsDelete = !address.IsDelete;
                addressRepository.UpdateEntity(address);
                await addressRepository.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Edit

        public async Task<bool> EditAddressAsync(AddressDTO newAddress, ContactAddress oldAddress)
        {
            try
            {

                oldAddress.Address = newAddress.Address;
                oldAddress.Telephone = newAddress.Telephone;
                oldAddress.CellPhone = newAddress.CellPhone;
                oldAddress.City = newAddress.City;
                oldAddress.WorkHour = newAddress.WorkHour;
               
                addressRepository.UpdateEntity(oldAddress);
                await addressRepository.SaveChanges();
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
            addressRepository?.Dispose();
        }

        #endregion

    }
}
