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
    public class BranchErrorConsumer : IConsumer<Fault<BranchContract>>
    {
        private readonly ILogger<BranchErrorConsumer> _logger;

        public BranchErrorConsumer(ILogger<BranchErrorConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<Fault<BranchContract>> context)
        {
            var error = new RabbitFaultLog<BranchContract>().GetLog(context);

            _logger.LogError(error);

            return Task.CompletedTask;
        }
    }
}
