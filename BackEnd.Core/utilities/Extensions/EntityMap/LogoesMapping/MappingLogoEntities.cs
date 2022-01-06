using BackEnd.Core.DTOs.Logo;
using BackEnd.DataLayer.Entities.Site;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Core.utilities.Extensions.EntityMap.LogoesMapping
{
    public static class MappingLogoEntities
    {
        public static List<LogoDTO> MapToLogoDTO(this List<Logo> source)
        {
            var returnLogosDTO = new List<LogoDTO>();
            foreach (var item in source)
            {
                var vm = new LogoDTO
                {
                    Id = item.Id,
                    ImageName = item.ImageName,
                    Title = item.Title,
                    Description = item.Description,
                    IsDelete = item.IsDelete,

                };
                returnLogosDTO.Add(vm);
            }
            return returnLogosDTO;
        }
    }
}
