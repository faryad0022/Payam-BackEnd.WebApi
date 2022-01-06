using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BackEnd.Core.DTOs.Address;
using BackEnd.Core.Services.Interfaces;
using BackEnd.Core.utilities.Common;
using BackEnd.Core.ViewModels.Address;
using BackEnd.Core.utilities.Extensions.EntityMap.Address;

namespace BackEnd.WebApi.Controllers.Admin
{

    public class AdminAddressesController : PanelBaseController
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

            return JsonResponseStatus.Success(addresses.MapToAddrressDTOList());
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

            return JsonResponseStatus.Success(updatedAddressState.MapToAddrressDTO());
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
