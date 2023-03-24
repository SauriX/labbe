using Integration.Pdf.Dtos;
using Integration.Pdf.Dtos.DeliverOrder;
using Integration.Pdf.Dtos.PriceQuote;
using Integration.Pdf.Extensions;
using Integration.Pdf.Models;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Integration.Pdf.Service
{
    public class QuotationService
    {
        public static byte[] Generate(QuotationTicketDto order)
        {
            Document document = CreateDocument(order);

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

        static Document CreateDocument(QuotationTicketDto order)
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

        static void Format(Section section, QuotationTicketDto order)
        {
            var logo = File.ReadAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets\\LabRamosLogo.png"));
            var labramoslogo = new Col(logo, 6, ParagraphAlignment.Center)
            {
                ImagenTamaño = Unit.FromCentimeter(6)
            };

            section.AddText(labramoslogo);

            section.AddSpace();

            var title = new Col($"Laboratorio Alfonso Ramos S.A. de C.V.", new Font("Calibri", 13) { Bold = true }, ParagraphAlignment.Center);
            section.AddText(title);

            section.AddSpace();
            var titledoc = new Col($"Cotización de estudios", new Font("Calibri", 11) { Bold = true }, ParagraphAlignment.Center);
            section.AddText(titledoc);

            section.AddSpace();

            var dates = new[] {
                new Col($"Fecha Creación: {order.Fecha}", 12, new Font("Calibri", 11) { Bold = true }, ParagraphAlignment.Left),
                new Col($"Fecha de impresión: {order.FechaImpresion}", 12, new Font("Calibri", 11) { Bold = true }, ParagraphAlignment.Right)
            };
            section.AddText(dates);
            section.AddSpace();
            //tabla total
            var Data = order.Estudios;
            float sectionWidth = section.PageSetup.PageWidth - section.PageSetup.LeftMargin - section.PageSetup.RightMargin;

            var table = section.AddTable();
            table.Rows.Alignment = RowAlignment.Center;

            var font = table.Format.Font.Clone();
            font.ApplyFont(Col.FONT_DEFAULT);
            table.Format.Font = font;
            table.Borders.Visible = true;
            table.Borders.Color = Colors.CadetBlue;
            table.BottomPadding = 3;
            table.LeftPadding = 3;
            table.TopPadding = 3;
            table.RightPadding = 3;

            var col1 = table.AddColumn(sectionWidth / 8 * 2);
            table.AddColumn(sectionWidth / 8 * 2);
            table.AddColumn(sectionWidth / 8);
            table.AddColumn(sectionWidth / 8);
            table.AddColumn(sectionWidth / 8);
            table.AddColumn(sectionWidth / 8);

            foreach (var item in Data)
            {
                var row = table.AddRow();
                row.Cells[0].MergeRight = 1;
                row.Cells[0].Borders.Visible = false;
                row.Cells[2].Shading.Color = Color.FromRgb(127, 165, 249);
                row.Cells[2].AddParagraph("Precio de Lista");
                row.Cells[3].Shading.Color = Color.FromRgb(127, 165, 249);
                row.Cells[3].AddParagraph("Desc.");
                row.Cells[4].Shading.Color = Color.FromRgb(127, 165, 249);
                row.Cells[4].AddParagraph("IVA");
                row.Cells[5].Shading.Color = Color.FromRgb(127, 165, 249);
                row.Cells[5].AddParagraph("Precio (Final)");

                row = table.AddRow();
                row.Format.Font.Color = Colors.WhiteSmoke;
                row.Cells[0].Shading.Color = Color.FromRgb(127, 165, 249);
                row.Cells[0].Format.Font.Color = Colors.Black;
                var parapraph = row.Cells[0].AddParagraph();
                parapraph.AddText("Clave: ");
                parapraph.AddFormattedText(item.Clave, Col.FONT_BOLD);
                row.Cells[1].Shading.Color = Colors.SlateGray;
                row.Cells[1].AddParagraph(item.Nombre);
                row.Cells[2].Shading.Color = Colors.SlateGray;
                row.Cells[2].AddParagraph($"{item.Precio}");
                row.Cells[3].Shading.Color = Colors.SlateGray;
                row.Cells[3].AddParagraph($"{item.Descuento}");
                row.Cells[4].Shading.Color = Colors.SlateGray;
                row.Cells[4].AddParagraph(item.IVA);
                row.Cells[5].Shading.Color = Colors.SlateGray;
                row.Cells[5].AddParagraph(item.PrecioFinal);

                row = table.AddRow();
                row.Format.Font.Color = Colors.WhiteSmoke;
                row.Cells[0].Format.Font.Color = Colors.Black;
                row.Cells[0].Shading.Color = Color.FromRgb(127, 165, 249);
                row.Cells[0].AddParagraph("Tiempo de entrega");
                row.Cells[1].MergeRight = 4;
                row.Cells[1].Format.Font.Color = Colors.Black;
                row.Cells[1].AddParagraph(item.TiempoEntrega);

                row = table.AddRow();
                row.Format.Font.Color = Colors.WhiteSmoke;
                row.Cells[0].Shading.Color = Color.FromRgb(127, 165, 249);
                row.Cells[0].Format.Font.Color = Colors.Black;
                row.Cells[0].AddParagraph("Tipo de muestra");
                row.Cells[1].MergeRight = 4;
                row.Cells[1].Shading.Color = Colors.SlateGray;
                row.Cells[1].AddParagraph(item.TipoMuestra);

                row = table.AddRow();
                row.Format.Font.Color = Colors.WhiteSmoke;
                row.Cells[0].Shading.Color = Color.FromRgb(127, 165, 249);
                row.Cells[0].Format.Font.Color = Colors.Black;
                row.Cells[0].AddParagraph("Preparacion del paciente");
                row.Cells[1].MergeRight = 4;
                row.Cells[1].Format.Font.Color = Colors.Black;
                row.Cells[1].AddParagraph(item.PreparacionPaciente);
            }

            section.AddSpace(15);
            List<Col> columns = new List<Col>()
            {
                new Col("TOTAL DE PRECIO SIN IVA", ParagraphAlignment.Left),
                new Col("DESCUENTO POR PROMOCIÓN", ParagraphAlignment.Left),
                new Col("IVA 16%", ParagraphAlignment.Left),
                new Col("TOTAL A PAGAR ", ParagraphAlignment.Left)
,
            };
            var totales = new Dictionary<string, object>{
                { "TOTAL DE PRECIO SIN IVA", order.Total },
                { "DESCUENTO POR PROMOCIÓN", order.Descuento },
                { "IVA 16%", order.IVA },
                { "TOTAL A PAGAR ", order.TotalPago },
            };

            var datatotal = new List<Dictionary<string, object>>();
            datatotal.Add(totales);

            Table tabletotal = new Table();
            tabletotal.Rows.Alignment = RowAlignment.Center;
            tabletotal.Borders.Width = 0.75;
            tabletotal.Borders.Color = Colors.LightGray;
            tabletotal.TopPadding = 3;
            tabletotal.BottomPadding = 3;
            var contentWidthtotal = section.PageSetup.PageHeight - section.PageSetup.LeftMargin - section.PageSetup.RightMargin;
            var colWidthtotal = contentWidthtotal / 8;

            if (columns == null || columns.Count == 0)
            {
                return;
            }

            for (int i = 0; i < columns.Count; i++)
            {
                Col item = columns[i];
                Column column = tabletotal.AddColumn(colWidthtotal * item.Tamaño);
                column.Format.Alignment = item.Horizontal;
            }

            Row rowtotal = tabletotal.AddRow();

            for (int i = 0; i < columns.Count; i++)
            {
                rowtotal.Shading.Color = Colors.Gray;
                rowtotal.Format.Font.Color = Colors.White;

                Cell celltotal = rowtotal.Cells[i];
                celltotal.Borders.Left.Visible = false;
                celltotal.Borders.Right.Visible = false;
                celltotal.Borders.Bottom.Visible = false;
                celltotal.AddParagraph(columns[i].Texto);
            }

            if (datatotal != null && datatotal.Count > 0)
            {
                foreach (var item in datatotal)
                {
                    rowtotal = tabletotal.AddRow();
                    for (int i = 0; i < columns.Count; i++)
                    {
                        var key = columns[i].Texto;

                        if (item.ContainsKey(key))
                        {
                            Cell celltotal = rowtotal.Cells[i];
                            celltotal.Borders.Left.Visible = false;
                            celltotal.Borders.Right.Visible = false;

                            var format = columns[i].Formato;
                            var cellData = item[key].ToString();

                            if (!string.IsNullOrWhiteSpace(format))
                            {
                                if (cellData[0] == '[' && cellData[cellData.Length - 1] == ']')
                                {
                                    var datalist = JsonConvert.DeserializeObject<List<string>>(cellData);
                                    datalist.Where(x => x != null).ToList().ForEach(x => celltotal.AddParagraph(x));
                                }
                                else
                                {
                                    celltotal.AddParagraph(Convert.ToDouble(item[key]).ToString(format));
                                }
                            }
                            else
                            {
                                var CelldataValidationInicial = ' ';
                                var CelldataValidationFinal = ' ';

                                if (cellData.Length > 0)
                                {
                                    CelldataValidationInicial = cellData[0];
                                    CelldataValidationFinal = cellData[cellData.Length - 1];
                                }

                                if (CelldataValidationInicial == '[' && CelldataValidationFinal == ']')
                                {
                                    var datalist = JsonConvert.DeserializeObject<List<string>>(cellData);
                                    datalist.Where(x => x != null).ToList().ForEach(x => celltotal.AddParagraph(x));
                                }
                                else
                                {
                                    celltotal.AddParagraph(cellData);
                                }
                            }
                        }
                    }
                }

                tabletotal.Rows.Alignment = RowAlignment.Center;
            }
            else
            {
                rowtotal = tabletotal.AddRow();

                Cell cell = rowtotal.Cells[0];
                cell.MergeRight = columns.Count - 1;
                cell.Borders.Left.Visible = false;
                cell.Borders.Right.Visible = false;
                cell.Borders.Bottom.Visible = false;
                cell.Format.Alignment = ParagraphAlignment.Center;

                cell.AddParagraph("No hay registros");
            }

            section.Add(tabletotal);
            section.AddSpace(15);
        }
    }
}