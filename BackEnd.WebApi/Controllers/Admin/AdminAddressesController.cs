using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Core.DTOs.Address;
using BackEnd.Core.Services.Interfaces;
using BackEnd.Core.utilities.Common;
using BackEnd.Core.ViewModels.Address;
using BackEnd.DataLayer.Entities.Site;
using BackEnd.WebApi.Controllers.Site;

namespace BackEnd.WebApi.Controllers.Admin
{

    public class AdminAddressesController : SiteBaseController
    {
        #region Constructor


        private readonly IAddressService addressService;

        public AdminAddressesController(IAddressService addressService)
        {
            this.addressService = addressService;
        }
        #endregion

        [HttpGet("get-address")]
        public async Task<IActionResult> GetAllAddress()
        {
            var addresses = await addressService.GetAllAddressAsync();
            var all = new List<VmReturnAddress>();
            foreach (var address in addresses)
            {
                var addr = new VmReturnAddress
                {
                    Id = address.Id,
                    Telephone = address.Telephone,
                    Address = address.Address,
                    CellPhone = address.CellPhone,
                    City = address.City,
                    IsDelete = address.IsDelete,
                    WorkHour = address.WorkHour
                };
                all.Add(addr);
            }
            return JsonResponseStatus.Success(all);
        }

        [HttpPost("add-new-address")]
        public async Task<IActionResult> AddAddress([FromBody]AddressDTO address)
        {
            if (!ModelState.IsValid) return JsonResponseStatus.ModelError();
            if (!await addressService.AddNewAddressAsync(address)) return JsonResponseStatus.ServerError();
            var addr = new VmReturnAddress
            {
                Telephone = address.Telephone,
                Address = address.Address,
                CellPhone = address.CellPhone,
                City = address.City,
                IsDelete = false,
                WorkHour = address.WorkHour
            };
            return JsonResponseStatus.Success(addr);
        }

        [HttpPost("delete-address")]

        public async Task<IActionResult> ChangeAddressState([FromBody]long id)
        {
            if (id == 0) return JsonResponseStatus.ModelError("پارامتر ارسالی خالی است"); 
            var address =await  addressService.GetAddressByIdAsync(id);
            if (address == null) return JsonResponseStatus.NotFound("آدرس یافت نشد");
            if (!await addressService.ChangeAddressStateAsync(address)) return JsonResponseStatus.ServerError("خطا از سمت سرور");
            
            var updatedAddressState = await addressService.GetAddressByIdAsync(id);
            var returnAddress = new VmReturnAddress
            {
                Address = updatedAddressState.Address,
                CellPhone = updatedAddressState.CellPhone,
                City = updatedAddressState.City,
                Id = updatedAddressState.Id,
                IsDelete = updatedAddressState.IsDelete,
                Telephone = updatedAddressState.Telephone,
                WorkHour = updatedAddressState.WorkHour
            };
            return JsonResponseStatus.Success(returnAddress);
        }

        [HttpPost("edit-address")]
        public async Task<IActionResult> EditAddress([FromBody] AddressDTO newAddress)
        {
            if (newAddress == null) return JsonResponseStatus.NotFound();
            if (!ModelState.IsValid) return JsonResponseStatus.ModelError();
            var oldAddress = await addressService.GetAddressByIdAsync(newAddress.Id);
            if (oldAddress == null) return JsonResponseStatus.NotFound();
            if (!await addressService.EditAddressAsync(newAddress,oldAddress)) return JsonResponseStatus.ServerError();

            var vmReturnAddress = new VmReturnAddress
            {
                Address = newAddress.Address,
                City = newAddress.City,
                WorkHour = newAddress.WorkHour,
                CellPhone = newAddress.CellPhone,
                Id = newAddress.Id,
                IsDelete = oldAddress.IsDelete,
                Telephone = newAddress.Telephone
            };
            return JsonResponseStatus.Success(vmReturnAddress);
        }
    }
}
