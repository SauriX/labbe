using Microsoft.Extensions.Configuration;
using Service.Billing.Client.IClient;
using Service.Billing.Dtos.Notification;
using Shared.Error;
using Shared.Extensions;
using System.Net.Http;
using System.Threading.Tasks;

namespace Service.Billing.Client
{
    public class SenderClient : ISenderClient
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _client;

        public SenderClient(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
        }

        public class Dummy
        {
            public string Name { get; set; }
        }

        public async Task Notify(NotificationDto notification)
        {
            var url = $"{_configuration.GetValue<string>("ClientRoutes:Sender")}/api/notification/notify";

            await _client.PostAsJson<bool>(url, notification);
        }
    }
}
