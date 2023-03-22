using EventBus.Messages.Common;
using MassTransit;
using MassTransit.Transports;
using Quartz;
using Service.MedicalRecord.Client.IClient;
using Service.MedicalRecord.Dtos.Appointment;
using Service.MedicalRecord.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Jobs
{


    public class NotificationCitaJob : IJob
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ICatalogClient _catalogClient;
        private readonly IAppointmentResposiotry _AppoitmentRepository;
        public NotificationCitaJob(IPublishEndpoint publishEndpoint,ICatalogClient catalogClient,IAppointmentResposiotry appointmentResposiotry)
        {
            _publishEndpoint = publishEndpoint;
            _catalogClient = catalogClient;
            _AppoitmentRepository = appointmentResposiotry;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            var search = new SearchAppointment
            {
                fecha = new List<DateTime> { DateTime.Now, DateTime.Now }.ToArray(),
            };
            var citas = await _AppoitmentRepository.GetAllLab(search);
            citas = citas.Where(x=> x.FechaCita.Subtract(DateTime.Now).TotalMinutes<=15).ToList();
            var notifications = await _catalogClient.GetNotifications("Citas");
            var createnotification = notifications.FirstOrDefault(x => x.Tipo == "Cita");

            if (createnotification.Activo)
            {
                foreach (var cita in citas) {
                    var mensaje = createnotification.Contenido.Replace("[Ndispositivo]","536");
                    mensaje = mensaje.Replace("[Ncita]", cita.Cita);
                    var contract = new NotificationContract(mensaje, false);
                    await _publishEndpoint.Publish(contract);
                }


            }
            _ = Task.FromResult(true);
        }
    }
}
