using Microsoft.EntityFrameworkCore;
using Service.Report.Context;
using Service.Report.Domain.Indicators;
using Service.Report.Domain.Request;
using Service.Report.Dtos;
using Service.Report.Repository.IRepository;
using System;
using EFCore.BulkExtensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Repository
{
    public class ReportRepository : IReportRepository
    {
        private const int Correo = 1;
        private const int Telefono = 2;
        private const int Urgencia = 1;
        private const int UrgenciaConCargo = 2;
        private const int Convenio = 1;
        private const int Todas = 2;

        private readonly ApplicationDbContext _context;

        public ReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Indicators>> GetBudgetByDate(DateTime startDate, DateTime endDate)
        {
            var budget = new List<Indicators>();
            if (startDate != endDate)
            {
                budget = await _context.CAT_Indicadores.Where(x => startDate.Date <= x.Fecha.Date && endDate.Date >= x.Fecha.Date).ToListAsync();
            }
            else
            {
                budget = await _context.CAT_Indicadores.Where(x => startDate.Date == x.Fecha.Date).ToListAsync();
            }

            return budget;
        }

        public async Task<List<SamplesCosts>> GetSamplesByDate(DateTime startDate, DateTime endDate)
        {
            var sample = new List<SamplesCosts>();
            if (startDate != endDate)
            {
                sample = await _context.CAT_CostosToma.Where(x => startDate.Month <= x.FechaAlta.Month && endDate.Month >= x.FechaAlta.Month).ToListAsync();
            }
            else
            {
                sample = await _context.CAT_CostosToma.Where(x => startDate.Month == x.FechaAlta.Month).ToListAsync();
            }

            return sample;
        }

        public async Task<List<SamplesCosts>> GetSamplesCostsByFilter(ReportModalFilterDto search)
        {
            var samplesCosts = _context.CAT_CostosToma.AsQueryable();

            if (search.Fecha != null)
            {
                samplesCosts = samplesCosts.Where(x => x.FechaAlta.Month >= search.Fecha.First().Month && x.FechaAlta.Month <= search.Fecha.Last().Month);
            }

            if (search.SucursalId != null && search.SucursalId.Count > 0)
            {
                samplesCosts = samplesCosts.Where(x => search.SucursalId.Contains(x.SucursalId));
            }

            if (search.Ciudad != null && search.Ciudad.Count > 0)
            {
                samplesCosts = samplesCosts.Where(x => search.Ciudad.Contains(x.Ciudad));
            }

            return await samplesCosts.ToListAsync();
        }

        public async Task CreateIndicators(Indicators indicator)
        {
            _context.Add(indicator);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateIndicators(Indicators indicator)
        {
            _context.Update(indicator);

            await _context.SaveChangesAsync();
        }

        public async Task CreateSamples(List<SamplesCosts> sample)
        {
            await _context.BulkInsertAsync(sample);
        }

        public async Task UpdateSamples(SamplesCosts sample)
        {
            _context.Update(sample);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsIndicatorDuplicate(Indicators indicator)
        {
            var isDuplicate = await _context.CAT_Indicadores.AnyAsync(x => x.SucursalId != indicator.SucursalId);

            return isDuplicate;
        }

        public async Task<Indicators> GetIndicatorById(Guid branchId, DateTime date)
        {
            var indicator = await _context.CAT_Indicadores.FirstOrDefaultAsync(x => x.SucursalId == branchId && x.Fecha.Date == date.Date);

            return indicator;
        }

        public async Task<SamplesCosts> GetSampleCostById(Guid branchId, DateTime date)
        {
            var sample = await _context.CAT_CostosToma.FirstOrDefaultAsync(x => x.SucursalId == branchId && x.FechaAlta.Month == date.Month && x.FechaAlta.Year == date.Year);

            return sample;
        }

    }
}
