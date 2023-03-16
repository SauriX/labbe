﻿using Service.MedicalRecord.Context;
using Service.MedicalRecord.Repository.IRepository;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using MoreLinq;
using Service.MedicalRecord.Domain.Invoice;
using Service.MedicalRecord.Domain.Request;
using Service.MedicalRecord.Dtos.InvoiceCompany;
using Service.MedicalRecord.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Repository
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly ApplicationDbContext _context;
        public InvoiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<List<Request>> InvoiceCompanyFilter(InvoiceCompanyFilterDto filter)
        {

            var requests = _context.CAT_Solicitud
                .Include(x => x.Expediente)
                .Include(x => x.Compañia)
                .Include(x => x.Pagos)
                .Include(x => x.Sucursal)
                .Include(x => x.FacturasCompañia)
                .Include(x => x.Estudios).ThenInclude(x => x.Estatus)
                .Include(x => x.Estudios).ThenInclude(x => x.Tapon)
                .Include(x => x.Pagos).ThenInclude(x => x.Estatus).OrderBy(x => x.FechaCreo)
                .OrderBy(x => x.FechaCreo)
                //.Where(x => x.Procedencia == 2)
                .AsQueryable();

            requests = requests.Where(x => x.Pagos.Count() > 0);

            if (filter.FacturaMetodo == "company")
            {
                requests = requests.Where(x => x.Procedencia == 1);
            }
            if (filter.FacturaMetodo == "request")
            {
                requests = requests.Where(x => x.Procedencia == 2);
            }
            if (string.IsNullOrWhiteSpace(filter.Buscar) && filter.FechaInicial != null && filter.FechaFinal != null)
            {
                requests = requests.Where(x => ((DateTime)filter.FechaInicial).Date <= x.FechaCreo.Date && ((DateTime)filter.FechaFinal).Date >= x.FechaCreo.Date);
            }

            if (filter.SucursalId != null && filter.SucursalId.Any())
            {
                requests = requests.Where(x => filter.SucursalId.Contains(x.SucursalId));
            }

            if (filter.Companias != null && filter.Companias.Any())
            {
                requests = requests.Where(x => x.CompañiaId != null && filter.Companias.Contains((Guid)x.CompañiaId));
            }
            if (filter.Departamentos != null && filter.Departamentos.Any())
            {
                requests = requests.Where(x => x.Estudios.Any(y => filter.Departamentos.Contains(y.DepartamentoId)));
            }
            if (filter.Medicos != null && filter.Medicos.Any())
            {
                requests = requests.Where(x => x.MedicoId != null && filter.Medicos.Contains((Guid)x.MedicoId));
            }
            if (filter.Urgencias != null && filter.Urgencias.Any())
            {
                requests = requests.Where(x => filter.Urgencias.Contains(x.Urgencia));
            }
            if (!string.IsNullOrWhiteSpace(filter.Buscar))
            {
                requests = requests.Where(x => x.Clave.ToLower().Contains(filter.Buscar)
                || x.ClavePatologica.ToLower().Contains(filter.Buscar)
                || (x.Expediente.NombrePaciente + " " + x.Expediente.PrimerApellido + " " + x.Expediente.SegundoApellido).ToLower().Contains(filter.Buscar));
            }
            if (filter.TipoFactura.Count() > 0)
            {
                if (filter.TipoFactura.Contains("facturadas"))
                {
                    requests = requests.Where(x => x.FacturasCompañia.Any(y => y.Estatus == "Facturado"));

                }
                if (filter.TipoFactura.Contains("noFacturadas"))
                {
                    requests = requests.Where(x => x.FacturasCompañia.Count() == 0);

                }
                if (filter.TipoFactura.Contains("canceladas"))
                {
                    requests = requests.Where(x => x.FacturasCompañia.Any(y => y.Estatus == "Cancelado"));

                }
            }
            return requests.ToListAsync();
        }
        public Task<List<InvoiceCompany>> InvoiceFreeFilter(InvoiceFreeFilterDto filter)
        {
            var invoices = _context.Factura_Compania.AsQueryable();

            if (filter.Compania != Guid.Empty)
            {
                invoices = invoices.Where(x => x.CompañiaId == filter.Compania);
            }

            if (filter.FechaInicial.Date != DateTime.MinValue.Date)
            {
                invoices = invoices.Where(x => x.FechaCreo.Date >= filter.FechaInicial.Date);
            } 
            
            if (filter.FechaFinal.Date != DateTime.MinValue.Date)
            {
                invoices = invoices.Where(x => x.FechaCreo.Date <= filter.FechaFinal.Date);
            }

            if (filter.Estatus != null && filter.Estatus.Count() > 0)
            {
                invoices = invoices.Where(x => filter.Estatus.Contains(x.Estatus));
            }
            
            if (filter.Tipo != null && filter.Tipo.Count() > 0)
            {
                invoices = invoices.Where(x => filter.Tipo.Contains(x.TipoFactura));
            }

            return invoices.ToListAsync();
        }
        public async Task CreateInvoiceCompanyData(InvoiceCompany invoiceCompnay, List<RequestInvoiceCompany> requestInvoiceCompany)
        {

            var detalles = invoiceCompnay.DetalleFactura.ToList();

            invoiceCompnay.DetalleFactura = null;

            _context.Factura_Compania.Add(invoiceCompnay);

            await _context.SaveChangesAsync();

            var config = new BulkConfig() { SetOutputIdentity = true, PreserveInsertOrder = true };

            await _context.BulkInsertOrUpdateAsync(requestInvoiceCompany, config);

            config.SetSynchronizeFilter<InvoiceCompanyDetail>(x => x.FacturaId == invoiceCompnay.Id);

            detalles.ForEach(x => x.FacturaId = invoiceCompnay.Id);

            await _context.BulkInsertOrUpdateAsync(detalles, config);
        }

        public async Task<InvoiceCompany> GetInvoiceById(string invoiceId)
        {
            return await _context.Factura_Compania
                .Include(x => x.DetalleFactura)
                .Where(x => x.FacturapiId == invoiceId)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateInvoiceCompany(InvoiceCompany invoiceCompnay)
        {
            _context.Factura_Compania.Update(invoiceCompnay);

            await _context.SaveChangesAsync();

            _context.ChangeTracker.Clear();
        }
        public async Task<InvoiceCompany> GetInvoiceCompanyByFacturapiId(string id)
        {
            var request = await _context.Factura_Compania
                .FirstOrDefaultAsync(x => x.FacturapiId == id);

            return request;
        }

        
    }
}
