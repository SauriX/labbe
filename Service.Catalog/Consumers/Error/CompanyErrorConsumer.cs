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
    public class CompanyErrorConsumer : IConsumer<Fault<CompanyContract>>
    {
        private readonly ILogger<CompanyErrorConsumer> _logger;

        public CompanyErrorConsumer(ILogger<CompanyErrorConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<Fault<CompanyContract>> context)
        {
            var error = new RabbitFaultLog<CompanyContract>().GetLog(context);

            _logger.LogError(error);

            return Task.CompletedTask;
        }
    }
}
