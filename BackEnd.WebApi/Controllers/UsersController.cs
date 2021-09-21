using BackEnd.Core.Services.Implementations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.WebApi.Controllers
{
    public class UsersController : SiteBaseController
    {
        #region Construcetor
        private IUserService userService;
        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }
        #endregion

        #region Users 
        [HttpGet("users")]
        public async Task<IActionResult> Users()
        {
            return new ObjectResult(await userService.GetAllUsers());
        }
        #endregion
    }
}
