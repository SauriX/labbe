using Service.MedicalRecord.Dtos;
using Service.MedicalRecord.Dtos.General;
using Service.MedicalRecord.Dtos.MedicalRecords;
using Service.MedicalRecord.Dtos.Reports;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Application.IApplication
{
    public interface IMedicalRecordApplication
    {
        Task<List<MedicalRecordsListDto>> GetAll();
        Task<List<MedicalRecordsListDto>> GetNow(GeneralFilterDto search);
        Task<List<MedicalRecordDto>> GetMedicalRecord(List<Guid> records);
        Task<List<MedicalRecordsListDto>> GetActive();
        Task<List<TaxDataDto>> GetTaxData(Guid recordId);
        Task UpdateDefaultTaxData(Guid id);
        Task<MedicalRecordsFormDto> GetById(Guid id);
        Task<MedicalRecordsListDto> Create(MedicalRecordsFormDto expediente);
        Task<string> CreateTaxData(TaxDataDto taxData);
        Task<MedicalRecordsListDto> Update(MedicalRecordsFormDto expediente);
        Task UpdateObservation(MedicalRecordObservationsDto expediente);
        Task UpdateTaxData(TaxDataDto taxData);
        Task<List<MedicalRecordsListDto>> Coincidencias(MedicalRecordsFormDto expediente);
        Task<(byte[] file, string fileName)> ExportList(GeneralFilterDto search = null);

        Task<(byte[] file, string fileName)> ExportForm(Guid id);
        Task<bool> UpdateWallet(ExpedienteMonederoDto monedero);
        

    }
}
