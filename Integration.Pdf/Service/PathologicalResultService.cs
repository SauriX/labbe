using Integration.Pdf.Dtos;
using Integration.Pdf.Extensions;
using Integration.Pdf.Models;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Integration.Pdf.Service
{
    public class PathologicalResultService
    {
        public static async Task<byte[]> GeneratePathologicalResultPdf(PathologicalResultsDto results)
        {
            Document document = await CreateDocumentPathological(results);

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
        static async Task<Document> CreateDocumentPathological(PathologicalResultsDto results)
        {
            Document document = new Document();

            Section section = document.AddSection();

            section.PageSetup = document.DefaultPageSetup.Clone();

            section.PageSetup.Orientation = Orientation.Portrait;
            section.PageSetup.PageFormat = PageFormat.A4;
            if (results.ImprimrLogos)
            {
                section.PageSetup.TopMargin = Unit.FromCentimeter(6);

            }
            else
            {
                section.PageSetup.TopMargin = Unit.FromCentimeter(3);
            }
            section.PageSetup.BottomMargin = Unit.FromCentimeter(1);
            section.PageSetup.LeftMargin = Unit.FromCentimeter(1);
            section.PageSetup.RightMargin = Unit.FromCentimeter(1);

            await FormatPathological(section, results);

            return document;
        }
        static async Task FormatPathological(Section section, PathologicalResultsDto result)
        {
            var firma = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets\\firmaEjemplo.png");
            var firmaImage = File.ReadAllBytes(firma);
            string nombreMedico = string.Empty;
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

            for (int i = 0; i < result.Information.Count; i++)
            {

                

                section.AddSpace();
                var line1 = new Col[]
                {
                    new Col("Médico: ", 3, ParagraphAlignment.Left),
                    new Col($"{result.Information[i].Medico}", 19, Col.FONT_BOLD, ParagraphAlignment.Left),
                    new Col("Fecha de entrega: ", 7, ParagraphAlignment.Left),
                    new Col($"{result.Information[i].FechaEntrega.Trim()}", 5,Col.FONT_BOLD, ParagraphAlignment.Left)
                };
                section.AddText(line1, false, 13);

                var line2 = new Col[]
                {
                    new Col("Paciente: ", 3, ParagraphAlignment.Left),
                    new Col($"{result.Information[i].Paciente.Trim()}", 18, Col.FONT_BOLD, ParagraphAlignment.Left),
                    new Col("Edad: ", 3, ParagraphAlignment.Left),
                    new Col($"{result.Information[i].Edad} años", 3, Col.FONT_BOLD, ParagraphAlignment.Left)
                };
                section.AddText(line2, false, 13);

                var line3 = new Col[]
                {
                    new Col("Estudio: ", 3, ParagraphAlignment.Left),
                    new Col($"{result.Information[i].Estudio.Trim()}", 21, Col.FONT_BOLD, ParagraphAlignment.Left)
                };

                section.AddText(line3, false, 13);

                var line4 = new Col[]
                {
                    new Col($"{result.Information[i].Clave}", 20, Col.FONT_BOLD, ParagraphAlignment.Left)
                };

                section.AddText(line4, false, 13);

                string studyType = result.Information[i].isHistopathologic
                    ? "REPORTE DE ESTUDIO HISTOPATOLÓGICO"
                    : "REPORTE DE ESTUDIO CITOLÓGICO";

                var line5 = new Col[]
                {
                    
                    new Col(studyType, 25, Col.FONT_BOLD, ParagraphAlignment.Center)
                };

                section.AddText(line5, false, 13);

                var line6 = new Col[]
                {
                    new Col($"Muestra recibida: {result.Information[i].MuestraRecibida}", 20, Col.FONT_BOLD, ParagraphAlignment.Left)
                };

                section.AddText(line6, false, 13);

                var line7 = new Col[]
                {
                    new Col($"DESCRIPCIÓN MACROSCÓPICA", 25, Col.FONT_BOLD, ParagraphAlignment.Left)
                };

                section.AddText(line7, false, 13);

                var line8 = new Col[]
                {
                    new Col($"{result.Information[i].DescripcionMacroscopica}", 8, Col.FONT_BOLD, ParagraphAlignment.Left)
                };

                section.AddRichTextFormat(line8);

                var line9 = new Col[]
                {
                    new Col($"DESCRIPCIÓN MICROSCÓPICA", 25, Col.FONT_BOLD, ParagraphAlignment.Left)
                };

                section.AddText(line9, false, 13);

                var line10 = new Col[]
               {
                    new Col($"{result.Information[i].DescripcionMicroscopica}", 8, Col.FONT_BOLD, ParagraphAlignment.Left)
               };

                section.AddRichTextFormat(line10);
                if (result.Information[i].isHistopathologic && result.Information[i].ImagenesHistopatologicas.Count > 0)
                {

                    var cliente = new HttpClient();

                    var histoImageLine = new List<Col> { };

                    var tasks = result.Information[i].ImagenesHistopatologicas.Select(imagePromise => cliente.GetByteArrayAsync(imagePromise));
                
                    var resultsTask = await Task.WhenAll(tasks);

                    foreach (var task in resultsTask)
                    {
                        histoImageLine.Add(new Col(task)
                        {
                            ImagenTamaño = Unit.FromCentimeter(4)
                        });
                    }

                    section.AddText(histoImageLine.ToArray());

                }


                var line11 = new Col[]
                {
                    new Col($"DIAGNÓSTICO", 25, Col.FONT_BOLD, ParagraphAlignment.Left)
                };

                section.AddText(line11, false, 13);

                var line12 = new Col[]
                {
                        new Col($"{result.Information[i].Diagnostico}", 8, Col.FONT_BOLD, ParagraphAlignment.Left)
                };

                //section.AddText(line12);
                section.AddRichTextFormat(line12);

                nombreMedico = result.Information[i].NombreFirma;
                var firmaLine = new Col[]
               {
                    new Col(firmaImage)
                    {
                        ImagenTamaño = Unit.FromCentimeter(5)
                    },

               };

                section.AddText(firmaLine);

                var line13 = new Col[]
                {
                    new Col($"ATENTAMENTE", 10, Col.FONT_BOLD, ParagraphAlignment.Center)
                };

                section.AddText(line13, false, 13);

                var line14 = new Col[]
                {
                        new Col($"{result.Information[i].NombreFirma}", 8, Col.FONT_BOLD, ParagraphAlignment.Center)
                };

                section.AddText(line14, false, 11);

                if (i < result.Information.Count - 1)
                {
                    section.AddPageBreak();
                }


            }
           

            








        }
    }
}