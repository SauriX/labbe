using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Repository.IRepository
{
    public interface IMedicalRecordRepository
    {
        Task<List<MedicalRecord.Domain.MedicalRecord.MedicalRecord>> GetAll();
        Task<List<MedicalRecord.Domain.MedicalRecord.MedicalRecord>> GetNow();
        Task<List<MedicalRecord.Domain.MedicalRecord.MedicalRecord>> GetActive();
        Task<MedicalRecord.Domain.MedicalRecord.MedicalRecord> GetById(Guid id);
        Task Create(MedicalRecord.Domain.MedicalRecord.MedicalRecord expediente);
        Task Update(MedicalRecord.Domain.MedicalRecord.MedicalRecord expediente);
    }
}
