using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Service.MedicalRecord.Client.IClient;
using Service.MedicalRecord.Dtos.Request;
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

        public async Task<byte[]> GenerateTicket()
        {
            try
            {
                var response = await _client.GetAsync($"{_configuration.GetValue<string>("ClientRoutes:Pdf")}/api/pdf/ticket");

                if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
                {
                    return await response.Content.ReadAsByteArrayAsync();
                }

                var error = await response.Content.ReadFromJsonAsync<ServerException>();

                var ex = Exceptions.GetException(error);

                throw ex;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<byte[]> GenerateQuotation()
        {
            try
            {
                var response = await _client.GetAsync($"{_configuration.GetValue<string>("ClientRoutes:Pdf")}/api/pdf/quotation");

                if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
                {
                    return await response.Content.ReadAsByteArrayAsync();
                }

                var error = await response.Content.ReadFromJsonAsync<ServerException>();

                var ex = Exceptions.GetException(error);

                throw ex;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<byte[]> GenerateOrder(RequestOrderDto order)
        {
            try
            {
                var json = JsonConvert.SerializeObject(order);

                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _client.PostAsync($"{_configuration.GetValue<string>("ClientRoutes:Pdf")}/api/pdf/order", stringContent);

                if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
                {
                    return await response.Content.ReadAsByteArrayAsync();
                }

                var error = await response.Content.ReadFromJsonAsync<ServerException>();

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
