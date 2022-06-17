using Microsoft.EntityFrameworkCore;
using Service.MedicalRecord.Context;
using Service.MedicalRecord.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<List<MedicalRecord.Domain.MedicalRecord.MedicalRecord>> GetNow()
        {
            
            
            var expedientes = await _context.CAT_Expedientes.Where(x => x.FechaCreo.Date <= DateTime.Now.Date &&  x.FechaCreo.Date >DateTime.Now.AddDays(-1).Date).ToListAsync();
            
            return expedientes;
        }

        public async Task<List<MedicalRecord.Domain.MedicalRecord.MedicalRecord>> GetActive()
        {
            var expedientes = await _context.CAT_Expedientes.Where(x => x.Activo).ToListAsync();

            return expedientes;
        }

        public async Task<MedicalRecord.Domain.MedicalRecord.MedicalRecord> GetById(Guid id)
        {
            var expedientes = await _context.CAT_Expedientes.FindAsync(id);

            return expedientes;
        }

        public async Task Create(MedicalRecord.Domain.MedicalRecord.MedicalRecord expediente)
        {
            _context.CAT_Expedientes.Add(expediente);

            await _context.SaveChangesAsync();
        }

        public async Task Update(MedicalRecord.Domain.MedicalRecord.MedicalRecord expediente)
        {
            _context.CAT_Expedientes.Update(expediente);

            await _context.SaveChangesAsync();
        }
    }
}
