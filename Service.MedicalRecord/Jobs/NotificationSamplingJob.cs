using EventBus.Messages.Common;
using MassTransit;
using Quartz;
using Service.MedicalRecord.Client.IClient;
using Service.MedicalRecord.Dtos.General;
using Service.MedicalRecord.Dtos.RequestedStudy;
using Service.MedicalRecord.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Jobs
{
    public class NotificationSamplingJob : IJob
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ICatalogClient _catalogClient;
        private readonly ISamplingRepository _samplingRepository;
        public NotificationSamplingJob(IPublishEndpoint publishEndpoint, ICatalogClient catalogClient, ISamplingRepository SamplingRepository)
        {
            _publishEndpoint = publishEndpoint;
            _catalogClient = catalogClient;
            _samplingRepository = SamplingRepository;
        }
        public async  Task Execute(IJobExecutionContext context)
        {
            var search = new GeneralFilterDto
            {
                Fecha = new List<DateTime> { DateTime.Now, DateTime.Now },
            };
            var solicitudes = await _samplingRepository.GetAll(search);
            List<string> requests = new List<string>();
            foreach (var solicitud in solicitudes) {
                if (solicitud.Estudios.Any(x => x.EstatusId == 1)) {
                    requests.Add(solicitud.Clave);
                
                }
                
            
            }

            var notifications = await _catalogClient.GetNotifications("Captura de resultados");
            var createnotification = notifications.FirstOrDefault(x => x.Tipo == "Pending");
            var mensaje = createnotification.Contenido.Replace("Lsolicitud", string.Join(",", requests));
            var contract = new NotificationContract(mensaje, false, DateTime.Now);
            await _publishEndpoint.Publish(contract);
            _ = Task.FromResult(true);
        }
    }
}
