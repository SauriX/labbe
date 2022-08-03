using EventBus.Messages;
using EventBus.Messages.Common;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Service.Sender.SignalR;
using Shared.Dictionary;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Service.Sender.Consumers.Error
{
    public class EmailErrorConsumer : IConsumer<Fault<EmailContract>>
    {
        private readonly ILogger<EmailErrorConsumer> _logger;
        private readonly IHubContext<NotificationHub> _hubContext;

        public EmailErrorConsumer(ILogger<EmailErrorConsumer> logger, IHubContext<NotificationHub> hubContext)
        {
            _logger = logger;
            _hubContext = hubContext;
        }

        public async Task Consume(ConsumeContext<Fault<EmailContract>> context)
        {
            var contract = typeof(EmailContract);
            var messageId = context.Message.FaultedMessageId;
            var messageEx = "";
            if (context.Message?.Exceptions != null && context.Message.Exceptions.Any())
            {
                messageEx = string.Join("\n", context.Message.Exceptions.Select(x => $"Exception: {x.Message}\nStackTrace: {x.StackTrace}"));
            }

            var messageData = "";
            if (context.Message.Message != null)
            {
                messageData = JsonSerializer.Serialize(context.Message.Message);
            }

            var retry = context.GetRetryCount();

            var exMessage = Responses.RabbitMQError("email-queue", retry, contract.FullName, messageId.ToString(), messageData, messageEx);

            var message = context.Message.Message;

            if (message.Notificar)
            {
                var notification = new NotificationContract("Hubo un error al enviar el correo, intenta más tarde", true);

                await _hubContext.Clients.Group(message.RemitenteId).SendAsync("Notify", notification.Serialize());
            }

            _logger.LogError(exMessage);
        }
    }
}
