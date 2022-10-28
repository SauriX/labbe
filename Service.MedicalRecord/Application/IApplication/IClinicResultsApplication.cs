﻿using Service.MedicalRecord.Domain;
using Service.MedicalRecord.Domain.Request;
using Service.MedicalRecord.Dtos;
using Service.MedicalRecord.Dtos.ClinicResults;
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
        Task<List<ClinicResultsDto>> GetAll(ClinicResultSearchDto search);
        Task<(byte[] file, string fileName)> ExportList(ClinicResultSearchDto search);
        Task SaveLabResults(List<ClinicResultsFormDto> results);
        Task UpdateLabResults(List<ClinicResultsFormDto> results);
        Task SaveResultPathologicalStudy(ClinicalResultPathologicalFormDto search);
        Task UpdateResultPathologicalStudy(ClinicalResultPathologicalFormDto search);
        Task UpdateStatusStudy(int RequestStudyId, byte status, string usuario);
        Task<ClinicalResultsPathological> GetResultPathological(int RequestStudyId);
        /*Task<ClinicResults> GetLaboratoryResults(int RequestStudyId);*/
        Task<List<ClinicResultsFormDto>> GetLabResultsById(string id);
        Task<RequestStudy> GetRequestStudyById(int RequestStudyId);
        Task<RequestStudyUpdateDto> GetStudies(Guid recordId, Guid requestId);
        Task<byte[]> PrintSelectedStudies(ConfigurationToPrintStudies configuration);
    }
}
