using BackEnd.Core.DTOs.Account;
using BackEnd.Core.Services.Interfaces;
using BackEnd.Core.utilities.Common;
using BackEnd.WebApi.Controllers.Site;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static BackEnd.Core.DTOs.Account.RegisterUserDTO;

namespace BackEnd.WebApi.Controllers.Auth
{

    public class AccountController : SiteBaseController
    {
        #region Constructor
        private readonly IUserService userService;
        private IConfiguration Confiuration { get; }
        public AccountController(IUserService userService, IConfiguration configuration)
        {
            this.Confiuration = Confiuration;
            this.userService = userService;
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
                        return JsonResponseStatus.Error(new { data = "ایمیل وارد شده تکراری است" });

                }
            }
            return JsonResponseStatus.Success();
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
                        return JsonResponseStatus.NotFound(new { data = "اطلاعات وارد شده اشتباه است" });
                    case LogInUserDTO.LogInUserResult.NotActive:
                        return JsonResponseStatus.Error(new { data = "حساب کاربری فعال نشده است" });
                    case LogInUserDTO.LogInUserResult.Success:
                        var user = await userService.GetUserByEmailAsync(logIn.Email);
                        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Confiuration["Jwt:Key"]));
                        var signInCredential = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                        var tokenOptions = new JwtSecurityToken(
                             issuer: Confiuration["Jwt:Issuer"],
                             claims: new List<Claim>
                             {
                                 new Claim(ClaimTypes.Name, user.Email),
                                 new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
                             },
                             expires: DateTime.Now.AddDays(30),
                             signingCredentials: signInCredential

                            );
                        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                        return JsonResponseStatus.Success(new { token= tokenString, expireTime=30, firstName=user.FirstName, lastName=user.LastName, id=user.Id});



                }
            }
            return JsonResponseStatus.Error();
        }
        #endregion

        #region LogOut
        [HttpGet("logout")]
        public async Task<IActionResult> LogOut()
        {
            if(User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync();
                return JsonResponseStatus.Success();
            }
            return JsonResponseStatus.Error();
        }
        #endregion
    }
}
