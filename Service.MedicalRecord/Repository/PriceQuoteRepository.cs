using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using MoreLinq;
using Service.MedicalRecord.Context;
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

        public async Task<List<PriceQuote>> GetByFilter(PriceQuoteFilterDto filter)
        {
            var quotations = _context.CAT_Cotizaciones
                .Include(x => x.Expediente)
                .Include(x => x.Estudios)
                .AsQueryable();

            if (filter.FechaAInicial != null && filter.FechaAFinal != null)
            {
                quotations = quotations.Where(x => x.FechaCreo.Date <= ((DateTime)filter.FechaAInicial).Date
                && x.FechaCreo.Date >= ((DateTime)filter.FechaAFinal));
            }

            if (filter.Sucursales != null && filter.Sucursales.Count > 0)
            {
                //quotations = quotations.Where(x => filter.Sucursales.Contains(x.SucursalId));
            }

            if (filter.FechaNInicial != null && filter.FechaNFinal != null)
            {
                quotations = quotations.Where(x => x.Expediente != null
                && x.Expediente.FechaDeNacimiento.Date <= ((DateTime)filter.FechaNInicial).Date
                && x.Expediente.FechaDeNacimiento.Date >= ((DateTime)filter.FechaNFinal));
            }

            if (!string.IsNullOrWhiteSpace(filter.CorreoTelefono))
            {
                quotations = quotations.Where(x => (x.Correo != null && x.Correo.ToLower().Contains(filter.CorreoTelefono))
                || (x.Whatsapp != null && x.Whatsapp.ToLower().Contains(filter.CorreoTelefono)));
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

        public async Task<List<MedicalRecord.Domain.MedicalRecord.MedicalRecord>> GetMedicalRecord(PriceQuoteExpedienteSearch search)
        {
            var expedientes = _context.CAT_Expedientes.AsQueryable();

            if (!string.IsNullOrEmpty(search.Buscar))
            {

                expedientes = expedientes.Where(x => x.NombrePaciente.Contains(search.Buscar) || x.PrimerApellido.Contains(search.Buscar) || x.Expediente.Contains(search.Buscar));


            }
            if (!string.IsNullOrEmpty(search.Telefono))
            {

                expedientes = expedientes.Where(x => x.Telefono == search.Telefono);


            }
            if (search.FechaInicial.Date != DateTime.Now.Date && search.FechaFinal.Date != DateTime.Now.Date)
            {

                expedientes = expedientes.Where(x => (x.FechaCreo >= search.FechaInicial.Date && x.FechaCreo.Date <= search.FechaFinal.Date));


            }

            if (!string.IsNullOrEmpty(search.Email))
            {

                expedientes = expedientes.Where(x => x.Correo.Contains(search.Email));


            }
            return expedientes.ToList(); ;
        }
        public async Task<string> GetLastCode(string date)
        {
            var lastRequest = await _context.CAT_Cotizaciones
                .OrderBy(x => x.FechaCreo)
                .LastOrDefaultAsync(x => x.Afiliacion.EndsWith(date));

            return lastRequest?.Afiliacion;
        }

        public async Task Create(PriceQuote expediente)
        {
            var estudios = expediente.Estudios.ToList();
            expediente.Estudios = null;
            _context.CAT_Cotizaciones.Add(expediente);
            await _context.SaveChangesAsync();

            var config = new BulkConfig();
            config.SetSynchronizeFilter<CotizacionStudy>(x => x.CotizacionId == expediente.Id);
            estudios.ForEach(x => x.CotizacionId = expediente.Id);
            estudios.ForEach(x => x.id = Guid.NewGuid());
            await _context.BulkInsertOrUpdateOrDeleteAsync(estudios, config);
        }
        public async Task Update(PriceQuote expediente)
        {
            var estudios = expediente.Estudios.ToList();
            expediente.Estudios = null;
            _context.CAT_Cotizaciones.Update(expediente);
            await _context.SaveChangesAsync();

            var config = new BulkConfig();
            config.SetSynchronizeFilter<CotizacionStudy>(x => x.CotizacionId == expediente.Id);
            estudios.ForEach(x => x.CotizacionId = expediente.Id);
            estudios.ForEach(x => x.id = Guid.NewGuid());
            await _context.BulkInsertOrUpdateOrDeleteAsync(estudios, config);
        }
        public async Task<List<PriceQuote>> GetActive()
        {
            var expedientes = await _context.CAT_Cotizaciones.Where(x => x.Activo).ToListAsync();

            return expedientes;
        }

        public async Task<PriceQuote> GetById(Guid id)
        {
            var expedientes = await _context.CAT_Cotizaciones.Include(x => x.Expediente).FirstOrDefaultAsync(x => x.Id == id);
            var estudios = await _context.cotizacionStudies.Where(x => x.CotizacionId == expedientes.Id).ToListAsync();
            expedientes.Estudios = estudios;
            return expedientes;
        }
    }
}
