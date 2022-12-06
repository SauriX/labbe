using Service.Report.Dtos.MedicalRecord;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Report.Client.IClient
{
    public interface IMedicalRecordClient
    {
        Task<MedicalRecordDto> GetMedicalRecord(List<Guid> records);
    }
}
