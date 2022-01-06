using BackEnd.Core.DTOs.Paging;
using BackEnd.DataLayer.Entities.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Core.DTOs.Account
{
    public class FilterUserDTO : BasePaging
    {
        public string UserName { get; set; }
        public List<UserDTO> Users { get; set; }

        public FilterUserDTO SetPaging(BasePaging paging)
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
        public FilterUserDTO SetUsers(List<UserDTO> users)
        {
            this.Users = users;
            return this;
        }
    }
}
