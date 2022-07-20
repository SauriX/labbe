using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Report.Client.IClient;
using Service.Report.Dtos.Request;
using Service.Report.PdfModel;
using Shared.Dictionary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IPdfClient _pdfClient;

        public TestController(IPdfClient pdfClient)
        {
            _pdfClient = pdfClient;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Test()
        {
            List<ChartSeries> series = new(){
               new ChartSeries("Clave", true),
               new ChartSeries("Visitas"),
            };

            List<Col> columns = new(){
               new Col("Clave", 5, ParagraphAlignment.Left),
               new Col("Visitas"),
            };

            var list = new List<RequestDto>()
            {
                new RequestDto()
                {
                    ExpedienteNombre = "Clave 1",
                    Visitas = 5,
                },
                new RequestDto()
                {
                    ExpedienteNombre= "Clave 2",
                    Visitas = 7,
                }
            };

            var data = list.Select(x => new Dictionary<string, object>
            {
                { "Clave", x.ExpedienteNombre },
                { "Visitas", x.Visitas }
            }).ToList();

            //List<ChartSeries> series = new List<ChartSeries>(){
            //   new ChartSeries("Expediente", true),
            //   new ChartSeries("Precio", "#00ff00", "C"),
            //   new ChartSeries("Cantidad", "#ff0000"),
            //   new ChartSeries("Edad", "#0000ff"),
            //};

            //List<Col> columns = new List<Col>(){
            //   new Col("Expediente", 3, ParagraphAlignment.Left),
            //   new Col("Precio", ParagraphAlignment.Right, "C"),
            //   new Col("Cantidad", ParagraphAlignment.Right),
            //   new Col("Total", ParagraphAlignment.Right, "C"),
            //};

            //List<Dictionary<string, object>> data = new List<Dictionary<string, object>>()
            //{
            //    new Dictionary<string, object>{
            //        { "Expediente", "Miguel" },
            //        { "Precio", 54 },
            //        { "Cantidad", 4 },
            //        { "Total", 23523 },
            //        { "Edad", 10 }
            //    },
            //    new Dictionary<string, object>{
            //        { "Expediente", "Alejandro" },
            //        { "Precio", 344 },
            //        { "Cantidad", 5 },
            //        { "Total", 34534 },
            //        { "Edad", 15 }
            //    },
            //};

            var reportData = new ReportData()
            {
                Columnas = columns,
                Series = series,
                Datos = data
            };

            var file = await _pdfClient.GenerateReport(reportData);

            return File(file, MimeType.PDF, "reporte.pdf");
        }
    }
}