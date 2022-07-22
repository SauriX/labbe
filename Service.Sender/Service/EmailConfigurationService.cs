using Service.Sender.Dtos;
using Service.Sender.Mapper;
using Service.Sender.Repository.IRepository;
using Service.Sender.Service.IService;
using System.Threading.Tasks;

namespace Service.Sender.Service
{
    public class EmailConfigurationService : IEmailConfigurationService
    {
        private readonly IEmailConfigurationRepository _repository;

        public EmailConfigurationService(IEmailConfigurationRepository repository)
        {
            _repository = repository;
        }

        public async Task<EmailConfigurationDto> GetEmail()
        {
            var configuration = await _repository.GetEmail();

            return configuration.ToEmailconfigurationDto();
        }

        public async Task UpdateEmail(EmailConfigurationDto email)
        {
            var conf = email.ToModel();

            await _repository.UpdateEmail(conf);
        }
    }
}
