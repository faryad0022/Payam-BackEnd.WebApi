using BackEnd.Core.DTOs.Blog;
using BackEnd.DataLayer.Entities.Blog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Core.Services.Interfaces
{
    public interface IBlogContentService: IDisposable
    {
        Task<List<BlogContent>> GetAllBlogsAsync();
        Task<List<BlogContent>> GetAllBlogsAOfBlogGroupAsync(long blogId);
        Task<List<BlogContent>> GetAllBlogsByTagsAsync();
        Task<BlogContent> GetBlogByIdAsync(long Id);
        Task<List<BlogContent>> GetBlogByTitleAsync(string Title);


        Task<bool> CheckUniqueTitleAsync(string Title);

        Task<bool> AddBlog(BlogContentDTO blogContentDTO);

        Task<bool> EditBlog(BlogContentDTO blogContentDTO);
        Task<bool> ChangeBlogStatus(long blogId,bool status);
    }
}
