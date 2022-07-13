using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Service.Report.Client.IClient;
using Service.Report.PdfModel;
using Shared.Error;
using Shared.Helpers;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Service.Report.Client
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

        public async Task<byte[]> GenerateReport(ReportData reportData)
        {
            try
            {
                var json = JsonConvert.SerializeObject(reportData);

                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _client.PostAsync($"{_configuration.GetValue<string>("ClientRoutes:Pdf")}/api/pdf/report/generate", stringContent);

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
