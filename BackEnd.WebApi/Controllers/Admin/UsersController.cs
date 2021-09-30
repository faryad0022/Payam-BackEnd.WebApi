using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BackEnd.Core.Services.Implementations;
using BackEnd.Core.Services.Interfaces;
using BackEnd.Core.utilities.Common;
using BackEnd.WebApi.Controllers.Site;

namespace BackEnd.WebApi.Controllers.Admin
{

    public class UsersController : SiteBaseController
    {
        #region Constructor

        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        #endregion

        [HttpGet("get-all-users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return JsonResponseStatus.Success(users);
        }
    }
}
