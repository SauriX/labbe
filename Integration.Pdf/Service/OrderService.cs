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
    public class OrderService
    {
        public static byte[] Generate(RequestOrderDto order)
        {
            Document document = CreateDocument(order);

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

        static Document CreateDocument(RequestOrderDto order)
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

            Format(section, order);

            return document;
        }

        static void Format(Section section, RequestOrderDto order)
        {
            var title = new Col("Laboratorio Alfonso Ramos S.A. de C.V. (HERMOSILLO)", new Font("Calibri", 11) { Bold = true }, ParagraphAlignment.Right);
            section.AddText(title);

            section.AddSpace();

            var line1 = new Col[]
            {
                new Col("SOLICITUD NO.", 3, ParagraphAlignment.Left),
                new Col($": {order.Clave}", 21, Col.FONT_BOLD, ParagraphAlignment.Left)
            };
            section.AddBorderedText(line1, top: true, right: true, left: true);

            var line2 = new Col[]
            {
                new Col("FECHA", 3, ParagraphAlignment.Left),
                new Col($": {order.FechaSolicitud}", 8, Col.FONT_BOLD, ParagraphAlignment.Left),
                new Col("", 1),
                new Col("FECH. NAC.", 3, ParagraphAlignment.Left),
                new Col($": {order.FechaNacimiento}", 4, Col.FONT_BOLD, ParagraphAlignment.Left),
                new Col("", 1),
                new Col("EDAD", 1, ParagraphAlignment.Left),
                new Col($": {order.Edad}", 3, Col.FONT_BOLD, ParagraphAlignment.Left)
            };
            section.AddBorderedText(line2, right: true, left: true);

            var line3 = new Col[]
            {
                new Col("PACIENTE", 3, ParagraphAlignment.Left),
                new Col($": {order.Paciente}", 8, Col.FONT_BOLD, ParagraphAlignment.Left),
                new Col("", 1),
                new Col("SEXO", 3, ParagraphAlignment.Left),
                new Col($": {order.Sexo}", 4, Col.FONT_BOLD, ParagraphAlignment.Left),
                new Col("", 1),
                new Col("TEL", 1, ParagraphAlignment.Left),
                new Col($": {order.TelefonoPaciente}", 3, Col.FONT_BOLD, ParagraphAlignment.Left)
            };
            section.AddBorderedText(line3, right: true, left: true);

            var line4 = new Col[]
            {
                new Col("MEDICO", 3, ParagraphAlignment.Left),
                new Col($": {order.Medico}", 8, Col.FONT_BOLD, ParagraphAlignment.Left),
                new Col("", 1),
                new Col("TEL", 3, ParagraphAlignment.Left),
                new Col($": {order.TelefonoMedico}", 9, Col.FONT_BOLD, ParagraphAlignment.Left),
            };
            section.AddBorderedText(line4, right: true, left: true);

            var line5 = new Col[]
            {
                new Col("SUCURSAL", 3, ParagraphAlignment.Left),
                new Col($": {order.Sucursal}", 8, Col.FONT_BOLD, ParagraphAlignment.Left),
                new Col("", 1),
                new Col("COMPAÑIA", 3, ParagraphAlignment.Left),
                new Col($": {order.Compañia}", 9, Col.FONT_BOLD, ParagraphAlignment.Left),
            };
            section.AddBorderedText(line5, right: true, left: true);

            var line6 = new Col[]
            {
                new Col("E-MAIL", 3, ParagraphAlignment.Left),
                new Col($": {order.Correo}", 8, Col.FONT_BOLD, ParagraphAlignment.Left),
                new Col("", 1),
                new Col($"{order.EnvioPaciente}", 12, ParagraphAlignment.Left),
            };
            section.AddBorderedText(line6, right: true, left: true);

            var line7 = new Col[]
            {
                new Col("OBS", 3, ParagraphAlignment.Left),
                new Col($": {order.Observaciones}", 21, Col.FONT_BOLD, ParagraphAlignment.Left),
            };
            section.AddBorderedText(line7, right: true, left: true, bottom: true);

            section.AddSpace(10);

            var studyHeader = new Col[]
            {
                new Col("CLAVE", 3, Col.FONT_BOLD),
                new Col("ESTUDIO", 18, Col.FONT_BOLD, ParagraphAlignment.Left),
                new Col("PRECIO", 3, Col.FONT_BOLD),
            };
            section.AddBorderedText(studyHeader, top: true, right: true, bottom: true, left: true);

            foreach (var study in order.Estudios)
            {
                var col = new Col[]
                {
                    new Col(study.Clave, 3),
                    new Col(study.Estudio, 18, ParagraphAlignment.Left),
                    new Col(study.Precio, 3, ParagraphAlignment.Right),
                };
                section.AddBorderedText(col, right: true, left: true);
            }

            var hr = new StringBuilder(125).Insert(0, "-", 125).ToString();
            var s4 = new Col[]
            {
                new Col("", 3),
                new Col(hr, 18, ParagraphAlignment.Left),
                new Col("", 3, ParagraphAlignment.Right),
            };
            section.AddBorderedText(s4, right: true, left: true);

            var discount = new Col[]
            {
                new Col("", 3),
                new Col("DESCUENTO", 18, ParagraphAlignment.Left),
                new Col(order.Descuento, 3, ParagraphAlignment.Right),
            };
            section.AddBorderedText(discount, right: true, left: true);


            var charge = new Col[]
            {
                new Col("", 3),
                new Col("CARGO", 18, ParagraphAlignment.Left),
                new Col(order.Cargo, 3, ParagraphAlignment.Right),
            };
            section.AddBorderedText(charge, right: true, left: true);


            var points = new Col[]
            {
                new Col("", 3),
                new Col("PUNTOS APLICADOS", 18, ParagraphAlignment.Left),
                new Col(order.PuntosAplicados, 3, ParagraphAlignment.Right),
            };
            section.AddBorderedText(points, right: true, left: true);

            var s8 = new Col[]
            {
                new Col("", 3),
                new Col("", 18, ParagraphAlignment.Left),
                new Col($"TOTAL {order.Total}", 3, ParagraphAlignment.Right),
            };
            section.AddBorderedText(s8, right: true, left: true, bottom: true);

            section.AddSpace(35);

            var footer = new Col[]
            {
                new Col("FIRMA PACIENTE", 5),
                new Col("TOTAL", 5),
                new Col($"LE ATENDIO: {order.Atiende}", 5, ParagraphAlignment.Right),
            };
            section.AddText(footer);

            var footer2 = new Col[]
            {
                new Col("", 5),
                new Col(order.Total, 5),
                new Col(order.Fecha, 5, ParagraphAlignment.Right),
            };
            section.AddText(footer2);
        }
    }
}