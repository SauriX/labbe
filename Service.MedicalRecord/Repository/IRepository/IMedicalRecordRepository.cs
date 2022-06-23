using Service.MedicalRecord.Dtos;
using Service.MedicalRecord.Dtos.MedicalRecords;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Repository.IRepository
{
    public interface IMedicalRecordRepository
    {
        Task<List<MedicalRecord.Domain.MedicalRecord.MedicalRecord>> GetAll();
        Task<List<MedicalRecord.Domain.MedicalRecord.MedicalRecord>> GetNow(MedicalRecordSearch search);
        Task<List<MedicalRecord.Domain.MedicalRecord.MedicalRecord>> GetActive();
        Task<MedicalRecord.Domain.MedicalRecord.MedicalRecord> GetById(Guid id);
        Task Create(MedicalRecord.Domain.MedicalRecord.MedicalRecord expediente, IEnumerable<TaxDataDto> taxdata);
        Task Update(MedicalRecord.Domain.MedicalRecord.MedicalRecord expediente, IEnumerable<TaxDataDto> taxdata);
        Task<List<MedicalRecord.Domain.MedicalRecord.MedicalRecord>> Coincidencias(MedicalRecord.Domain.MedicalRecord.MedicalRecord expediente);
    }
}
