﻿using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
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
    public class MedicalRecordClient
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

        public async Task<List<BudgetStatsDto>> GetQuotationByFilter(ReportFilterDto search)
        {
            try
            {
                var json = JsonConvert.SerializeObject(search);

                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _client.PostAsync($"{_configuration.GetValue<string>("ClientRoutes:MedicalRecord")}/api/report/presupuestos/filter", stringContent);

                if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
                {
                    return await response.Content.ReadFromJsonAsync<List<BudgetStatsDto>>();
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

        public async Task<BudgetDto> GetQuotationTableByFilter(ReportFilterDto search)
        {
            try
            {
                var json = JsonConvert.SerializeObject(search);

                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _client.PostAsync($"{_configuration.GetValue<string>("ClientRoutes:MedicalRecord")}/api/report/presupuestos/table/filter", stringContent);

                if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
                {
                    return await response.Content.ReadFromJsonAsync<BudgetDto>();
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

        public async Task<List<BudgetStatsChartDto>> GetQuotationChartByFilter(ReportFilterDto search)
        {
            try
            {
                var json = JsonConvert.SerializeObject(search);

                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _client.PostAsync($"{_configuration.GetValue<string>("ClientRoutes:MedicalRecord")}/api/report/presupuestos/chart/filter", stringContent);

                if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
                {
                    return await response.Content.ReadFromJsonAsync<List<BudgetStatsChartDto>>();
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
