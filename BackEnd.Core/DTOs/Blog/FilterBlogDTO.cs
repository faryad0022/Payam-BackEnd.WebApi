using BackEnd.Core.DTOs.Paging;
using BackEnd.Core.ViewModels.Blog;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Core.DTOs.Blog
{
    public class FilterBlogDTO : BasePaging
    {
        public string Title { get; set; }
        public List<VmReturnBlog> Blogs { get; set; }

        public FilterBlogDTO SetPaging(BasePaging paging)
        {
            this.PageId = paging.PageId;
            this.PageCount = paging.PageCount;
            this.ActivePage = paging.ActivePage;
            this.StartPage = paging.StartPage;
            this.EndPage = paging.EndPage;
            this.TakeEntity = paging.TakeEntity;
            this.SkipEntity = paging.SkipEntity;
            return this;
        }
        public FilterBlogDTO SetBlogs(List<VmReturnBlog> blogs)
        {
            this.Blogs = blogs;
            return this;
        }
    }
}
