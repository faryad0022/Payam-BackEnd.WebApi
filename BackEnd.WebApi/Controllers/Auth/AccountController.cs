using BackEnd.Core.DTOs.Account;
using BackEnd.Core.Services.Interfaces;
using BackEnd.Core.utilities.Common;
using BackEnd.Core.utilities.Extensions.Identity;
using BackEnd.WebApi.Controllers.Site;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BackEnd.Core.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using static BackEnd.Core.DTOs.Account.RegisterUserDTO;

namespace BackEnd.WebApi.Controllers.Auth
{

    public class AccountController : SiteBaseController
    {
        #region Constructor
        private readonly IUserService userService;
        private readonly IConfiguration confiuration;
        public AccountController(IUserService userService, IConfiguration iconfig)
        {
            confiuration = iconfig;
            this.userService = userService;
        }
        #endregion

        #region CreateToken

        private string CreateToken(string Email, long id)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(confiuration.GetValue<string>("Jwt:Key")));
            var signInCredential = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken(
                issuer: confiuration.GetValue<string>("Jwt:Issuer"),
                claims: new List<Claim>
                {
                    new Claim(ClaimTypes.Name, Email),
                    new Claim(ClaimTypes.NameIdentifier,id.ToString())
                },
                expires: DateTime.Now.AddDays(30),
                signingCredentials: signInCredential

            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return tokenString;
        }

        #endregion
        #region Register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO register)
        {
            if (ModelState.IsValid)
            {
                var res = await userService.RegisterUserAsync(register);
                switch (res)
                {
                    case RegisterUserResult.EmailExist:
                        return JsonResponseStatus.Error(new { message = "ایمیل وارد شده تکراری است" });
                    case RegisterUserResult.EmailServerError:
                        return JsonResponseStatus.ServerError(new { message = "خطا در ارسال ایمیل فعالسازی" });
                    case RegisterUserResult.Success:
                        return JsonResponseStatus.Success();

                }
            }
            return JsonResponseStatus.ModelError();
        }
        #endregion

        #region  Activate User Account
        [HttpPost("activate-email")]
        public async Task<IActionResult> ActiveAccount(ActiveUserAccountDTO acitiveParam)
        {
            var user = await userService.GetUserByEmailAsync(acitiveParam.UserEmail);
            if (user.EmailActiveCode == acitiveParam.ActiveCode)
            {
                await userService.ActivateUser(user);
                return JsonResponseStatus.Success();
            }

            return JsonResponseStatus.NotFound();
        }

        #endregion

        #region LogIn
        [HttpPost("login")]
        public async Task<IActionResult> LogIn([FromBody] LogInUserDTO logIn)
        {

            if (ModelState.IsValid)
            {
                var res = await userService.LoginUserAsync(logIn);
                switch (res)
                {

                    case LogInUserDTO.LogInUserResult.Incorrect:
                        return JsonResponseStatus.NotFound(new { message = "اطلاعات وارد شده اشتباه است" });
                    case LogInUserDTO.LogInUserResult.NotActive:
                        return JsonResponseStatus.Error(new { message = "حساب کاربری فعال نشده است" });
                    case LogInUserDTO.LogInUserResult.Success:
                        var user = await userService.GetUserByEmailAsync(logIn.Email);
                        var tokenString = CreateToken(user.Email, user.Id);
                        var returnUser = new VmReturnUser
                        {
                            Address = user.Address,
                            CreateDate = user.CreateDate,
                            Email = user.Email,
                            FirstName = user.FirstName,
                            Id = user.Id,
                            IsActivated = user.IsActivated,
                            IsDelete = user.IsDelete,
                            LastName = user.LastName,
                            LastUpdateDate = user.LastUpdateDate,
                            Token = tokenString,
                            ExpireTime = 30
                        };
                        return JsonResponseStatus.Success(returnUser);



                }
            }
            return JsonResponseStatus.Error();
        }
        #endregion

        #region Ckeck User Authentication
        [HttpPost("check-auth")]
        public async Task<IActionResult> CheckUserAuth()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await userService.GetUserById(User.GetUserId());
                var tokenString = CreateToken(user.Email, user.Id);

                var returnUser = new VmReturnUser
                {
                    Address = user.Address,
                    CreateDate = user.CreateDate,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    Id = user.Id,
                    IsActivated = user.IsActivated,
                    IsDelete = user.IsDelete,
                    LastName = user.LastName,
                    LastUpdateDate = user.LastUpdateDate,
                    ExpireTime = 30,
                    Token = tokenString
                };
                return JsonResponseStatus.Success(returnUser);
            }
            return JsonResponseStatus.Error();
        }
        #endregion

        #region Reset User Password

        [HttpGet("send-reset-email/{email}")]
        public async Task<IActionResult> SendResetEmail(string email)
        {
            var user = await userService.GetUserByEmailAsync(email);
            if (user == null) return JsonResponseStatus.NotFound(new { message = "نام کاربری یافت نشد" });
            if (await userService.SendResetEmail(user)) return JsonResponseStatus.Success(new { message = "ایمیل بازیابی ارسال شد" });
            return JsonResponseStatus.ServerError(new { message = "خطا در سرور" });

        }

        [HttpPost("set-new-password")]
        public async Task<IActionResult> SetNewPassword(SetNewPasswordDTO setNewPassword)
        {
            var user = await userService.GetUserByEmailAsync(setNewPassword.UserEmail);
            if (user.EmailActiveCode == setNewPassword.ActiveCode)
            {
                await userService.SetNewPassword(user, setNewPassword.Password);
                return JsonResponseStatus.Success();
            }

            return JsonResponseStatus.NotFound();
        }
        #endregion

        #region LogOut
        [HttpGet("logout")]
        public async Task<IActionResult> LogOut()
        {
            if (User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync();
                return JsonResponseStatus.Success();
            }
            return JsonResponseStatus.Error();
        }
        #endregion
    }
}
