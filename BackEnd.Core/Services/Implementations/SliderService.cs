using BackEnd.Core.DTOs.Paging;
using BackEnd.Core.DTOs.Sliders;
using BackEnd.Core.Services.Interfaces;
using BackEnd.Core.utilities.Extensions.Paging;
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
        private readonly IGenericRepository<Slider> sliderRepository;
        public SliderService(IGenericRepository<Slider> sliderRepository)
        {
            this.sliderRepository = sliderRepository;
        }

        #endregion

        #region Get
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

        public async Task<FilterSliderDTO> GetAllSlidersFilterPagingAsync(FilterSliderDTO filter)
        {
            var sliderQuery = sliderRepository.GetEntitiesQuery().OrderByDescending(s => s.LastUpdateDate).AsQueryable();//فیلتر نزولی بر اساس تاریخ
            if (!string.IsNullOrEmpty(filter.SearchKey))
            {
                sliderQuery = sliderQuery.Where(s => s.Title == filter.SearchKey);
            }

            var count = (int)Math.Ceiling(sliderQuery.Count() / (double)filter.TakeEntity);// تعداد صفحات
            var pager = Pager.Build(count, filter.PageId, filter.TakeEntity);
            var sliders = await sliderQuery.Paging(pager).ToListAsync();

            var returnSliders = new List<SliderDTO>();
            foreach (var item in sliders)
            {
                var vm = new SliderDTO
                {
                    Id = item.Id,
                    ImageName = item.ImageName,
                    Title = item.Title,
                    Link = item.Link,
                    Description = item.Description,
                    IsDelete = item.IsDelete,
                };
                returnSliders.Add(vm);
            }
            return filter.SetSliders(returnSliders).SetPaging(pager);
        }



        #endregion

        #region Add
        public async Task<SliderDTO.SliderResult> AddSliderAsync(SliderDTO sliderDTO)
        {
            var slider = new Slider
            {
                Description = sliderDTO.Description,
                Link = sliderDTO.Link,
                Title = sliderDTO.Title,
                IsDelete = false,
                ImageName = sliderDTO.ImageName,
            };
            try
            {
                await sliderRepository.AddEntity(slider);
                await sliderRepository.SaveChanges();
                return SliderDTO.SliderResult.Success;
            }
            catch (Exception)
            {

                return SliderDTO.SliderResult.ServerError;
            }
        }

        #endregion

        #region Update
        public async Task<SliderDTO.SliderResult> UpdateSliderAsync(SliderDTO sliderDTO)
        {
            var slider = await GetSliderById(sliderDTO.Id);
            if (slider == null) return SliderDTO.SliderResult.NotFound;

            slider.ImageName = sliderDTO.ImageName;
            slider.Link = sliderDTO.Link;
            slider.Title = sliderDTO.Title;
            slider.Description = sliderDTO.Description;
            slider.IsDelete = sliderDTO.IsDelete;

            try
            {
                sliderRepository.UpdateEntity(slider);
                await sliderRepository.SaveChanges();
                return SliderDTO.SliderResult.Success;
            }
            catch (Exception)
            {

                return SliderDTO.SliderResult.ServerError;
            }
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
