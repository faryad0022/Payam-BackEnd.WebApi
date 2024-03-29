﻿using BackEnd.Core.DTOs.Account;
using BackEnd.DataLayer.Entities.Access;
using BackEnd.DataLayer.Entities.Account;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static BackEnd.Core.DTOs.Account.LogInUserDTO;
using static BackEnd.Core.DTOs.Account.RegisterUserDTO;

namespace BackEnd.Core.Services.Interfaces
{
    public interface IUserService: IDisposable
    {
        Task<List<User>> GetAllUsersAsync();
        Task<FilterUserDTO> FilterUserssAsync(FilterUserDTO filter);
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserById(long userId);
        Task<List<RoleDTO>> GetUserRole(User user);
        Task<Role> GetRoleByIdAsync(long roleId);
        Task<List<RoleDTO>> GetRolesAsync();

        Task<RegisterUserResult> RegisterUserAsync(RegisterUserDTO register);


        Task<bool> IsUserExistByEmailAsync(string email);


        Task<LogInUserResult> LoginUserAsync(LogInUserDTO logIn);


        Task<bool> CheckUserHasRoles(User user);

        Task ActivateUser(User user);
        Task SetNewPassword(User user, string password);
        Task<bool> SendResetEmail(User user);
        Task<bool> ChangeUserActivationAsync(User user);
        Task<bool> ChangeUserBanStatusAsync(User user);
        Task<bool> SetUserRoleAsync(User user, Role role);

    }
}
