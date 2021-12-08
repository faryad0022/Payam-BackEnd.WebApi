using BackEnd.Core.DTOs.Blog;
using BackEnd.DataLayer.Entities.Blog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Core.Services.Interfaces
{
    public interface IBlogGroupService: IDisposable
    {
        Task<List<BlogGroup>> GetAllBlogGroupsAsync();
        Task<BlogGroup> GetBlogGroupByIdAsync(long Id);
        Task<BlogGroup> GetBlogGroupByTitleAsync(string Title);
        Task<FilterBlogGroupDTO> GetFilterBlogGourps(FilterBlogGroupDTO filter);

        Task<bool> CheckUniqueTitleAsync(string Title);

        Task<bool> AddBlogGroup(BlogGroupDTO blogGroupDTO);

        Task<bool> EditBlogGroup(BlogGroupDTO blogGroupDTO);

        Task<bool> RemoveBlogGroup(BlogGroupDTO blogGroupDTO);



    }
}
