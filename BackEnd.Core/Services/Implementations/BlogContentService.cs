using BackEnd.Core.DTOs.Blog;
using BackEnd.Core.DTOs.Paging;
using BackEnd.Core.Services.Interfaces;
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

        public  Task<List<BlogContent>> GetAllBlogsByTagsAsync()
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

        public async Task<FilterBlogDTO> GetFilterBlogs(FilterBlogDTO filter)
        {
            var blogQuery = blogContentRepository.GetEntitiesQuery().Where(bg=>!bg.BlogGroup.IsDelete).OrderByDescending(b => b.LastUpdateDate).AsQueryable();
            if (!string.IsNullOrEmpty(filter.Title))
            {
                blogQuery = blogQuery.Include(s=>s.BlogGroup).Where(b => b.BlogGroup.Title == filter.Title);
            }

            var count = (int)Math.Ceiling(blogQuery.Count() / (double)filter.TakeEntity); // بدست آوردن تعداد صفحات
            var pager = Pager.Build(count, filter.PageId, filter.TakeEntity);
            var blogs = await blogQuery.Paging(pager).ToListAsync();
            var returnBlogs = new List<VmReturnBlog>();
            foreach (var blog in blogs)
            {
                var vm = new VmReturnBlog
                {
                    Id = blog.Id,
                    Title = blog.Title,
                    IsDelete = blog.IsDelete,
                    Tags = blog.Tags,
                    ImageName = blog.ImageName,
                    Text = blog.Text,
                    BlogGroupId = blog.BlogGroupId,
                    UserId = blog.UserId,
                    UserName = blog.UserName,
                    Status = blog.Status,
                    IsSelected = blog.IsSelected,
                    ViewCount = blog.ViewCount,
                    BlogGroupName = blog.BlogGroupName

                };
                returnBlogs.Add(vm);
            }

            return filter.SetPaging(pager).SetBlogs(returnBlogs);
        }


        #endregion

        #region Get For Site
        public async Task<List<SiteBlogDTO>> GetLatestBlogs()
        {
            var blogs =  await blogContentRepository.GetEntitiesQuery().OrderByDescending(s => s.LastUpdateDate).Take(4).AsQueryable().ToListAsync();
            var returnLastBlogsDTO = new List<SiteBlogDTO>();
            foreach (var item in blogs)
            {
                var vm = new SiteBlogDTO
                {
                    BlogGroupId = item.BlogGroupId,
                    BlogGroupName = item.BlogGroupName,
                    ImageName = item.ImageName,
                    Id = item.Id,
                    Tags = item.Tags,
                    Text = item.Text,
                    Title = item.Title,
                    ViewCount = item.ViewCount
                };
                returnLastBlogsDTO.Add(vm);
            }
            return returnLastBlogsDTO;
        }
        public async Task<FilterSiteBlogDTO> GetSiteFilterBlogs(FilterSiteBlogDTO filter)
        {
            var blogQuery = blogContentRepository.GetEntitiesQuery().Where(bg => !bg.BlogGroup.IsDelete).OrderByDescending(b => b.LastUpdateDate).AsQueryable();
            if (!string.IsNullOrEmpty(filter.Title) && filter.Title != "All")
            {
                blogQuery = blogQuery.Include(s => s.BlogGroup).Where(b => b.BlogGroup.Title == filter.Title);
            }

            var count = (int)Math.Ceiling(blogQuery.Count() / (double)filter.TakeEntity); // بدست آوردن تعداد صفحات
            var pager = Pager.Build(count, filter.PageId, filter.TakeEntity);
            var blogs = await blogQuery.Paging(pager).ToListAsync();
            var returnBlogs = new List<SiteBlogDTO>();
            foreach (var item in blogs)
            {
                var vm = new SiteBlogDTO
                {
                    BlogGroupId = item.BlogGroupId,
                    BlogGroupName = item.BlogGroupName,
                    ImageName = item.ImageName,
                    Id = item.Id,
                    Tags = item.Tags,
                    Text = item.Text,
                    Title = item.Title,
                    ViewCount = item.ViewCount

                };
                returnBlogs.Add(vm);
            }

            return filter.SetPaging(pager).SetBlogs(returnBlogs);
        }
        #endregion

        #region Add
        public async Task<bool> AddBlogAsync(BlogContentDTO blogContentDTO)
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
                UserName = blogContentDTO.UserName,
                BlogGroupName = blogContentDTO.BlogGroupName
            };

            try
            {
                await blogContentRepository.AddEntity(blogContent);
                await blogContentRepository.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        #endregion

        #region Edit
        public async Task<bool> EditBlogAsync(BlogContentDTO blogContentDTO)
        {
            var oldBlogContent = await GetBlogByIdAsync(blogContentDTO.Id);

            oldBlogContent.Title = blogContentDTO.Title;
            oldBlogContent.ImageName = blogContentDTO.ImageName;
            oldBlogContent.Tags = blogContentDTO.Tags;
            oldBlogContent.Text = blogContentDTO.Text;
            oldBlogContent.BlogGroupId = blogContentDTO.BlogGroupId;
            oldBlogContent.UserId = blogContentDTO.UserId;
            oldBlogContent.UserName = blogContentDTO.UserName;
            oldBlogContent.BlogGroupName = blogContentDTO.BlogGroupName;


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


        #endregion

        #region Change status
        public async Task<bool> ChangeBlogStatusAsync(long id)
        {
            var oldBlog = await GetBlogByIdAsync(id);

            oldBlog.Status = !oldBlog.Status;



            try
            {
                blogContentRepository.UpdateEntity(oldBlog);
                await blogContentRepository.SaveChanges();
                return true;
            }
            catch
            {

                return false;
            }
        }

        #endregion

        #region Delete
        public async Task<bool> DeleteBlogAsync(long Id)
        {
            var blog = await GetBlogByIdAsync(Id);
            try
            {
                blogContentRepository.DeleteEntity(blog);
                await blogContentRepository.SaveChanges();
                return true;
            }
            catch (Exception)
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
