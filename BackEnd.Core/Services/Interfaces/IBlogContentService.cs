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
        Task<BlogContent> GetBlogByIdAsync(long Id);
        Task<List<BlogContent>> GetBlogByTitleAsync(string Title);
        Task<FilterBlogDTO> GetFilterBlogs(FilterBlogDTO filter);

        Task<List<SiteBlogDTO>> GetLatestBlogs();
        Task<FilterSiteBlogDTO> GetSiteFilterBlogs(FilterSiteBlogDTO filter);


        Task<bool> CheckUniqueTitleAsync(string Title);

        Task<bool> AddBlogAsync(BlogContentDTO blogContentDTO);

        Task<bool> EditBlogAsync(BlogContentDTO blogContentDTO);
        Task<bool> ChangeBlogStatusAsync(long id);

        Task<bool> DeleteBlogAsync(long Id);

    }
}
