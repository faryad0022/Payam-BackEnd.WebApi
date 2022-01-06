using BackEnd.Core.DTOs.Blog;
using BackEnd.DataLayer.Entities.Blog;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Core.utilities.Extensions.EntityMap.BlogSection
{
    public static class MapBlogSectionEntities
    {
        public static List<BlogGroupDTO> MapToBlogGroupDTO(this List<BlogGroup> source)
        {
            var returnBlogGroupsDTO = new List<BlogGroupDTO>();
            foreach (var blogGroup in source)
            {
                var vm = new BlogGroupDTO
                {
                    Id = blogGroup.Id,
                    Description = blogGroup.Description,
                    Title = blogGroup.Title,
                    IsDelete = blogGroup.IsDelete
                };
                returnBlogGroupsDTO.Add(vm);
            }
            return returnBlogGroupsDTO;
        }

        public static List<BlogContentDTO> MapToBlogContentDTO(this List<BlogContent> source)
        {
            var returnBlogs = new List<BlogContentDTO>();
            foreach (var blog in source)
            {
                var vm = new BlogContentDTO
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

            return returnBlogs;
        }

        public static List<SiteBlogDTO> MapToSiteBlogContentDTO(this List<BlogContent> source)
        {
            var returnBlogs = new List<SiteBlogDTO>();
            foreach (var item in source)
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
            return returnBlogs;
        }
    }
}
