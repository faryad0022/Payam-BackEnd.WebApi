using BackEnd.Core.DTOs.Paging;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Core.DTOs.Blog
{
    public class FilterSiteBlogDTO : BasePaging
    {
        public string Title { get; set; }
        public List<SiteBlogDTO> Blogs { get; set; }

        public FilterSiteBlogDTO SetPaging(BasePaging paging)
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
        public FilterSiteBlogDTO SetBlogs(List<SiteBlogDTO> blogs)
        {
            this.Blogs = blogs;
            return this;
        }
    }
}
