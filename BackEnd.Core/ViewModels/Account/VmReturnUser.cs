using BackEnd.Core.DTOs.Account;
using BackEnd.DataLayer.Entities.Access;
using BackEnd.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Core.ViewModels.Account
{
    public class VmReturnUser
    {
        #region propertie

        public long Id { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public bool IsActivated { get; set; }
        public string Token { get; set; }
        public int ExpireTime { get; set; }
        public List<RoleDTO> Roles { get; set; }
        #endregion
    }
}
