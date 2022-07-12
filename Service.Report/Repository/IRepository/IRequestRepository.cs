using Service.Report.Domain.Request;
using Service.Report.Dtos.Request;
using Service.Report.PdfModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Report.Repository.IRepository
{
    public interface IRequestRepository
    {
        Task<List<Request>> GetRequestByCount();
        Task<List<Report.Domain.Request.Request>> GetFilter(RequestSearchDto search);
    }
}
