using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackEnd.Core.DTOs.Images;
using BackEnd.Core.DTOs.Paging;
using BackEnd.Core.Services.Interfaces;
using BackEnd.Core.utilities.Extensions.EntityMap.ImageGalleryMapping;
using BackEnd.Core.utilities.Extensions.Paging;
using BackEnd.DataLayer.Entities.Gallery;
using BackEnd.DataLayer.Repository;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Core.Services.Implementations
{
    public class ImageGaleryService : IImageGalleryService
    {
        #region Constructor

        private readonly IGenericRepository<ImageGallery> imageRepository;

        public ImageGaleryService(IGenericRepository<ImageGallery> imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        #endregion

        #region Get
        public async Task<bool> ImageExistById(long Id)
        {
            var img = await imageRepository.GetEntityById(Id);
            if (img == null) return false;
            return true;
        }

        #endregion

        #region Filter && Paging
        public async Task<FilterImageDTO> FilterImagesAsync(FilterImageDTO filter)
        {
            var imageQuery = imageRepository.GetEntitiesQuery().OrderByDescending(s=>s.CreateDate).AsQueryable();//فیلتر نزولی بر اساس تاریخ
            if (!string.IsNullOrEmpty(filter.Title))
            {
                imageQuery = imageQuery.Where(s => s.Title.Contains(filter.Title));
            }

            var count = (int)Math.Ceiling(imageQuery.Count() / (double)filter.TakeEntity);// تعداد صفحات
            var pager = Pager.Build(count, filter.PageId, filter.TakeEntity);
            var images = await imageQuery.Paging(pager).ToListAsync();
            return filter.SetImages(images.MapToImageGalleryDTO()).SetPaging(pager);
        }


        #endregion

        #region UploadImage

        public async Task<bool> UploadImageToGalleryAsync(ImageGalleryDTO image)
        {
            var img = new ImageGallery
            {
                Description = image.Description,
                ImageComments = null,
                ImageName = image.ImageName,
                Title = image.Title
            };
            try
            {
                await imageRepository.AddEntity(img);
                await imageRepository.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Delete
        public async Task<bool> DeleteImage(ImageGalleryDTO image)
        {
            try
            {
                var img = await imageRepository.GetEntityById(image.Id);
                imageRepository.DeleteEntity(img);
                await imageRepository.SaveChanges();
                return true;
            }
            catch 
            {

                return false;
            }

        }

        #endregion

        #region Dispose

        public void Dispose()
        {
            imageRepository?.Dispose();
        }

        #endregion

    }
}
