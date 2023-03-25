using EventBus.Messages.Common;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Service.Sender.Mapper;
using Service.Sender.Repository.IRepository;
using Service.Sender.SignalR;
using Shared.Helpers;
using System;
using System.Threading.Tasks;

namespace Service.Sender.Consumers
{
    public class NotificationConsumer : IConsumer<NotificationContract>
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly INotificationStoryRepository _notificationStoryRepository;

        public NotificationConsumer(IHubContext<NotificationHub> hubContext,INotificationStoryRepository notificationStory)
        {
            _hubContext = hubContext;
            _notificationStoryRepository = notificationStory;
        }

        public async Task Consume(ConsumeContext<NotificationContract> context)
        {
            try
            {
                var message = context.Message;
                var newMessage = message.toNotificationHistory();
                await _notificationStoryRepository.createNotification(newMessage);
                await _hubContext.Clients.Group(message.Para).SendAsync("Notify", message);

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
