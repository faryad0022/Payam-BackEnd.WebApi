using BackEnd.Core.DTOs.About;
using System;
using System.Threading.Tasks;

namespace BackEnd.Core.Services.Interfaces
{
    public interface IAboutService: IDisposable
    {
        Task<AboutDTO> GetAboutAsync();
        Task<AboutDTO.AboutResult> AddAboutAsync(AboutDTO aboutDTO);
        Task<AboutDTO.AboutResult> UpdateAboutAsync(AboutDTO aboutDTO);
    }
}
