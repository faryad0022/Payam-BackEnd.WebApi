using BackEnd.Core.Services.Interfaces;
using BackEnd.DataLayer.Entities.Access;
using BackEnd.DataLayer.Entities.Account;
using BackEnd.DataLayer.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Core.Services.Implementations
{
    public class AccessService : IAccessService
    {

        #region Constructor
        private readonly IGenericRepository<Role> roleRepository;
        private readonly IGenericRepository<UserRole> userRoleoleRepository;
        private readonly IGenericRepository<User> userRepository;
        public AccessService(
                   IGenericRepository<Role> roleRepository,
                   IGenericRepository<UserRole> userRoleoleRepository,
                   IGenericRepository<User> userRepository
            )
        {
            this.roleRepository = roleRepository;
            this.userRoleoleRepository = userRoleoleRepository;
            this.userRepository = userRepository;
        }

        #endregion

        #region user role
        public async Task<bool> CheckUserRoleAsync(long userId, string roleName)
        {
            return await userRoleoleRepository.GetEntitiesQuery()
                                              .AsQueryable()
                                              .Include(s=>s.Role)
                                              .AnyAsync(s => s.UserId == userId && s.Role.Name == roleName);
        }

        #endregion


        #region Dispose
        public void Dispose()
        {
            roleRepository?.Dispose();
            userRoleoleRepository?.Dispose();
            userRepository?.Dispose();
        }
        #endregion

    }
}
