using Microsoft.Extensions.Configuration;
using Service.MedicalRecord.Client.IClient;
using Shared.Error;
using Shared.Helpers;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Client
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

        public async Task<string> GetCodeRange(Guid branchId)
        {
            try
            {
                var response = await _client.GetAsync($"{_configuration.GetValue<string>("ClientRoutes:Catalog")}/api/branch/getCodeRange/{branchId}");

                if (response.IsSuccessStatusCode && (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent))
                {
                    if (response.StatusCode == HttpStatusCode.NoContent)
                    {
                        return null;
                    }

                    return await response.Content.ReadFromJsonAsync<string>();
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
