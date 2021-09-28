using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Core.Services.Interfaces;
using BackEnd.Core.utilities.Common;

namespace BackEnd.WebApi.Controllers.Site
{

    public class AddressesController : SiteBaseController
    {
        #region Constructor

        
        private readonly IAddressService addressService;

        public AddressesController(IAddressService addressService)
        {
            this.addressService = addressService;   
        }
        #endregion

        [HttpGet("get-address")]
        public async Task<IActionResult> GetAllActiveAddress()
        {
            var addresses = await addressService.GetAllActiveAddressAsync();
            return JsonResponseStatus.Success(addresses);
        }

    }
}
