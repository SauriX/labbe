﻿using Integration.Pdf.Extensions;
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
    public class ReportService
    {
        public static byte[] Generate(ReportData reportData)
        {
            Document document = CreateDocument(reportData);

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

        static Document CreateDocument(ReportData reportData)
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

            Format(section, reportData.Columnas, reportData.Series, reportData.Datos, reportData.DatosGrafica, reportData.Invoice, reportData.ColumnasTotales, reportData.Totales, reportData.Header);

            return document;
        }

        static void Format(Section section, List<Col> columns, List<ChartSeries> seriesInfo, List<Dictionary<string, object>> data, List<Dictionary<string, object>> datachart, InvoiceData invoice, List<Col> totalColumns, Dictionary<string, object> totales, HeaderData Header)
        {
            var fontTitle = new Font("calibri", 14);
            var fontSubtitle = new Font("calibri", 12);
            var fontTitleChart = new Font("calibri", 11) { Bold = true };
            var fontText = new Font("calibri", 10);

            var contentWidth = section.PageSetup.PageHeight - section.PageSetup.LeftMargin - section.PageSetup.RightMargin;

            var title = Header.NombreReporte;
            var branchType = "Sucursal " + Header.Sucursal;
            var periodType = "Periodo: " + Header.Fecha;

            if (Header.Sucursal == string.Empty || Header.Sucursal == "string")
            {
                branchType = Header.Sucursal = "Todas las Sucursales";
            }

            var printDate = "Fecha de impresión: " + DateTime.Now.ToString("dd/MM/yyyy");
            var logo = File.ReadAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets\\LabRamosLogo.png"));

            Table headerTable = new Table();

            Column headerColumn = headerTable.AddColumn();
            headerColumn.Width = contentWidth / 7 * 2;
            headerColumn.Format.Alignment = ParagraphAlignment.Center;

            Row headerRow = headerTable.AddRow();
            Paragraph headerParagraph = headerRow.Cells[0].AddParagraph();
            headerParagraph.AddFormattedText(Header.NombreReporte + "\n", fontTitle);
            headerParagraph.AddFormattedText(branchType + "\n", fontSubtitle);
            headerParagraph.AddFormattedText(periodType + "\n", fontSubtitle);
            if (!string.IsNullOrWhiteSpace(Header.Extra))
            {
                headerParagraph.AddFormattedText(Header.Extra + "\n", fontSubtitle);
            }
            headerParagraph.AddFormattedText(printDate, fontText);

            var headerInfo = new Col[]
            {
                new Col(logo, 3, ParagraphAlignment.Left)
                {
                    ImagenTamaño = Unit.FromCentimeter(5)
                },
                new Col("", 5, ParagraphAlignment.Right)
                {
                    Tabla = headerTable
                }
            };

            section.AddText(headerInfo);

            section.AddSpace();

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
                            if (item.ContainsKey("Children"))
                            {
                                cell.Borders.Bottom.Visible = false;
                            }
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

                    if (item.ContainsKey("Children"))
                    {
                        row = table.AddRow();
                        Cell cell = row.Cells[0];
                        cell.MergeRight = columns.Count - 1;

                        Table childrenTable = new Table();
                        childrenTable.Borders.Visible = false;
                        childrenTable.Borders.Width = 0;

                        var childrenData = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(item["Children"].ToString());

                        var noCol = childrenData.Max(x => x.Count);
                        var cellSize = (int)contentWidth / noCol;

                        for (int i = 0; i < noCol; i++)
                        {
                            Column column = childrenTable.AddColumn(cellSize + 1);
                        }

                        Dictionary<string, object> titles = new Dictionary<string, object>();

                        if (noCol == 3)
                        {
                            titles = new Dictionary<string, object>
                            {
                                {"Clave", "Clave"},
                                {"Estudio", "Estudio"},
                                {"Estatus", "Estatus"},
                            };
                        } 
                        else
                        {
                            titles = new Dictionary<string, object>
                        {
                            {"Clave", "Clave"},
                            {"Estudio", "Estudio"},
                            {"Precio", "Precio"},
                            {"Desc.", "Desc."},
                            {"Paquete", "Paquete"},
                            {"Promoción", "Promoción"},
                        };
                        }

                        childrenData.Insert(0, titles);

                        Row childrenRow = childrenTable.AddRow();
                        Cell titleCell_1 = childrenRow.Cells[0];
                        titleCell_1.Borders.Visible = false;
                        titleCell_1.MergeRight = noCol - 1;
                        var paragraphCell_1 = titleCell_1.AddParagraph();
                        paragraphCell_1.AddFormattedText("Estudio(s)", fontTitleChart);

                        for (int i = 0; i < childrenData.Count; i++)
                        {
                            childrenRow = childrenTable.AddRow();
                            childrenRow.Borders.Visible = false;

                            var values = childrenData[i].Values.ToList();

                            for (int j = 0; j < noCol; j++)
                            {
                                if (j < values.Count)
                                {
                                    Cell childrenCell = childrenRow.Cells[j];
                                    childrenCell.Borders.Visible = false;
                                    childrenCell.AddParagraph(values[j].ToString());
                                }
                            }
                        }

                        cell.Elements.Add(childrenTable);

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
            section.AddSpace(25);

            if (totales != null && totales.Count > 0)
            {
                Table totalTable = new Table();
                totalTable.Borders.Width = 0.75;
                totalTable.Borders.Color = Colors.LightGray;
                totalTable.TopPadding = 3;
                totalTable.BottomPadding = 3;
                totalTable.Rows.Alignment = RowAlignment.Right;

                if (totalColumns == null || totalColumns.Count == 0)
                {
                    return;
                }

                var totalWidth = contentWidth / totalColumns.Sum(x => x.Tamaño);

                for (int i = 0; i < totalColumns.Count; i++)
                {
                    Col item = columns[i];
                    Column totalColumn = totalTable.AddColumn("2.5cm");
                    totalColumn.Format.Alignment = item.Horizontal;
                }

                row = totalTable.AddRow();

                for (int i = 0; i < totalColumns.Count; i++)
                {
                    row.Shading.Color = Colors.Gray;
                    row.Format.Font.Color = Colors.White;

                    Cell cell = row.Cells[i];
                    cell.Borders.Left.Visible = false;
                    cell.Borders.Right.Visible = false;
                    cell.Borders.Bottom.Visible = false;
                    cell.AddParagraph(totalColumns[i].Texto);
                }

                row = totalTable.AddRow();

                for (int i = 0; i < totalColumns.Count; i++)
                {
                    var key = totalColumns[i].Texto;

                    if (totales.ContainsKey(key))
                    {
                        Cell cell = row.Cells[i];
                        cell.Borders.Visible = false;
                        var format = totalColumns[i].Formato;
                        var cellData = totales[key].ToString();

                        if (!string.IsNullOrWhiteSpace(format))
                        {
                            if (cellData.Length > 0 && cellData[0] == '[' && cellData[cellData.Length - 1] == ']')
                            {
                                var datalist = JsonConvert.DeserializeObject<List<string>>(cellData);
                                datalist.Where(x => x != null).ToList().ForEach(x => cell.AddParagraph(x));
                            }
                            else
                            {
                                cell.AddParagraph(Convert.ToDouble(totales[key]).ToString(format));
                            }
                        }
                        else
                        {
                            if (cellData.Length > 0 && cellData[0] == '[' && cellData[cellData.Length - 1] == ']')
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

                totalTable.Rows.Alignment = RowAlignment.Right;
                row = totalTable.AddRow();
                row.Borders.Visible = false;

                if (invoice != null)
                {
                    row = totalTable.AddRow();
                    row.Cells[0].Borders.Visible = false;
                    row.Cells[0].AddParagraph("Subtotal");
                    row.Cells[0].Format.Font.Bold = true;
                    row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
                    row.Cells[0].MergeRight = totales.Count() - 2;
                    row.Cells[totales.Count() - 1].AddParagraph(invoice.Subtotal.ToString("$" + "0.00"));

                    row = totalTable.AddRow();
                    row.Cells[0].Borders.Visible = false;
                    row.Cells[0].AddParagraph("IVA");
                    row.Cells[0].Format.Font.Bold = true;
                    row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
                    row.Cells[0].MergeRight = totales.Count() - 2;
                    row.Cells[totales.Count() - 1].AddParagraph(invoice.IVA.ToString("$" + "0.00"));

                    row = totalTable.AddRow();
                    row.Cells[0].Borders.Visible = false;
                    row.Cells[0].AddParagraph("Total");
                    row.Cells[0].Format.Font.Bold = true;
                    row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
                    row.Cells[0].MergeRight = totales.Count() - 2;
                    row.Cells[totales.Count() - 1].AddParagraph(invoice.Total.ToString("$" + "0.00"));
                }

                section.Add(totalTable);
            }

            if (seriesInfo == null || !seriesInfo.Any(x => x.SerieX) || data.Count == 0)
            {
                return;
            }

            section.AddSpace(25);

            Chart chart = new Chart();

            chart.Width = contentWidth;
            chart.Height = Unit.FromCentimeter(8);
            chart.Left = 0;



            foreach (var serie in seriesInfo.Where(x => !x.SerieX))
            {
                Series series = chart.SeriesCollection.AddSeries();
                series.ChartType = ChartType.Column2D;
                if (!string.IsNullOrWhiteSpace(serie.Color) && serie.Color.Length == 7 && serie.Color[0] == '#')
                {
                    var r = Convert.ToByte(serie.Color.Substring(1, 2), 16);
                    var g = Convert.ToByte(serie.Color.Substring(3, 2), 16);
                    var b = Convert.ToByte(serie.Color.Substring(5, 2), 16);
                    series.FillFormat.Color = Color.FromRgb(r, g, b);
                }
                else
                {
                    series.FillFormat.Color = Color.FromRgb(Convert.ToByte("18", 16), Convert.ToByte("90", 16), Convert.ToByte("FF", 16));
                }
                if (datachart != null && datachart.Count > 0)
                {
                    series.Add(datachart.Select(x => Convert.ToDouble(x[serie.Serie])).ToArray());
                }
                else
                {
                    series.Add(data.Select(x => Convert.ToDouble(x[serie.Serie])).ToArray());
                }
                series.HasDataLabel = true;
                series.DataLabel.Format = serie.Formato;
                series.DataLabel.Position = DataLabelPosition.OutsideEnd;
                series.Name = serie.Serie;

                var elements = series.Elements.Cast<Point>().ToArray();

                if (datachart != null && datachart.All(x => x.ContainsKey("Color")))
                {

                    for (int i = 0; i < datachart.Count; i++)
                    {
                        var color = datachart[i]["Color"].ToString();
                        var r = Convert.ToByte(color.Substring(1, 2), 16);
                        var g = Convert.ToByte(color.Substring(3, 2), 16);
                        var b = Convert.ToByte(color.Substring(5, 2), 16);
                        elements[i].FillFormat.Color = Color.FromRgb(r, g, b);
                    }
                }
                else
                {
                    chart.TopArea.AddLegend();
                }
            }

            var serieX = seriesInfo.FirstOrDefault(s => s.SerieX)?.Serie;

            XSeries xseries = chart.XValues.AddXSeries();
            if (datachart != null && datachart.Count > 0)
            {
                xseries.Add(datachart.Select((x, i) => x[serieX].ToString() ?? "S-" + i).ToArray());
            }
            else
            {
                xseries.Add(data.Select((x, i) => x[serieX].ToString() ?? "S-" + i).ToArray());
            }

            chart.XAxis.MajorTickMark = TickMarkType.Outside;
            chart.XAxis.Title.Caption = serieX ?? "Serie X";
            chart.XAxis.LineFormat.Color = Colors.LightGray;

            chart.YAxis.MajorTickMark = TickMarkType.Outside;
            chart.YAxis.HasMajorGridlines = true;
            chart.YAxis.MajorGridlines.LineFormat.Color = Colors.LightGray;
            chart.YAxis.LineFormat.Color = Colors.LightGray;

            chart.PlotArea.LineFormat.Color = Colors.LightGray;
            chart.PlotArea.LineFormat.Width = 1;

            section.Add(chart);
        }
    }
}