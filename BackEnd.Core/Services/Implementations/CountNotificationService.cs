using BackEnd.Core.DTOs.Notification;
using BackEnd.Core.Services.Interfaces;
using BackEnd.DataLayer.Entities.Account;
using BackEnd.DataLayer.Entities.Blog;
using BackEnd.DataLayer.Entities.Gallery;
using BackEnd.DataLayer.Entities.Site;
using BackEnd.DataLayer.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Core.Services.Implementations
{
    public class CountNotificationService: ICountNotificationService
    {
        #region Constructor
        private readonly IGenericRepository<BlogContent> blogContentRepository;
        private readonly IGenericRepository<User> userRepository;
        private readonly IGenericRepository<ImageGallery> galleryRepository;
        private readonly IGenericRepository<Appointment> appointmentRepository;
        public CountNotificationService(
                    IGenericRepository<BlogContent> blogContentRepository,
                    IGenericRepository<User> userRepository,
                    IGenericRepository<ImageGallery> galleryRepository,
                    IGenericRepository<Appointment> appointmentRepository
            )
        {
            this.appointmentRepository = appointmentRepository;
            this.blogContentRepository = blogContentRepository;
            this.galleryRepository = galleryRepository;
            this.userRepository = userRepository;
        }

        #endregion

        #region GetNotification
        public async Task<CountNotificationDTO> getCountNotifications()
        {
            var dto = new CountNotificationDTO
            {
                AppointmentCount = await appointmentRepository.GetEntitiesQuery().Where(s=>!s.IsDelete).CountAsync(),
                BlogsCount = await blogContentRepository.GetEntitiesQuery().CountAsync(),
                GalleryCount = await galleryRepository.GetEntitiesQuery().CountAsync(),
                UsersCount = await userRepository.GetEntitiesQuery().CountAsync(),
            };
            return dto;
        }
        #endregion

        #region Dispose
        public void Dispose()
        {
            appointmentRepository?.Dispose();
            blogContentRepository?.Dispose();
            galleryRepository?.Dispose();
            userRepository?.Dispose();
        }

   

        #endregion
    }
}
