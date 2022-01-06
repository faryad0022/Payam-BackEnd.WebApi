using BackEnd.Core.DTOs.Blog;
using BackEnd.Core.DTOs.Paging;
using BackEnd.Core.Services.Interfaces;
using BackEnd.Core.utilities.Extensions.EntityMap.BlogSection;
using BackEnd.Core.utilities.Extensions.Paging;
using BackEnd.Core.ViewModels.Blog;
using BackEnd.DataLayer.Entities.Blog;
using BackEnd.DataLayer.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Core.Services.Implementations
{
    public class BlogGroupService : IBlogGroupService
    {
        #region Constructor
        private readonly IGenericRepository<BlogGroup> blogGroupRepository;
        public BlogGroupService(IGenericRepository<BlogGroup> blogGroupRepository)
        {
            this.blogGroupRepository = blogGroupRepository;
        }

        #endregion

        #region Get
        public async Task<List<BlogGroupDTO>> GetAllActiveBlogGroupsAsync()
        {
            try
            {
                var blogGroups = await blogGroupRepository.GetEntitiesQuery().Where(b => !b.IsDelete).ToListAsync();
                return blogGroups.MapToBlogGroupDTO();
            }
            catch (Exception)
            {

                return null;
            }

        }

        public async Task<BlogGroup> GetBlogGroupByIdAsync(long Id)
        {
            return await blogGroupRepository.GetEntityById(Id);
        }

        public async Task<FilterBlogGroupDTO> GetFilterBlogGourps(FilterBlogGroupDTO filter)
        {
            var blogGroupQuery = blogGroupRepository.GetEntitiesQuery().OrderByDescending(b => b.LastUpdateDate).AsQueryable();
            if (!string.IsNullOrEmpty(filter.Title))
            {
                blogGroupQuery = blogGroupQuery.Where(b => b.Title.Contains(filter.Title));
            }

            var count = (int)Math.Ceiling(blogGroupQuery.Count() / (double)filter.TakeEntity); // بدست آوردن تعداد صفحات
            var pager = Pager.Build(count, filter.PageId, filter.TakeEntity);
            var blogGroups = await blogGroupQuery.Paging(pager).ToListAsync();

            return filter.SetBlogGroups(blogGroups.MapToBlogGroupDTO()).SetPaging(pager);
        }


        public async Task<BlogGroup> GetBlogGroupByTitleAsync(string Title)
        {
            return await blogGroupRepository.GetEntitiesQuery().Where(b => b.Title == Title).SingleOrDefaultAsync();
        }

        public async Task<List<BlogGroupDTO>> GetActiveBlogGroups()
        {
            var blogGroups = await blogGroupRepository.GetEntitiesQuery().AsQueryable().Where(b => !b.IsDelete).ToListAsync();

            return blogGroups.MapToBlogGroupDTO();
        }

        #endregion

        #region UniqueTest
        public async Task<bool> CheckUniqueTitleAsync(string Title)
        {
            var exist = await GetBlogGroupByTitleAsync(Title);
            if (exist == null) return false;
            return true;
        }
        #endregion

        #region Add
        public async Task<bool> AddBlogGroup(BlogGroupDTO blogGroupDTO)
        {
            var blogGroup = new BlogGroup
            {
                Title = blogGroupDTO.Title,
                Description = blogGroupDTO.Description
            };

            try
            {
                await blogGroupRepository.AddEntity(blogGroup);
                await blogGroupRepository.SaveChanges();
                return true;
            }
            catch
            {

                return false;
            }


        }

        #endregion

        #region Edit
        public async Task<bool> EditBlogGroup(BlogGroupDTO blogGroupDTO)
        {
            var oldBlogGroup = await GetBlogGroupByIdAsync(blogGroupDTO.Id);

            oldBlogGroup.Title = blogGroupDTO.Title;
            oldBlogGroup.Description = blogGroupDTO.Description;


            try
            {
                blogGroupRepository.UpdateEntity(oldBlogGroup);
                await blogGroupRepository.SaveChanges();
                return true;
            }
            catch
            {

                return false;
            }
        }

        #endregion


        #region Remove
        public async Task<bool> RemoveBlogGroup(BlogGroupDTO blogGroupDTO)
        {
            var oldBlogGroup = await GetBlogGroupByIdAsync(blogGroupDTO.Id);

            oldBlogGroup.IsDelete = !oldBlogGroup.IsDelete;



            try
            {
                blogGroupRepository.UpdateEntity(oldBlogGroup);
                await blogGroupRepository.SaveChanges();
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
            blogGroupRepository?.Dispose();
        }



        #endregion
    }
}
