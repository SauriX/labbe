using EventBus.Messages;
using EventBus.Messages.Common;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Service.Sender.SignalR;
using Shared.Dictionary;
using Shared.Utils;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Service.Sender.Consumers.Error
{
    public class WhatsappErrorConsumer : IConsumer<Fault<WhatsappContract>>
    {
        private readonly ILogger<WhatsappErrorConsumer> _logger;
        private readonly IHubContext<NotificationHub> _hubContext;

        public WhatsappErrorConsumer(ILogger<WhatsappErrorConsumer> logger, IHubContext<NotificationHub> hubContext)
        {
            _logger = logger;
            _hubContext = hubContext;
        }

        public async Task Consume(ConsumeContext<Fault<WhatsappContract>> context)
        {
            var message = context.Message.Message;
            var error = new RabbitFaultLog<WhatsappContract>().GetLog(context);

            if (message.Notificar)
            {
                var notification = new NotificationContract("Hubo un error al enviar el correo, intenta más tarde", true);

                await _hubContext.Clients.Group(message.RemitenteId).SendAsync("Notify", notification);
            }

            _logger.LogError(error);
        }
    }
}
