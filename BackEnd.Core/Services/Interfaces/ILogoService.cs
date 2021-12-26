using BackEnd.Core.DTOs.AdminLogo;
using BackEnd.Core.DTOs.Logo;
using BackEnd.DataLayer.Entities.Site;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Core.Services.Interfaces
{
    public interface ILogoService: IDisposable
    {
        Task<Logo> GetLogoByIdAsync(long Id);
        Task<FilterLogoDTO> GetLogoFilterPagingAsync(FilterLogoDTO filter);

        Task<LogoDTO.LogoResult> AddLogoAsync(LogoDTO logoDTO);
        Task<LogoDTO.LogoResult> ActiveLogoAsync(LogoDTO logoDTO);
        Task<LogoDTO.LogoResult> UpdateLogoAsync(Logo logo);
        Task<Logo> GetActiveLogo();
    }
}
