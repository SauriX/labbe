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
        public async Task<List<MedicalRecord.Domain.MedicalRecord.MedicalRecord>> GetMedicalRecord(PriceQuoteExpedienteSearch search)
        {
            var expedientes =  _context.CAT_Expedientes.AsQueryable();

            if (!string.IsNullOrEmpty(search.Buscar))
            {

                 expedientes =expedientes.Where(x => x.NombrePaciente.Contains(search.Buscar) || x.PrimerApellido.Contains(search.Buscar) || x.Expediente.Contains(search.Buscar));

                
            }
            if ( !string.IsNullOrEmpty(search.Telefono) )
            {

                expedientes = expedientes.Where(x => x.Telefono == search.Telefono  );


            }
            if ( search.FechaInicial.Date != DateTime.Now.Date && search.FechaFinal.Date != DateTime.Now.Date )
            {

                expedientes = expedientes.Where(x =>  (x.FechaCreo >= search.FechaInicial.Date && x.FechaCreo.Date <= search.FechaFinal.Date));


            }

            if ( !string.IsNullOrEmpty(search.Email))
            {

                expedientes = expedientes.Where(x => x.Correo.Contains(search.Email)) ;


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
        public async Task<List<PriceQuote>> GetNow(PriceQuoteSearchDto search)
        {

            if (!string.IsNullOrEmpty(search.Presupuesto) || !string.IsNullOrEmpty(search.Email) || !string.IsNullOrEmpty(search.Ciudad) || !string.IsNullOrEmpty(search.Sucursal) || !string.IsNullOrEmpty(search.Telefono) || search.Activo || search.FechaAlta.Date != DateTime.Now.Date)
            {
                var sucursal = Guid.Empty;
                if (!string.IsNullOrEmpty(search.Sucursal))
                {
                    sucursal = Guid.Parse(search.Sucursal);
                }
                var expedientes = await _context.CAT_Cotizaciones.Include(x => x.Expediente).Where(x => x.Expediente.Ciudad == search.Ciudad || x.NombrePaciente.Contains(search.Presupuesto) || x.FechaCreo.Date == search.FechaAlta.Date || x.Expediente.IdSucursal == sucursal).ToListAsync();

                return expedientes.Select(x =>
                {
                    var estudios = _context.cotizacionStudies.Where(y => y.CotizacionId == x.Id);
                    x.Estudios = estudios;
                    return x;
                }).ToList();
            }
            else
            {


                var expedientes = await _context.CAT_Cotizaciones.Include(x => x.Expediente).Where(x => x.FechaCreo.Date <= DateTime.Now.Date && x.FechaCreo.Date > DateTime.Now.AddDays(-1).Date).ToListAsync();

                return expedientes.Select(x =>
                {
                    var estudios = _context.cotizacionStudies.Where(y => y.CotizacionId == x.Id);
                    x.Estudios = estudios;
                    return x;
                }).ToList();
            }
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
            var estudios = _context.cotizacionStudies.Where(x => x.CotizacionId == expedientes.Id);
            expedientes.Estudios = estudios;
            return expedientes;
        }
    }
}
