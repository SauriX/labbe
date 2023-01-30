using Integration.NetPay.Services.IServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Integration.NetPay.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> Login()
        {
            var url = "oauth-service/oauth/token";

            var loginData = new Dictionary<string, string>()
            {
                ["grant_type"] = "password",
                ["username"] = "Nacional",
                ["password"] = "netpay"
            };

            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var response = await _httpClient.PostAsync(url, new FormUrlEncodedContent(loginData));

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                return data;
            }

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> Refresh()
        {
            var url = "oauth-service/oauth/token";

            var loginData = new Dictionary<string, string>()
            {
                ["grant_type"] = "refresh_token",
                ["refresh_token"] = "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJhdWQiOlsib2F1dGgyX2lkIl0sInVzZXJfbmFtZSI6ImludGVncmFjaW9uZXNAbmV0cGF5LmNvbS5teCIsInNjb3BlIjpbInJlYWQiLCJ3cml0ZSJdLCJhdGkiOiJiNWEzY2M1NS0yZmM2LTRhOWItOGU3Yy04ODgwY2I4YmEyYmYiLCJleHAiOjE2NzUxODQ3MjQsImF1dGhvcml0aWVzIjpbIlJPTEVfVVNFUiJdLCJqdGkiOiIwNDRhNDIzMS05ZTRiLTRlYTktYWQ5Yy1mZDJmZDc2MzFmZTMiLCJjbGllbnRfaWQiOiJ0cnVzdGVkLWFwcCJ9.XKwWS0cs7Hzeto7Pl_1xjHQSBizjLddz1KRk3IbrrS3_M-L2Y2z68D6UdFdT3FRup6nE1xzBzZOv4pFPK3VmYgFjIIqKrSCm76M4Q4QxWNaEeX9UKFXWM-CSoHdw0u2PIlTkrjM8yKwomHis1TUxIrrR0vIeXeYzfj5oSifdIZ0215hqokDc27cxiYZnZJcV6njL5VpcsZqB39P5qGP8lXCg8u6x6Es2Fa6YHhRAra08_WcxJUDKqFyfPUCsAq-C26JnhSLUiCpNgd5iFZdva9ItTmd9ZwM0ecRVCMtGIoAlsfvAyrv-bCtDoBPslzgrkyNMw2hmE4-YF5GjrlYFyA",
            };

            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var response = await _httpClient.PostAsync(url, new FormUrlEncodedContent(loginData));

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                return data;
            }

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> Check()
        {
            var url = "integration-service/transactions/sale";


            var json = JsonConvert.SerializeObject(new
            {
                traceability = new
                {
                    idProducto = "123456",
                    idTienda = "0987"
                },
                serialNumber = "1491680132",
                amount = 1,
                folioNumber = "123",
                storeId = "9194"
            });

            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

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
