using Integration.Pdf.Dtos;
using Integration.Pdf.Extensions;
using Integration.Pdf.Models;
using MigraDoc.DocumentObjectModel;
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

            section.PageSetup.TopMargin = Unit.FromCentimeter(1);
            section.PageSetup.BottomMargin = Unit.FromCentimeter(1);
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

            var headerParagraph = "ALFONSO RAMOS SALAZAR, QBP, MSC, DBC\nUNIVERSIDAD Y HOSPITAL GENERAL DE TORONTO\nCED. DGP No. 703973 REG. S.S.A. 10-86\nDGP F-370, No. REG. 0111\nwww.laboratorioramos.com.mx";

            var header = section.Headers.Primary;

            var headerInfo = new Col[]
            {
                new Col(LabRamosImage),
                new Col(headerParagraph),
                new Col(ISOImage)
                {
                    ImagenTamaño = Unit.FromCentimeter(4)
                },
            };
            
            header.AddText(headerInfo);

            var title = new Col("Laboratorio Alfonso Ramos S.A. de C.V. (HERMOSILLO)", new Font("Calibri", 11) { Bold = true }, ParagraphAlignment.Right);
            section.AddText(title);

            section.AddSpace();

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
                new Col($": {results.SolicitudInfo.FechaAdmision}", 9, Col.FONT_BOLD, ParagraphAlignment.Left),
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

            section.AddSpace(10);

            var studyHeader = new Col[]
            {
                new Col("EXAMEN", 17, Col.FONT_BOLD),
                new Col("RESULTADO", 4, Col.FONT_BOLD, ParagraphAlignment.Left),
                new Col("UNIDADES", 3, Col.FONT_BOLD),
                new Col("REFERENCIA", 3, Col.FONT_BOLD),
            };
            section.AddBorderedText(studyHeader, top: true, right: true, bottom: true, left: true);

            foreach (var param in results.CapturaResultados)
            {
                var col = new Col[]
                {
                    new Col(param.Nombre, 17),
                    new Col(param.Resultado, 4, ParagraphAlignment.Right),
                    new Col(param.TipoValor.ToString(), 3, ParagraphAlignment.Left),
                    new Col(param.ValorInicial + " - " + param.ValorFinal, 3, ParagraphAlignment.Right),
                };
                section.AddBorderedText(col, right: true, left: true);
            }

            var footer = section.Footers.Primary;

            var footerInfo = new Col[]
            {
                new Col(results.SolicitudInfo.FechaAdmision, 5, ParagraphAlignment.Left),
                new Col(results.SolicitudInfo.FechaAdmision, 5, ParagraphAlignment.Left),
            };

            footer.AddText(footerInfo);
            section.AddSpace(5);

            var footerBranches = new Col[]
            {
                new Col("")
            };
        }
    }
}