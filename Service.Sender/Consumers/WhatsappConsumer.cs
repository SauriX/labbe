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
using System;
using System.Threading.Tasks;

namespace Service.Sender.Consumers
{
    public class WhatsappConsumer : IConsumer<WhatsappContract>
    {
        private readonly IWhatsappService _emailService;
        private readonly IHubContext<NotificationHub> _hubContext;

        public WhatsappConsumer(IWhatsappService emailService, IHubContext<NotificationHub> hubContext)
        {
            _emailService = emailService;
            _hubContext = hubContext;
        }

        public async Task Consume(ConsumeContext<WhatsappContract> context)
        {
            try
            {
                var message = context.Message;

                if(message.SenderFiles != null && message.SenderFiles.Count > 0)
                {
                    foreach (var file in message.SenderFiles)
                    {
                        await _emailService.SendFile(message.Telefono, file.Ruta, file.Nombre);

                    }
                }
                else
                {
                    await _emailService.Send(message.Telefono, message.Mensaje);

                }


                if (message.Notificar)
                {
                    var notification = new NotificationContract("Whatsapp enviado correctamente", true);

                    await _hubContext.Clients.Group(message.RemitenteId).SendAsync("Notify", notification);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
