using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Core.DTOs.Paging
{
    public static class Pager
    {
        public static BasePaging Build(int pageCount, int activePage, int takeEntity)
        {
            if (activePage <= 1) activePage = 1;
            return new BasePaging
            {
                ActivePage = activePage,
                PageCount = pageCount,
                PageId = activePage,
                TakeEntity = takeEntity,
                SkipEntity = (activePage - 1) * takeEntity,
                StartPage = activePage - 3 <= 0 ? 1 : activePage - 3,
                EndPage = activePage + 3 > pageCount ? pageCount : activePage + 3
            };
        }
    }
}
