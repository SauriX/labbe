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

        public Task<FacturapiDto> GetInvoiceById(string facturapiId)
        {
            throw new NotImplementedException();
        }

        public Task<FacturapiDto> CreateInvoice(FacturapiDto invoice)
        {
            throw new NotImplementedException();
        }

        public Task<byte[]> GetInvoiceXML(string facturapiId)
        {
            throw new NotImplementedException();
        }

        //public async Task<string> GetCodeRange(Guid branchId)
        //{
        //    try
        //    {
        //        var response = await _client.GetAsync($"{_configuration.GetValue<string>("ClientRoutes:Catalog")}/api/branch/getCodeRange/{branchId}");

        //        if (response.IsSuccessStatusCode && (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent))
        //        {
        //            if (response.StatusCode == HttpStatusCode.NoContent)
        //            {
        //                return null;
        //            }

        //            return await response.Content.ReadFromJsonAsync<string>();
        //        }

        //        var error = await response.Content.ReadFromJsonAsync<ServerException>();

        //        var ex = Exceptions.GetException(error);

        //        throw ex;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public async Task<byte[]> GenerateTicket(RequestOrderDto order)
        //{
        //    try
        //    {
        //        var json = JsonConvert.SerializeObject(order);

        //        var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

        //        var response = await _client.PostAsync($"{_configuration.GetValue<string>("ClientRoutes:Pdf")}/api/pdf/ticket", stringContent);

        //        if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
        //        {
        //            return await response.Content.ReadAsByteArrayAsync();
        //        }

        //        var error = await response.Content.ReadFromJsonAsync<ServerException>();

        //        var ex = Exceptions.GetException(error);

        //        throw ex;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
    }
}
