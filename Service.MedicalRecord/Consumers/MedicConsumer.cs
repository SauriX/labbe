using EventBus.Messages.Catalog;
using MassTransit;
using Microsoft.Extensions.Logging;
using Service.MedicalRecord.Domain.Catalogs;
using Service.MedicalRecord.Repository.IRepository;
using Shared.Helpers;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Consumers
{
    public class MedicConsumer : IConsumer<MedicContract>
    {
        private readonly ILogger<MedicConsumer> _logger;
        private readonly IRepository<Medic> _repository;

        public MedicConsumer(ILogger<MedicConsumer> logger, IRepository<Medic> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<MedicContract> context)
        {
            try
            {
                var message = context.Message;
                var medic = new Medic(message.Id, message.Clave, message.Nombre);

                var existing = await _repository.GetOne(x => x.Id == message.Id);

                if (existing == null)
                {
                    await _repository.Create(medic);
                }
                else
                {
                    await _repository.Update(medic);
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
