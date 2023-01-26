using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Service.Billing.Client.IClient;
using Service.Billing.Domain.Catalogs;
using Service.Billing.Dto.Series;
using Shared.Error;
using Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Service.Billing.Client
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

        public async Task<BranchInfo> GetBranchByName(string name)
        {
            try
            {
                var json = JsonConvert.SerializeObject(name);

                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _client.PostAsync($"{_configuration.GetValue<string>("ClientRoutes:Catalog")}/api/branch/branchName/{name}", stringContent);

                if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
                {
                    return await response.Content.ReadFromJsonAsync<BranchInfo>();
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

        public async Task<OwnerInfoDto> GetFiscalConfig()
        {
            try
            {
                var response = await _client.GetAsync($"{_configuration.GetValue<string>("ClientRoutes:Catalog")}/api/configuration/fiscal");

                if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
                {
                    return await response.Content.ReadFromJsonAsync<OwnerInfoDto>();
                }

                throw new CustomException(response.StatusCode, response.ReasonPhrase);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
