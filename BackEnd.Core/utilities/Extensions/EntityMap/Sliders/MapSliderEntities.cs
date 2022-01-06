using BackEnd.Core.DTOs.Sliders;
using BackEnd.DataLayer.Entities.Site;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Core.utilities.Extensions.EntityMap.Sliders
{
    public static class MapSliderEntities
    {
        public static List<SliderDTO> MapToSliderDTOList(this List<Slider> source)
        {
            var returnSlidersDTO = new List<SliderDTO>();
            foreach (var item in source)
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
                returnSlidersDTO.Add(vm);
            }
            return returnSlidersDTO;
        }
    }
}
