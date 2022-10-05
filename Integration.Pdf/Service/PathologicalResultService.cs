using Integration.Pdf.Dtos;
using Integration.Pdf.Extensions;
using Integration.Pdf.Models;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Integration.Pdf.Service
{
    public class PathologicalResultService
    {
        public static byte[] GeneratePathologicalResultPdf(PathologicalResultsDto results)
        {
            Document document = CreateDocumentPathological(results);

            document.UseCmykColor = true;
            const bool unicode = false;

            DocumentRenderer renderer = new DocumentRenderer(document);
            renderer.PrepareDocument();

            PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(unicode)
            {
                Document = document,
            };

            pdfRenderer.RenderDocument();

            byte[] buffer;

            using(MemoryStream ms = new MemoryStream())
            {
                pdfRenderer.PdfDocument.Save(ms, false);
                buffer = new byte[ms.Length];
                ms.Seek(0, SeekOrigin.Begin);
                ms.Flush();
                ms.Read(buffer, 0, (int)ms.Length);
            }

            return buffer;
        }
        static Document CreateDocumentPathological(PathologicalResultsDto results)
        {
            Document document = new Document();

            Section section = document.AddSection();

            section.PageSetup = document.DefaultPageSetup.Clone();

            section.PageSetup.Orientation = Orientation.Portrait;
            section.PageSetup.PageFormat = PageFormat.A4;

            section.PageSetup.TopMargin = Unit.FromCentimeter(5);
            section.PageSetup.BottomMargin = Unit.FromCentimeter(1);
            section.PageSetup.LeftMargin = Unit.FromCentimeter(1);
            section.PageSetup.RightMargin = Unit.FromCentimeter(1);

            FormatPathological(section, results);

            return document;
        }
        static void FormatPathological(Section section, PathologicalResultsDto result)
        {
            var firma = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets\\firmaEjemplo.png");
            var firmaImage = File.ReadAllBytes(firma);

            for(int i = 0; i < result.Information.Count; i++)
            {

                if (result.ImprimrLogos)
                {
                    var logoLab = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets\\LabRamosLogo.png");

                    var LabRamosImage = File.ReadAllBytes(logoLab);

                    var headerParagraph = "DEPARTAMENTO DE ANATOMIA PATOLOGICA\n" +
                        "DR. PEDRO RAMOS SALAZAR\n" +
                        "ANATOMIA PATOLOGICA CENTRO MEDICO NACIONAL \"LA RAZA\"\n" +
                        "CITOPATOLOGIA HOSPITAL GENERAL MANUEL GEA GONZALEZ\n" +
                        "CERTIFICADO POR EL CONSEJO MEXICANO DE ANATOMOPATOLOGOS\n" +
                        "CED.D.G.P 2079985 S.S.A.  7488/99\n" +
                        "SINALOA 144 SUR, CD. OBREGÓN, SONORA, MEXICO.\n" +
                        "TEL  (01-644)414-08-41 Y  415-31-39 FAX: (01-644)414-08-42\n" +
                        "CD. OBREGÓN, SONORA.\n" +
                        "www.laboratorioramos.com.mx";
                    //var headerUrl = new Col("www.laboratorioramos.com.mx", new Font("Calibri", 11) { Bold = true }, ParagraphAlignment.Center);
                    var header = section.Headers.Primary;
                    var headerInfo = new Col[]
                    {
                        new Col(LabRamosImage),
                        new Col(headerParagraph),
                
                    };
                    header.AddText(headerInfo);

                }

                section.AddSpace();
                var line1 = new Col[]
                {
                    new Col("Médico: ", 3, ParagraphAlignment.Left),
                    new Col($"{result.Information[i].Medico}", 21, Col.FONT_BOLD, ParagraphAlignment.Left),
                    new Col("Fecha de entrega: ", 3, ParagraphAlignment.Left),
                    new Col($"{result.Information[i].FechaEntrega}", 21, Col.FONT_BOLD, ParagraphAlignment.Left)
                };
                section.AddBorderedText(line1, top: true, right: true, left: true);

                var line2 = new Col[]
                {
                    new Col("Paciente: ", 3, ParagraphAlignment.Left),
                    new Col($"{result.Information[i].Paciente}", 8, Col.FONT_BOLD, ParagraphAlignment.Left),
                    new Col("Edad: ", 1, ParagraphAlignment.Left),
                    new Col($"{result.Information[i].Edad}", 3, Col.FONT_BOLD, ParagraphAlignment.Left)
                };
                section.AddBorderedText(line2, right: true, left: true);

                var line3 = new Col[]
                {
                    new Col("Estudio: ", 3, ParagraphAlignment.Left),
                    new Col($"{result.Information[i].Estudio}", 8, Col.FONT_BOLD, ParagraphAlignment.Left)
                };

                section.AddBorderedText(line3, right: true, left: true);

                var line4 = new Col[]
                {
                    new Col($"{result.Information[i].Clave}", 8, Col.FONT_BOLD, ParagraphAlignment.Left)
                };

                section.AddBorderedText(line4, right: true, left: true);

                var line5 = new Col[]
                {
                    //new Col($"REPORTE DE ESTUDIO {result.Departamento == 30 ? 'HISTOPATOLÓGICO' : 'CITOLÓGICO'}", 10, Col.FONT_BOLD, ParagraphAlignment.Center)
                    new Col($"REPORTE DE ESTUDIO HISTOPATOLÓGICO", 10, Col.FONT_BOLD, ParagraphAlignment.Center)
                };

                section.AddBorderedText(line5, right: true, left: true);

                var line6 = new Col[]
                {
                    new Col($"Muestra recibida: {result.Information[i].MuestraRecibida}", 8, Col.FONT_BOLD, ParagraphAlignment.Left)
                };

                section.AddBorderedText(line6, right: true, left: true);

                var line7 = new Col[]
                {
                    new Col($"DESCRIPCIÓN MACROSCÓPICA", 10, Col.FONT_BOLD, ParagraphAlignment.Left)
                };

                section.AddBorderedText(line7, right: true, left: true);

                var line8 = new Col[]
                {
                    new Col($"{result.Information[i].DescripcionMacroscopica}", 8, Col.FONT_BOLD, ParagraphAlignment.Left)
                };

                section.AddBorderedText(line8, right: true, left: true);

                var line9 = new Col[]
                {
                    new Col($"DESCRIPCIÓN MICROSCÓPICA", 10, Col.FONT_BOLD, ParagraphAlignment.Left)
                };

                section.AddBorderedText(line9, right: true, left: true);

                var line10 = new Col[]
               {
                    new Col($"{result.Information[i].DescripcionMicroscopica}", 8, Col.FONT_BOLD, ParagraphAlignment.Left)
               };

                section.AddBorderedText(line10, right: true, left: true);

                var line11 = new Col[]
                {
                    new Col($"DIAGNÓSTICO", 10, Col.FONT_BOLD, ParagraphAlignment.Left)
                };

                section.AddBorderedText(line11, right: true, left: true);

                var line12 = new Col[]
                {
                        new Col($"{result.Information[i].Diagnostico}", 8, Col.FONT_BOLD, ParagraphAlignment.Left)
                };

                section.AddBorderedText(line12, right: true, left: true);

                var firmaLine = new Col[]
                {
                    new Col(firmaImage),

                };

                section.AddBorderedText(firmaLine, right: true, left: true);

                var line13 = new Col[]
                {
                    new Col($"ATENTAMENTE", 10, Col.FONT_BOLD, ParagraphAlignment.Center)
                };

                section.AddBorderedText(line13, right: true, left: true);

                var line14 = new Col[]
                {
                        new Col($"{result.Information[i].NombreFirma}", 8, Col.FONT_BOLD, ParagraphAlignment.Center)
                };

                section.AddBorderedText(line14, right: true, left: true);

                if (i < result.Information.Count - 1)
                {
                    section.AddPageBreak();
                }
                
            }
            

            




        }
    }
}