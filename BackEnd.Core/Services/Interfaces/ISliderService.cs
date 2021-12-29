using BackEnd.Core.DTOs.Sliders;
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
        Task<FilterSliderDTO> GetAllSlidersFilterPagingAsync(FilterSliderDTO filter);

        Task<SliderDTO.SliderResult> AddSliderAsync(SliderDTO sliderDTO);
        Task<SliderDTO.SliderResult> UpdateSliderAsync(SliderDTO sliderDTO);



    }
}
