using BackEnd.Core.DTOs.Images;
using BackEnd.DataLayer.Entities.Gallery;
using System.Collections.Generic;

namespace BackEnd.Core.utilities.Extensions.EntityMap.ImageGalleryMapping
{
    public static class MapImageGalleryEntities
    {
        public static List<ImageGalleryDTO> MapToImageGalleryDTO(this List<ImageGallery> source)
        {
            var returnImageGalleryDTO = new List<ImageGalleryDTO>();
            foreach (var image in source)
            {
                var imageDTO = new ImageGalleryDTO
                {
                    Description = image.Description,
                    Id = image.Id,
                    ImageName = image.ImageName,
                    Title = image.Title
                };
                returnImageGalleryDTO.Add(imageDTO);
            }
            return returnImageGalleryDTO;
        }
    }
}
