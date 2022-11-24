using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Service.MedicalRecord.Context;
using Service.MedicalRecord.Domain.MedicalRecord;
using Service.MedicalRecord.Domain.TaxData;
using Service.MedicalRecord.Dtos;
using Service.MedicalRecord.Dtos.MedicalRecords;
using Service.MedicalRecord.Mapper;
using Service.MedicalRecord.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Repository
{
    public class MedicalRecordRepository : IMedicalRecordRepository
    {
        private readonly ApplicationDbContext _context;

        public MedicalRecordRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Domain.MedicalRecord.MedicalRecord>> GetAll()
        {
            var expedientes = _context.CAT_Expedientes.AsQueryable();

            return await expedientes.ToListAsync();
        }

        public async Task<List<Domain.MedicalRecord.MedicalRecord>> GetNow(MedicalRecordSearch search)
        
        {
            var expedientes = _context.CAT_Expedientes.AsQueryable();

            if (!string.IsNullOrEmpty(search.ciudad))
            {


                expedientes = expedientes.Where(x => x.Ciudad == search.ciudad);


            }
            if (!string.IsNullOrEmpty(search.expediente))
            {
                expedientes = expedientes.Where(x => x.Expediente.Contains(search.expediente) || x.NombrePaciente.ToLower().Contains(search.expediente.ToLower()) || x.PrimerApellido.ToLower().Contains(search.expediente.ToLower()));

            }
            if (search.fechaNacimiento.Date != DateTime.MinValue.Date)
            {
                expedientes = expedientes.Where(x => x.FechaDeNacimiento.Date == search.fechaNacimiento.Date);

            }

            if (search.fechaAlta != null && search.fechaAlta[0].Date != DateTime.MinValue.Date && search.fechaAlta[1].Date != DateTime.MinValue.Date)
            {
                expedientes = expedientes.Where(x => x.FechaCreo.Date >= search.fechaAlta[0].Date && x.FechaCreo.Date <= search.fechaAlta[1].Date);
            }
            if (!string.IsNullOrEmpty(search.sucursal))
            {
                expedientes = expedientes.Where(x => x.IdSucursal == Guid.Parse(search.sucursal));

            }
            if (!string.IsNullOrEmpty(search.telefono))
            {
                expedientes = expedientes.Where(x => x.Telefono == search.telefono);
            }
            return expedientes.ToList();
        }

        public async Task<List<Domain.MedicalRecord.MedicalRecord>> GetActive()
        {
            var expedientes = await _context.CAT_Expedientes.Where(x => x.Activo).ToListAsync();

            return expedientes;
        }

        public async Task<List<TaxData>> GetTaxData(Guid recordId)
        {
            var taxData = await _context.Relacion_Expediente_Factura
                .Where(x => x.ExpedienteID == recordId)
                .Include(x => x.Factura)
                .Select(x => x.Factura)
                .ToListAsync();

            return taxData;
        }

        public async Task<Domain.MedicalRecord.MedicalRecord> GetById(Guid id)
        {
            var expedientes = await _context.CAT_Expedientes.Include(x => x.TaxData).ThenInclude(x => x.Factura).FirstOrDefaultAsync(x => x.Id == id);

            return expedientes;
        }

        public async Task<TaxData> GetTaxDataById(Guid id, Guid recordId)
        {
            var taxData = await _context.Relacion_Expediente_Factura
                .Where(x => x.FacturaID == id && x.ExpedienteID == recordId)
                .Include(x => x.Factura)
                .Select(x => x.Factura)
                .FirstOrDefaultAsync();

            return taxData;
        }

        public async Task Create(Domain.MedicalRecord.MedicalRecord expediente, IEnumerable<TaxDataDto> taxdata)
        {
            expediente.TaxData = null;
            var newtaxdata = taxdata.ToTaxData() ?? new List<TaxData>();

            _context.CAT_Expedientes.Add(expediente);
            await _context.SaveChangesAsync();

            var config = new BulkConfig() { SetOutputIdentity = true };
            await _context.BulkInsertOrUpdateOrDeleteAsync(newtaxdata, config);

            var taxdataMedicalRecord = newtaxdata.ToTaxDataMedicalRecord();
            config.SetSynchronizeFilter<MedicalRecordTaxData>(x => x.ExpedienteID == expediente.Id);
            taxdataMedicalRecord.ForEach(x => x.ExpedienteID = expediente.Id);
            await _context.BulkInsertOrUpdateOrDeleteAsync(taxdataMedicalRecord, config);
        }

        public async Task CreateTaxData(TaxData taxData, MedicalRecordTaxData recordTaxData)
        {
            _context.CAT_Datos_Fiscales.Add(taxData);

            _context.Relacion_Expediente_Factura.Add(recordTaxData);

            await _context.SaveChangesAsync();
        }

        public async Task Update(Domain.MedicalRecord.MedicalRecord expediente, IEnumerable<TaxDataDto> taxdata)
        {
            expediente.TaxData = null;
            if (taxdata == null) { taxdata = new List<TaxDataDto>(); }
            var newtaxdata = taxdata.Where(x => x.Id == Guid.Empty).ToTaxData();
            var oldtaxData = taxdata.Where(x => x.Id != Guid.Empty).ToTaxDataUpdate();
            var finalTaxData = newtaxdata.Concat(oldtaxData).ToList();
            _context.CAT_Expedientes.Update(expediente);
            await _context.SaveChangesAsync();
            var config = new BulkConfig() { SetOutputIdentity = true };
            await _context.BulkInsertOrUpdateOrDeleteAsync(finalTaxData, config);

            var taxdataMedicalRecord = finalTaxData.ToTaxDataMedicalRecord();
            config.SetSynchronizeFilter<MedicalRecordTaxData>(x => x.ExpedienteID == expediente.Id);
            taxdataMedicalRecord.ForEach(x => x.ExpedienteID = expediente.Id);
            await _context.BulkInsertOrUpdateOrDeleteAsync(taxdataMedicalRecord, config);
        }
        public async Task UpdateWallet(Domain.MedicalRecord.MedicalRecord expediente)
        {
            _context.CAT_Expedientes.Update(expediente);
            await _context.SaveChangesAsync();

        }
        public async Task UpdateTaxData(TaxData taxData)
        {
            _context.CAT_Datos_Fiscales.Update(taxData);

            await _context.SaveChangesAsync();
        }
        public async Task<string> GetLastCode(Guid branchId, string date)
        {
            var lastRequest = await _context.CAT_Expedientes
                .OrderBy(x => x.FechaCreo)
                .LastOrDefaultAsync(x => x.IdSucursal == branchId && x.Expediente.EndsWith(date));

            return lastRequest?.Expediente;
        }
        public async Task<List<Domain.MedicalRecord.MedicalRecord>> Coincidencias(MedicalRecord.Domain.MedicalRecord.MedicalRecord expediente)
        {
            var expedientes = await _context.CAT_Expedientes
                .Where(x => x.NombrePaciente == expediente.NombrePaciente 
                && x.PrimerApellido == expediente.PrimerApellido 
                && x.FechaDeNacimiento.Date == expediente.FechaDeNacimiento.Date)
                .ToListAsync();

            return expedientes;
        }


    }
}
