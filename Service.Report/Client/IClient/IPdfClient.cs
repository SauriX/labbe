﻿using Integration.Pdf.Models;
using Service.Report.PdfModel;
using System.Threading.Tasks;

namespace Service.Report.Client.IClient
{
    public interface IPdfClient
    {
        Task<byte[]> GenerateReport(ReportData reportData);
        Task<byte[]> CashRegisterReport(CashData cashData);
    }
}
