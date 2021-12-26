using BackEnd.Core.DTOs.About;
using BackEnd.Core.Security;
using BackEnd.Core.Services.Interfaces;
using BackEnd.DataLayer.Entities.Site;
using BackEnd.DataLayer.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Core.Services.Implementations
{
    public class AboutService : IAboutService
    {
        #region Constructor
        private readonly IGenericRepository<About> aboutRepository;
        public AboutService(IGenericRepository<About> aboutRepository)
        {
            this.aboutRepository = aboutRepository;
        }
        #endregion


        #region Get
        public async Task<AboutDTO> GetAboutAsync()
        {
            var about = await aboutRepository.GetEntitiesQuery().SingleOrDefaultAsync(b=>!b.IsDelete);
            if (about == null) return null;
            var returnAboutDTO = new AboutDTO
            {
                Id = about.Id,
                Text = about.Text,
                ViewCount = about.ViewCount
            };
            return returnAboutDTO;
        }

        #endregion

        #region Add
        public async Task<AboutDTO.AboutResult> AddAboutAsync(AboutDTO aboutDTO)
        {
            var getAbout = await GetAboutAsync();
            if (getAbout != null) return AboutDTO.AboutResult.Exist;
            var about = new About
            {
                Text = aboutDTO.Text.SanitizeText(),

            };
            try
            {
                await aboutRepository.AddEntity(about);
                await aboutRepository.SaveChanges();
                return AboutDTO.AboutResult.SuccessFull;
            }
            catch (Exception)
            {

                return AboutDTO.AboutResult.ServerError;
            }
        }
        #endregion

        #region Edit
        public async Task<AboutDTO.AboutResult> UpdateAboutAsync(AboutDTO aboutDTO)
        {
            var about = await aboutRepository.GetEntityById(aboutDTO.Id);
            if (about == null) return AboutDTO.AboutResult.NotFound;
            about.Text = aboutDTO.Text.SanitizeText();
            try
            {
                aboutRepository.UpdateEntity(about);
                await aboutRepository.SaveChanges();
                return AboutDTO.AboutResult.SuccessFull;
            }
            catch (Exception)
            {

                return AboutDTO.AboutResult.ServerError;
            }
        }
        #endregion






        #region Dispose
        public void Dispose()
        {
            aboutRepository?.Dispose();
        }

        #endregion
    }
}
