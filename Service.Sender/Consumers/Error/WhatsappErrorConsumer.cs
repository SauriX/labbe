using EventBus.Messages.Common;
using MassTransit;
using Microsoft.Extensions.Logging;
using Shared.Dictionary;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Service.Sender.Consumers.Error
{
    public class WhatsappErrorConsumer : IConsumer<Fault<WhatsappContract>>
    {
        private readonly ILogger<WhatsappErrorConsumer> _logger;

        public WhatsappErrorConsumer(ILogger<WhatsappErrorConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<Fault<WhatsappContract>> context)
        {
            var contract = typeof(WhatsappContract);
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

            var exMessage = Responses.RabbitMQError("email-queue", retry, contract.FullName, messageId.ToString(), messageData, messageEx);

            _logger.LogError(exMessage);

            return Task.CompletedTask;
        }
    }
}
