using DocumentFormat.OpenXml.Office2010.Excel;
using EventBus.Messages.Common;
using MassTransit;
using Service.Catalog.Application.IApplication;
using Service.Catalog.Client.IClient;
using Service.Catalog.Domain.Notifications;
using Service.Catalog.Dtos.Notifications;
using Service.Catalog.Mapper;
using Service.Catalog.Repository;
using Service.Catalog.Repository.IRepository;
using Shared.Dictionary;
using Shared.Error;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Service.Catalog.Application
{
    public class NotificationsApplication : INotificationsApplication
    {
        private readonly INotificationsRepository _repository;
        private readonly IPublishEndpoint _publishEndpoint;
        public NotificationsApplication(INotificationsRepository repository,IPublishEndpoint publishEndpoint)
        {
            _repository = repository;
            _publishEndpoint = publishEndpoint;
           
        }
        public async Task<IEnumerable<NotificationListDto>> GetAllNotifications(string search)
        {
            var notifications = await _repository.GetAll(search, true);

            return notifications.toNotificationListDto();
        }
        public async Task<IEnumerable<NotificationListDto>> GetAllAvisos(string search)
        {
            var notifications = await _repository.GetAll(search, false);

            return notifications.toNotificationListDto();
        }
        public async Task<NotificationFormDto> GetById(Guid Id)
        {
            var notification = await _repository.GetById(Id);
            return notification.toNotificationFormDto();
        }
        public async Task<NotificationListDto> Create(NotificationFormDto notification)
        {
            notification.Id = Guid.NewGuid();
            var newNotifcation = notification.toModel();
            await CheckDuplicate(newNotifcation);
            await _repository.Create(newNotifcation);
            return newNotifcation.toNotificationListDto();


        }
        public async Task<NotificationListDto> Update(NotificationFormDto notification)
        {
            var existing = await _repository.GetById(notification.Id.Value);
        
            var updateNotification = notification.toModel(existing);
            await CheckDuplicate(updateNotification);
            await _repository.Update(updateNotification);
            return updateNotification.toNotificationListDto();
        }

        public async Task<NotificationListDto> UpdateStatus(Guid id)
        {
            var existing = await _repository.GetById(id);


            existing.Activo = !existing.Activo;
            await _repository.Update(existing);
            
           
            if (existing.Activo && !existing.IsNotifi)
            {

                var contract = new NotificationContract(existing.Contenido, false, DateTime.Now);
                await _publishEndpoint.Publish(contract);

            }
            return existing.toNotificationListDto();
        }
        private async Task CheckDuplicate(Notifications notification)
        {
            var isDuplicate = await _repository.IsDuplicate(notification);

            if (isDuplicate)
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.Duplicated("El titulo"));
            }
        }
    }
}
