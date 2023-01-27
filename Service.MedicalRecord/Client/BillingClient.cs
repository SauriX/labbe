using Newtonsoft.Json;
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
using Service.MedicalRecord.Dtos.InvoiceCompany;
using Service.MedicalRecord.Dtos.Series;
using Service.MedicalRecord.Dtos.InvoiceCatalog;

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


        public async Task<List<InvoiceDto>> getAllInvoice(InvoiceCatalogSearch search)
        {
            var json = JsonConvert.SerializeObject(search);

            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"{_configuration.GetValue<string>("ClientRoutes:Billing")}/api/invoice/all",stringContent);

            if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
            {
                return await response.Content.ReadFromJsonAsync<List<InvoiceDto>>();
            }

            var error = await response.Content.ReadFromJsonAsync<ClientException>();

            throw new CustomException(HttpStatusCode.BadRequest, error.Errors);
        }
        public async Task<List<SeriesDto>> GetBranchSeries(Guid branchId, byte type)
                    
        {
            var response = await _client.GetAsync($"{_configuration.GetValue<string>("ClientRoutes:Billing")}/api/series/branch/{branchId}/{type}");

            if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
            {
                return await response.Content.ReadFromJsonAsync<List<SeriesDto>>();

            }

            var error = await response.Content.ReadFromJsonAsync<ClientException>();

            throw new CustomException(HttpStatusCode.BadRequest, error.Errors);
        }

        public async Task<InvoiceDto> CheckInPayment(InvoiceDto invoiceDto)
        {
            var json = JsonConvert.SerializeObject(invoiceDto);

            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync($"{_configuration.GetValue<string>("ClientRoutes:Billing")}/api/invoice", stringContent);

            if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
            {
                return await response.Content.ReadFromJsonAsync<InvoiceDto>();
            }

            var error = await response.Content.ReadFromJsonAsync<ClientException>();

            throw new CustomException(HttpStatusCode.BadRequest, error.Errors);
        }

        public async Task<InvoiceDto> CheckInPaymentCompany(InvoiceDto invoiceDto)
        {
            var json = JsonConvert.SerializeObject(invoiceDto);

            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync($"{_configuration.GetValue<string>("ClientRoutes:Billing")}/api/invoice/create/invoiceCompany", stringContent);

            if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
            {
                return await response.Content.ReadFromJsonAsync<InvoiceDto>();
            }

            var error = await response.Content.ReadFromJsonAsync<ClientException>();

            throw new CustomException(HttpStatusCode.BadRequest, error.Errors);
        }

        public async Task<byte[]> DownloadPDF(string invoiceId)
        {
            var json = JsonConvert.SerializeObject(invoiceId);

            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync($"{_configuration.GetValue<string>("ClientRoutes:Billing")}/api/invoice/print/pdf/{invoiceId}", stringContent);

            if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
            {
                return await response.Content.ReadAsByteArrayAsync();
            }

            var error = await response.Content.ReadFromJsonAsync<ClientException>();

            throw new CustomException(HttpStatusCode.BadRequest, error.Errors);
        }

        public async Task<string> CancelInvoice(InvoiceCancelation invoiceDto)
        {
            var json = JsonConvert.SerializeObject(invoiceDto);

            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync($"{_configuration.GetValue<string>("ClientRoutes:Billing")}/api/invoice/cancel", stringContent);

            if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
            {
                return await response.Content.ReadFromJsonAsync<string>();
            }

            var error = await response.Content.ReadFromJsonAsync<ClientException>();

            throw new CustomException(HttpStatusCode.BadRequest, error.Errors);
        }
    }
}
