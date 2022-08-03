using EventBus.Messages.Catalog;
using MassTransit;
using Microsoft.Extensions.Logging;
using Shared.Helpers;
using System.Threading.Tasks;

namespace Service.Report.Consumers
{
    public class BranchConsumer : IConsumer<BranchContract>
    {
        private readonly ILogger<BranchConsumer> _logger;

        public BranchConsumer(ILogger<BranchConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<BranchContract> context)
        {
            try
            {
                var message = context.Message;

                await Task.CompletedTask;
            }
            catch (System.Exception ex)
            {
                var message = Exceptions.GetMessage(ex);
                _logger.LogError(message);

                throw;
            }
        }
    }
}
