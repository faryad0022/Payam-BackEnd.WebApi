using BackEnd.DataLayer.Entities.Account;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Core.Services.Implementations
{
    public interface IUserService: IDisposable
    {
        Task<List<User>> GetAllUsers();
    }
}
