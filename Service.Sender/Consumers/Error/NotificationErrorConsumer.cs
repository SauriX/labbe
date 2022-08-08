using EventBus.Messages.Common;
using MassTransit;
using Microsoft.Extensions.Logging;
using Shared.Dictionary;
using Shared.Utils;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Service.Sender.Consumers.Error
{
    public class NotificationErrorConsumer : IConsumer<Fault<NotificationContract>>
    {
        private readonly ILogger<NotificationErrorConsumer> _logger;

        public NotificationErrorConsumer(ILogger<NotificationErrorConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<Fault<NotificationContract>> context)
        {
            var error = new RabbitFaultLog<NotificationContract>().GetLog(context);

            _logger.LogError(error);

            return Task.CompletedTask;
        }
    }
}
