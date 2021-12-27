using BackEnd.Core.DTOs.Account;
using BackEnd.Core.Services.Interfaces;
using BackEnd.Core.utilities.Common;
using BackEnd.Core.ViewModels.Account;
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

namespace BackEnd.WebApi.Controllers.Admin
{

    public class PanelAccountController : PanelBaseController
    {
        #region Constructor
        private readonly IUserService userService;
        private readonly IConfiguration confiuration;

        public PanelAccountController(IUserService userService, IConfiguration confiuration)
        {
            this.userService = userService;
            this.confiuration = confiuration;
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
                    case LogInUserDTO.LogInUserResult.NotAdmin:
                        return JsonResponseStatus.Error(new { message = "شما به این بخش دسترسی ندارید" });
                    case LogInUserDTO.LogInUserResult.NotActive:
                        return JsonResponseStatus.Error(new { message = "حساب کاربری فعال نشده است" });
                    case LogInUserDTO.LogInUserResult.Banned:
                        return JsonResponseStatus.Error(new { message = "با ادمین تماس بگیرید" });
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
    }
}
