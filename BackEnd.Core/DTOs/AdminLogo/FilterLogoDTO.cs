using BackEnd.Core.DTOs.Logo;
using BackEnd.Core.DTOs.Paging;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Core.DTOs.AdminLogo
{
    public class FilterLogoDTO: BasePaging
    {
        public string Title { get; set; }
        public List<LogoDTO> Logoes { get; set; }

        public FilterLogoDTO SetPaging(BasePaging paging)
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
        public FilterLogoDTO SetLogos(List<LogoDTO> logoes)
        {
            this.Logoes = logoes;
            return this;
        }
    }
}
