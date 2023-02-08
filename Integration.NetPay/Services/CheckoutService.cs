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
        public string token = "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJhdWQiOlsib2F1dGgyX2lkIl0sInVzZXJfbmFtZSI6ImludGVncmFjaW9uZXNAbmV0cGF5LmNvbS5teCIsInNjb3BlIjpbInJlYWQiLCJ3cml0ZSJdLCJleHAiOjE2NzU5MTgxMTQsImF1dGhvcml0aWVzIjpbIlJPTEVfVVNFUiJdLCJqdGkiOiI0MWZhNWRlNS04NzllLTQxYzUtOGU0YS1jNmQyNmIzYWFiMWEiLCJjbGllbnRfaWQiOiJ0cnVzdGVkLWFwcCJ9.ZOKhXdLy2qJroW2n6ZDeUBUf4XFr-9B5Fat8qY8mAOTNKkDLMk2n6Q3TfkfEi5q0Z8wuj9fOolC8rZQVX_lzkgDPK4HOYLHV8RmoeroT-WLllDG-Qblf1XoWRrhTZ64brStCwVHTapYQzerCxz9EO_Z-NQrLTxsi5H7fpkE8gI-FQvu1SiYx3tS7tKkpPEDMJ0-I9sOsSAkuQFcH2mh9aDVIokLBiUQ9YE_hpc6hp_RIP1y-cKsrVDkSaUY7wAj33N77x1HAdieOii1b8Vxz_0TnmR_QS72jyFsNhsortGr_obHiiqMMOQQcwKHEYQeUgk4s_-LzCnLQpQcA2j-gbA";
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
