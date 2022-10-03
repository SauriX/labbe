using Microsoft.Extensions.Configuration;
using Service.MedicalRecord.Client.IClient;
using Service.MedicalRecord.Dtos;
using Service.MedicalRecord.Dtos.Scopes;
using Shared.Error;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Client
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

        public async Task<UsersDto> GetByid(string id)
        {
            var response = await _client.GetAsync($"{_configuration.GetValue<string>("ClientRoutes:Identity")}/api/user/{id}");

            if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
            {
                return await response.Content.ReadFromJsonAsync<UsersDto>();
            }

            throw new CustomException(response.StatusCode, response.ReasonPhrase);
        }
    }
}
