using BackEnd.Core.DTOs.Paging;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Core.DTOs.Social
{
    public class FilterSocialDTO : BasePaging
    {
        public string SearchKey { get; set; }
        public List<SocialDTO> Socials { get; set; }

        public FilterSocialDTO SetPaging(BasePaging paging)
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
        public FilterSocialDTO SetSocials(List<SocialDTO> socials)
        {
            this.Socials = socials;
            return this;
        }
    }
}
