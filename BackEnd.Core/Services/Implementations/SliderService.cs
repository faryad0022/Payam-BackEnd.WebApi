using BackEnd.Core.Services.Interfaces;
using BackEnd.DataLayer.Entities.Site;
using BackEnd.DataLayer.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Core.Services.Implementations
{
    public class SliderService : ISliderService
    {
        #region Constructor
        private IGenericRepository<Slider> sliderRepository;
        public SliderService(IGenericRepository<Slider> sliderRepository)
        {
            this.sliderRepository = sliderRepository;
        }

        #endregion

        #region All Actions For Slider
        public async Task<List<Slider>> GetAllActiveSliders()
        {
            return await sliderRepository.GetEntitiesQuery().Where(s=>!s.IsDelete).ToListAsync();
        }

        public async Task<List<Slider>> GetAllSliders()
        {
            return await sliderRepository.GetEntitiesQuery().ToListAsync();
        }

        public async Task<Slider> GetSliderById(long id)
        {
            return await sliderRepository.GetEntityById(id);
        }

        public async Task UpdateSlider(Slider slider)
        {
            sliderRepository.UpdateEntity(slider);
            await sliderRepository.SaveChanges();
        }

        public async Task AddSlider(Slider slider)
        {
            await sliderRepository.AddEntity(slider);
            await sliderRepository.SaveChanges();
        }


        #endregion

        #region dispose
        public void Dispose()
        {
            sliderRepository?.Dispose();
        }


        #endregion
    }
}
