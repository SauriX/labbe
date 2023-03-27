using EventBus.Messages.Common;
using Service.Sender.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Sender.Application.IApplication
{
    public interface INotificationHistoryApplication
    {
        Task<List<NotificationContract>> getByFilter(NotificationFilterDto filter);
    }
}
