using BackEnd.Core.DTOs.AdminLogo;
using BackEnd.Core.DTOs.Logo;
using BackEnd.Core.DTOs.Paging;
using BackEnd.Core.Services.Interfaces;
using BackEnd.Core.utilities.Extensions.Paging;
using BackEnd.DataLayer.Entities.Site;
using BackEnd.DataLayer.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Core.Services.Implementations
{
    public class LogoService : ILogoService
    {
        #region constructor
        private readonly IGenericRepository<Logo> logoRepository;
        public LogoService(IGenericRepository<Logo> logoRepository)
        {
            this.logoRepository = logoRepository;
        }


        #endregion


        #region Get
        public async Task<Logo> GetLogoByIdAsync(long Id)
        {
            return await logoRepository.GetEntityById(Id);
        }
        public async Task<Logo> GetActiveLogo()
        {
 
            return await logoRepository.GetEntitiesQuery().Where(b => !b.IsDelete).SingleOrDefaultAsync();
        }

        public async Task<FilterLogoDTO> GetLogoFilterPagingAsync(FilterLogoDTO filter)
        {
            var logoQuery = logoRepository.GetEntitiesQuery().OrderByDescending(b => b.LastUpdateDate).AsQueryable();
  

            var count = (int)Math.Ceiling(logoQuery.Count() / (double)filter.TakeEntity); // بدست آوردن تعداد صفحات
            var pager = Pager.Build(count, filter.PageId, filter.TakeEntity);
            var logos = await logoQuery.Paging(pager).ToListAsync();
            var returnLogos = new List<LogoDTO>();
            foreach (var item in logos)
            {
                var vm = new LogoDTO
                {
                    Id = item.Id,
                    ImageName = item.ImageName,
                    Title = item.Title,
                    Description = item.Description,
                    IsDelete = item.IsDelete,
                    
                };
                returnLogos.Add(vm);
            }

            return filter.SetLogos(returnLogos).SetPaging(pager);
        }
        #endregion


        #region Add
        public async Task<LogoDTO.LogoResult> AddLogoAsync(LogoDTO logoDTO)
        {
            if (logoDTO == null) return LogoDTO.LogoResult.NotFount;

            var logo = new Logo
            {
                Description = logoDTO.Description,
                ImageName = logoDTO.ImageName,
                IsDelete = false,
                Title = logoDTO.Title,
            };

            //در صورت وجود داشتن لوگوهای دیگر
            var all = await logoRepository.GetEntitiesQuery().ToListAsync();
            if (all.Any())
            {
                var activeLogo = await GetActiveLogo();


                await logoRepository.AddEntity(logo);
                var res = await UpdateLogoAsync(activeLogo);
                if (res == LogoDTO.LogoResult.MustHaveOneActiveLogo) return LogoDTO.LogoResult.MustHaveOneActiveLogo;

                if (res == LogoDTO.LogoResult.ServerError) return LogoDTO.LogoResult.ServerError;
                return LogoDTO.LogoResult.Successfull;
            }

            //اگر اولین لوگو بود
            try
            {
                await logoRepository.AddEntity(logo);
                await logoRepository.SaveChanges();
                return LogoDTO.LogoResult.Successfull;
            }
            catch (Exception)
            {

                return LogoDTO.LogoResult.ServerError;
            }

        }
        #endregion


        #region Active Logo
        public async Task<LogoDTO.LogoResult> ActiveLogoAsync(LogoDTO logoDTO)
        {
            var newLogoToActive = await GetLogoByIdAsync(logoDTO.Id);
            if (newLogoToActive == null) return LogoDTO.LogoResult.NotFount;
            newLogoToActive.IsDelete = false;
            logoRepository.UpdateEntity(newLogoToActive);


            var prevActiveLogo = await GetActiveLogo();
            if (prevActiveLogo == null) 
            {
                await logoRepository.SaveChanges();
                return LogoDTO.LogoResult.Successfull;
            }


            var res = await UpdateLogoAsync(prevActiveLogo);
            if (res == LogoDTO.LogoResult.MustHaveOneActiveLogo) return LogoDTO.LogoResult.MustHaveOneActiveLogo;
            if (res == LogoDTO.LogoResult.ServerError) return LogoDTO.LogoResult.ServerError;
            return LogoDTO.LogoResult.Successfull;
        }

        #endregion

        #region Update
        public async Task<LogoDTO.LogoResult> UpdateLogoAsync(Logo logo)
        {
            if (logo == null) return LogoDTO.LogoResult.MustHaveOneActiveLogo;
            logo.IsDelete = true;
            try
            {
                logoRepository.UpdateEntity(logo);
                await logoRepository.SaveChanges();
                return LogoDTO.LogoResult.Successfull;
            }
            catch (Exception)
            {

                return LogoDTO.LogoResult.ServerError;
            }
        }
        #endregion


        #region Dispose
        public void Dispose()
        {
            logoRepository?.Dispose();
        }

        #endregion
    }
}
