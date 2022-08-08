using EventBus.Messages.Catalog;
using MassTransit;
using Microsoft.Extensions.Logging;
using Shared.Dictionary;
using Shared.Utils;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Service.Catalog.Consumers.Error
{
    public class MedicErrorConsumer : IConsumer<Fault<MedicContract>>
    {
        private readonly ILogger<MedicErrorConsumer> _logger;

        public MedicErrorConsumer(ILogger<MedicErrorConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<Fault<MedicContract>> context)
        {
            var error = new RabbitFaultLog<MedicContract>().GetLog(context);

            _logger.LogError(error);

            return Task.CompletedTask;
        }
    }
}
