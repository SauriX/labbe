﻿using Service.MedicalRecord.Domain.MedicalRecord;
using Service.MedicalRecord.Domain.TaxData;
using Service.MedicalRecord.Dtos;
using Service.MedicalRecord.Dtos.General;
using Service.MedicalRecord.Dtos.MedicalRecords;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Repository.IRepository
{
    public interface IMedicalRecordRepository
    {
        Task<List<Domain.MedicalRecord.MedicalRecord>> GetAll();
        Task<List<Domain.MedicalRecord.MedicalRecord>> GetNow(GeneralFilterDto search);
        Task<List<Domain.MedicalRecord.MedicalRecord>> GetActive();
        Task<List<TaxData>> GetTaxData(Guid recordId);
        Task<Domain.MedicalRecord.MedicalRecord> Find(Guid id);
        Task<Domain.MedicalRecord.MedicalRecord> GetById(Guid id);
        Task<TaxData> GetTaxDataById(Guid id, Guid recordId);
        Task<TaxData> GetTaxDataoOnlyById(Guid id);
        Task Create(Domain.MedicalRecord.MedicalRecord expediente, IEnumerable<TaxDataDto> taxdata);
        Task CreateTaxData(TaxData taxData, MedicalRecordTaxData recordTaxData);
        Task Update(Domain.MedicalRecord.MedicalRecord expediente, IEnumerable<TaxDataDto> taxdata);
        Task UpdateWallet(Domain.MedicalRecord.MedicalRecord expediente);
        Task UpdateObservation(Domain.MedicalRecord.MedicalRecord expediente);
        Task UpdateTaxData(TaxData taxData);
        Task<string> GetLastCode(Guid branchId, string date);
        Task<List<Domain.MedicalRecord.MedicalRecord>> Coincidencias(MedicalRecord.Domain.MedicalRecord.MedicalRecord expediente);
        Task<List<Domain.MedicalRecord.MedicalRecord>> GetRecordsByIds(List<Guid> records);
    }
}
