using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BackEnd.Core.DTOs.Images;

namespace BackEnd.Core.Services.Interfaces
{
    public interface IImageGalleryService: IDisposable
    {
        Task<FilterImageDTO> FilterImagesAsync(FilterImageDTO filter);
        Task<bool> ImageExistById(long Id);
        Task<bool> UploadImageToGalleryAsync(ImageGalleryDTO image);
        Task<bool> DeleteImage(ImageGalleryDTO image);
    }
}
