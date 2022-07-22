using EventBus.Messages.Catalog;
using MassTransit;
using Microsoft.Extensions.Logging;
using Shared.Dictionary;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Service.Catalog.Consumers.Error
{
    public class BranchErrorConsumer : IConsumer<Fault<BranchContract>>
    {
        private readonly ILogger<BranchErrorConsumer> _logger;

        public BranchErrorConsumer(ILogger<BranchErrorConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<Fault<BranchContract>> context)
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

            var exMessage = Responses.RabbitMQError("branch-queue", retry, contract.FullName, messageId.ToString(), messageData, messageEx);

            _logger.LogError(exMessage);

            return Task.CompletedTask;
        }
    }
}
