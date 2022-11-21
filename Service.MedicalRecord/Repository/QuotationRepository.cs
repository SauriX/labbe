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
                .Include(x => x.Estudios)
                .AsQueryable();

            if (filter.FechaAInicial != null && filter.FechaAFinal != null)
            {
                quotations = quotations.Where(x => x.FechaCreo.Date >= ((DateTime)filter.FechaAInicial).Date
                && x.FechaCreo.Date <= ((DateTime)filter.FechaAFinal).Date);
            }

            if (filter.Sucursales != null && filter.Sucursales.Count > 0)
            {
                quotations = quotations.Where(x => x.Sucursal != null && filter.Sucursales.Contains((Guid)x.SucursalId));
            }

            if (filter.FechaNInicial != null && filter.FechaNFinal != null)
            {
                quotations = quotations.Where(x => x.Expediente != null
                && x.Expediente.FechaDeNacimiento.Date <= ((DateTime)filter.FechaNInicial).Date
                && x.Expediente.FechaDeNacimiento.Date >= ((DateTime)filter.FechaNFinal).Date);
            }

            if (!string.IsNullOrWhiteSpace(filter.CorreoTelefono))
            {
                quotations = quotations.Where(x => (x.EnvioCorreo != null && x.EnvioCorreo.ToLower().Contains(filter.CorreoTelefono))
                || (x.EnvioWhatsApp != null && x.EnvioWhatsApp.ToLower().Contains(filter.CorreoTelefono)));
            }

            if (!string.IsNullOrWhiteSpace(filter.Expediente))
            {
                quotations = quotations
                    .Where(x => x.Expediente != null
                    && ((x.Expediente.NombrePaciente + " " + x.Expediente.PrimerApellido + " " + x.Expediente.SegundoApellido).ToLower().Contains(filter.Expediente.ToLower())
                    || (x.Expediente != null && x.Expediente.Expediente.ToLower().Contains(filter.Expediente.ToLower()))));
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
                .FirstOrDefaultAsync(x => x.Id == id);

            return quotation;
        }

        public async Task<string> GetLastCode(Guid branchId, string date)
        {
            var quotation = await _context.CAT_Cotizacion
                .OrderBy(x => x.FechaCreo)
                .LastOrDefaultAsync(x => x.SucursalId == branchId && x.Clave.EndsWith(date));

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

        public async Task BulkInsertUpdatePacks(Guid quotationId, List<QuotationPack> packs)
        {
            var config = new BulkConfig();
            config.SetSynchronizeFilter<QuotationPack>(x => x.CotizacionId == quotationId);

            await _context.BulkInsertOrUpdateAsync(packs, config);
        }

        public async Task BulkDeletePacks(Guid quotationId, List<QuotationPack> packs)
        {
            var config = new BulkConfig();
            config.SetSynchronizeFilter<QuotationPack>(x => x.CotizacionId == quotationId);

            await _context.BulkDeleteAsync(packs, config);
        }

        public async Task BulkInsertUpdateStudies(Guid quotationId, List<QuotationStudy> studies)
        {
            var config = new BulkConfig();
            config.SetSynchronizeFilter<QuotationStudy>(x => x.CotizacionId == quotationId);

            await _context.BulkInsertOrUpdateAsync(studies, config);
        }

        public async Task BulkDeleteStudies(Guid quotationId, List<QuotationStudy> studies)
        {
            var config = new BulkConfig();
            config.SetSynchronizeFilter<QuotationStudy>(x => x.CotizacionId == quotationId);

            await _context.BulkDeleteAsync(studies, config);
        }
    }
}
