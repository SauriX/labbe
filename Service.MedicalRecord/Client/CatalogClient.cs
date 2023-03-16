using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Service.MedicalRecord.Client.IClient;
using Service.MedicalRecord.Dtos;
using Service.MedicalRecord.Dtos.Branch;
using Service.MedicalRecord.Dtos.Catalogs;
using Service.MedicalRecord.Dtos.Promotion;
using Service.MedicalRecord.Dtos.Request;
using Service.MedicalRecord.Dtos.Route;
using Service.MedicalRecord.Dtos.Series;
using Shared.Error;
using Shared.Extensions;
using Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
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

                var error = await response.Content.ReadFromJsonAsync<ClientException>();

                var ex = Exceptions.GetException(error);

                throw ex;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<BranchFormDto> GetBranch(Guid id)
        {
            var response = await _client.GetAsync($"{_configuration.GetValue<string>("ClientRoutes:Catalog")}/api/branch/{id}");

            if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
            {
                return await response.Content.ReadFromJsonAsync<BranchFormDto>();
            }

            throw new CustomException(response.StatusCode, response.ReasonPhrase);
        }
        public async Task<List<BranchCityDto>> GetBranchbycity()
        {
            var response = await _client.GetAsync($"{_configuration.GetValue<string>("ClientRoutes:Catalog")}/api/branch/getSucursalByCity");

            if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
            {
                return await response.Content.ReadFromJsonAsync<List<BranchCityDto>>();
            }

            throw new CustomException(response.StatusCode, response.ReasonPhrase);
        }

        public async Task<RouteFormDto> GetRuta(Guid id)
        {
            var response = await _client.GetAsync($"{_configuration.GetValue<string>("ClientRoutes:Catalog")}/api/route/{id}");

            if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
            {
                return await response.Content.ReadFromJsonAsync<RouteFormDto>();
            }

            throw new CustomException(response.StatusCode, response.ReasonPhrase);
        }

        public async Task<List<RouteFormDto>> GetRutas(List<Guid> id)

        {
            var json = JsonConvert.SerializeObject(id);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"{_configuration.GetValue<string>("ClientRoutes:Catalog")}/api/route/multiple", stringContent);

            if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
            {
                return await response.Content.ReadFromJsonAsync<List<RouteFormDto>>();
            }

            throw new CustomException(response.StatusCode, response.ReasonPhrase);
        }
        public async Task<List<NotificationListDto>> GetNotifications(string search)

        {
            var response = await _client.GetAsync($"{_configuration.GetValue<string>("ClientRoutes:Catalog")}/api/notifications/all/notification/{search}");

            if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
            {
                return await response.Content.ReadFromJsonAsync<List<NotificationListDto>>();
            }

            throw new CustomException(response.StatusCode, response.ReasonPhrase);
        }
        public async Task<AreaListDto> GetArea(int id)
        {
            var response = await _client.GetAsync($"{_configuration.GetValue<string>("ClientRoutes:Catalog")}/api/area/{id}");

            if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
            {
                return await response.Content.ReadFromJsonAsync<AreaListDto>();
            }

            throw new CustomException(response.StatusCode, response.ReasonPhrase);
        }

        public async Task<List<RequestStudyParamsDto>> GetStudies(List<int> studies)
        {
            var url = $"{_configuration.GetValue<string>("ClientRoutes:Catalog")}/api/study/multiple";
            var response = await _client.PostAsJson<List<RequestStudyParamsDto>>(url, studies);

            return response;
        }

        public async Task<List<PriceListInfoPromoDto>> GetStudiesPromos(List<PriceListInfoFilterDto> studies)
        {
            try
            {
                var json = JsonConvert.SerializeObject(studies);

                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _client.PostAsync($"{_configuration.GetValue<string>("ClientRoutes:Catalog")}/api/promo/info/study", stringContent);

                if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
                {
                    return await response.Content.ReadFromJsonAsync<List<PriceListInfoPromoDto>>();
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

        public async Task<List<PriceListInfoPromoDto>> GetPacksPromos(List<PriceListInfoFilterDto> packs)
        {
            try
            {
                var json = JsonConvert.SerializeObject(packs);

                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _client.PostAsync($"{_configuration.GetValue<string>("ClientRoutes:Catalog")}/api/promo/info/pack", stringContent);

                if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
                {
                    return await response.Content.ReadFromJsonAsync<List<PriceListInfoPromoDto>>();
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

        public async Task<List<RequestStudyDto>> GetStudiesInfo(PriceListInfoFilterDto studies)
        {
            try
            {
                studies.OmitirPrecio = true;
                var json = JsonConvert.SerializeObject(studies);

                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _client.PostAsync($"{_configuration.GetValue<string>("ClientRoutes:Catalog")}/api/price/multiple/info/study", stringContent);

                if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
                {
                    return await response.Content.ReadFromJsonAsync<List<RequestStudyDto>>();
                }

                var error = await response.Content.ReadFromJsonAsync<ClientException>();

                //var ex = Exceptions.GetException(error);

                if (response.StatusCode != HttpStatusCode.InternalServerError)
                {
                    throw new CustomException(response.StatusCode, error.Errors);
                }

                throw new Exception();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<SeriesDto>> GetBranchSeries(Guid branchId, byte type)

        {
            var response = await _client.GetAsync($"{_configuration.GetValue<string>("ClientRoutes:Catalog")}/api/series/branch/{branchId}/{type}");

            if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
            {
                return await response.Content.ReadFromJsonAsync<List<SeriesDto>>();
            }

            var error = await response.Content.ReadFromJsonAsync<ClientException>();

            throw new CustomException(HttpStatusCode.BadRequest, error.Errors);
        }
    }
}
