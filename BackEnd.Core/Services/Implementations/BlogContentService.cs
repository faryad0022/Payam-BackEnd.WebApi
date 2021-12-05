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
    public class BlogContentService : IBlogContentService
    {
        #region Constructor
        private readonly IGenericRepository<BlogContent> blogContentRepository;
        public BlogContentService(IGenericRepository<BlogContent> blogContentRepository)
        {
            this.blogContentRepository = blogContentRepository;
        }


        #endregion

        #region Get
        public async Task<List<BlogContent>> GetAllBlogsAOfBlogGroupAsync(long blogId)
        {
            return await blogContentRepository.GetEntitiesQuery().Where(b => b.BlogGroupId == blogId).ToListAsync();
        }

        public async Task<List<BlogContent>> GetAllBlogsAsync()
        {
            return await blogContentRepository.GetEntitiesQuery().ToListAsync();
        }

        public async Task<List<BlogContent>> GetAllBlogsByTagsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<BlogContent> GetBlogByIdAsync(long Id)
        {
            return await blogContentRepository.GetEntitiesQuery().Where(b => b.Id == Id).SingleOrDefaultAsync();

        }

        public async Task<List<BlogContent>> GetBlogByTitleAsync(string Title)
        {
            return await blogContentRepository.GetEntitiesQuery().Where(b => b.Title.Contains(Title)).ToListAsync();

        }
        #endregion

        #region Add
        public async Task<bool> AddBlog(BlogContentDTO blogContentDTO)
        {
            var blogContent = new BlogContent
            {
                Title = blogContentDTO.Title,
                ImageName = blogContentDTO.ImageName,
                IsSelected = blogContentDTO.IsSelected,
                Status = blogContentDTO.Status,
                Tags = blogContentDTO.Tags,
                Text = blogContentDTO.Text,
                ViewCount = blogContentDTO.ViewCount,
                BlogGroupId = blogContentDTO.BlogGroupId,
                UserId = blogContentDTO.UserId,
                UserName = blogContentDTO.UserName
            };

            try
            {
                await blogContentRepository.AddEntity(blogContent);
                await blogContentRepository.SaveChanges();
                return true;
            }
            catch
            {

                return false;
            }
        }
        #endregion

        #region Edit
        public async Task<bool> EditBlog(BlogContentDTO blogContentDTO)
        {
            var oldBlogContent = await GetBlogByIdAsync(blogContentDTO.Id);

            oldBlogContent.Title = blogContentDTO.Title;
            oldBlogContent.ImageName = blogContentDTO.ImageName;
            oldBlogContent.IsSelected = blogContentDTO.IsSelected;
            oldBlogContent.Status = blogContentDTO.Status;
            oldBlogContent.Tags = blogContentDTO.Tags;
            oldBlogContent.Text = blogContentDTO.Text;
            oldBlogContent.ViewCount = blogContentDTO.ViewCount;
            oldBlogContent.BlogGroupId = blogContentDTO.BlogGroupId;
            oldBlogContent.UserId = blogContentDTO.UserId;
            oldBlogContent.UserName = blogContentDTO.UserName;


            try
            {
                blogContentRepository.UpdateEntity(oldBlogContent);
                await blogContentRepository.SaveChanges();
                return true;
            }
            catch
            {

                return false;
            }
        }
        public async Task<bool> ChangeBlogStatus(long blogId, bool status)
        {
            var blog = await GetBlogByIdAsync(blogId);
            blog.Status = status;
            try
            {
                blogContentRepository.UpdateEntity(blog);
                await blogContentRepository.SaveChanges();
                return true;
            }
            catch 
            {

                return false;
            }

        }

        #endregion

        #region Unique
        public async Task<bool> CheckUniqueTitleAsync(string Title)
        {
            var exist = await GetBlogByTitleAsync(Title);
            if (exist == null) return false;
            return true;
        }
        #endregion

        #region Dispose
        public void Dispose()
        {
            blogContentRepository?.Dispose();
        }

        #endregion
    }
}
