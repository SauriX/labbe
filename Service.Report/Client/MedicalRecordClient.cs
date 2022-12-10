using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Service.Report.Client.IClient;
using Service.Report.Domain.MedicalRecord;
using Service.Report.Dtos;
using Service.Report.Dtos.BudgetStats;
using Service.Report.Dtos.MedicalRecord;
using Shared.Error;
using Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Service.Report.Client
{
    public class MedicalRecordClient : IMedicalRecordClient
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _client;

        public MedicalRecordClient(IConfiguration configuration, HttpClient client)
        {
            _client = client;
            _configuration = configuration;
        }

        public async Task<List<MedicalRecordDto>> GetMedicalRecord(List<Guid> records)
        {
            try
            {
                var json = JsonConvert.SerializeObject(records);

                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _client.PostAsync($"{_configuration.GetValue<string>("ClientRoutes:MedicalRecord")}/api/MedicalRecord/report/expedientes", stringContent);

                if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
                {
                    return await response.Content.ReadFromJsonAsync<List<MedicalRecordDto>>();
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

        public async Task<List<Quotation>> GetQuotationByFilter(ReportFilterDto search)
        {
            try
            {
                var json = JsonConvert.SerializeObject(search);

                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _client.PostAsync($"{_configuration.GetValue<string>("ClientRoutes:MedicalRecord")}/api/reportdata/cotizaciones/filter", stringContent);

                if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
                {
                    return await response.Content.ReadFromJsonAsync<List<Quotation>>();
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

        public async Task<List<RequestInfo>> GetRequestByFilter(ReportFilterDto search)
        {
            try
            {
                var json = JsonConvert.SerializeObject(search);

                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _client.PostAsync($"{_configuration.GetValue<string>("ClientRoutes:MedicalRecord")}/api/reportdata/solicitudes/filter", stringContent);

                if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
                {
                    return await response.Content.ReadFromJsonAsync<List<RequestInfo>>();
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

        public async Task<List<RequestStudies>> GetStudiesByFilter(ReportFilterDto search)
        {
            try
            {
                var json = JsonConvert.SerializeObject(search);

                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _client.PostAsync($"{_configuration.GetValue<string>("ClientRoutes:MedicalRecord")}/api/reportdata/estudios/filter", stringContent);

                if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
                {
                    return await response.Content.ReadFromJsonAsync<List<RequestStudies>>();
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
