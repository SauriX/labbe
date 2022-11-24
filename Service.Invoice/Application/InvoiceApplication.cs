using Service.Billing.Application.IApplication;
using Service.Billing.Dtos;
using Service.Billing.Mapper;
using Service.Billing.Repository.IRepository;
using Shared.Dictionary;
using Shared.Error;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Service.Billing.Application
{
    public class InvoiceApplication : IInvoiceApplication
    {
        private readonly IInvoiceRepository _repository;

        public InvoiceApplication(IInvoiceRepository repository)
        {
            _repository = repository;
        }

        public async Task<InvoiceDto> GetById(Guid invoiceId)
        {
            var invoice = await _repository.GetById(invoiceId);

            if (invoice == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            return invoice.ToInvoiceDto();
        }

        public async Task<List<InvoiceDto>> GetByRecord(Guid recordId)
        {
            var invoices = await _repository.GetByRecord(recordId);

            return invoices.ToInvoiceDto();
        }

        public Task<List<InvoiceDto>> GetByRequest(Guid requestId)
        {
            throw new NotImplementedException();
        }

        public Task<InvoiceDto> Create(InvoiceDto invoice)
        {
            throw new NotImplementedException();
        }

        public Task<byte[]> PrintInvoiceXML(Guid invoiceId)
        {
            throw new NotImplementedException();
        }
    }
}
