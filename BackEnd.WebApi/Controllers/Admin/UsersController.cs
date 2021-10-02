using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BackEnd.Core.Services.Implementations;
using BackEnd.Core.Services.Interfaces;
using BackEnd.Core.utilities.Common;
using BackEnd.Core.ViewModels.Account;
using BackEnd.WebApi.Controllers.Site;
using Microsoft.Extensions.Logging.Abstractions;

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
            var returnUsers = new List<VmReturnUser>();
            foreach (var item in users)
            {
                var user = new VmReturnUser()
                {
                    Id = item.Id,
                    IsActivated = item.IsActivated,
                    Email = item.Email,
                    Address = item.Address,
                    FirstName = item.FirstName,
                    LastName = item.FirstName,
                    IsDelete = item.IsDelete

                };
                returnUsers.Add(user);
            }
            return JsonResponseStatus.Success(returnUsers);
        }

        [HttpPost("change-user-activation")]
        public async Task<IActionResult> ChangeUserActivation([FromBody]long id)
        {
            var user = await _userService.GetUserById(id);
            if (user==null) return JsonResponseStatus.NotFound();
            if (!await _userService.ChangeUserActivationAsync(user)) return JsonResponseStatus.ServerError();
            var editedUser = new VmReturnUser()
            {
                Id = user.Id,
                IsActivated = user.IsActivated,
                Email = user.Email,
                Address = user.Address,
                FirstName = user.FirstName,
                LastName = user.FirstName,
                IsDelete = user.IsDelete

            };
            return JsonResponseStatus.Success(editedUser);

        }
    }
}
