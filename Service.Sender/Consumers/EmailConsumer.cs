using EventBus.Messages.Common;
using MassTransit;
using Microsoft.Extensions.Logging;
using Service.Sender.Service.IService;
using Shared.Helpers;
using System.Threading.Tasks;

namespace Service.Sender.Consumers
{
    public class EmailConsumer : IConsumer<EmailContract>
    {
        private readonly ILogger<EmailConsumer> _logger;
        private readonly IEmailService _emailService;

        public EmailConsumer(ILogger<EmailConsumer> logger, IEmailService emailService)
        {
            _logger = logger;
            _emailService = emailService;
        }

        public async Task Consume(ConsumeContext<EmailContract> context)
        {
            try
            {
                var message = context.Message;

                var isSingle = !string.IsNullOrWhiteSpace(message.Para);

                if (isSingle)
                {
                    await _emailService.Send(message.Para, message.Asunto, message.Titulo, message.Contenido);
                }
                else
                {
                    await _emailService.Send(message.ParaMultiple, message.Asunto, message.Titulo, message.Contenido);
                }
            }
            catch (System.Exception ex)
            {
                var message = Exceptions.GetMessage(ex);
                _logger.LogError($"MessageId: {context.MessageId}\n{message}");

                throw;
            }
        }
    }
}
