using EventBus.Messages.Common;
using MassTransit;
using Quartz;
using Service.Catalog.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Service.Catalog.Jobs
{
    public class NotificationJob : IJob
    {
        private readonly IPublishEndpoint _endpoint;
        private readonly INotificationsRepository _repository;
        public NotificationJob(IPublishEndpoint publishEndpoint, INotificationsRepository repository)
        {
            _endpoint = publishEndpoint;
            _repository = repository;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            var notifications = await _repository.GetAllComplete("all", false,DateTime.Now);
            var filternotifications = notifications.Any(x => x.Activo);
            if (filternotifications)
            {

                foreach (var notification in notifications)
                {
                    /*Sunday = 0
                    Monday = 1
                    Tuesday = 2
                    Wednesday = 3
                    Thursday = 4
                    Friday = 5
                    Saturday = 6*/
                    List<int> dias = new List<int>();
                    if (notification.Lunes)
                    {
                        dias.Add(1);
                    }
                    if (notification.Martes)
                    {
                        dias.Add(2);
                    }
                    if (notification.Miercoles)
                    {
                        dias.Add(3);
                    }
                    if (notification.Jueves)
                    {
                        dias.Add(4);
                    }
                    if (notification.Viernes)
                    {
                        dias.Add(5);
                    }
                    if (notification.Sabado)
                    {
                        dias.Add(6);
                    }
                    if (notification.Domingo)
                    {
                        dias.Add(0);
                    }
                    var dia = (int)DateTime.Now.DayOfWeek;
                    if (dias.Any(x => x == dia)) {
                        if (notification.Sucursales.Any()) {
                            foreach (var sucursal in notification.Sucursales) {
                                var contract = new NotificationContract(notification.Contenido, false,sucursal.Branch.Id.ToString());
                                await _endpoint.Publish(contract);
                            }
                        }
                        if (notification.Roles.Any()) {

                            foreach (var rol in notification.Roles)
                            {
                                var contract = new NotificationContract(notification.Contenido, false, rol.RolId.ToString());
                                await _endpoint.Publish(contract);
                            }
                        }
                    }

                }

            }

            _ = Task.FromResult(true);

        }
    }
}
