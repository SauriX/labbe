using EventBus.Messages.Catalog;
using MassTransit;
using Microsoft.Extensions.Logging;
using Shared.Dictionary;
using Shared.Utils;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Service.Sender.Consumers.Error
{
    public class EmailConfigurationErrorConsumer : IConsumer<Fault<EmailConfigurationContract>>
    {
        private readonly ILogger<EmailConfigurationErrorConsumer> _logger;

        public EmailConfigurationErrorConsumer(ILogger<EmailConfigurationErrorConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<Fault<EmailConfigurationContract>> context)
        {
            var error = new RabbitFaultLog<EmailConfigurationContract>().GetLog(context);

            _logger.LogError(error);

            return Task.CompletedTask;
        }
    }
}
