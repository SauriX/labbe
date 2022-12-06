using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Service.Catalog.Application;
using Service.Catalog.Dtos.Equipmentmantain;
using Shared.Error;
using Shared.Helpers;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Client
{
    public class PdfClient : IPdfClient
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _client;

        public PdfClient(IConfiguration configuration, HttpClient client)
        {
            _client = client;
            _configuration = configuration;
        }



        public async Task<byte[]> GenerateOrder(MantainFormDto order)
        {
            try
            {
                var json = JsonConvert.SerializeObject(order);

                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _client.PostAsync($"{_configuration.GetValue<string>("ClientRoutes:Pdf")}/api/pdf/mantain", stringContent);

                if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
                {
                    return await response.Content.ReadAsByteArrayAsync();
                }

                var error = await response.Content.ReadFromJsonAsync<ClientException>();

                var ex = Exceptions.GetException(error);

                throw ex;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
