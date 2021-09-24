using BackEnd.Core.DTOs.Account;
using BackEnd.Core.Security;
using BackEnd.Core.Services.Interfaces;
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
        private IGenericRepository<User> userRepository;
        private IPasswordHelper passwordHelper;

        public UserService(IGenericRepository<User> userRepository, IPasswordHelper passwordHelper)
        {
            this.userRepository = userRepository;
            this.passwordHelper = passwordHelper;
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

            var user = new User
            {
                Email = register.Email.SanitizeText(),
                Password = passwordHelper.EncodePasswordMd5(register.Password),
                EmailActiveCode = Guid.NewGuid().ToString()
            };

            await userRepository.AddEntity(user);
            await userRepository.SaveChanges();
            return RegisterUserResult.Success;
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
        #endregion

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await userRepository.GetEntitiesQuery().SingleOrDefaultAsync(u => u.Email == email.ToLower().Trim());
        }


       


        #region Dispose
        public void Dispose()
        {
            userRepository?.Dispose();
        }
        #endregion
    }
}
