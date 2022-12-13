using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Service.Sender.Dtos;
using Service.Sender.Service.IService;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Service.Sender.Service
{
    public class WhatsappService : IWhatsappService
    {
        public async Task Send(string phone, string message)
        {
            var token = "01eo1fdxodwi6s88";

            var json = JsonConvert.SerializeObject(new { Phone = phone, Body = message }, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            using HttpClient _client = new HttpClient();

            var response = await _client.PostAsync($"https://api.chat-api.com/instance431309/sendMessage?token={token}", stringContent);

            var data = await response.Content.ReadFromJsonAsync<WhatsappResponseDto>();

            if (!data.Sent)
            {
                throw new Exception(data.Message);
            }
        }

        public async Task SendFile(string phone, Uri filePath, string fileName)
        {
            var token = "01eo1fdxodwi6s88";
            
            var client = new HttpClient();

            var pdf = await client.GetByteArrayAsync(filePath);

            string pdfBase64 = Convert.ToBase64String(pdf);

            var json = JsonConvert.SerializeObject(new { Phone = phone, Body = "data:application/pdf;base64," + pdfBase64, Filename = fileName, Caption = "Diagnóstico" }, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            using HttpClient _client = new HttpClient();

            var response = await _client.PostAsync($"https://api.chat-api.com/instance431309/sendFile?token={token}", stringContent);

            var data = await response.Content.ReadFromJsonAsync<WhatsappResponseDto>();

            if (!data.Sent)
            {
                throw new Exception(data.Message);
            }
        }
    }
}
