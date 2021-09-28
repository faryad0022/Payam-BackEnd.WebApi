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

        public async Task<List<ContactAddress>> GetAllActiveAddress()
        {
            return  await addressRepository.GetEntitiesQuery().Where(a => !a.IsDelete).ToListAsync();
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
