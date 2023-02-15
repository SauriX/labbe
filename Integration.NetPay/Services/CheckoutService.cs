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
        public string token = "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJhdWQiOlsib2F1dGgyX2lkIl0sInVzZXJfbmFtZSI6ImludGVncmFjaW9uZXNAbmV0cGF5LmNvbS5teCIsInNjb3BlIjpbInJlYWQiLCJ3cml0ZSJdLCJleHAiOjE2NzY1MjQxNzEsImF1dGhvcml0aWVzIjpbIlJPTEVfVVNFUiJdLCJqdGkiOiI1NDUwMTdlYy1jNmJlLTQ0ZWUtYTg1YS03NGEzMzM1MWFlN2UiLCJjbGllbnRfaWQiOiJ0cnVzdGVkLWFwcCJ9.B8qXorlpryLoISalD-TmKOg8xJdzY6ZvOfYGyKIB0bNgP9jDGaQ7JWdyyPc1xOuc4idcTujhCvGRQrzbagbjcKT1DEPAb8E5NNjUYJvXCP8g0ZCzKuAo6K6nufKMOyuExhvvE-ujsmsZfMiy70vDbagDiTFmaSczZLnf1T-BagBGOx1FlOQLQ7syLQEYC7n0Zg8qn_z__Y-tusTbHFHVaOi41KivMpmXCPr1E4bKVJEGv0Kg6lWj_KjPTDNBwmAxrqs1WRZlFS1gWitDvHoUHojgUQoFdHM-_fQURVLHXiHD3zEBp7fU5nrcSUYF2sh1rNWXoCRtbtIyPKvUjh6ZYA";
        private readonly HttpClient _httpClient;

        public SaleService(HttpClient httpClient)
        {
            _httpClient = httpClient;
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
