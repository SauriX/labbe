using Microsoft.Extensions.Configuration;
using Service.Billing.Client.IClient;
using Service.Billing.Dtos.Payment;
using Shared.Extensions;
using System.Net.Http;
using System.Threading.Tasks;

namespace Service.Billing.Client
{
    public class MedicalRecordClient : IMedicalRecordClient
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _client;

        public MedicalRecordClient(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
        }

        public class Dummy
        {
            public string Name { get; set; }
        }

        public async Task<PayPalPaymentDto> CreatePayment(PayPalPaymentDto payment)
        {
            var url = $"{_configuration.GetValue<string>("ClientRoutes:MedicalRecord")}/api/request/payment";
            var response = await _client.PostAsJson<PayPalPaymentDto>(url, payment);

            return response;
        }
    }
}
