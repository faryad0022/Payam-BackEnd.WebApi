using BackEnd.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Core.ViewModels.Account
{
    public class VmReturnUser : BaseEntity
    {
        #region propertie
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public bool IsActivated { get; set; }
        public string Token { get; set; }
        public int ExpireTime { get; set; }
#endregion
    }
}
