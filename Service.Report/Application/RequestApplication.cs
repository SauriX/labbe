﻿using Microsoft.AspNetCore.Mvc;
using ClosedXML.Excel;
using ClosedXML.Report;
using Service.Report.Application.IApplication;
using Service.Report.Dictionary;
using Service.Report.Dtos.Request;
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
using Service.Report.Dtos;

namespace Service.Report.Application
{
    public class RequestApplication : IRequestApplication
    {
        public readonly IReportRepository _repository;
        private readonly IPdfClient _pdfClient;

        public RequestApplication(IReportRepository repository, IPdfClient pdfClient)
        {
            _repository = repository;
            _pdfClient = pdfClient;
        }

        public async Task<IEnumerable<RequestDto>> GetBranchByCount()
        {
            var req = await _repository.GetAll();
            var results = from c in req
                          group c by new { c.Expediente.Nombre, c.Expediente.Expediente } into grupo
                          select new RequestDto
                          {
                              Visitas = grupo.Count(),
                              PacienteNombre = grupo.Key.Nombre,
                              ExpedienteNombre = grupo.Key.Expediente
                          };


            return results;
        }

        public async Task<IEnumerable<RequestDto>> GetFilter(ReportFiltroDto search)
        {
            var req = await _repository.GetFilter(search);
            var results = from c in req
                          group c by new { c.Expediente.Nombre, c.Expediente.Expediente } into grupo
                          select new RequestDto
                          {
                              Visitas = grupo.Count(),
                              PacienteNombre = grupo.Key.Nombre,
                              ExpedienteNombre = grupo.Key.Expediente
                          };

            return results;
        }

        public async Task<(byte[] file, string fileName)> ExportTableBranch(string search = null)
        {
            var indication = await GetBranchByCount();

            var path = Assets.ReportTable;

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

            return (template.ToByteArray(), "Estadística de expedientes.xlsx");
        }

        public async Task<(byte[] file, string fileName)> ExportGraphicBranch(string search = null)
        {
            var indication = await GetBranchByCount();

            var path = Assets.ReportGraphic;

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

            return (template.ToByteArray(), "Estadística de Expedientes.xlsx");
        }

        public async Task<byte[]> GenerateReportPDF(ReportFiltroDto search)
        {
            var requestsData = await GetFilter(search);

            List<Col> columns = new()
            {
                new Col("Clave", ParagraphAlignment.Left),
                new Col("Paciente", ParagraphAlignment.Left),
                new Col("Visitas"),
               
            };

            List<ChartSeries> series = new()
            {
                new ChartSeries("Clave", true),
                new ChartSeries("Visitas"),
            };

            var data = requestsData.Select(x => new Dictionary<string, object>
            {
                { "Clave", x.ExpedienteNombre },
                { "Visitas", x.Visitas },
                {"Paciente", x.PacienteNombre }
            }).ToList();

            var headerData = new HeaderData()
            {
                NombreReporte = "Estadística de Solicitudes por Paciente",
                Sucursal = search.Sucursal,
                Fecha = $"{ search.Fecha.Min():dd/MM/yyyy} - {search.Fecha.Max().ToString("dd/MM/yyyy")}"
            };

            var reportData = new ReportData()
            {
                Columnas = columns,
                Series = series,
                Datos = data,
                Header = headerData,
                //ColumnaFinal = column,
            };
            var file = await _pdfClient.GenerateReport(reportData);

            return file;
        }

    }
}
