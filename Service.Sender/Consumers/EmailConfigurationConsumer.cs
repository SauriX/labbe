using EventBus.Messages.Catalog;
using MassTransit;
using Microsoft.Extensions.Logging;
using Service.Sender.Dtos;
using Service.Sender.Service.IService;
using Shared.Helpers;
using System.Threading.Tasks;

namespace Service.Sender.Consumers
{
    public class EmailConfigurationConsumer : IConsumer<EmailConfigurationContract>
    {
        private readonly ILogger<EmailConsumer> _logger;
        private readonly IEmailConfigurationService _emailService;

        public EmailConfigurationConsumer(ILogger<EmailConsumer> logger, IEmailConfigurationService emailService)
        {
            _logger = logger;
            _emailService = emailService;
        }

        public async Task Consume(ConsumeContext<EmailConfigurationContract> context)
        {
            try
            {
                var message = context.Message;

                var conf = new EmailConfigurationDto(message.Remitente, message.Correo, message.Smtp, message.RequiereContraseña, message.Contraseña);

                await _emailService.UpdateEmail(conf);
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
