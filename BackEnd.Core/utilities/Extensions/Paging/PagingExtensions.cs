using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BackEnd.Core.DTOs.Paging;

namespace BackEnd.Core.utilities.Extensions.Paging
{
    public static class PagingExtensions
    {
        public static IQueryable<T> Paging<T>(this IQueryable<T> queryable, BasePaging pager)
        {
            return queryable.Skip(pager.SkipEntity).Take(pager.TakeEntity);
        }
    }
}
