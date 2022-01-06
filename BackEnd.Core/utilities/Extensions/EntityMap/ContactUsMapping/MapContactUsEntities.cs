using BackEnd.Core.DTOs.ContactUs;
using BackEnd.DataLayer.Entities.Site;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Core.utilities.Extensions.EntityMap.ContactUsMapping
{
    public static class MapContactUsEntities
    {
        public static List<ContactUsDTO> MapToContactUsDTO(this List<ContactUs> source)
        {
            var returnContactUsDTO = new List<ContactUsDTO>();
            foreach (var item in source)
            {
                var vm = new ContactUsDTO
                {
                    Id = item.Id,
                    Name = item.Name,
                    Status = item.Status,
                    Telephone = item.Telephone,
                    IsDelete = item.IsDelete,
                    Description = item.Description,
                    Email = item.Email,
                    Title = item.Title,
                    CreateDate = item.CreateDate
                };
                returnContactUsDTO.Add(vm);
            }
            return returnContactUsDTO;
        }
    }
}
