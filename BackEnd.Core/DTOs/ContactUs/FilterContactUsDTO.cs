

using BackEnd.Core.DTOs.Paging;
using System.Collections.Generic;

namespace BackEnd.Core.DTOs.ContactUs
{
    public class FilterContactUsDTO : BasePaging
    {
        public string SearchKey { get; set; }
        public bool showRemoved { get; set; } = false;

        public List<ContactUsDTO> ContactUsList { get; set; }

        public FilterContactUsDTO SetPaging(BasePaging paging)
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
        public FilterContactUsDTO SetContactUsList(List<ContactUsDTO> contactUsList)
        {
            this.ContactUsList = contactUsList;
            return this;
        }
    }
}
