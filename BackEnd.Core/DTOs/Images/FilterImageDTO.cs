using System.Collections.Generic;
using BackEnd.Core.DTOs.Paging;
using BackEnd.DataLayer.Entities.Gallery;

namespace BackEnd.Core.DTOs.Images
{
    public class FilterImageDTO: BasePaging
    {
        public string Title { get; set; } = "";
        public List<ImageGallery> Images { get; set; }

        public FilterImageDTO SetPaging(BasePaging paging)
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
        public FilterImageDTO SetImages(List<ImageGallery> images)
        {
            this.Images = images;
            return this;
        }


    }
}
