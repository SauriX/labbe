using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Application.IApplication;
using Service.Catalog.Dtos.Notifications;
using Shared.Dictionary;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationsApplication _Service;
        public NotificationsController(INotificationsApplication notifications)
        {
            _Service = notifications;
        }

        [HttpGet("{id}")]
        [Authorize(Policies.Access)]
        public async Task<NotificationFormDto> GetById(Guid id)
        {
            return await _Service.GetById(id);
        }

        [HttpGet("all/notification/{search}")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<NotificationListDto>> GetAllNotifications(string search = null)
        {
            return await _Service.GetAllNotifications(search);
        }
        [HttpGet("all/notificationJob/{search}")]
        [AllowAnonymous]
        public async Task<IEnumerable<NotificationListDto>> GetAllNotificationsPublic(string search = null)
        {
            return await _Service.GetAllNotifications(search);
        }

        [HttpGet("all/avisos/{search}")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<NotificationListDto>> GetAllAvisos(string search = null)
        {
            return await _Service.GetAllAvisos(search);
        }

        [HttpPost]
        [Authorize(Policies.Create)]
        public async Task<NotificationListDto> Create(NotificationFormDto notification)
        {
            notification.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _Service.Create(notification);
        }

        [HttpPut]
        [Authorize(Policies.Create)]
        public async Task<NotificationListDto> Update(NotificationFormDto notification)
        {
            notification.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _Service.Update(notification);
        }

        [HttpPut("status/{id}")]
        [Authorize(Policies.Create)]
        public async Task<NotificationListDto> Update(Guid id)
        {

            return await _Service.UpdateStatus(id);
        }
    }
}
