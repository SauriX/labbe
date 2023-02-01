using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Service.Catalog.Client.IClient;
using Service.Catalog.Dtos.Configuration;
using Service.Catalog.Dtos.Scopes;
using Shared.Error;
using Shared.Helpers;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
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

            if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.NoContent)
            {
                return new ScopesDto()
                {
                    Modificar = true
                };
            }

            throw new CustomException(response.StatusCode, response.ReasonPhrase);
        }

        public async Task<UserInfo> GetUserById(string id)
        {
            try
            {
                var json = JsonConvert.SerializeObject(id);

                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _client.PostAsync($"{_configuration.GetValue<string>("ClientRoutes:Identity")}/api/scopes/{id}", stringContent);

                if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
                {
                    return await response.Content.ReadFromJsonAsync<UserInfo>();
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
