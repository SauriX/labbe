using Service.Catalog.Dtos.Notifications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Application.IApplication
{
    public interface INotificationsApplication
    {
        Task<IEnumerable<NotificationListDto>> GetAllNotifications(string search);
        Task<IEnumerable<NotificationListDto>> GetAllAvisos(string search);
        Task<NotificationFormDto> GetById(Guid Id);
        Task<NotificationListDto> Create(NotificationFormDto notification);
        Task<NotificationListDto> Update(NotificationFormDto notification);
        Task<NotificationListDto> UpdateStatus(Guid id);
    }
}
