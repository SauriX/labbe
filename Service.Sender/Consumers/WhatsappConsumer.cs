using EventBus.Messages.Common;
using MassTransit;
using Microsoft.Extensions.Logging;
using Service.Sender.Service.IService;
using Shared.Helpers;
using System.Threading.Tasks;

namespace Service.Sender.Consumers
{
    public class WhatsappConsumer : IConsumer<WhatsappContract>
    {
        private readonly ILogger<WhatsappConsumer> _logger;
        private readonly IWhatsappService _emailService;

        public WhatsappConsumer(ILogger<WhatsappConsumer> logger, IWhatsappService emailService)
        {
            _logger = logger;
            _emailService = emailService;
        }

        public async Task Consume(ConsumeContext<WhatsappContract> context)
        {
            try
            {
                var message = context.Message;

                await _emailService.Send(message.Telefono, message.Mensaje);
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
