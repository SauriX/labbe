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
using Service.Report.Dtos;
using Service.Report.Dtos.Indicators;
using Service.Report.Domain.Catalogs;

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

        public async Task<List<ServicesCost>> GetBudgetsByBranch(ReportModalFilterDto search)
        {
            if(search == null) return new List<ServicesCost>();

            try
            {
                var json = JsonConvert.SerializeObject(search);

                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _client.PostAsync($"{_configuration.GetValue<string>("ClientRoutes:Catalog")}/api/catalog/costofijo/getBranches", stringContent);

                if(response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.NoContent) 
                {
                    return new List<ServicesCost>();
                }

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
        
        public async Task<List<ServiceUpdateDto>> GetServiceCostByBranch(ReportModalFilterDto search)
        {
            if(search == null) return new List<ServiceUpdateDto>();

            try
            {
                var json = JsonConvert.SerializeObject(search);

                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _client.PostAsync($"{_configuration.GetValue<string>("ClientRoutes:Catalog")}/api/catalog/costofijo/getServiceCost", stringContent);

                if(response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.NoContent) 
                {
                    return new List<ServiceUpdateDto>();
                }

                if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
                {
                    return await response.Content.ReadFromJsonAsync<List<ServiceUpdateDto>>();
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

        public async Task CreateList(List<BudgetFormDto> bugets)
        {
            try
            {
                var json = JsonConvert.SerializeObject(bugets);

                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _client.PostAsync($"{_configuration.GetValue<string>("ClientRoutes:Catalog")}/api/catalog/costofijo/list", stringContent);

                if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
                {
                    return;
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
        
        public async Task UpdateService(UpdateServiceDto bugets)
        {
            try
            {
                var json = JsonConvert.SerializeObject(bugets);

                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _client.PutAsync($"{_configuration.GetValue<string>("ClientRoutes:Catalog")}/api/catalog/costofijo/update", stringContent);

                if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
                {
                    return;
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
