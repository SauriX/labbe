using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Shared.Error;
using Shared.Helpers;
using System.Collections.Generic;
using System.Net;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Service.Report.Domain.MedicalRecord;
using Service.Report.Client.IClient;

namespace Service.Report.Client
{
    public class CatalogClient : ICatalogClient
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _client;

        public CatalogClient(IConfiguration configuration, HttpClient client)
        {
            _client = client;
            _configuration = configuration;
        }

        public async Task<List<ServicesCost>> GetBudgetsByBranch(List<Guid> branchIds)
        {
            try
            {
                var json = JsonConvert.SerializeObject(branchIds);

                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _client.PostAsync($"{_configuration.GetValue<string>("ClientRoutes:Catalog")}/api/costofijo/getBranches", stringContent);

                if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
                {
                    return await response.Content.ReadFromJsonAsync<List<ServicesCost>>();
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
