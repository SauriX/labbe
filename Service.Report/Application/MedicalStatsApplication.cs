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
using Service.Report.Dtos.MedicalStats;
using Service.Report.Dtos;

namespace Service.Report.Application
{
    public class MedicalStatsApplication : IMedicalStatsApplication
    {
        public readonly IReportRepository _repository;
        private readonly IPdfClient _pdfClient;

        public MedicalStatsApplication(IReportRepository repository, IPdfClient pdfClient)
        {
            _repository = repository;
            _pdfClient = pdfClient;
        }

        public async Task<IEnumerable<MedicalStatsDto>> GetByDoctor ()
        {
            var req = await _repository.GetAll();
            var results = (from c in req
                           group c by new { c.Medico.NombreMedico, c.Medico.ClaveMedico, c.MedicoId, c.Fecha.Year, c.Fecha.Month } into grupo
                           select new MedicalStatsDto
                           {
                               ClaveMedico = grupo.Key.ClaveMedico,
                               NombreMedico = grupo.Key.NombreMedico,
                               Total = grupo.Sum(x => x.PrecioFinal),
                               Solicitudes = grupo.Count(),
                               Pacientes = grupo.Select(x => x.ExpedienteId).Distinct().Count(),
                           }).ToList();

            results.Add(new MedicalStatsDto
            {
                ClaveMedico = "Total",
                NombreMedico = " ",
                Total = results.Sum(x => x.Total),
                Solicitudes = results.Sum(x => x.Solicitudes),
                Pacientes = results.Sum(x => x.Pacientes),
            });

            return results;
        }

        public async Task<IEnumerable<MedicalStatsDto>> GetFilter(ReportFiltroDto search)
        {
            var req = await _repository.GetFilter(search);
            var results = (from c in req
                           group c by new { c.Medico.NombreMedico, c.Medico.ClaveMedico, c.MedicoId } into grupo
                           select new MedicalStatsDto
                           {
                               ClaveMedico = grupo.Key.ClaveMedico,
                               NombreMedico = grupo.Key.NombreMedico,
                               Total = grupo.Sum(x => x.PrecioFinal),
                               Solicitudes = grupo.Count(),
                               Pacientes = grupo.Select(x => x.ExpedienteId).Distinct().Count(),
                           }).ToList();

            results.Add(new MedicalStatsDto
            {
                ClaveMedico = "Total",
                NombreMedico = " ",
                Total = results.Sum(x => x.Total),
                Solicitudes = results.Sum(x => x.Solicitudes),
                Pacientes = results.Sum(x => x.Pacientes),
            });

            return results;
        }

        public async Task<byte[]> GenerateReportPDF(ReportFiltroDto search)
        {
            var requestData = await GetFilter(search);

            List<Col> columns = new()
            {
                new Col("Clave", ParagraphAlignment.Left),
                new Col("Nombre del Médico", ParagraphAlignment.Left),
                new Col("Importe", ParagraphAlignment.Right, "C"),
                new Col("Solicitudes", ParagraphAlignment.Right),
                new Col("Pacientes", ParagraphAlignment.Right),
            };

            List<ChartSeries> series = new()
            {
                new ChartSeries("Clave", true),
                new ChartSeries("Importe", null, "C"),
                new ChartSeries("Solicitudes", "#c4c4c4"),
                new ChartSeries("Pacientes", "#ea899a"),
            };

            var data = requestData.Select(x => new Dictionary<string, object>
            {
                { "Clave", x.ClaveMedico},
                { "Nombre del Médico", x.NombreMedico},
                { "Importe", x.Total },
                { "Solicitudes", x.Solicitudes },
                { "Pacientes", x.Pacientes },
            }).ToList();

            var headerData = new HeaderData()
            {
                NombreReporte = "Solicitudes por Médico Condensado",
                Sucursal = search.Sucursal,
                Fecha = $"{ search.Fecha.Min():dd/MM/yyyy} - {search.Fecha.Max().ToString("dd/MM/yyyy")}"
            };

            var reportData = new ReportData()
            {
                Columnas = columns,
                Series = series,
                Datos = data,
                Header = headerData,
            };


            var file = await _pdfClient.GenerateReport(reportData);

            return file;

        }
    }
}