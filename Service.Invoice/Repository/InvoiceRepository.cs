using Microsoft.EntityFrameworkCore;
using Service.Billing.Context;
using Service.Billing.Domain.Invoice;
using Service.Billing.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Billing.Repository
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly ApplicationDbContext _context;

        public InvoiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Invoice> GetById(Guid invoiceId)
        {
            var invoice = await _context.CAT_Factura.FindAsync(invoiceId);

            return invoice;
        }

        public async Task<List<Invoice>> GetByRecord(Guid recordId)
        {
            var invoices = await _context.CAT_Factura.Where(x => x.ExpedienteId == recordId).ToListAsync();

            return invoices;
        }

        public async Task<List<Invoice>> GetByRequest(Guid requestId)
        {
            var invoices = await _context.CAT_Factura.Where(x => x.SolicitudId == requestId).ToListAsync();

            return invoices;
        }

        public async Task Create(Invoice invoice)
        {
            _context.CAT_Factura.Add(invoice);

            await _context.SaveChangesAsync();
        }      
        
        public async Task Update(Invoice invoice)
        {
            _context.CAT_Factura.Update(invoice);

            await _context.SaveChangesAsync();
        }
    }
}
