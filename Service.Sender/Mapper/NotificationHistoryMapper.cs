using Automatonymous.Activities;
using EventBus.Messages.Common;
using Service.Sender.Domain.NotificationHistory;
using Service.Sender.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Sender.Mapper
{
    public static class NotificationHistoryMapper
    {
        public static List<NotificationContract> toNotificationDto(this List<NotificationHistory> notifications) {
            return notifications.Select(x => new NotificationContract
            {
                Para = x.Para,
                Mensaje = x.Mensaje,
                EsAlerta = x.EsAlerta,
                Fecha=x.FechaCreacion
            }).ToList();

        }

        public static NotificationHistory toNotificationHistory(this NotificationContract notification) {
            return new NotificationHistory
            {
                Para = notification.Para,
                Mensaje = notification.Mensaje,
                EsAlerta = notification.EsAlerta,
                FechaCreacion= DateTime.Now
            };
        
        }
    }
}
