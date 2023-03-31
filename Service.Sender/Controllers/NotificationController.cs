
using EventBus.Messages.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Service.Sender.Application.IApplication;
using Service.Sender.Dtos;
using Service.Sender.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Sender.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly INotificationHistoryApplication _application;

        public NotificationController(IHubContext<NotificationHub> hubContext,INotificationHistoryApplication notificationHistory)
        {
            _hubContext = hubContext;
            _application = notificationHistory;

        }

        [HttpPost("notify")]
        [AllowAnonymous]
        public async Task<bool> Notify(NotificationDto notification)
        {
            await _hubContext.Clients.Group(notification.Usuario).SendAsync(notification.Metodo, notification.Datos);

            return true;
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<List<NotificationContract>> getByFilter(NotificationFilterDto filter)
        {
            return await _application.getByFilter(filter);
        }
    }
}
