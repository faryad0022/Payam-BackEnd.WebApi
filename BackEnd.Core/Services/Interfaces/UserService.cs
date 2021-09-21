using BackEnd.Core.Services.Implementations;
using BackEnd.DataLayer.Entities.Account;
using BackEnd.DataLayer.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Core.Services.Interfaces
{
    public class UserService : IUserService
    {
        #region Constructor
        private IGenericRepository<User> userRepository;
        public UserService(IGenericRepository<User> userRepository)
        {
            this.userRepository = userRepository;
        }
        #endregion

        #region User Section
        public async Task<List<User>> GetAllUsers()
        {
            return await userRepository.GetEntitiesQuery().ToListAsync();

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
