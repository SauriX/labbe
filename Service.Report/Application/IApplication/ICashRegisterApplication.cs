using Service.Report.Dtos;
using Service.Report.Dtos.CashRegister;
using System.Threading.Tasks;

namespace Service.Report.Application.IApplication
{
    public interface ICashRegisterApplication
    {
        Task<CashDto> GetByFilter(ReportFilterDto search);
        Task<byte[]> DownloadReportPdf(ReportFilterDto search);
    }
}
