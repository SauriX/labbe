using EventBus.Messages.Catalog;
using MassTransit;
using Microsoft.Extensions.Logging;
using Service.MedicalRecord.Domain.Catalogs;
using Service.MedicalRecord.Repository.IRepository;
using Shared.Helpers;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Consumers
{
    public class CompanyConsumer : IConsumer<CompanyContract>
    {
        private readonly ILogger<CompanyConsumer> _logger;
        private readonly IRepository<Company> _repository;

        public CompanyConsumer(ILogger<CompanyConsumer> logger, IRepository<Company> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<CompanyContract> context)
        {
            try
            {
                var message = context.Message;
                var company = new Company(message.Id, message.Clave, message.Nombre);

                var existing = await _repository.GetOne(x => x.Id == message.Id);

                if (existing == null)
                {
                    await _repository.Create(company);
                }
                else
                {
                    await _repository.Create(company);
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
