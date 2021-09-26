using BackEnd.Core.DTOs.Account;
using BackEnd.Core.Security;
using BackEnd.Core.Services.Interfaces;
using BackEnd.Core.utilities.Convertors;
using BackEnd.DataLayer.Entities.Account;
using BackEnd.DataLayer.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BackEnd.Core.DTOs.Account.LogInUserDTO;
using static BackEnd.Core.DTOs.Account.RegisterUserDTO;

namespace BackEnd.Core.Services.Implementations
{
    public class UserService : IUserService
    {
        #region Constructor
        private readonly IGenericRepository<User> userRepository;
        private IPasswordHelper passwordHelper;
        private IMailSender mailSender;
        private IViewRenderService viewRender;

        public UserService(IGenericRepository<User> userRepository, IPasswordHelper passwordHelper, IMailSender mailSender, IViewRenderService viewRender)
        {
            this.userRepository = userRepository;
            this.passwordHelper = passwordHelper;
            this.mailSender = mailSender;
            this.viewRender = viewRender;
        }


        #endregion

        #region User Section
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await userRepository.GetEntitiesQuery().ToListAsync();

        }

        #endregion


        #region Register
        public async Task<bool> IsUserExistByEmailAsync(string email)
        {
            return await userRepository.GetEntitiesQuery().AnyAsync(u => u.Email == email.ToLower().Trim());

        }

        public async Task<RegisterUserResult> RegisterUserAsync(RegisterUserDTO register)
        {
            if (await IsUserExistByEmailAsync(register.Email))
                return RegisterUserResult.EmailExist;
            try
            {
                var user = new User
                {
                    Email = register.Email.SanitizeText(),
                    Password = passwordHelper.EncodePasswordMd5(register.Password),
                    EmailActiveCode = Guid.NewGuid().ToString()
                };
                var body = await viewRender.RenderToStringAsync("Email/ActivateAccount", user);
                mailSender.Send(user.Email, "فعال سازی حساب کاربری", body);
                await userRepository.AddEntity(user);
                await userRepository.SaveChanges();
                return RegisterUserResult.Success;
            }
            catch
            {
                return RegisterUserResult.EmailServerError;
            }

        }
        #endregion


        #region LogIn
        public async Task<LogInUserResult> LoginUserAsync(LogInUserDTO logIn)
        {
            var password = passwordHelper.EncodePasswordMd5(logIn.Password);
            var user = await userRepository.GetEntitiesQuery().SingleOrDefaultAsync(u => u.Email == logIn.Email.ToLower().Trim() && u.Password == password);
            if (user == null)
                return LogInUserResult.Incorrect;
            if (!user.IsActivated)
                return LogInUserResult.NotActive;
            return LogInUserResult.Success;

        }
        #endregion Activate User

        public async Task ActivateUser(User user)
        {

            user.IsActivated = true;
            user.EmailActiveCode = Guid.NewGuid().ToString();
            userRepository.UpdateEntity(user);
            await userRepository.SaveChanges();


        }


        #region  



        #endregion

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await userRepository.GetEntitiesQuery().SingleOrDefaultAsync(u => u.Email == email.ToLower().Trim());
        }


        public async Task<User> GetUserById(long userId)
        {
            return await userRepository.GetEntityById(userId);
        }


        #region Dispose
        public void Dispose()
        {
            userRepository?.Dispose();
        }
        #endregion
    }
}
