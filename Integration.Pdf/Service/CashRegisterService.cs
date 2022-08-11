using Integration.Pdf.Extensions;
using Integration.Pdf.Models;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes.Charts;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Integration.Pdf.Service
{
    public class CashRegisterService
    {
        public static byte[] Generate(CashData cashData)
        {
            Document document = CreateDocument(cashData);

            document.UseCmykColor = true;
            const bool unicode = false;

            DocumentRenderer renderer = new DocumentRenderer(document);
            renderer.PrepareDocument();

            RenderInfo[] info = renderer.GetRenderInfoFromPage(1);
            int index = info.Length - 1;

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

        static Document CreateDocument(CashData cashData)
        {
            Document document = new Document();

            Section section = document.AddSection();

            section.PageSetup = document.DefaultPageSetup.Clone();

            section.PageSetup.Orientation = Orientation.Landscape;
            section.PageSetup.PageFormat = PageFormat.A4;

            section.PageSetup.TopMargin = Unit.FromCentimeter(1);
            section.PageSetup.BottomMargin = Unit.FromCentimeter(1);
            section.PageSetup.LeftMargin = Unit.FromCentimeter(1);
            section.PageSetup.RightMargin = Unit.FromCentimeter(1);

            Format(section, cashData.Columnas, cashData.Datos ,cashData.PerDay, cashData.Canceled, cashData.OtherDay, cashData.Invoice, cashData.ColumnasTotales, cashData.Totales, cashData.Header);

            return document;
        }
        static void Format(Section section, List<Col> columns, List<Dictionary<string, object>> data, List<Dictionary<string, object>> perDay, List<Dictionary<string, object>> canceled, List<Dictionary<string, object>> otherDay, InvoiceData invoice, List<Col> totalColumns, Dictionary<string, object> totales, HeaderData Header)
        {
            var fontTitle = new Font("calibri", 18) { Bold = true };
            var fontSubtitle = new Font("calibri", 14);
            var fontText = new Font("calibri", 11);
            var fontTitleChart = new Font("calibri", 11) { Bold = true };

            var businessName = "Laboratorio Alfonso Ramos S.A de C.V MONTERREY";
            var location = "Avenida Humberto Lobo #555";
            var city = "San Pedro Garza García, Nuevo León";

            var title = new Col(businessName + "\n" + location + "\n" + city, fontTitle);
            var subtitle = new Col("CORTE DE CAJA DEL " + Header.Fecha + " " + Header.Hora, fontSubtitle);
            var branch = new Col(Header.Sucursal, fontSubtitle);
            string[] messageTable = { "PACIENTES DEL DIA", "**CANCELACIONES DEL DIA**", "PAGOS DE OTROS DIAS" };

            section.AddText(title);
            section.AddSpace(5);
            section.AddText(subtitle);
            if (!string.IsNullOrEmpty(Header.Sucursal))
            {
                section.AddText(branch);
            }
            section.AddSpace(10);

            var contentWidth = section.PageSetup.PageHeight - section.PageSetup.LeftMargin - section.PageSetup.RightMargin;

           for (int typeTable = 0; typeTable < messageTable.Length; typeTable++)
            {
                section.AddText(new Col(messageTable[typeTable], fontSubtitle, ParagraphAlignment.Left));

                if (messageTable[typeTable] == "PACIENTES DEL DIA")
                {
                    data = perDay;
                }
                else if (messageTable[typeTable] == "**CANCELACIONES DEL DIA**")
                {
                    data = canceled;
                }
                else if (messageTable[typeTable] == "PAGOS DE OTROS DIAS")
                {
                    data = otherDay;
                }

                Table table = new Table();
                table.Borders.Width = 0.75;
                table.Borders.Color = Colors.LightGray;
                table.TopPadding = 3;
                table.BottomPadding = 3;

                var colWidth = contentWidth / columns.Sum(x => x.Tamaño);

                if (columns == null || columns.Count == 0)
                {
                    return;
                }

                for (int i = 0; i < columns.Count; i++)
                {
                    Col item = columns[i];
                    Column column = table.AddColumn(colWidth * item.Tamaño);
                    column.Format.Alignment = item.Horizontal;
                }

                Row row = table.AddRow();

                for (int i = 0; i < columns.Count; i++)
                {
                    row.Shading.Color = Colors.Gray;
                    row.Format.Font.Color = Colors.White;

                    Cell cell = row.Cells[i];
                    cell.Borders.Left.Visible = false;
                    cell.Borders.Right.Visible = false;
                    cell.Borders.Bottom.Visible = false;
                    cell.AddParagraph(columns[i].Texto);
                }

                if (data != null && data.Count > 0)
                {
                    foreach (var item in data)
                    {
                        row = table.AddRow();
                        for (int i = 0; i < columns.Count; i++)
                        {
                            var key = columns[i].Texto;

                            if (item.ContainsKey(key))
                            {
                                Cell cell = row.Cells[i];
                                cell.Borders.Left.Visible = false;
                                cell.Borders.Right.Visible = false;

                                var format = columns[i].Formato;
                                var cellData = item[key].ToString();

                                if (!string.IsNullOrWhiteSpace(format))
                                {
                                    if (cellData[0] == '[' && cellData[cellData.Length - 1] == ']')
                                    {
                                        var datalist = JsonConvert.DeserializeObject<List<string>>(cellData);
                                        datalist.Where(x => x != null).ToList().ForEach(x => cell.AddParagraph(x));
                                    }
                                    else
                                    {
                                        cell.AddParagraph(Convert.ToDouble(item[key]).ToString(format));
                                    }
                                }
                                else
                                {
                                    if (cellData[0] == '[' && cellData[cellData.Length - 1] == ']')
                                    {
                                        var datalist = JsonConvert.DeserializeObject<List<string>>(cellData);
                                        datalist.Where(x => x != null).ToList().ForEach(x => cell.AddParagraph(x));
                                    }
                                    else
                                    {
                                        cell.AddParagraph(cellData);
                                    }
                                }
                            }
                        }
                    }

                    table.Rows.Alignment = RowAlignment.Center;
                }
                else
                {
                    row = table.AddRow();

                    Cell cell = row.Cells[0];
                    cell.MergeRight = columns.Count - 1;
                    cell.Borders.Left.Visible = false;
                    cell.Borders.Right.Visible = false;
                    cell.Borders.Bottom.Visible = false;
                    cell.Format.Alignment = ParagraphAlignment.Center;

                    cell.AddParagraph("No hay registros");
                }

                section.Add(table);
                section.AddSpace(10);
            }
        }
    }
}