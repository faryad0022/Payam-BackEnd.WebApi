using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BackEnd.DataLayer.Entities.Site;

namespace BackEnd.Core.Services.Interfaces
{
    public interface ISocialService: IDisposable
    {
        Task<List<Social>> GetAllActiveSocialsAsync();
    }
}
