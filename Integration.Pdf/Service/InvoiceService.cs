using Integration.Pdf.Extensions;
using Integration.Pdf.Models;
using Integration.Pdf.Utils;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ZXing;

namespace Integration.Pdf.Service
{
    public class InvoiceService
    {
        public static byte[] Generate()
        {
            Document document = CreateDocument();

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
            var space = Unit.FromPoint(0.5);
            var border = Unit.FromPoint(0.7);
            float sectionWidth = section.PageSetup.PageWidth - section.PageSetup.LeftMargin - section.PageSetup.RightMargin;

            var logo = File.ReadAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets\\LabRamosLogo.png"));

            Table headerTable = new Table();

            Column column = headerTable.AddColumn();
            column.Width = sectionWidth / 7 * 3;
            column.Format.Alignment = ParagraphAlignment.Center;

            Row row = headerTable.AddRow();
            Paragraph paragraph = row.Cells[0].AddParagraph();

            var fontName = "Calibri";
            paragraph.AddFormattedText("LABORATORIO ALFONSO RAMOS\n", new Font(fontName, 14));
            paragraph.AddFormattedText("Lugar de expedición (C.P.): 85000\n", new Font(fontName, 8));
            paragraph.AddFormattedText("R.F.C. LAR900731TL0\n", new Font(fontName, 11));
            paragraph.AddFormattedText("601 General de Ley Personas Morales\n", new Font(fontName, 8));
            paragraph.AddFormattedText("Fecha y hora de certificación:\n", new Font(fontName, 11));
            paragraph.AddFormattedText("2022-06-08T11:22:22\n", new Font(fontName, 8));

            Table invoiceTable = new Table();
            invoiceTable.Rows.Alignment = RowAlignment.Right;

            Column columnI = invoiceTable.AddColumn();
            columnI.Format.Alignment = ParagraphAlignment.Center;
            columnI.Width = sectionWidth / 7 * 2;

            Row rowI = invoiceTable.AddRow();

            Paragraph paragraph1 = rowI.Cells[0].AddParagraph();
            paragraph1.Format.Alignment = ParagraphAlignment.Left;
            paragraph1.AddFormattedText("Tipo de Comprobante: Ingreso", new Font(fontName, 10));

            rowI = invoiceTable.AddRow();
            rowI.Shading.Color = Colors.LightGray;

            var cell2 = rowI.Cells[0];
            cell2.Borders.Width = border;
            cell2.Borders.Color = Colors.Black;

            Paragraph paragraph2 = cell2.AddParagraph();
            paragraph2.AddFormattedText("Factura", Col.FONT_DEFAULT);

            rowI = invoiceTable.AddRow();

            var cell3 = rowI.Cells[0];
            cell3.Borders.Left = new Border() { Width = border, Color = Colors.Black };
            cell3.Borders.Right = new Border() { Width = border, Color = Colors.Black };

            Paragraph paragraph3 = rowI.Cells[0].AddParagraph();
            paragraph3.AddFormattedText("Serie y Folio: R 15913", Col.FONT_BOLD);
            paragraph3.Format.SpaceBefore = Unit.FromMillimeter(1);
            paragraph3.Format.SpaceAfter = Unit.FromMillimeter(2);

            rowI = invoiceTable.AddRow();

            var cell4 = rowI.Cells[0];
            cell4.Borders.Left = new Border() { Width = border, Color = Colors.Black };
            cell4.Borders.Right = new Border() { Width = border, Color = Colors.Black };
            cell4.Borders.Bottom = new Border() { Width = border, Color = Colors.Black };

            Paragraph paragraph4 = cell4.AddParagraph();
            paragraph4.AddFormattedText("Folio Fiscal del CFDI (UUID):\n", new Font(fontName, 7));
            paragraph4.AddFormattedText("B3DA19F3-1666-42AB-89D3-B070D8059C1F", new Font(fontName, 7));
            paragraph4.Format.SpaceAfter = Unit.FromMillimeter(3);

            var headerInfo = new Col[]
            {
                new Col(logo, 2, ParagraphAlignment.Left)
                {
                    ImagenTamaño = Unit.FromCentimeter(5)
                },
                new Col("LABORATORIO", 3, new Font("Calibri", 10), ParagraphAlignment.Center)
                {
                    Tabla = headerTable
                },
                new Col("", 2, ParagraphAlignment.Right)
                {
                    Tabla = invoiceTable
                },
            };

            section.AddText(headerInfo);

            section.AddSpace();

            var line1 = new Col[]
            {
                new Col("CLIENTE: ", 3, Col.FONT_BOLD, ParagraphAlignment.Left),
                new Col($"SONORA FORMING", 21, ParagraphAlignment.Left),
            };
            section.AddBorderedText(line1, top: true, right: true, left: true, verticalSpace: space, borderWidth: border);

            var line2 = new Col[]
            {
                new Col("RFC: ", 3, Col.FONT_BOLD, ParagraphAlignment.Left),
                new Col($"SFO031014IH2", 7, ParagraphAlignment.Left),
                new Col("USO CFDI: ", 2, Col.FONT_BOLD, ParagraphAlignment.Left),
                new Col($"Gastos en general", 7, ParagraphAlignment.Left),
                new Col("COD. CTE.: ", 2, ParagraphAlignment.Left),
                new Col($"0364", 3, Col.FONT_BOLD, ParagraphAlignment.Left)
            };
            section.AddBorderedText(line2, right: true, left: true, verticalSpace: space, borderWidth: border);

            var line3 = new Col[]
            {
                new Col("DOMICILIO: ", 3, Col.FONT_BOLD, ParagraphAlignment.Left),
                new Col($"BLVD HENRY FORD 43 PARQUE INDUSTRIAL DYNA TECH SUR Hermosillo Sonora 83297", 21, ParagraphAlignment.Left),

            };
            section.AddBorderedText(line3, right: true, left: true, verticalSpace: space, borderWidth: border);

            var line4 = new Col[]
            {
                new Col("TELEFONO: ", 3, Col.FONT_BOLD, ParagraphAlignment.Left),
                new Col($"", 21, ParagraphAlignment.Left),
            };
            section.AddBorderedText(line4, right: true, left: true, verticalSpace: space, borderWidth: border);

            var line5 = new Col[]
            {
                new Col("VENDEDOR: ", 3, Col.FONT_BOLD, ParagraphAlignment.Left),
                new Col($"(Ninguno)", 21, ParagraphAlignment.Left),
            };
            section.AddBorderedText(line5, right: true, left: true, bottom: true, verticalSpace: space, borderWidth: border);

            section.AddSpace(10);

            var tableHeaders = new Col[]
            {
                new Col("Clave Prod/Servicio", 1),
                new Col("Cantidad", 1),
                new Col("Clave Unidad SAT", 1),
                new Col("Concepto / Descripción", 4),
                new Col("Precio unitario", 1),
                new Col("Impuestos", 1),
                new Col("Importe", 1),
            };

            section.AddBorderedText(tableHeaders, all: true, borderWidth: border, inverted: true, fontSize: 8);

            section.AddSpace();

            var data = GetData();

            foreach (var datum in data)
            {
                var tableData = new Col[]
                {
                    new Col(datum.ClaveProducto, 1),
                    new Col(datum.Cantidad, 1),
                    new Col(datum.ClaveUnidad, 1),
                    new Col(datum.Concepto, 4),
                    new Col(datum.PrecioUnitario, 1),
                    new Col(datum.Impuestos, 1),
                    new Col(datum.Importe, 1),
                };

                section.AddText(tableData, fontSize: 7, verticalPadding: Unit.FromPoint(8));
            }

            section.AddSpace(15);

            Table paymentTable = new Table();

            Column columnP = paymentTable.AddColumn();
            columnP.Width = sectionWidth / 7 * 5;
            columnP.Format.Alignment = ParagraphAlignment.Center;

            Row rowP = paymentTable.AddRow();
            Paragraph paragraphP = rowP.Cells[0].AddParagraph();

            paragraphP.AddFormattedText("Forma de pago: 99\n", new Font(fontName, 8) { Bold = true });
            paragraphP.AddFormattedText("Método de pago: PPD Pago en parcialidades o diferido\n", new Font(fontName, 8) { Bold = true });
            paragraphP.AddFormattedText("VEINTISIETE MIL QUINIENTOS VEINTITRES PESOS 29/100 M.N.", Col.FONT_DEFAULT);
            paragraphP.Format.LineSpacing = Unit.FromMillimeter(2);

            Table tableTotals = new Table();
            tableTotals.Borders.Visible = true;

            Column columnT1 = tableTotals.AddColumn();
            columnT1.Width = sectionWidth / 7;
            columnT1.Format.Alignment = ParagraphAlignment.Right;
            columnT1.Shading.Color = Colors.LightGray;

            Column columnT2 = tableTotals.AddColumn();
            columnT2.Width = sectionWidth / 7;
            columnT2.Format.Alignment = ParagraphAlignment.Right;

            Row rowT = tableTotals.AddRow();

            Paragraph paragraphL = rowT.Cells[0].AddParagraph();
            paragraphL.AddFormattedText("Subtotal:", Col.FONT_DEFAULT);
            Paragraph paragraphR = rowT.Cells[1].AddParagraph();
            paragraphR.AddFormattedText("$23,726.96", Col.FONT_DEFAULT);


            Row rowT2 = tableTotals.AddRow();

            Paragraph paragraphL1 = rowT2.Cells[0].AddParagraph();
            paragraphL1.AddFormattedText("Subtotal:", Col.FONT_DEFAULT);
            Paragraph paragraphR1 = rowT2.Cells[1].AddParagraph();
            paragraphR1.AddFormattedText("$23,726.96", Col.FONT_DEFAULT);


            Row rowT3 = tableTotals.AddRow();

            Paragraph paragraphL2 = rowT3.Cells[0].AddParagraph();
            paragraphL2.AddFormattedText("Subtotal:", Col.FONT_DEFAULT);
            Paragraph paragraphR2 = rowT3.Cells[1].AddParagraph();
            paragraphR2.AddFormattedText("$23,726.96", Col.FONT_DEFAULT);

            var t2 = new Col[]
            {
                new Col("", 5)
                {
                    Tabla = paymentTable
                },
                new Col("", 2)
                {
                    Tabla = tableTotals
                },
            };

            section.AddText(t2);

            //barcode.Width = Unit.FromCentimeter(4.8);

            var tableSAT = GetSATInfoTable(section);
            //section.AddText(new Col() { Tabla = tableSAT });
        }

        private static List<Detail> GetData()
        {
            return new List<Detail>
        {
            new Detail("85121800 - Laboratorios médicos", "20.00", "SERVICIO-E48-Unidad de servicio", "BIOMETRIA HEMATICA COMPLETA", "88.35", "002, -IVA, -Importe: 282.72", "1,767.00"),
            new Detail("85121800 - Laboratorios médicos", "20.00", "SERVICIO-E48-Unidad de servicio", "BIOMETRIA HEMATICA COMPLETA", "88.35", "002, -IVA, -Importe: 282.72", "1,767.00"),
            new Detail("85121800 - Laboratorios médicos", "20.00", "SERVICIO-E48-Unidad de servicio", "BIOMETRIA HEMATICA COMPLETA", "88.35", "002, -IVA, -Importe: 282.72", "1,767.00"),
            new Detail("85121800 - Laboratorios médicos", "20.00", "SERVICIO-E48-Unidad de servicio", "BIOMETRIA HEMATICA COMPLETA", "88.35", "002, -IVA, -Importe: 282.72", "1,767.00"),
            new Detail("85121800 - Laboratorios médicos", "20.00", "SERVICIO-E48-Unidad de servicio", "BIOMETRIA HEMATICA COMPLETA", "88.35", "002, -IVA, -Importe: 282.72", "1,767.00"),
            new Detail("85121800 - Laboratorios médicos", "20.00", "SERVICIO-E48-Unidad de servicio", "BIOMETRIA HEMATICA COMPLETA", "88.35", "002, -IVA, -Importe: 282.72", "1,767.00"),
            new Detail("85121800 - Laboratorios médicos", "20.00", "SERVICIO-E48-Unidad de servicio", "BIOMETRIA HEMATICA COMPLETA", "88.35", "002, -IVA, -Importe: 282.72", "1,767.00")
        };
        }

        private static Table GetSATInfoTable(Section section)
        {
            float sectionWidth = section.PageSetup.PageWidth - section.PageSetup.LeftMargin - section.PageSetup.RightMargin;

            var table = section.AddTable();
            table.Borders.Visible = true;
            table.Rows.Alignment = RowAlignment.Center;

            table.AddColumn(sectionWidth / 5);
            table.AddColumn(sectionWidth / 5);
            table.AddColumn(sectionWidth / 5);
            table.AddColumn(sectionWidth / 5);
            table.AddColumn(sectionWidth / 5);

            var row = table.AddRow();
            row.Cells[0].AddParagraph("Tipo Relación: -");
            row.Cells[0].MergeDown = 1;
            row.Cells[0].MergeRight = 1;
            row.Cells[0].Format.Font = new Font("Calibri", 7);
            row.Cells[2].AddParagraph("Efectos fiscales al pago. Condiciones de pago:");
            row.Cells[2].MergeRight = 1;
            row.Cells[2].Format.Font = new Font("Calibri", 6);

            var url = "https://verificacfdi.facturaelectronica.sat.gob.mx/default.aspx?&id=B3DA19F3-1666-42AB-89D3-B070D8059C1F&re=LAR900731TL0&rr=SFO031014IH2&tt=000000000000027523.290000&fe=Lr3/sg==";
            byte[] barcodeImage = BarCode.Generate(url, 240, 240, BarcodeFormat.QR_CODE);
            string imageFilename = barcodeImage.MigraDocFilenameFromByteArray();

            row.Cells[4].MergeDown = 6;
            var paragraph = row.Cells[4].AddParagraph();
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.AddImage(imageFilename);

            row = table.AddRow();
            row.Cells[2].AddParagraph("Este documento es una representación impresa de un CFDI");
            row.Cells[2].MergeRight = 1;
            row.Cells[2].Format.Font = new Font("Calibri", 6);

            row = table.AddRow();
            row.Cells[0].AddParagraph("Serie del Certificado del emisor:00001000000505971875");
            row.Cells[0].MergeRight = 1;
            row.Cells[0].Format.Font = new Font("Calibri", 6);
            row.Cells[2].AddParagraph("No. de serie del Certificado del SAT: 00001000000505142236");
            row.Cells[2].MergeRight = 1;
            row.Cells[2].Format.Font = new Font("Calibri", 6);

            row = table.AddRow();
            row.Cells[0].AddParagraph("Sello del SAT");
            row.Cells[0].Format.Font = new Font("Calibri", 6);

            row = table.AddRow();
            var sello = "Sc9w2+fm+i1Me3k9lRPHvhaGcRsy0AzISG4+wTmxz3oofDFDUCaBuIEkHrvrXOv+B4UKyxWgdYZACXC3z5qhdy/LBv1fhhJOnI8Q9BA/ruEaahe7MKbskNcv4YnY4jWilZ4O9RbSV++qxq/I6gSF7KYWitkI69miw+H/mvt+yUgvyHsUfC6JRRVXCBFR1/QKkCSaEvmpMRwS++emU3TLn6fdlkFRmEpLVjzLXuMsBR1W7qfkPmVis5Win7czbJjznZaDKAegyUmTRXMyM/ftW30LCV0P5oK3n2ijAfDQJnBW4/UK4glteug042zp3u0X5/pP3XkjYunfc+AmY7vk2A==";
            row.Cells[0].AddParagraph(string.Join(Environment.NewLine, sello.ToChunks(140).Select(x => string.Join("", x))));
            row.Cells[0].MergeRight = 3;
            row.Cells[0].Format.Font = new Font("Courier New", 5);

            row = table.AddRow();
            row.Cells[0].AddParagraph("Sello digital del CFDI");
            row.Cells[0].Format.Font = new Font("Calibri", 6);

            row = table.AddRow();
            var selloCFDI = "tWq2PtaTz4+aDeN7agD1TL5+aZRfqr6OeccilcGHW7k4nBPIdpdAg86PbaiYzc2kowE2f4Ra9IWiII2CzAu1nNFI9dPolLMIWjgkQsZdos4HAtpMoDIlJmY6E1sauh3N4AAanwoiz4LZ2tsK0SkcDKr6w4w8f+EusRFvKRAaUCghjI+xVhbOR0CJB/mJZ+pHNu5Zpg6c2Kth23yfjGMvofV8WnoSBYuCIHXAf6LY9903fr/2gGulUu5Xh5Jw9TF/U23zduFdTPoPvLHmoa885wnTJ/K7AVCG/vkoGNgltDtQcCVw21aC173iVk5n5kuJsJZ1XkedYiJIkQbYLr3/sg==";
            row.Cells[0].AddParagraph(string.Join(Environment.NewLine, selloCFDI.ToChunks(140).Select(x => string.Join("", x))));
            row.Cells[0].MergeRight = 3;
            row.Cells[0].Format.Font = new Font("Courier New", 5);

            row = table.AddRow();
            row.Cells[0].AddParagraph("Cadena original del complemento del certificación digital del SAT");
            row.Cells[0].MergeRight = 2;
            row.Cells[0].Format.Font = new Font("Calibri", 6);

            row = table.AddRow();
            var cert = "||1.1|B3DA19F3-1666-42AB-89D3-B070D8059C1F|2022-06-08T11:22:22|MAS0810247C0|tWq2PtaTz4+aDeN7agD1TL5+aZRfqr6OeccilcGHW7k4nBPIdpdAg86PbaiYzc2kowE2f4Ra9IWiII2CzAu1nNFI9dPolLMIWjgkQsZdos4HAtpMoDIlJmY6E1sauh3N4AAanwoiz4LZ2tsK0SkcDKr6w4w8f+EusRFvKRAaUCghjI+xVhbOR0CJB/mJZ+pHNu5Zpg6c2Kth23yfjGMvofV8WnoSBYuCIHXAf6LY9903fr/2gGulUu5Xh5Jw9TF/U23zduFdTPoPvLHmoa885wnTJ/K7AVCG/vkoGNgltDtQcCVw21aC173iVk5n5kuJsJZ1XkedYiJIkQbYLr3/sg==|00001000000505142236||";
            row.Cells[0].AddParagraph(string.Join(Environment.NewLine, cert.ToChunks(175).Select(x => string.Join("", x))));
            row.Cells[0].MergeRight = 4;
            row.Cells[0].Format.Font = new Font("Courier New", 5);

            return table;
        }
    }

    public class Detail
    {
        public Detail(string claveProducto, string cantidad, string claveUnidad, string concepto, string precioUnitario, string impuestos, string importe)
        {
            ClaveProducto = claveProducto;
            Cantidad = cantidad;
            ClaveUnidad = claveUnidad;
            Concepto = concepto;
            PrecioUnitario = precioUnitario;
            Impuestos = impuestos;
            Importe = importe;
        }

        public string ClaveProducto { get; set; }
        public string Cantidad { get; set; }
        public string ClaveUnidad { get; set; }
        public string Concepto { get; set; }
        public string PrecioUnitario { get; set; }
        public string Impuestos { get; set; }
        public string Importe { get; set; }
    }
}