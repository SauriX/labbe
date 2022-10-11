using Integration.Pdf.Dtos;
using Integration.Pdf.Extensions;
using Integration.Pdf.Models;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using System;
using System.IO;
using System.Text;
namespace Integration.Pdf.Service
{
    public class LabResultsService
    {
        public static byte[] Generate(ClinicResultsPdfDto results)
        {
            Document document = CreateDocument(results);

            document.UseCmykColor = true;
            const bool unicode = false;

            DocumentRenderer renderer = new DocumentRenderer(document);
            renderer.PrepareDocument();

            PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(unicode)
            {
                Document = document
            };

            pdfRenderer.RenderDocument();

            byte[] buffer;

            using (MemoryStream ms = new MemoryStream())
            {
                pdfRenderer.PdfDocument.Save(ms, false);
                buffer = new byte[ms.Length];
                ms.Seek(0, SeekOrigin.Begin);
                ms.Flush();
                ms.Read(buffer, 0, (int)ms.Length);
            }

            return buffer;
        }

        static Document CreateDocument(ClinicResultsPdfDto results)
        {
            Document document = new Document();

            Section section = document.AddSection();

            section.PageSetup = document.DefaultPageSetup.Clone();

            section.PageSetup.Orientation = Orientation.Portrait;
            section.PageSetup.PageFormat = PageFormat.A4;

            section.PageSetup.TopMargin = Unit.FromCentimeter(5);
            section.PageSetup.BottomMargin = Unit.FromCentimeter(5);
            section.PageSetup.LeftMargin = Unit.FromCentimeter(1);
            section.PageSetup.RightMargin = Unit.FromCentimeter(1);

            Format(section, results);

            return document;
        }

        static void Format(Section section, ClinicResultsPdfDto results)
        {
            var logoLab = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets\\LabRamosLogo.png");
            var logoISO = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets\\ISOLogo.png");

            var LabRamosImage = File.ReadAllBytes(logoLab);
            var ISOImage = File.ReadAllBytes(logoISO);

            var headerParagraph = "ALFONSO RAMOS SALAZAR, QBP, MSC, DBC\nUNIVERSIDAD Y HOSPITAL GENERAL DE TORONTO\nCED. DGP No. 703973 REG. S.S.A. 10-86\nDGP F-370, No. REG. 0111";
            var headerUrl = new Col("www.laboratorioramos.com.mx", 4, new Font("Calibri", 8) { Bold = true }, ParagraphAlignment.Center);
            var header = section.Headers.Primary;

            if (results.ImprimrLogos)
            {
                var headerInfo = new Col[]
            {
                new Col(LabRamosImage, 6, ParagraphAlignment.Left)
                {
                    ImagenTamaño = Unit.FromCentimeter(6)
                },
                new Col(headerParagraph, 4, new Font("Calibri", 8), ParagraphAlignment.Center),
                new Col(ISOImage, 4, ParagraphAlignment.Right)
                {
                    ImagenTamaño = Unit.FromCentimeter(2)
                },
            };

                header.AddText(headerInfo);

                var headerURL = new Col[]
                {
                new Col("", 6),
                headerUrl,
                new Col("", 4),
                };

                header.AddText(headerURL);
            }

            section.AddSpace(2);

            var line1 = new Col[]
            {
                new Col("Doctor (a)", 3, ParagraphAlignment.Left),
                new Col($": {results.SolicitudInfo.Medico}", 21, Col.FONT_BOLD, ParagraphAlignment.Left),
                new Col("Expediente", 3, ParagraphAlignment.Left),
                new Col($": {results.SolicitudInfo.Clave}", 21, Col.FONT_BOLD, ParagraphAlignment.Left)
            };
            section.AddBorderedText(line1, top: true, right: true, left: true);

            var line2 = new Col[]
            {
                new Col("Paciente", 3, ParagraphAlignment.Left),
                new Col($": {results.SolicitudInfo.Paciente}", 8, Col.FONT_BOLD, ParagraphAlignment.Left),
                new Col("Edad", 1, ParagraphAlignment.Left),
                new Col($": {results.SolicitudInfo.Edad}", 3, Col.FONT_BOLD, ParagraphAlignment.Left)
            };
            section.AddBorderedText(line2, right: true, left: true);

            var line3 = new Col[]
            {
                new Col("Paciente Número", 3, ParagraphAlignment.Left),
                new Col($": {results.SolicitudInfo.Expediente}", 8, Col.FONT_BOLD, ParagraphAlignment.Left),
                new Col("", 1),
                new Col("Sexo", 3, ParagraphAlignment.Left),
                new Col($": {results.SolicitudInfo.Sexo}", 4, Col.FONT_BOLD, ParagraphAlignment.Left),
                new Col("", 1),
            };
            section.AddBorderedText(line3, right: true, left: true);

            var line4 = new Col[]
            {
                new Col("Fecha de Admisión", 3, ParagraphAlignment.Left),
                new Col($": {results.SolicitudInfo.FechaAdmision}", 8, Col.FONT_BOLD, ParagraphAlignment.Left),
                new Col("", 1),
                new Col("Fecha de Entrega", 3, ParagraphAlignment.Left),
                new Col($": {results.SolicitudInfo.FechaEntrega}", 9, Col.FONT_BOLD, ParagraphAlignment.Left),
            };
            section.AddBorderedText(line4, right: true, left: true);

            var line5 = new Col[]
            {
                new Col("Compañía", 3, ParagraphAlignment.Left),
                new Col($": {results.SolicitudInfo.Compañia}", 9, Col.FONT_BOLD, ParagraphAlignment.Left),
                new Col("Impreso a las", 3, ParagraphAlignment.Left),
                new Col($": {DateTime.Now.ToString("t")}", 8, Col.FONT_BOLD, ParagraphAlignment.Left),
                new Col("", 1),
            };
            section.AddBorderedText(line5, right: true, left: true);

            section.AddSpace(25);

            var studyHeader = new Col[]
            {
                new Col("EXAMEN", 17, Col.FONT_BOLD),
                new Col("RESULTADO", 4, Col.FONT_BOLD),
                new Col("UNIDADES", 3, Col.FONT_BOLD),
                new Col("REFERENCIA", 3, Col.FONT_BOLD),
            };
            section.AddBorderedText(studyHeader, top: true, right: false, bottom: true, left: false);

            foreach (var param in results.CapturaResultados)
            {
                var col = new Col[]
                {
                    new Col(param.Nombre, 17),
                    new Col(param.Resultado, 4, ParagraphAlignment.Right),
                    new Col(param.TipoValor.ToString(), 3, ParagraphAlignment.Left),
                    new Col(param.ValorInicial + " - " + param.ValorFinal, 3, ParagraphAlignment.Right),
                };
                section.AddBorderedText(col, top: false, right: false, bottom: true, left: false);
            }
            section.AddSpace(5);

            var footer = section.Footers.Primary;

            var footerToma = new Col("Toma de Muestra" + results.SolicitudInfo.FechaAdmision + " " + " " + results.SolicitudInfo.User, 5, ParagraphAlignment.Left);
            var footerLibera = new Col("Liberó: " + results.SolicitudInfo.FechaEntrega + " " + " " + results.SolicitudInfo.User, 5, ParagraphAlignment.Left);

            footer.AddText(footerToma);
            section.AddSpace(5);

            footer.AddText(footerLibera);
            section.AddSpace(5);

            var footerTitleBranches = new Col[]
            {
                new Col("CD. OBREGÓN, SON.", 4, new Font("Calibri", 11) { Bold = true },  ParagraphAlignment.Center),
                new Col("GUAYMAS, SON.", 1, new Font("Calibri", 11) { Bold = true },  ParagraphAlignment.Center),
                new Col("HERMOSILLO, SON.", 3, new Font("Calibri", 11) { Bold = true },  ParagraphAlignment.Center),
            };

            footer.AddText(footerTitleBranches);

            var footerBranchesUnits = new Col[]
            {
                new Col("UNIDAD CENTRO", 1,new Font("Calibri", 7) { Bold = true },  ParagraphAlignment.Center),
                new Col("UNIDAD 200", 1, new Font("Calibri", 7) { Bold = true },  ParagraphAlignment.Center),
                new Col("UNIDAD 300", 1, new Font("Calibri", 7) { Bold = true },  ParagraphAlignment.Center),
                new Col("CENTRO MÉDICO\nSUR SONORA", 1, new Font("Calibri", 7) { Bold = true },  ParagraphAlignment.Center),
                new Col("UNIDAD GUAYMAS", 1, new Font("Calibri", 7) { Bold = true },  ParagraphAlignment.Center),
                new Col("CENTRO MÉDICO\nDEL RÍO", 1, new Font("Calibri", 7) { Bold = true },  ParagraphAlignment.Center),
                new Col("MORELOS", 1, new Font("Calibri", 7) { Bold = true },  ParagraphAlignment.Center),
                new Col("NAVARRETE", 1, new Font("Calibri", 7) { Bold = true },  ParagraphAlignment.Center),
            };

            footer.AddText(footerBranchesUnits);

            var footerBranchesAddress = new Col[]
            {
                new Col("Sinaloa No. 144 Sur Col. Centro\nTels. (664) 415-16-92,\n414-08-41, 414-08-42, 415-31-39\ny 415-31-40", 1,new Font("Calibri", 6),  ParagraphAlignment.Center),
                new Col("Calle 200 Casi\nEsq. con Michoacán\nTels. (644)412-31-56\ny 416-14-07", 1, new Font("Calibri", 6),  ParagraphAlignment.Center),
                new Col("Jalisco No. 2250 Esq.\nCalle 300 Plaza Perisur\nCd. Obregón, Son.\nTel. (644) 444-66-69", 1, new Font("Calibri", 6),  ParagraphAlignment.Center),
                new Col("Calle Norte No. 749 Ote.\nEsq. con Sonora\nTel(644) 415-06-66", 1, new Font("Calibri", 6) ,  ParagraphAlignment.Center),
                new Col("Calzada Agustín García López\nLocal 6 esq. Paseo de las Villas\nFracc. Las Villas\nTel. (622) 221-9183", 1, new Font("Calibri", 6),  ParagraphAlignment.Center),
                new Col("Reforma No. 273 Sur\nTel. (662) 213-6866", 1, new Font("Calibri", 6),  ParagraphAlignment.Center),
                new Col("Blvd. Morelos No. 357\nCol. El Bachoco\nTels. (662) 267-8635 y 37", 1, new Font("Calibri", 6),  ParagraphAlignment.Center),
                new Col("Blvd. Navarrete No. 292\nCol. Raquet Club\nTel. (662) 216-3342", 1, new Font("Calibri", 6),  ParagraphAlignment.Center),
            };  

            footer.AddText(footerBranchesAddress);
        }
    }
}