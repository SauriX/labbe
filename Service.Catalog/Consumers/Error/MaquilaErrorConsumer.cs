using EventBus.Messages.Catalog;
using MassTransit;
using Microsoft.Extensions.Logging;
using Shared.Utils;
using System.Threading.Tasks;

namespace Service.Catalog.Consumers.Error
{
    public class MaquilaErrorConsumer : IConsumer<Fault<MaquilaContract>>
    {
        private readonly ILogger<MaquilaErrorConsumer> _logger;

        public MaquilaErrorConsumer(ILogger<MaquilaErrorConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<Fault<MaquilaContract>> context)
        {
            var error = new RabbitFaultLog<MaquilaContract>().GetLog(context);

            _logger.LogError(error);

            return Task.CompletedTask;
        }
    }
}
