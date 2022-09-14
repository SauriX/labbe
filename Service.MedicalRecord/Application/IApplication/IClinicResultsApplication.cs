using Service.MedicalRecord.Dtos;
using Service.MedicalRecord.Dtos.RequestedStudy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Application.IApplication
{
    public interface IClinicResultsApplication
    {
        Task<List<ClinicResultsDto>> GetAll(RequestedStudySearchDto search);
        Task<(byte[] file, string fileName)> ExportList(RequestedStudySearchDto search);
    }
}
