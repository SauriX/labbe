using Integration.Pdf.Extensions;
using Integration.Pdf.Models;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using System.Collections.Generic;
using System.IO;

namespace Integration.Pdf.Service
{
    public class QuotationService
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
            section.PageSetup.PageWidth = Unit.FromCentimeter(7.2);
            section.PageSetup.PageHeight = Unit.FromCentimeter(29.7);

            section.PageSetup.TopMargin = 0;
            section.PageSetup.BottomMargin = Unit.FromMillimeter(5);
            section.PageSetup.LeftMargin = 0;
            section.PageSetup.RightMargin = 0;

            Format(section);

            return document;
        }

        static void Format(Section section)
        {
            var branchInfo = new Col("Laboratorio Alfonso Ramos, S.A. de C.V. Avenida Humberto Lobo #555 A, Col. del Valle C.P. 66220 San Pedro Garza García, Nuevo León.");
            section.AddText(branchInfo);

            var phoneInfo = new Col("Tel/WhatsApp: 81 4170 0769 RFC: LAR900731TL0");
            section.AddText(phoneInfo);

            var branchName = new Col("SUCURSAL MONTERREY", Col.FONT_BOLD);
            section.AddText(branchName);

            var budget = new Col("PRESUPUESTO", Col.FONT_DEFAULT);
            section.AddText(budget, inverted: true);

            var date = new Col("Fecha: 22/02/2022", ParagraphAlignment.Left);
            section.AddText(date);

            var patient = new Col("Paciente: André Ruiz Montalvo", ParagraphAlignment.Left);
            section.AddText(patient);

            var st = new Col("ESTUDIOS", Col.FONT_DEFAULT);
            section.AddText(st, inverted: true);

            var colDesc = new Col("DESC", 3, Col.FONT_BOLD);
            var colQty = new Col("CANT", 1, Col.FONT_BOLD);
            var colSub = new Col("SUB", 1, Col.FONT_BOLD);
            var colDisc = new Col("DES", 1, Col.FONT_BOLD);
            var colTotal = new Col("TOTAL", 1, Col.FONT_BOLD);
            section.AddText(new[] { colDesc, colQty, colSub, colDisc, colTotal });

            var studies = GetStudies();

            foreach (var study in studies)
            {
                section.AddText(
                    new[] {
                        new Col(study.Descripcion + " " + $"({study.Codigo})", 3, ParagraphAlignment.Left),
                        new Col(study.Cantidad.ToString(), 1),
                        new Col(study.SubTotal.ToString("F"), 1),
                        new Col(study.Descuento.ToString("F"), 1),
                        new Col(study.Total.ToString("F"), 1)
                    });
            }

            section.AddDivider();

            section.AddText(new[] { new Col("SUBTOTAL", 1, ParagraphAlignment.Left), new Col("$ 150.80", 1, ParagraphAlignment.Right), new Col("") });
            section.AddText(new[] { new Col("DESCUENTO", 1, ParagraphAlignment.Left), new Col("$ 0.00", 1, ParagraphAlignment.Right), new Col("") });
            section.AddText(new[] { new Col("IVA", 1, ParagraphAlignment.Left), new Col("$ 24.14", 1, ParagraphAlignment.Right), new Col("") });
            section.AddText(new[] { new Col("TOTAL", 1, ParagraphAlignment.Left), new Col("$ 175.00", 1, ParagraphAlignment.Right), new Col("") });

            var totalString = new Col("SON: CIENTO SETENTA Y CINCO PESOS 00/100 M.N");
            section.AddText(totalString);

            section.AddDivider();

            //BarcodeWriter<Bitmap> writer = new BarcodeWriter<Bitmap>()
            //{
            //    Format = BarcodeFormat.CODE_128,
            //    Renderer = new ZXing.Rendering.BitmapRenderer()
            //};

            //var barHeight = 48;
            //var barWidth = 150;

            //writer.Options = new ZXing.Common.EncodingOptions { Width = barWidth, Height = barHeight, Margin = 0 };

            //graphics.DrawImage(writer.Write("USUARIO:2202178006"), (TicketUtil.TICKET_WIDTH - barWidth) / 2, barWidth, barHeight);
        }

        public class Info
        {
            public string Codigo { get; set; }
            public string Descripcion { get; set; }
            public int Cantidad { get; set; }
            public decimal SubTotal { get; set; }
            public decimal Impuestos { get; set; }
            public decimal Descuento { get; set; }
            public decimal IVA { get; set; }
            public decimal Total { get; set; }
        }

        public static List<Info> GetStudies()
        {
            return new List<Info>
            {
                new Info
                {
                    Codigo = "ETX",
                    Descripcion = "TELERADIOGRAFIA DE TORAX",
                    Cantidad = 1,
                    SubTotal = 150.86m,
                    Impuestos = 15.45m,
                    Descuento = 0m,
                    IVA = 5.3m,
                    Total = 175,
                },
                new Info
                {
                    Codigo = "TEZ",
                    Descripcion = "TELERADIO GRAFIA DE TORAX",
                    Cantidad = 1,
                    SubTotal = 150.86m,
                    Impuestos = 15.45m,
                    Descuento = 0m,
                    IVA = 5.3m,
                    Total = 175,
                },
                new Info
                {
                    Codigo = "ETX",
                    Descripcion = "TELERADIOGRAFIA DE TORAX",
                    Cantidad = 1,
                    SubTotal = 150.86m,
                    Impuestos = 15.45m,
                    Descuento = 0m,
                    IVA = 5.3m,
                    Total = 175,
                },
                new Info
                {
                    Codigo = "TEZ",
                    Descripcion = "TELERADIO GRAFIA DE TORAX",
                    Cantidad = 1,
                    SubTotal = 150.86m,
                    Impuestos = 15.45m,
                    Descuento = 0m,
                    IVA = 5.3m,
                    Total = 175,
                },
                new Info
                {
                    Codigo = "ETX",
                    Descripcion = "TELERADIOGRAFIA DE TORAX",
                    Cantidad = 1,
                    SubTotal = 150.86m,
                    Impuestos = 15.45m,
                    Descuento = 0m,
                    IVA = 5.3m,
                    Total = 175,
                },
                new Info
                {
                    Codigo = "TEZ",
                    Descripcion = "TELERADIO GRAFIA DE TORAX",
                    Cantidad = 1,
                    SubTotal = 150.86m,
                    Impuestos = 15.45m,
                    Descuento = 0m,
                    IVA = 5.3m,
                    Total = 175,
                },
                new Info
                {
                    Codigo = "ETX",
                    Descripcion = "TELERADIOGRAFIA DE TORAX",
                    Cantidad = 1,
                    SubTotal = 150.86m,
                    Impuestos = 15.45m,
                    Descuento = 0m,
                    IVA = 5.3m,
                    Total = 175,
                },
                new Info
                {
                    Codigo = "TEZ",
                    Descripcion = "TELERADIO GRAFIA DE TORAX",
                    Cantidad = 1,
                    SubTotal = 150.86m,
                    Impuestos = 15.45m,
                    Descuento = 0m,
                    IVA = 5.3m,
                    Total = 175,
                },
            };
        }
    }
}