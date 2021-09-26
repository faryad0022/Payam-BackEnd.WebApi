﻿using BackEnd.Core.DTOs.Account;
using BackEnd.DataLayer.Entities.Account;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static BackEnd.Core.DTOs.Account.LogInUserDTO;
using static BackEnd.Core.DTOs.Account.RegisterUserDTO;

namespace BackEnd.Core.Services.Interfaces
{
    public interface IUserService: IDisposable
    {
        Task<List<User>> GetAllUsersAsync();
        Task<RegisterUserResult> RegisterUserAsync(RegisterUserDTO register);
        Task<bool> IsUserExistByEmailAsync(string email);
        Task<LogInUserResult> LoginUserAsync(LogInUserDTO logIn);
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserById(long userId);
        Task ActivateUser(User user);
    }
}
