using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BackEnd.Core.Services.Implementations;
using BackEnd.Core.Services.Interfaces;
using BackEnd.Core.utilities.Common;
using BackEnd.Core.ViewModels.Account;
using BackEnd.WebApi.Controllers.Site;
using Microsoft.Extensions.Logging.Abstractions;
using BackEnd.Core.DTOs.Account;
using BackEnd.WebApi.Identity;

namespace BackEnd.WebApi.Controllers.Admin
{

    public class UsersController : PanelBaseController
    {
        #region Constructor

        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        #endregion

        #region Get Users
        [PermissionCheckerAttribute("Admin")]
        [PermissionCheckerAttribute("SuperAdmin")]
        [HttpGet("get-filter-users")]
        public async Task<IActionResult> GetFilterUsers([FromQuery] FilterUserDTO filter)
        {
            var users = await _userService.FilterUserssAsync(filter);

            return JsonResponseStatus.Success(users);
        }


        [PermissionCheckerAttribute("Admin")]
        [PermissionCheckerAttribute("SuperAdmin")]
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
        #endregion

        #region Get All System Role
        [PermissionCheckerAttribute("Admin")]
        [PermissionCheckerAttribute("SuperAdmin")]
        [HttpGet("get-system-roles")]
        public async Task<IActionResult> GetAllSystemRoles()
        {
            var roles = await _userService.GetRolesAsync();
            return JsonResponseStatus.Success(roles);
        } 
        #endregion

        #region Change User Activation
        [PermissionCheckerAttribute("Admin")]
        [PermissionCheckerAttribute("SuperAdmin")]
        [HttpPost("change-user-activation")]
        public async Task<IActionResult> ChangeUserActivation([FromBody] UserDTO user, [FromQuery] FilterUserDTO filter)
        {
            if (!ModelState.IsValid) return JsonResponseStatus.ModelError();
            if (user == null) return JsonResponseStatus.NotFound();
            var _user = await _userService.GetUserById(user.Id);
            if (_user == null) return JsonResponseStatus.NotFound();

            if (!await _userService.ChangeUserActivationAsync(_user)) return JsonResponseStatus.ServerError();


            var users = await _userService.FilterUserssAsync(filter);

            return JsonResponseStatus.Success(users);

        }
        #endregion


        #region Change User Ban State
        [PermissionCheckerAttribute("Admin")]
        [PermissionCheckerAttribute("SuperAdmin")]
        [HttpPost("change-user-ban-state")]
        public async Task<IActionResult> ChangeUserBanState([FromBody] UserDTO user, [FromQuery] FilterUserDTO filter)
        {
            if (!ModelState.IsValid) return JsonResponseStatus.ModelError();
            if (user == null) return JsonResponseStatus.NotFound();
            var _user = await _userService.GetUserById(user.Id);
            if (_user == null) return JsonResponseStatus.NotFound();

            if (!await _userService.ChangeUserBanStatusAsync(_user)) return JsonResponseStatus.ServerError();


            var users = await _userService.FilterUserssAsync(filter);

            return JsonResponseStatus.Success(users);

        }
        #endregion

        #region  User Roles
        [PermissionCheckerAttribute("Admin")]
        [PermissionCheckerAttribute("SuperAdmin")]
        [HttpPost("set-role/{roleId}")]
        public async Task<IActionResult> SetUserRoles([FromBody] UserDTO userDTO, long roleId)
        {
            var user = await _userService.GetUserById(userDTO.Id);
            if (user == null) return JsonResponseStatus.NotFound();

            var role = await _userService.GetRoleByIdAsync(roleId);
            if (role == null) return JsonResponseStatus.NotFound();

            if (!await _userService.SetUserRoleAsync(user, role)) return JsonResponseStatus.ServerError();
            return JsonResponseStatus.Success();
        }


        [PermissionCheckerAttribute("Admin")]
        [PermissionCheckerAttribute("SuperAdmin")]
        [HttpGet("get-user-roles/{userId}")]
        public async Task<IActionResult> GetUserRole(long userId)
        {
            var user = await _userService.GetUserById(userId);
            if (user == null) return JsonResponseStatus.NotFound();

            var userRole = await _userService.GetUserRole(user);
            return JsonResponseStatus.Success(userRole);
        }
        #endregion

    }
}
