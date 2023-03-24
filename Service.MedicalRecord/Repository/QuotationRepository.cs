using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using MoreLinq;
using Service.MedicalRecord.Context;
using Service.MedicalRecord.Domain.Quotation;
using Service.MedicalRecord.Dtos.Quotation;
using Service.MedicalRecord.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Repository
{
    public class QuotationRepository : IQuotationRepository
    {
        private readonly ApplicationDbContext _context;

        public QuotationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Quotation> FindAsync(Guid id)
        {
            var quotation = await _context.CAT_Cotizacion.FindAsync(id);

            return quotation;
        }

        public async Task<List<Quotation>> GetByFilter(QuotationFilterDto filter)
        {
            var quotations = _context.CAT_Cotizacion
                .Include(x => x.Expediente)
                .Include(x => x.Sucursal)
                .Include(x => x.Estudios)
                .OrderBy(x => x.FechaCreo)
                .AsQueryable();

            if (string.IsNullOrWhiteSpace(filter.Expediente) && filter.FechaAInicial != null && filter.FechaAFinal != null)
            {
                quotations = quotations.Where(x => x.FechaCreo.Date >= ((DateTime)filter.FechaAInicial).Date
                && x.FechaCreo.Date <= ((DateTime)filter.FechaAFinal).Date);
            }

            if (filter.Ciudad != null)
            {
                quotations = quotations.Where(x => x.Sucursal != null && filter.Ciudad.Contains(x.Sucursal.Ciudad));
            }

            if (filter.Sucursales != null && filter.Sucursales.Count > 0)
            {
                quotations = quotations.Where(x => x.Sucursal != null && filter.Sucursales.Contains(x.SucursalId));
            }

            if (filter.FechaNInicial != null)
            {
                quotations = quotations.Where(x => x.Expediente != null
                && x.Expediente.FechaDeNacimiento.Date == ((DateTime)filter.FechaNInicial).Date);
            }

            if (!string.IsNullOrWhiteSpace(filter.Correo))
            {
                quotations = quotations.Where(x => (x.Expediente != null && x.Expediente.Correo.ToLower().Contains(filter.Correo.ToLower()))
                || (x.EnvioCorreo != null && x.EnvioCorreo.ToLower().Contains(filter.Correo)));
            }

            if (!string.IsNullOrWhiteSpace(filter.Telefono))
            {
                quotations = quotations.Where(x => (x.Expediente != null
                    && (x.Expediente.Telefono.ToLower().Contains(filter.Telefono.ToLower())
                    || x.Expediente.Celular.ToLower().Contains(filter.Telefono.ToLower())))
                || (x.EnvioWhatsApp != null && x.EnvioWhatsApp.ToLower().Contains(filter.Telefono)));
            }

            if (!string.IsNullOrWhiteSpace(filter.Expediente))
            {
                quotations = quotations
                    .Where(x => x.Expediente != null
                    && ((x.Expediente.NombrePaciente + " " + x.Expediente.PrimerApellido + " " + x.Expediente.SegundoApellido).ToLower().Contains(filter.Expediente.ToLower())
                    || x.Clave == filter.Expediente));
            }

            return await quotations.ToListAsync();
        }

        public async Task<List<Quotation>> GetActive()
        {
            var quotations = await _context.CAT_Cotizacion.Where(x => x.Activo).ToListAsync();

            return quotations;
        }

        public async Task<Quotation> GetById(Guid id)
        {
            var quotation = await _context.CAT_Cotizacion
                .Include(x => x.Expediente)
                .Include(x => x.Estudios)
                .Include(x => x.Paquetes)
                .AsSplitQuery()
                .FirstOrDefaultAsync(x => x.Id == id);

            return quotation;
        }

        public async Task<string> GetLastCode(Guid branchId, string date)
        {
            var quotation = await _context.CAT_Cotizacion
                .OrderByDescending(x => x.FechaCreo)
                .FirstOrDefaultAsync(x => x.SucursalId == branchId && x.Clave.StartsWith(date));

            return quotation?.Clave;
        }

        public async Task<List<QuotationStudy>> GetStudyById(Guid quotationId, IEnumerable<int> studiesIds)
        {
            var studies = await _context.Relacion_Cotizacion_Estudio
                .Where(x => x.CotizacionId == quotationId && studiesIds.Contains(x.Id))
                .ToListAsync();

            return studies;
        }

        public async Task<List<QuotationStudy>> GetStudiesByQuotation(Guid quotationId)
        {
            var studies = await _context.Relacion_Cotizacion_Estudio
                .Include(x => x.Paquete)
                .Where(x => x.CotizacionId == quotationId && x.PaqueteId == null)
                .ToListAsync();

            return studies;
        }

        public async Task<List<QuotationPack>> GetPacksByQuotation(Guid quotationId)
        {
            var studies = await _context.Relacion_Cotizacion_Paquete
                .Include(x => x.Estudios)
                .Where(x => x.CotizacionId == quotationId)
                .ToListAsync();

            return new List<QuotationPack>();
        }

        public async Task Create(Quotation quotation)
        {
            _context.CAT_Cotizacion.Add(quotation);

            await _context.SaveChangesAsync();
        }

        public async Task Update(Quotation quotation)
        {
            _context.CAT_Cotizacion.Update(quotation);

            await _context.SaveChangesAsync();
        }

        public async Task Delete(Quotation quotation)
        {
            _context.CAT_Cotizacion.Remove(quotation);

            await _context.SaveChangesAsync();
        }

        public async Task BulkUpdateDelete(Guid quotationId, List<QuotationPack> packs)
        {
            var config = new BulkConfig();
            config.SetSynchronizeFilter<QuotationPack>(x => x.CotizacionId == quotationId);
            config.SetOutputIdentity = true;

            await _context.BulkInsertOrUpdateAsync(packs, config);
        }

        public async Task BulkDeletePacks(Guid quotationId, List<QuotationPack> packs)
        {
            var config = new BulkConfig();
            config.SetSynchronizeFilter<QuotationPack>(x => x.CotizacionId == quotationId);

            await _context.BulkDeleteAsync(packs, config);
        }

        public async Task BulkUpdateDeleteStudies(Guid quotationId, List<QuotationStudy> studies)
        {
            var config = new BulkConfig();
            config.SetSynchronizeFilter<QuotationStudy>(x => x.CotizacionId == quotationId);
            config.SetOutputIdentity = true;

            await _context.BulkInsertOrUpdateOrDeleteAsync(studies, config);
        }

        public async Task BulkDeleteStudies(Guid quotationId, List<QuotationStudy> studies)
        {
            var config = new BulkConfig();
            config.SetSynchronizeFilter<QuotationStudy>(x => x.CotizacionId == quotationId);

            await _context.BulkDeleteAsync(studies, config);
        }


    }
}
