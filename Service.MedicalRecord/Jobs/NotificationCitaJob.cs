using EventBus.Messages.Common;
using MassTransit;
using MassTransit.Transports;
using Quartz;
using Service.MedicalRecord.Client.IClient;
using Service.MedicalRecord.Repository.IRepository;
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
            var notifications = await _catalogClient.GetNotifications("Toma de muestra");
            var createnotification = notifications.FirstOrDefault(x => x.Tipo == "Citas");

            if (createnotification.Activo)
            {

                var mensaje = createnotification.Contenido.Replace("Nsolicitud", request.Clave);
                var contract = new NotificationContract(mensaje, false);
                await _publishEndpoint.Publish(contract);

            }
            _ = Task.FromResult(true);
        }
    }
}
