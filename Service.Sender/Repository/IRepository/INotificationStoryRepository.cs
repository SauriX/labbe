using Service.Sender.Domain.NotificationHistory;
using Service.Sender.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Sender.Repository.IRepository
{
    public interface INotificationStoryRepository
    {
        Task<List<NotificationHistory>> getByFilter(NotificationFilterDto filter);
        Task createNotification(NotificationHistory notification);
    }
}
