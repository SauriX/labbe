using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Service.Billing.Client.IClient;
using Service.Billing.Dtos.Facturapi;
using Shared.Error;
using Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Service.Billing.Client
{
    public class InvoiceClient : IInvoiceClient
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _client;

        public InvoiceClient(IConfiguration configuration, HttpClient client)
        {
            _client = client;
            _configuration = configuration;
        }

        public async Task<FacturapiDto> GetInvoiceById(string facturapiId)
        {
            try
            {
                var response = await _client.GetAsync($"{_configuration.GetValue<string>("ClientRoutes:Invoice")}/api/invoice/{facturapiId}");

                if (response.IsSuccessStatusCode && (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent))
                {
                    if (response.StatusCode == HttpStatusCode.NoContent)
                    {
                        return null;
                    }

                    return await response.Content.ReadFromJsonAsync<FacturapiDto>();
                }

                var error = await response.Content.ReadFromJsonAsync<ClientExceptionFramework>();

                throw new CustomException(HttpStatusCode.BadRequest, error.ExceptionMessage);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<FacturapiDto> CreateInvoice(FacturapiDto invoice)
        {
            var json = JsonConvert.SerializeObject(invoice);

            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync($"{_configuration.GetValue<string>("ClientRoutes:Invoice")}/api/invoice", stringContent);

            if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
            {
                return await response.Content.ReadFromJsonAsync<FacturapiDto>();
            }

            var error = await response.Content.ReadFromJsonAsync<ClientExceptionFramework>();

            throw new CustomException(HttpStatusCode.BadRequest, error.ExceptionMessage);
        }

        public async Task<byte[]> GetInvoiceXML(string facturapiId)
        {
            try
            {
                var response = await _client.GetAsync($"{_configuration.GetValue<string>("ClientRoutes:Invoice")}/api/invoice/xml/{facturapiId}");

                if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
                {
                    return await response.Content.ReadAsByteArrayAsync();
                }

                var error = await response.Content.ReadFromJsonAsync<ClientExceptionFramework>();

                throw new CustomException(HttpStatusCode.BadRequest, error.ExceptionMessage);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<byte[]> GetInvoicePDF(string facturapiId)
        {
            try
            {
                var response = await _client.GetAsync($"{_configuration.GetValue<string>("ClientRoutes:Invoice")}/api/invoice/pdf/{facturapiId}");

                if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
                {
                    return await response.Content.ReadAsByteArrayAsync();
                }

                var error = await response.Content.ReadFromJsonAsync<ClientExceptionFramework>();

                throw new CustomException(HttpStatusCode.BadRequest, error.ExceptionMessage);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<byte[]> Cancel(InvoiceCancelation factura)
        {
            try
            {
                var response = await _client.GetAsync($"{_configuration.GetValue<string>("ClientRoutes:Invoice")}/api/invoice/pdf/{factura.FacturapiId}");

                if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
                {
                    return await response.Content.ReadAsByteArrayAsync();
                }

                var error = await response.Content.ReadFromJsonAsync<ClientExceptionFramework>();

                throw new CustomException(HttpStatusCode.BadRequest, error.ExceptionMessage);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
