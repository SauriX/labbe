using EventBus.Messages.Common;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Service.Sender.SignalR;
using Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Sender.Consumers
{
    public class NotificationConsumer : IConsumer<NotificationContract>
    {
        private readonly ILogger<NotificationConsumer> _logger;
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationConsumer(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task Consume(ConsumeContext<NotificationContract> context)
        {
            try
            {
                var message = context.Message;

                await _hubContext.Clients.Group(message.Para).SendAsync("Notify", message);
            }
            catch (Exception ex)
            {
                var message = Exceptions.GetMessage(ex);
                _logger.LogError($"MessageId: {context.MessageId}\n{message}");

                throw;
            }
        }
    }
}
