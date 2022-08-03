using EventBus.Messages;
using EventBus.Messages.Common;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Service.Sender.Service.IService;
using Service.Sender.SignalR;
using Shared.Helpers;
using System.Threading.Tasks;

namespace Service.Sender.Consumers
{
    public class WhatsappConsumer : IConsumer<WhatsappContract>
    {
        private readonly ILogger<WhatsappConsumer> _logger;
        private readonly IWhatsappService _emailService;
        private readonly IHubContext<NotificationHub> _hubContext;

        public WhatsappConsumer(ILogger<WhatsappConsumer> logger, IWhatsappService emailService, IHubContext<NotificationHub> hubContext)
        {
            _logger = logger;
            _emailService = emailService;
            _hubContext = hubContext;
        }

        public async Task Consume(ConsumeContext<WhatsappContract> context)
        {
            try
            {
                var message = context.Message;

                await _emailService.Send(message.Telefono, message.Mensaje);

                if (message.Notificar)
                {
                    var notification = new NotificationContract("Whatsapp enviado correctamente", true);

                    await _hubContext.Clients.Group(message.RemitenteId).SendAsync("Notify", notification);
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
