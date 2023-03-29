
using EventBus.Messages.Common;
using Service.Sender.Application.IApplication;
using Service.Sender.Dtos;
using Service.Sender.Mapper;
using Service.Sender.Repository.IRepository;
using Shared.Dictionary;
using Shared.Error;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Service.Sender.Application
{
    public class NotificationHistoryApplication : INotificationHistoryApplication
    {
        private readonly INotificationStoryRepository _repository;
        public NotificationHistoryApplication(INotificationStoryRepository notificationStory)
        {
            _repository = notificationStory;
        }

        public async Task<List<NotificationContract>> getByFilter(NotificationFilterDto filter)
        {
            var notifications = await _repository.getByFilter(filter);
            if (notifications.Any())
            {
                return notifications.toNotificationDto();
            }

            return null;
        }
    }
}
