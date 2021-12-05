using BackEnd.Core.DTOs.Blog;
using BackEnd.Core.Services.Interfaces;
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
        public async  Task<List<BlogGroup>> GetAllBlogGroupsAsync()
        {
            return await blogGroupRepository.GetEntitiesQuery().ToListAsync();
        }

        public async Task<BlogGroup> GetBlogGroupByIdAsync(long Id)
        {
            return await blogGroupRepository.GetEntityById(Id);
        }

        public async Task<BlogGroup> GetBlogGroupByTitleAsync(string Title)
        {
            return  await blogGroupRepository.GetEntitiesQuery().Where(b => b.Title == Title).SingleOrDefaultAsync();
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

        #region Dispose
        public void Dispose()
        {
            blogGroupRepository?.Dispose();
        }



        #endregion
    }
}
