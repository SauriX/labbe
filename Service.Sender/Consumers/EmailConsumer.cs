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
    public class EmailConsumer : IConsumer<EmailContract>
    {
        private readonly IEmailService _emailService;
        private readonly IHubContext<NotificationHub> _hubContext;

        public EmailConsumer(IEmailService emailService, IHubContext<NotificationHub> hubContext)
        {
            _emailService = emailService;
            _hubContext = hubContext;
        }

        public async Task Consume(ConsumeContext<EmailContract> context)
        {
            try
            {
                var message = context.Message;

                var isSingle = !string.IsNullOrWhiteSpace(message.Para);

                if (isSingle)
                {
                    await _emailService.Send(message.Para, message.Asunto, message.Titulo, message.Contenido, message.SenderFiles);
                }
                else
                {
                    if (message.CorreoIndividual)
                    {
                        foreach (var item in message.ParaMultiple)
                        {
                            await _emailService.Send(item, message.Asunto, message.Titulo, message.Contenido, message.SenderFiles);
                        }
                    }
                    else
                    {
                        await _emailService.Send(message.ParaMultiple, message.Asunto, message.Titulo, message.Contenido);
                    }
                }

                if (message.Notificar)
                {
                    var notification = new NotificationContract("Correo enviado correctamente", true,DateTime.Now);

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
