using BackEnd.DataLayer.Entities.Site;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Core.Services.Interfaces
{
    public interface ISliderService: IDisposable
    {
        Task<List<Slider>> GetAllSliders();
        Task<List<Slider>> GetAllActiveSliders();
        Task<Slider> GetSliderById(long id);
        Task AddSlider(Slider slider);
        Task UpdateSlider(Slider slider);

    }
}
