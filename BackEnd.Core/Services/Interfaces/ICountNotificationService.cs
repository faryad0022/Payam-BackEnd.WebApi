using BackEnd.Core.DTOs.Notification;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Core.Services.Interfaces
{
    public interface ICountNotificationService: IDisposable
    {
        Task<CountNotificationDTO> getCountNotifications();
    }
}
