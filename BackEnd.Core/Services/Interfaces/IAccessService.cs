using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Core.Services.Interfaces
{
    public interface IAccessService: IDisposable
    {
        #region user role
        Task<bool> CheckUserRoleAsync(long userId, string roleName);
        #endregion
    }
}
