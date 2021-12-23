using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Core.DTOs.Paging;
using BackEnd.Core.DTOs.Social;
using BackEnd.Core.Services.Interfaces;
using BackEnd.Core.utilities.Extensions.Paging;
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


        #region Get

        public async Task<List<Social>> GetAllActiveSocialsAsync()
        {
            return await socialRepository.GetEntitiesQuery().Where(s => !s.IsDelete).ToListAsync();
        }

        public async Task<Social> GetSocialByIdAsync(long Id)
        {
            return await socialRepository.GetEntityById(Id);
        }
        public async Task<FilterSocialDTO> GetSocialFilterPagingAsync(FilterSocialDTO filter)
        {
            var socialQuery = socialRepository.GetEntitiesQuery().OrderByDescending(b => b.LastUpdateDate).AsQueryable();
            if (!string.IsNullOrEmpty(filter.SearchKey))
            {
                socialQuery = socialQuery.Where(b => b.Name.Contains(filter.SearchKey));
            }

            var count = (int)Math.Ceiling(socialQuery.Count() / (double)filter.TakeEntity); // بدست آوردن تعداد صفحات
            var pager = Pager.Build(count, filter.PageId, filter.TakeEntity);
            var socials = await socialQuery.Paging(pager).ToListAsync();
            var returnSocialss = new List<SocialDTO>();
            foreach (var item in socials)
            {
                var vm = new SocialDTO
                {
                    Id = item.Id,
                    Name = item.Name,
                    Link = item.Link,
                    Icon = item.Icon,
                    IsDelete = item.IsDelete
                };
                returnSocialss.Add(vm);
            }

            return filter.SetSocials(returnSocialss).SetPaging(pager);
        }
        #endregion


        #region Add
        public async Task<bool> AddSocialAsync(SocialDTO socialDTO)
        {
            var social = new Social
            {
                Icon = socialDTO.Icon,
                Link = socialDTO.Link,
                Name = socialDTO.Name

            };

            try
            {
                await socialRepository.AddEntity(social);
                await socialRepository.SaveChanges();
                return true;
            }
            catch
            {

                return false;
            }
        }
        #endregion


        #region Delete
        public async Task<bool> RemoveSocialAsync(SocialDTO socialDTO)
        {
            var oldSocial = await GetSocialByIdAsync(socialDTO.Id);

            oldSocial.IsDelete = !oldSocial.IsDelete;



            try
            {
                socialRepository.UpdateEntity(oldSocial);
                await socialRepository.SaveChanges();
                return true;
            }
            catch
            {

                return false;
            }
        }
        #endregion


        #region Edit
        public async Task<bool> EditSocialAsync(SocialDTO socialDTO)
        {
            var oldSocial = await GetSocialByIdAsync(socialDTO.Id);

            oldSocial.Name = socialDTO.Name;
            oldSocial.Link = socialDTO.Link;
            oldSocial.Icon = socialDTO.Icon;


            try
            {
                socialRepository.UpdateEntity(oldSocial);
                await socialRepository.SaveChanges();
                return true;
            }
            catch
            {

                return false;
            }
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
