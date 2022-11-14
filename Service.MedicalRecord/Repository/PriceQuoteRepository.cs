using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using MoreLinq;
using Service.MedicalRecord.Context;
using Service.MedicalRecord.Domain.PriceQuote;
using Service.MedicalRecord.Domain.PriceQuote;
using Service.MedicalRecord.Dtos.PriceQuote;
using Service.MedicalRecord.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Repository
{
    public class PriceQuoteRepository : IPriceQuoteRepository
    {
        private readonly ApplicationDbContext _context;

        public PriceQuoteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PriceQuote> FindAsync(Guid id)
        {
            var priceQuote = await _context.CAT_Cotizaciones.FindAsync(id);

            return priceQuote;
        }

        public async Task<List<PriceQuote>> GetByFilter(PriceQuoteFilterDto filter)
        {
            var priceQuotes = _context.CAT_Cotizaciones
                .Include(x => x.Expediente)
                .Include(x => x.Estudios)
                .AsQueryable();

            if (filter.FechaAInicial != null && filter.FechaAFinal != null)
            {
                priceQuotes = priceQuotes.Where(x => x.FechaCreo.Date <= ((DateTime)filter.FechaAInicial).Date
                && x.FechaCreo.Date >= ((DateTime)filter.FechaAFinal));
            }

            if (filter.Sucursales != null && filter.Sucursales.Count > 0)
            {
                priceQuotes = priceQuotes.Where(x => x.Sucursal != null && filter.Sucursales.Contains((Guid)x.SucursalId));
            }

            if (filter.FechaNInicial != null && filter.FechaNFinal != null)
            {
                priceQuotes = priceQuotes.Where(x => x.Expediente != null
                && x.Expediente.FechaDeNacimiento.Date <= ((DateTime)filter.FechaNInicial).Date
                && x.Expediente.FechaDeNacimiento.Date >= ((DateTime)filter.FechaNFinal));
            }

            if (!string.IsNullOrWhiteSpace(filter.CorreoTelefono))
            {
                priceQuotes = priceQuotes.Where(x => (x.EnvioCorreo != null && x.EnvioCorreo.ToLower().Contains(filter.CorreoTelefono))
                || (x.EnvioWhatsapp != null && x.EnvioWhatsapp.ToLower().Contains(filter.CorreoTelefono)));
            }

            if (!string.IsNullOrWhiteSpace(filter.Expediente))
            {
                priceQuotes = priceQuotes
                    .Where(x => x.Expediente != null
                    && ((x.Expediente.NombrePaciente + " " + x.Expediente.PrimerApellido + " " + x.Expediente.SegundoApellido).ToLower().Contains(filter.Expediente.ToLower())
                    || (x.Expediente != null && x.Expediente.Expediente.ToLower().Contains(filter.Expediente.ToLower()))));
            }

            return await priceQuotes.ToListAsync();
        }

        public async Task<List<PriceQuote>> GetActive()
        {
            var priceQuotes = await _context.CAT_Cotizaciones.Where(x => x.Activo).ToListAsync();

            return priceQuotes;
        }

        public async Task<PriceQuote> GetById(Guid id)
        {
            var priceQuote = await _context.CAT_Cotizaciones
                .Include(x => x.Expediente)
                .Include(x => x.Estudios)
                .FirstOrDefaultAsync(x => x.Id == id);

            return priceQuote;
        }


        public async Task<string> GetLastCode(Guid branchId, string date)
        {
            var priceQuote = await _context.CAT_Cotizaciones
                .OrderBy(x => x.FechaCreo)
                .LastOrDefaultAsync(x => x.SucursalId == branchId && x.Clave.EndsWith(date));

            return priceQuote?.Clave;
        }

        public async Task<List<PriceQuoteStudy>> GetStudyById(Guid priceQuoteId, IEnumerable<int> studiesIds)
        {
            var studies = await _context.Relacion_Cotizacion_Estudio
                //.Where(x => x.CotizacionId == priceQuoteId && studiesIds.Contains(x.Id))
                .ToListAsync();

            return studies;
        }

        public async Task<List<PriceQuoteStudy>> GetStudiesByPriceQuote(Guid priceQuoteId)
        {
            var studies = await _context.Relacion_Cotizacion_Estudio
                .Include(x => x.Paquete)
                .Where(x => x.CotizacionId == priceQuoteId && x.PaqueteId == null)
                .ToListAsync();

            return studies;
        }

        public async Task<List<PriceQuotePack>> GetPacksByPriceQuote(Guid priceQuoteId)
        {
            //var studies = await _context.Relacion_Cotizacion_Estudio
            //    .Include(x => x.Estudios).ThenInclude(x => x.Estatus)
            //    .Where(x => x.SolicitudId == priceQuoteId)
            //    .ToListAsync();

            return new List<PriceQuotePack>();
        }

        public async Task Create(PriceQuote priceQuote)
        {
            _context.CAT_Cotizaciones.Add(priceQuote);

            await _context.SaveChangesAsync();
        }

        public async Task Update(PriceQuote expediente)
        {
            _context.CAT_Cotizaciones.Update(expediente);

            await _context.SaveChangesAsync();
        }

        public Task BulkUpdatePacks(Guid priceQuoteId, List<PriceQuotePack> studies)
        {
            throw new NotImplementedException();
        }

        public Task BulkUpdateDeletePacks(Guid priceQuoteId, List<PriceQuotePack> studies)
        {
            throw new NotImplementedException();
        }

        public Task BulkUpdateStudies(Guid priceQuoteId, List<PriceQuoteStudy> studies)
        {
            throw new NotImplementedException();
        }

        public Task BulkUpdateDeleteStudies(Guid priceQuoteId, List<PriceQuoteStudy> studies)
        {
            throw new NotImplementedException();
        }
    }
}
