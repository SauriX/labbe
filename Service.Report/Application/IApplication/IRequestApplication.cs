﻿using Service.Report.Domain.Request;
using Service.Report.Dtos;
using Service.Report.Dtos.Request;
using Service.Report.PdfModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Report.Application.IApplication
{
    public interface IRequestApplication
    {
        Task<IEnumerable<RequestDto>> GetBranchByCount();
        Task<IEnumerable<RequestDto>> GetFilter(ReportFiltroDto search);
        Task<(byte[] file, string fileName)> ExportTableBranch(string search = null);
        Task<(byte[] file, string fileName)> ExportGraphicBranch(string search = null);
        Task<byte[]> GenerateReportPDF(ReportFiltroDto search);
    }
}
