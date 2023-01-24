using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Service.Billing.Context;
using Service.Billing.Domain.Invoice;
using Service.Billing.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

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

        public async Task<string> GetLastSeriesCode(string serie, string year)
        {
            var last = await _context.CAT_Factura
                .Where(x => x.Serie == serie && x.SerieNumero.StartsWith(year))
                .OrderBy(x => Convert.ToInt32(x.SerieNumero ?? "0"))
                .LastOrDefaultAsync();

            return last?.SerieNumero;
        }

        public async Task Create(Invoice invoice)
        {
            _context.CAT_Factura.Add(invoice);

            await _context.SaveChangesAsync();

            _context.ChangeTracker.Clear();
        }

        public async Task CreateInvoiceCompany(InvoiceCompany invoice)
        {
            using var transaction = _context.Database.BeginTransaction();
            //using var scope = new TransactionScope();
            try
            {
                var solicitudes = invoice.Solicitudes.ToList();

                invoice.Solicitudes = null;
                _context.CAT_Factura_Companias.Add(invoice);

                await _context.SaveChangesAsync();

                solicitudes.ForEach(x => x.InvoiceCompanyId = invoice.Id);

                var config = new BulkConfig();
                config.SetSynchronizeFilter<InvoiceCompanyRequests>(x => x.InvoiceCompanyId == invoice.Id);

                await _context.BulkInsertOrUpdateOrDeleteAsync(solicitudes, config);

                transaction.Commit();
                //scope.Complete();
            }
            catch (System.Exception)
            {
                transaction.Rollback();
                //scope.Dispose();
                throw;
            }
        }

        public async Task Update(Invoice invoice)
        {
            _context.CAT_Factura.Update(invoice);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateCompany(InvoiceCompany invoice)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var solicitudes = invoice.Solicitudes.ToList();

                invoice.Solicitudes = null;

                _context.CAT_Factura_Companias.Update(invoice);

                await _context.SaveChangesAsync();

                var config = new BulkConfig();
                config.SetSynchronizeFilter<InvoiceCompanyRequests>(x => x.InvoiceCompanyId == invoice.Id);

                await _context.BulkInsertOrUpdateOrDeleteAsync(solicitudes, config);

                transaction.Commit();
            }
            catch (System.Exception)
            {
                transaction.Rollback();
                throw;
            }
        }
        public async Task UpdateInvoiceCompany(InvoiceCompany invoiceCompnay)
        {
            _context.CAT_Factura_Companias.Update(invoiceCompnay);

            await _context.SaveChangesAsync();

            _context.ChangeTracker.Clear();
        }
        public async Task<InvoiceCompany> GetInvoiceCompanyByFacturapiId(string id)
        {
            var request = await _context.CAT_Factura_Companias
                .FirstOrDefaultAsync(x => x.FacturapiId == id);

            return request;
        }
    }
}
