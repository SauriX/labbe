using EventBus.Messages.Catalog;
using MassTransit;
using Microsoft.Extensions.Logging;
using Shared.Dictionary;
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
            var contract = typeof(EmailConfigurationContract);
            var messageId = context.Message.FaultedMessageId;
            var messageEx = "";
            if (context.Message?.Exceptions != null && context.Message.Exceptions.Any())
            {
                messageEx = string.Join("\n", context.Message.Exceptions.Select(x => $"Exception: {x.Message}\nStackTrace: {x.StackTrace}"));
            }

            var messageData = "";
            if (context.Message?.Message != null)
            {
                messageData = JsonSerializer.Serialize(context.Message.Message);
            }

            var retry = context.GetRetryCount();

            var exMessage = Responses.RabbitMQError("email-configuration-queue", retry, contract.FullName, messageId.ToString(), messageData, messageEx);

            _logger.LogError(exMessage);

            return Task.CompletedTask;
        }
    }
}
