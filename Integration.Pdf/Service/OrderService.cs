using Integration.Pdf.Extensions;
using Integration.Pdf.Models;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using System.IO;
using System.Text;

namespace Integration.Pdf.Service
{
    public class OrderService
    {
        public static byte[] Generate()
        {
            Document document = CreateDocument();

            document.UseCmykColor = true;
            const bool unicode = false;

            DocumentRenderer renderer = new DocumentRenderer(document);
            renderer.PrepareDocument();

            //RenderInfo[] info = renderer.GetRenderInfoFromPage(1);
            //int index = info.Length - 1;

            //double stop = info[index].LayoutInfo.ContentArea.Y.Millimeter + info[index].LayoutInfo.ContentArea.Height.Millimeter + 10;
            //var section = document.LastSection;
            //section.PageSetup.PageHeight = Unit.FromMillimeter(stop);

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

        static Document CreateDocument()
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

            Format(section);

            return document;
        }

        static void Format(Section section)
        {
            var title = new Col("Laboratorio Alfonso Ramos S.A. de C.V. (HERMOSILLO)", new Font("Calibri", 11) { Bold = true }, ParagraphAlignment.Right);
            section.AddText(title);

            section.AddSpace();

            var line1 = new Col[]
            {
                new Col("SOLICITUD NO.", 3, ParagraphAlignment.Left),
                new Col(": 2110029501", 21, Col.FONT_BOLD, ParagraphAlignment.Left)
            };
            section.AddBorderedText(line1, top: true, right: true, left: true);

            var line2 = new Col[]
            {
                new Col("FECHA", 3, ParagraphAlignment.Left),
                new Col(": 2021-10-02", 8, Col.FONT_BOLD, ParagraphAlignment.Left),
                new Col("", 1),
                new Col("FECH. NAC.", 3, ParagraphAlignment.Left),
                new Col(": 1977-04-05", 4, Col.FONT_BOLD, ParagraphAlignment.Left),
                new Col("", 1),
                new Col("EDAD", 1, ParagraphAlignment.Left),
                new Col(": 44", 3, Col.FONT_BOLD, ParagraphAlignment.Left)
            };
            section.AddBorderedText(line2, right: true, left: true);

            var line3 = new Col[]
            {
                new Col("PACIENTE", 3, ParagraphAlignment.Left),
                new Col(": YOLANDA MONTAÑO MEDINA", 8, Col.FONT_BOLD, ParagraphAlignment.Left),
                new Col("", 1),
                new Col("SEXO", 3, ParagraphAlignment.Left),
                new Col(": F", 4, Col.FONT_BOLD, ParagraphAlignment.Left),
                new Col("", 1),
                new Col("TEL", 1, ParagraphAlignment.Left),
                new Col(": 622405852", 3, Col.FONT_BOLD, ParagraphAlignment.Left)
            };
            section.AddBorderedText(line3, right: true, left: true);

            var line4 = new Col[]
            {
                new Col("MEDICO", 3, ParagraphAlignment.Left),
                new Col(": MARISOL BARAJAS VALENZUELA", 8, Col.FONT_BOLD, ParagraphAlignment.Left),
                new Col("", 1),
                new Col("TEL", 3, ParagraphAlignment.Left),
                new Col(": ", 9, Col.FONT_BOLD, ParagraphAlignment.Left),
            };
            section.AddBorderedText(line4, right: true, left: true);

            var line5 = new Col[]
            {
                new Col("SUCURSAL", 3, ParagraphAlignment.Left),
                new Col(": CANTABRIA", 8, Col.FONT_BOLD, ParagraphAlignment.Left),
                new Col("", 1),
                new Col("COMPAÑIA", 3, ParagraphAlignment.Left),
                new Col(": PARTICULARES", 9, Col.FONT_BOLD, ParagraphAlignment.Left),
            };
            section.AddBorderedText(line5, right: true, left: true);

            var line6 = new Col[]
            {
                new Col("E-MAIL", 3, ParagraphAlignment.Left),
                new Col(": yomm6@hotmail.com", 8, Col.FONT_BOLD, ParagraphAlignment.Left),
                new Col("", 1),
                new Col("X ENVIAR A PACIENTES", 12, ParagraphAlignment.Left),
            };
            section.AddBorderedText(line6, right: true, left: true);

            var line7 = new Col[]
            {
                new Col("OBS", 3, ParagraphAlignment.Left),
                new Col(": TX. EUTIROX. HIERRO.", 21, Col.FONT_BOLD, ParagraphAlignment.Left),
            };
            section.AddBorderedText(line7, right: true, left: true, bottom: true);

            section.AddSpace(10);


            var s1 = new Col[]
            {
                new Col("CLAVE", 3, Col.FONT_BOLD),
                new Col("ESTUDIO", 18, Col.FONT_BOLD, ParagraphAlignment.Left),
                new Col("PRECIO", 3, Col.FONT_BOLD),
            };
            section.AddBorderedText(s1, top: true, right: true, bottom: true, left: true);

            var s2 = new Col[]
            {
                new Col("1", 3),
                new Col("CITOLOGIA HEMATICA", 18, ParagraphAlignment.Left),
                new Col("205.00", 3, ParagraphAlignment.Right),
            };
            section.AddBorderedText(s2, right: true, left: true);

            var s3 = new Col[]
            {
                new Col("EGO", 3),
                new Col("EXAMEN GENERAL DE ORINA", 18, ParagraphAlignment.Left),
                new Col("125.00", 3, ParagraphAlignment.Right),
            };
            section.AddBorderedText(s3, right: true, left: true);

            var ph = new StringBuilder(125).Insert(0, "-", 125).ToString();
            var s4 = new Col[]
            {
                new Col("", 3),
                new Col(ph, 18, ParagraphAlignment.Left),
                new Col("", 3, ParagraphAlignment.Right),
            };
            section.AddBorderedText(s4, right: true, left: true);


            var s5 = new Col[]
            {
                new Col("", 3),
                new Col("DESCUENTO", 18, ParagraphAlignment.Left),
                new Col("", 3, ParagraphAlignment.Right),
            };
            section.AddBorderedText(s5, right: true, left: true);


            var s6 = new Col[]
            {
                new Col("", 3),
                new Col("CARGO", 18, ParagraphAlignment.Left),
                new Col("", 3, ParagraphAlignment.Right),
            };
            section.AddBorderedText(s6, right: true, left: true);


            var s7 = new Col[]
            {
                new Col("", 3),
                new Col("PUNTOS APLICADOS", 18, ParagraphAlignment.Left),
                new Col("30", 3, ParagraphAlignment.Right),
            };
            section.AddBorderedText(s7, right: true, left: true);

            var s8 = new Col[]
            {
                new Col("", 3),
                new Col("", 18, ParagraphAlignment.Left),
                new Col("TOTAL 300.00", 3, ParagraphAlignment.Right),
            };
            section.AddBorderedText(s8, right: true, left: true, bottom: true);

            section.AddSpace(35);

            var footer = new Col[]
            {
                new Col("FIRMA PACIENTE", 5),
                new Col("TOTAL", 5),
                new Col("LE ATENDIO: KARLA GABRIELA VALDEZ", 5, ParagraphAlignment.Right),
            };
            section.AddText(footer);

            var footer2 = new Col[]
            {
                new Col("", 5),
                new Col("330.00", 5),
                new Col("06-19-13", 5, ParagraphAlignment.Right),
            };
            section.AddText(footer2);
        }
    }
}