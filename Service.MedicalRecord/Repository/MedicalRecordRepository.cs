using Microsoft.EntityFrameworkCore;
using Service.MedicalRecord.Context;
using Service.MedicalRecord.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Service.MedicalRecord.Mapper;
using Service.MedicalRecord.Domain.TaxData;
using Service.MedicalRecord.Dtos;
using EFCore.BulkExtensions;
using Service.MedicalRecord.Domain.MedicalRecord;
using Service.MedicalRecord.Dtos.MedicalRecords;

namespace Service.MedicalRecord.Repository
{
    public class MedicalRecordRepository: IMedicalRecordRepository
    {
        private readonly ApplicationDbContext _context;

        public MedicalRecordRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<List<MedicalRecord.Domain.MedicalRecord.MedicalRecord>> GetAll()
        {
            var expedientes = _context.CAT_Expedientes.AsQueryable();



            return await expedientes.ToListAsync();
        }

        public async Task<List<MedicalRecord.Domain.MedicalRecord.MedicalRecord>> GetNow(MedicalRecordSearch search)
        {

            if (!string.IsNullOrEmpty(search.ciudad) || !string.IsNullOrEmpty(search.expediente) || search.fechaNacimiento.Date != DateTime.Now.Date || search.fechaAlta.Date != DateTime.Now.Date || !string.IsNullOrEmpty(search.sucursal))
            {
                var sucursal = Guid.Empty;
                if (!string.IsNullOrEmpty(search.sucursal)) {
                    sucursal = Guid.Parse(search.sucursal);
                }
                var expedientes = await _context.CAT_Expedientes.Where(x => x.Ciudad == search.ciudad || x.Expediente == search.expediente || search.expediente.Contains(x.NombrePaciente) || x.FechaDeNacimiento.Date == search.fechaNacimiento.Date || x.FechaCreo.Date == search.fechaAlta.Date || x.IdSucursal==sucursal).ToListAsync();
               
                return expedientes;
            }
            else {


                var expedientes = await _context.CAT_Expedientes.Where(x => x.FechaCreo.Date <= DateTime.Now.Date && x.FechaCreo.Date > DateTime.Now.AddDays(-1).Date).ToListAsync();

                return expedientes;
            } 
        }

        public async Task<List<MedicalRecord.Domain.MedicalRecord.MedicalRecord>> GetActive()
        {
            var expedientes = await _context.CAT_Expedientes.Where(x => x.Activo).ToListAsync();

            return expedientes;
        }

        public async Task<MedicalRecord.Domain.MedicalRecord.MedicalRecord> GetById(Guid id)
        {
            var expedientes = await _context.CAT_Expedientes.Include(x => x.TaxData).ThenInclude(x=>x.Factura).FirstOrDefaultAsync(x => x.Id == id);

            return expedientes;
        }

        public async Task Create(MedicalRecord.Domain.MedicalRecord.MedicalRecord expediente,IEnumerable<TaxDataDto> taxdata)
        { 
            
            expediente.TaxData = null;
            var newtaxdata = taxdata.ToTaxData();
            
            _context.CAT_Expedientes.Add(expediente);
            await _context.SaveChangesAsync();

            var config = new BulkConfig() { SetOutputIdentity = true };
            await _context.BulkInsertOrUpdateOrDeleteAsync(newtaxdata, config);

            var taxdataMedicalRecord = newtaxdata.ToTaxDataMedicalrecord();
            config.SetSynchronizeFilter<MedicalRecordTaxData>(x => x.ExpedienteID== expediente.Id);
            taxdataMedicalRecord.ForEach(x => x.ExpedienteID = expediente.Id);
            await _context.BulkInsertOrUpdateOrDeleteAsync(taxdataMedicalRecord, config);

            
        }

        public async Task Update(MedicalRecord.Domain.MedicalRecord.MedicalRecord expediente, IEnumerable<TaxDataDto> taxdata )
        {
            expediente.TaxData = null;
            if (taxdata == null) { taxdata = new List<TaxDataDto>(); }
            var newtaxdata = taxdata.Where(x => x.Id == Guid.Empty).ToTaxData();
            var oldtaxData = taxdata.Where(x=>x.Id!=Guid.Empty ).ToTaxDataUpdate();
            var finalTaxData = newtaxdata.Concat(oldtaxData).ToList();
            _context.CAT_Expedientes.Update(expediente);
            await _context.SaveChangesAsync();
            var config = new BulkConfig() { SetOutputIdentity = true };
            await _context.BulkInsertOrUpdateOrDeleteAsync(finalTaxData, config);

            
            var taxdataMedicalRecord = finalTaxData.ToTaxDataMedicalrecord();
            config.SetSynchronizeFilter<MedicalRecordTaxData>(x => x.ExpedienteID == expediente.Id);
            taxdataMedicalRecord.ForEach(x => x.ExpedienteID = expediente.Id);
            await _context.BulkInsertOrUpdateOrDeleteAsync(taxdataMedicalRecord, config);
        }

        public async  Task<List<MedicalRecord.Domain.MedicalRecord.MedicalRecord>> Coincidencias(MedicalRecord.Domain.MedicalRecord.MedicalRecord expediente) {
            var expedientes = await _context.CAT_Expedientes.Where(x=>   x.NombrePaciente == expediente.NombrePaciente && x.PrimerApellido== expediente.PrimerApellido).ToListAsync();

            return expedientes;
        }
    }
}
