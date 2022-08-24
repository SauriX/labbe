using EventBus.Messages.Catalog;
using MassTransit;
using Microsoft.Extensions.Logging;
using Service.Report.Domain.Catalogs;
using Service.Report.Repository.IRepository;
using Shared.Helpers;
using System.Threading.Tasks;

namespace Service.Report.Consumers
{
    public class BranchConsumer : IConsumer<BranchContract>
    {
        private readonly ILogger<BranchConsumer> _logger;
        private readonly IRepository<Branch> _repository;

        public BranchConsumer(ILogger<BranchConsumer> logger, IRepository<Branch> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<BranchContract> context)
        {
            try
            {
                var message = context.Message;
                var branch = new Branch(message.Id, message.Nombre);

                var existing = await _repository.GetOne(x => x.Id == message.Id);

                if (existing == null)
                {
                    await _repository.Create(branch);
                }
                else
                {
                    await _repository.Update(branch);
                }
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
