using Service.MedicalRecord.Dtos.MedicalRecords;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Application.IApplication
{
    public interface IMedicalRecordApplication
    {
        Task<List<MedicalRecordsListDto>> GetAll();
        Task<List<MedicalRecordsListDto>> GetNow();
        Task<List<MedicalRecordsListDto>> GetActive();
        Task<MedicalRecordsFormDto> GetById(Guid id);
        Task<MedicalRecordsListDto> Create(MedicalRecordsFormDto expediente);
        Task<MedicalRecordsListDto> Update(MedicalRecordsFormDto expediente);
        Task<(byte[] file, string fileName)> ExportList(string search = null);
    }
}
