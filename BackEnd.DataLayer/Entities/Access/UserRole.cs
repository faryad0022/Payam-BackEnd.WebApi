using BackEnd.DataLayer.Entities.Account;
using BackEnd.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.DataLayer.Entities.Access
{
    public class UserRole: BaseEntity
    {
        #region Properties
        public long UserId { get; set; }
        public long RoleId { get; set; }
        #endregion

        #region Relations
        public User User { get; set; }
        public Role Role { get; set; }
        #endregion
    }
}
