using Service.Report.Domain.Request;
using Service.Report.Dtos.MedicalStats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Repository.IRepository
{
    public interface IMedicalStatsRepository
    {
        Task<List<Request>> GetByDoctor();
        Task<List<Report.Domain.Request.Request>> GetFilter(MedicalStatsSearchDto search);
    }
}
