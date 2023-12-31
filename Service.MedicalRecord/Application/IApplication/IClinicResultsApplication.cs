﻿using Service.MedicalRecord.Domain;
using Service.MedicalRecord.Domain.Request;
using Service.MedicalRecord.Dtos;
using Service.MedicalRecord.Dtos.Catalogs;
using Service.MedicalRecord.Dtos.ClinicResults;
using Service.MedicalRecord.Dtos.General;
using Service.MedicalRecord.Dtos.MassSearch;
using Service.MedicalRecord.Dtos.Request;
using Service.MedicalRecord.Dtos.RequestedStudy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Application.IApplication
{
    public interface IClinicResultsApplication
    {
        Task<List<ClinicResultsDto>> GetAll(GeneralFilterDto search);
        Task<(byte[] file, string fileName)> ExportList(GeneralFilterDto search);
        Task<(byte[] file, string fileName)> ExportGlucoseChart(ClinicResultsFormDto result);
        Task SaveLabResults(List<ClinicResultsFormDto> results);
        Task UpdateLabResults(List<ClinicResultsFormDto> results, bool EnvioManual);
        Task SaveResultPathologicalStudy(ClinicalResultPathologicalFormDto search);
        Task UpdateResultPathologicalStudy(ClinicalResultPathologicalFormDto search, bool EnvioManual);
        Task<bool> SendResultFile(DeliverResultsStudiesDto estudios);
        Task UpdateStatusStudy(int RequestStudyId, byte status, string idUsuario);
        Task<ClinicResultsPathologicalInfoDto> GetResultPathological(int RequestStudyId);
        Task<List<ClinicResultsFormDto>> GetLabResultsById(string id);
        Task<RequestStudy> GetRequestStudyById(int RequestStudyId);
        Task<RequestStudyUpdateDto> GetStudies(Guid recordId, Guid requestId);
        Task<byte[]> PrintSelectedStudies(ConfigurationToPrintStudies configuration);
        Task<List<ParameterValueDto>> ReferencesValues(List<int> studies);
        Task<List<DeliveryHistoryDto>> GetDeliveryHistoryByRequestId(Guid Id);
        Task<List<DeliveryHistoryDto>> CreateNoteHistoryRecord(HistoryRecordInfo record);
    }
}
