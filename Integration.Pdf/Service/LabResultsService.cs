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
        public static byte[] Generate(LabResultsDto results)
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

        static Document CreateDocument(LabResultsDto results)
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

        static void Format(Section section, LabResultsDto results)
        {
            var title = new Col("Laboratorio Alfonso Ramos S.A. de C.V. (HERMOSILLO)", new Font("Calibri", 11) { Bold = true }, ParagraphAlignment.Right);
            section.AddText(title);

            section.AddSpace();

            var line1 = new Col[]
            {
                new Col("Doctor (a)", 3, ParagraphAlignment.Left),
                new Col($": {results.NombreMedico}", 21, Col.FONT_BOLD, ParagraphAlignment.Left),
                new Col("Expediente", 3, ParagraphAlignment.Left),
                new Col($": {results.Solicitud}", 21, Col.FONT_BOLD, ParagraphAlignment.Left)
            };
            section.AddBorderedText(line1, top: true, right: true, left: true);

            var line2 = new Col[]
            {
                new Col("PACIENTE", 3, ParagraphAlignment.Left),
                new Col($": {results.Nombre}", 8, Col.FONT_BOLD, ParagraphAlignment.Left),
                new Col("EDAD", 1, ParagraphAlignment.Left),
                new Col($": {results.Edad}", 3, Col.FONT_BOLD, ParagraphAlignment.Left)
            };
            section.AddBorderedText(line2, right: true, left: true);

            var line3 = new Col[]
            {
                new Col("Paciente Número", 3, ParagraphAlignment.Left),
                new Col($": {results.Solicitud}", 8, Col.FONT_BOLD, ParagraphAlignment.Left),
                new Col("", 1),
                new Col("SEXO", 3, ParagraphAlignment.Left),
                new Col($": {results.Sexo}", 4, Col.FONT_BOLD, ParagraphAlignment.Left),
                new Col("", 1),
            };
            section.AddBorderedText(line3, right: true, left: true);

            var line4 = new Col[]
            {
                new Col("Fecha de Admisión", 3, ParagraphAlignment.Left),
                new Col($": {results.Registro}", 8, Col.FONT_BOLD, ParagraphAlignment.Left),
                new Col("", 1),
                new Col("Fecha de Entrega", 3, ParagraphAlignment.Left),
                new Col($": {results.Entrega}", 9, Col.FONT_BOLD, ParagraphAlignment.Left),
            };
            section.AddBorderedText(line4, right: true, left: true);

            var line5 = new Col[]
            {
                new Col("Compañía", 3, ParagraphAlignment.Left),
                new Col($": {results.Compañia}", 9, Col.FONT_BOLD, ParagraphAlignment.Left),
                new Col("Impreso a las", 3, ParagraphAlignment.Left),
                new Col($": {DateTime.Now.ToString("t")}", 8, Col.FONT_BOLD, ParagraphAlignment.Left),
                new Col("", 1),
            };
            section.AddBorderedText(line5, right: true, left: true);

            section.AddSpace(10);

            var studyHeader = new Col[]
            {
                new Col("EXAMEN", 3, Col.FONT_BOLD),
                new Col("RESULTADO", 18, Col.FONT_BOLD, ParagraphAlignment.Left),
                new Col("UNIDADES", 3, Col.FONT_BOLD),
                new Col("REFERENCIA", 3, Col.FONT_BOLD),
            };
            section.AddBorderedText(studyHeader, top: true, right: true, bottom: true, left: true);

            foreach (var study in results.Estudios)
            {
                var col = new Col[]
                {
                    new Col(study.Clave, 3),
                    new Col(study.Estudio, 18, ParagraphAlignment.Left),
                    new Col(study.Precio, 3, ParagraphAlignment.Right),
                    new Col(study.Precio, 3, ParagraphAlignment.Right),
                };
                section.AddBorderedText(col, right: true, left: true);
            }
        }
    }
}