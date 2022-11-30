﻿using Newtonsoft.Json;
using Service.MedicalRecord.Client.IClient;
using Service.MedicalRecord.Dtos.Invoice;
using Service.MedicalRecord.Dtos.Request;
using Shared.Error;
using Shared.Helpers;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Text;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace Service.MedicalRecord.Client
{
    public class BillingClient : IBillingClient
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _client;

        public BillingClient(IConfiguration configuration, HttpClient client)
        {
            _client = client;
            _configuration = configuration;
        }

        public async Task<InvoiceDto> CheckInPayment(InvoiceDto invoiceDto)
        {
            try
            {
                var json = JsonConvert.SerializeObject(invoiceDto);

                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _client.PostAsync($"{_configuration.GetValue<string>("ClientRoutes:Billing")}/api/invoice", stringContent);

                if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
                {
                    return await response.Content.ReadFromJsonAsync<InvoiceDto>();
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
