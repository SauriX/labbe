using Integration.NetPay.Services.IServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Integration.NetPay.Services
{
    public class SaleService : ISaleService
    {
        public string token = "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJhdWQiOlsib2F1dGgyX2lkIl0sInVzZXJfbmFtZSI6ImludGVncmFjaW9uZXNAbmV0cGF5LmNvbS5teCIsInNjb3BlIjpbInJlYWQiLCJ3cml0ZSJdLCJleHAiOjE2NzkxMTQ3ODIsImF1dGhvcml0aWVzIjpbIlJPTEVfVVNFUiJdLCJqdGkiOiIyZWMxOTJjZi0yOTlhLTQ4NjMtYTEwNC03NmUxYWU4NTQ3YTUiLCJjbGllbnRfaWQiOiJ0cnVzdGVkLWFwcCJ9.JRmjNisoy91miVWUYag7PHlMZfOImGbcxSv7GueAP6MtEdiEJOj54RkGy3nlmANoVhpMr14Eu20rxgL7XO5TT8YZFx3_chdV_9a6oLtz4KXkpIDgMcqAbzrN0Si8oC6S3KDpwaoGoNm1eAz0p1ZMnM0grHHfvCrC8SjkQXAuY8rQ2Orv5EHSGbg8HbFyZ3bEu7o9w4dszUUI8k6V6A698azEz4feMEPbDCtGxRB9bHT6XARhl35jBC2ZzpAux6IHGXdGgiCFnHUIoWPPhY5PF2H9Yp6pTgVsJ2KVjNgi4OWG9hHcMmbvmm4Mq6BWeWmmPW1HO7HwDQyTA-7k3tTp0g";
        private readonly HttpClient _httpClient;
        private readonly IAuthService _authService;

        public SaleService(HttpClient httpClient, IAuthService authService)
        {
            _httpClient = httpClient;
            _authService = authService;
        }

        public async Task<string> PaymentCharge(object payment)
        {
            var url = "integration-service/transactions/sale";

            var json = JsonConvert.SerializeObject(new
            {
                traceability = payment,
                serialNumber = "1491680132",
                amount = 1,
                folioNumber = "123",
                storeId = "9194"
            });

            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);// $"Bearer {token}";

            var response = await _httpClient.PostAsync(url, stringContent);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                return data;
            }

            return await response.Content.ReadAsStringAsync();
        }
    }
}
