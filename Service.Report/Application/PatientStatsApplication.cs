using Microsoft.AspNetCore.Mvc;
using ClosedXML.Excel;
using ClosedXML.Report;
using Service.Report.Application.IApplication;
using Service.Report.Dictionary;
using Service.Report.Dtos.PatientStats;
using Service.Report.PdfModel;
using Service.Report.Repository.IRepository;
using Shared.Dictionary;
using Shared.Extensions;
using System;
using System.IO;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Net;
using Shared.Error;
using Shared.Helpers;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using Service.Report.Client.IClient;

namespace Service.Report.Application
{
    public class PatientStatsApplication : IPatientStatsApplication
    {
        public readonly IPatientStatsRepository _repository;
        private readonly IPdfClient _pdfClient;

        public PatientStatsApplication(IPatientStatsRepository repository, IPdfClient pdfClient)
        {
            _repository = repository;
            _pdfClient = pdfClient;
        }

        public async Task<IEnumerable<PatientStatsFiltroDto>> GetByName()
        {
            var req = await _repository.GetByName();
            var results = from c in req
                         group c by c.Expediente into grupo
                         select new PatientStatsFiltroDto
                         {
                             NombrePaciente = grupo.Key.Nombre,
                             Solicitudes = grupo.Count(),
                             Total = grupo.Sum(x => x.PrecioFinal),
                         };

            return results;
        }

        public async Task<IEnumerable<PatientStatsFiltroDto>> GetFilter(PatientStatsSearchDto search)
        {
            var req = await _repository.GetByName();
            var results = from c in req
                          group c by c.Expediente into grupo
                          select new PatientStatsFiltroDto
                          {
                              NombrePaciente = grupo.Key.Nombre,
                              Solicitudes = grupo.Count(),
                              Total = grupo.Sum(x => x.PrecioFinal),
                          };

            return results;
        }

        public async Task<(byte[] file, string fileName)> ExportTableStats(string search = null)
        {
            var indication = await GetByName();
            var path = Assets.PatientStatsTable;
            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Sucursales");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("Expediente", indication);

            template.Generate();

            var range = template.Workbook.Worksheet("Sucursales").Range("Sucursales");
            var table = template.Workbook.Worksheet("Sucursales").Range("$A$3:" + range.RangeAddress.LastAddress).CreateTable();
            table.Theme = XLTableTheme.TableStyleMedium2;

            template.Format();

            return (template.ToByteArray(), "Estadística de Pacientes.xlsx");
        }

        public async Task<(byte[] file, string fileName)> ExportChartStats(string search = null)
        {
            var indication = await GetByName();

            var path = Assets.PatientStatsChart;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Sucursales");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("Gráfica", indication);

            template.Generate();

            var range = template.Workbook.Worksheet("Sucursales").Range("Sucursales");
            var table = template.Workbook.Worksheet("Sucursales").Range("$A$3:" + range.RangeAddress.LastAddress).CreateTable();
            table.Theme = XLTableTheme.TableStyleMedium2;

            template.Format();

            return (template.ToByteArray(), "Estadística de Pacientes.xlsx");
        }

        public async Task<byte[]> GenerateReportPDF()
        {
            var requestData = await GetByName();

            List<Col> columns = new()
            {
                new Col("Nombre de Paciente", 3, ParagraphAlignment.Left),
                new Col("Solicitudes", ParagraphAlignment.Left),
                new Col("Total Sol.", ParagraphAlignment.Right ,"C"),
            };

            List<ChartSeries> series = new()
            {
                new ChartSeries("Nombre de Paciente", true),
                new ChartSeries("Solicitudes"),
                new ChartSeries("Total"),
            };

            var data = requestData.Select(x => new Dictionary<string, object>
            {
                { "Nombre de Paciente", x.NombrePaciente},
                { "Solicitudes", x.Solicitudes },
                { "Total", x.Total }
            }).ToList();

            var reportData = new ReportData()
            {
                Columnas = columns,
                Series = series,
                Datos = data
            };

            var file = await _pdfClient.GenerateReport(reportData);

            return file;

        }
    }
}
