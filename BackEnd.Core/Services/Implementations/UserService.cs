using BackEnd.Core.DTOs.Account;
using BackEnd.Core.DTOs.Paging;
using BackEnd.Core.Security;
using BackEnd.Core.Services.Interfaces;
using BackEnd.Core.utilities.Convertors;
using BackEnd.Core.utilities.Extensions.Paging;
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

        public async Task<FilterUserDTO> FilterUserssAsync(FilterUserDTO filter)
        {
            var usersQuery = userRepository.GetEntitiesQuery().OrderByDescending(s => s.CreateDate).AsQueryable();//فیلتر نزولی بر اساس تاریخ
            if (!string.IsNullOrEmpty(filter.UserName))
            {
                usersQuery = usersQuery.Where(s => s.Email.Contains(filter.UserName));
            }

            var count = (int)Math.Ceiling(usersQuery.Count() / (double)filter.TakeEntity);// تعداد صفحات
            var pager = Pager.Build(count, filter.PageId, filter.TakeEntity);
            var users = await usersQuery.Paging(pager).ToListAsync();
            return filter.SetUsers(users).SetPaging(pager);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await userRepository.GetEntitiesQuery().ToListAsync();

        }
        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await userRepository.GetEntitiesQuery().SingleOrDefaultAsync(u => u.Email == email.ToLower().Trim());
        }


        public async Task<User> GetUserById(long userId)
        {
            return await userRepository.GetEntityById(userId);
        }

        public async Task<bool> ChangeUserActivationAsync(User user)
        {
            try
            {
                user.IsActivated = !user.IsActivated;
                userRepository.UpdateEntity(user);
                await userRepository.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public async Task<bool> ChangeUserBanStatusAsync(User user)
        {
            try
            {
                user.IsDelete = !user.IsDelete;
                userRepository.UpdateEntity(user);
                await userRepository.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

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
            if (user.IsDelete)
                return LogInUserResult.Banned;
            return LogInUserResult.Success;

        }
        #endregion

        #region  ActivateUser
        
        public async Task ActivateUser(User user)
        {

            user.IsActivated = true;
            user.EmailActiveCode = Guid.NewGuid().ToString();
            userRepository.UpdateEntity(user);
            await userRepository.SaveChanges();


        }

        #endregion

        #region Reset User Password

        public async Task<bool> SendResetEmail(User user)
        {
            try
            {
                var body = await viewRender.RenderToStringAsync("Email/ResetPassword", user);
                mailSender.Send(user.Email, "بازیابی کلمه عبور", body);
                return true;
            }
            catch(Exception e)
            {
                return false;
            }

        }

        public async Task SetNewPassword(User user, string password)
        {
            user.Password = passwordHelper.EncodePasswordMd5(password);
            user.EmailActiveCode = Guid.NewGuid().ToString();
            userRepository.UpdateEntity(user);
            await userRepository.SaveChanges();
        }


        #endregion

        #region Dispose
        public void Dispose()
        {
            userRepository?.Dispose();
        }
        #endregion
    }
}
