using EventBus.Messages.Catalog;
using MassTransit;
using Microsoft.Extensions.Logging;
using Service.MedicalRecord.Domain.Catalogs;
using Service.MedicalRecord.Repository.IRepository;
using Shared.Helpers;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Consumers
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
                var branch = new Branch(message.Id, message.Clave, message.Nombre, message.Clinicos, message.CodigoPostal, message.CiudadId);

                var existing = await _repository.GetOne(x => x.Id == message.Id);

                if (existing == null)
                {
                    await _repository.Create(branch);
                }
                else
                {
                    await _repository.Create(branch);
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
