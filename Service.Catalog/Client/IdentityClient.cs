using Microsoft.Extensions.Configuration;
using Service.Catalog.Client.IClient;
using Service.Catalog.Dtos.Scopes;
using Shared.Error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Service.Catalog.Client
{
    public class IdentityClient : IIdentityClient
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _client;

        public IdentityClient(IConfiguration configuration, HttpClient client)
        {
            _client = client;
            _configuration = configuration;
        }

        public async Task<ScopesDto> GetScopes(string module)
        {
            var response = await _client.GetAsync($"{_configuration.GetValue<string>("ClientRoutes:Identity")}/api/scopes/{module}");

            if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
            {
                return await response.Content.ReadFromJsonAsync<ScopesDto>();
            }

            throw new CustomException(response.StatusCode, response.ReasonPhrase);
        }
    }
}
