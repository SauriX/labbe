﻿using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Service.Billing.Context;
using Service.Billing.Domain.Invoice;
using Service.Billing.Dtos.Invoice;
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

        public async Task<List<Invoice>> GetAllInvoice(InvoiceSearch search) {
            var invoices = _context.CAT_Factura.AsQueryable(); ;
            if (string.IsNullOrEmpty(search.Buscar)&&search.Fecha != null)
            {
                invoices = invoices.Where(x => x.FechaCreo.Date >= search.Fecha[0].Date && x.FechaCreo.Date <= search.Fecha[1].Date);

            }

            if (!string.IsNullOrEmpty(search.Buscar))
            {
                invoices = invoices.Where(x => x.SerieNumero == search.Buscar || x.Solicitud == search.Buscar);

            }

            var invoicesFilter = await invoices.ToListAsync();
            return invoicesFilter;
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

        public async Task CreateInvoiceCompany(Invoice invoice)
        {
            using var transaction = _context.Database.BeginTransaction();
            //using var scope = new TransactionScope();
            try
            {
                var solicitudes = invoice.Solicitudes.ToList();

                invoice.Solicitudes = null;
                _context.CAT_Factura.Add(invoice);

                await _context.SaveChangesAsync();

                solicitudes.ForEach(x => x.InvoiceId = invoice.Id);

                var config = new BulkConfig();
                config.SetSynchronizeFilter<InvoiceCompanyRequests>(x => x.InvoiceId == invoice.Id);

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
                config.SetSynchronizeFilter<InvoiceCompanyRequests>(x => x.InvoiceId == invoice.Id);

                await _context.BulkInsertOrUpdateOrDeleteAsync(solicitudes, config);

                transaction.Commit();
            }
            catch (System.Exception)
            {
                transaction.Rollback();
                throw;
            }
        }
        public async Task UpdateInvoiceCompany(Invoice invoiceCompnay)
        {
            _context.CAT_Factura.Update(invoiceCompnay);

            await _context.SaveChangesAsync();

            _context.ChangeTracker.Clear();
        }
        public async Task<Invoice> GetInvoiceCompanyByFacturapiId(string id)
        {
            var request = await _context.CAT_Factura
                .FirstOrDefaultAsync(x => x.FacturapiId == id);

            return request;
        }
    }
}
