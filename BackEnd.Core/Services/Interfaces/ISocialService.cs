using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BackEnd.Core.DTOs.Social;
using BackEnd.DataLayer.Entities.Site;

namespace BackEnd.Core.Services.Interfaces
{
    public interface ISocialService: IDisposable
    {
        Task<List<Social>> GetAllActiveSocialsAsync();

        Task<Social> GetSocialByIdAsync(long Id);
        Task<FilterSocialDTO> GetSocialFilterPagingAsync(FilterSocialDTO filter);
        Task<bool> AddSocialAsync(SocialDTO socialDTO);

        Task<bool> EditSocialAsync(SocialDTO socialDTO);

        Task<bool> RemoveSocialAsync(SocialDTO socialDTO);
    }
}
