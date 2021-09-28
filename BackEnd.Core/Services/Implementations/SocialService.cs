using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackEnd.Core.Services.Interfaces;
using BackEnd.DataLayer.Entities.Site;
using BackEnd.DataLayer.Repository;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Core.Services.Implementations
{
    public class SocialService : ISocialService
    {
        #region Constructor

        private IGenericRepository<Social> socialRepository;

        public SocialService(IGenericRepository<Social> socialRepository)
        {
            this.socialRepository = socialRepository;
        }

        #endregion

        #region GetSocials

        public async Task<List<Social>> GetAllActiveSocialsAsync()
        {
            return await socialRepository.GetEntitiesQuery().Where(s => !s.IsDelete).ToListAsync();
        }

        #endregion

        #region Dispose

        public void Dispose()
        {
            socialRepository?.Dispose();
        }

        #endregion



    }
}
