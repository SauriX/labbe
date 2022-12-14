using EventBus.Messages.Catalog;
using MassTransit;
using Microsoft.Extensions.Logging;
using Service.MedicalRecord.Domain.Catalogs;
using Service.MedicalRecord.Repository.IRepository;
using Shared.Helpers;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Consumers
{
    public class MaquilaConsumer : IConsumer<MaquilaContract>
    {
        private readonly ILogger<MaquilaConsumer> _logger;
        private readonly IRepository<Maquila> _repository;

        public MaquilaConsumer(ILogger<MaquilaConsumer> logger, IRepository<Maquila> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<MaquilaContract> context)
        {
            try
            {
                var message = context.Message;
                var maquila = new Maquila(message.Id, message.Nombre);

                var existing = await _repository.GetOne(x => x.Id == message.Id);

                if (existing == null)
                {
                    await _repository.Create(maquila);
                }
                else
                {
                    await _repository.Update(maquila);
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
