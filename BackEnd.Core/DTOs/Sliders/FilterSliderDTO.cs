using BackEnd.Core.DTOs.Paging;
using System.Collections.Generic;

namespace BackEnd.Core.DTOs.Sliders
{
    public class FilterSliderDTO : BasePaging
    {
        public string SearchKey { get; set; }
        public List<SliderDTO> Sliders { get; set; }

        public FilterSliderDTO SetPaging(BasePaging paging)
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
        public FilterSliderDTO SetSliders(List<SliderDTO> sliders)
        {
            this.Sliders = sliders;
            return this;
        }
    }
}
