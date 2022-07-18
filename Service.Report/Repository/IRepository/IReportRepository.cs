using Service.Report.Domain.Request;
using Service.Report.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Repository.IRepository
{
    public interface IReportRepository<T> where T : Request
    {
        Task<List<Request>> GetAll();
        Task<List<Request>> GetFilter(ReportSearchDto search);
    }
}
