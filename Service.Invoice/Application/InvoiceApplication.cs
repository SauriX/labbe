﻿using Service.Billing.Application.IApplication;
using Service.Billing.Client.IClient;
using Service.Billing.Domain.Invoice;
using Service.Billing.Dtos.Facturapi;
using Service.Billing.Dtos.Invoice;
using Service.Billing.Mapper;
using Service.Billing.Repository.IRepository;
using Service.Billing.Transactions;
using Shared.Dictionary;
using Shared.Error;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Transactions;

namespace Service.Billing.Application
{
    public class InvoiceApplication : IInvoiceApplication
    {
        private readonly ITransactionProvider _transacionProvider;
        private readonly IInvoiceRepository _repository;
        private readonly IInvoiceClient _invoiceClient;

        public InvoiceApplication(ITransactionProvider transacionProvider, IInvoiceRepository repository, IInvoiceClient invoiceClient)
        {
            _transacionProvider = transacionProvider;
            _repository = repository;
            _invoiceClient = invoiceClient;
        }

        public async Task<InvoiceDto> GetById(Guid invoiceId)
        {
            var invoice = await GetExistingInvoice(invoiceId);

            return invoice.ToInvoiceDto();
        }

        public async Task<List<InvoiceDto>> GetByRecord(Guid recordId)
        {
            var invoices = await _repository.GetByRecord(recordId);

            return invoices.ToInvoiceDto();
        }

        public async Task<List<InvoiceDto>> GetByRequest(Guid requestId)
        {
            var invoices = await _repository.GetByRequest(requestId);

            return invoices.ToInvoiceDto();
        }

        public async Task<InvoiceDto> Create(InvoiceDto invoiceDto)
        {
            _transacionProvider.BeginTransaction();

            try
            {
                var invoice = invoiceDto.ToModel();

                await _repository.Create(invoice);

                var facturapiInvoice = invoiceDto.ToFacturapiDto();
                facturapiInvoice.ClaveExterna = invoice.Id.ToString();

                var facturapiResponse = await _invoiceClient.CreateInvoice(facturapiInvoice);

                invoice.FacturapiId = facturapiResponse.FacturapiId;
                await _repository.Update(invoice);

                _transacionProvider.CommitTransaction();

                invoiceDto.Id = invoice.Id;
                invoiceDto.FacturapiId = facturapiResponse.FacturapiId;

                return invoiceDto;
            }
            catch (Exception)
            {
                _transacionProvider.RollbackTransaction();
                throw;
            }
        }
        public async Task<InvoiceDto> CreateInvoiceCompany(InvoiceDto invoiceDto)
        {
            
            try
            {
                var invoice = invoiceDto.ToModelCompany();


                var facturapiInvoice = invoiceDto.ToFacturapiDto();
                facturapiInvoice.ClaveExterna = invoice.Id.ToString();

                var facturapiResponse = await _invoiceClient.CreateInvoice(facturapiInvoice);

                invoice.FacturapiId = facturapiResponse.FacturapiId;
                await _repository.CreateInvoiceCompany(invoice);

                invoiceDto.Id = invoice.Id;
                invoiceDto.FacturapiId = facturapiResponse.FacturapiId;

                return invoiceDto;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        public async Task<string> Cancel(InvoiceCancelation invoiceDto)
        {

            try
            {
                

                var facturapiResponse = await _invoiceClient.Cancel(invoiceDto);
                if (facturapiResponse.ToLower() == "canceled")
                {
                    var invoice = await _repository.GetInvoiceCompanyByFacturapiId(invoiceDto.FacturapiId);

                    invoice.Estatus = "Cancelado";

                    await _repository.UpdateInvoiceCompany(invoice);
                }


                return facturapiResponse;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<(byte[], string)> PrintInvoiceXML(string invoiceId)
        {
            //var invoice = await GetExistingInvoice(invoiceId);

            var xml = await _invoiceClient.GetInvoiceXML(invoiceId);

            return new(xml, invoiceId + ".xml");
        }
        public async Task<(byte[], string)> PrintInvoicePDF(string invoiceId)
        {
            //var invoice = await GetExistingInvoice(invoiceId);

            var pdf = await _invoiceClient.GetInvoicePDF(invoiceId);

            return new(pdf, invoiceId + ".pdf");
        }

        private async Task<Invoice> GetExistingInvoice(Guid invoiceId)
        {
            var invoice = await _repository.GetById(invoiceId);

            if (invoice == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            return invoice;
        }

    }
}
