using BackEnd.Core.DTOs.Paging;
using BackEnd.DataLayer.Entities.Site;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Core.DTOs.Address
{
    public class FilterAddressDTO : BasePaging
    {
        public string Title { get; set; }
        public List<ContactAddress> Addresses { get; set; }

        public FilterAddressDTO SetPaging(BasePaging paging)
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
        public FilterAddressDTO SetImages(List<ContactAddress> addresses)
        {
            this.Addresses = addresses;
            return this;
        }
    }
}
