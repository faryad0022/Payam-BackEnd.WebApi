﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackEnd.Core.DTOs.Images;
using BackEnd.Core.DTOs.Paging;
using BackEnd.Core.Services.Interfaces;
using BackEnd.Core.utilities.Extensions.Paging;
using BackEnd.DataLayer.Entities.Gallery;
using BackEnd.DataLayer.Repository;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Core.Services.Implementations
{
    public class ImageGaleryService : IImageGalleryService
    {
        #region Constructor

        private IGenericRepository<ImageGallery> imageRepository;

        public ImageGaleryService(IGenericRepository<ImageGallery> imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        #endregion

        #region Filter && Paging
        public async Task<FilterImageDTO> FilterImagesAsync(FilterImageDTO filter)
        {
            var imageQuery = imageRepository.GetEntitiesQuery().AsQueryable();
            if (!string.IsNullOrEmpty(filter.Title))
            {
                imageQuery = imageQuery.Where(s => s.Title.Contains(filter.Title));
            }

            var count = (int)Math.Ceiling(imageQuery.Count() / (double)filter.TakeEntity);// تعداد صفحات
            var pager = Pager.Build(count, filter.PageId, filter.TakeEntity);
            var images = await imageQuery.Paging(pager).ToListAsync();
            return filter.SetImages(images).SetPaging(pager);
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