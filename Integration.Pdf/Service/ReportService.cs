using Integration.Pdf.Extensions;
using Integration.Pdf.Models;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.DocumentObjectModel.Shapes.Charts;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Web;

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

            section.PageSetup.Orientation = Orientation.Portrait;
            section.PageSetup.PageFormat = PageFormat.A4;

            section.PageSetup.TopMargin = Unit.FromCentimeter(1);
            section.PageSetup.BottomMargin = Unit.FromCentimeter(1);
            section.PageSetup.LeftMargin = Unit.FromCentimeter(1);
            section.PageSetup.RightMargin = Unit.FromCentimeter(1);

            Format(section, reportData.Columnas, reportData.Series, reportData.Datos, reportData.Header);

            return document;
        }

        static void Format(Section section, List<Models.Col> columns, List<ChartSeries> seriesInfo, List<Dictionary<string, object>> data, HeaderData Header)
        {
            var fontTitle = new Font("calibri", 20);
            var fontSubtitle = new Font("calibri", 16);
            var fontText = new Font("calibri", 12);
            var title = new Col(Header.NombreReporte, fontTitle);
            var branchType = "Sucursal " + Header.Sucursal;

            if(Header.Sucursal == string.Empty || Header.Sucursal == "string")
            {
                branchType = Header.Sucursal = "Todas las Sucursales";
            }

            var branch = new Col(branchType, fontSubtitle);
            var period = new Col("Periodo: " + Header.Fecha, fontSubtitle);
            var printDate = new Col("Fecha de impresión: " + DateTime.Now.ToString("dd/MM/yyyy"), fontText, ParagraphAlignment.Right);
            var logo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets\\LabRamosLogo.png");
         
            section.AddImage(logo);
            section.AddText(title);
            section.AddSpace(10);
            section.AddText(branch);
            section.AddText(period);
            section.AddSpace(5);
            section.AddText(printDate);
            section.AddSpace(10);

            var contentWidth = section.PageSetup.PageWidth - section.PageSetup.LeftMargin - section.PageSetup.RightMargin;

            Table table = new Table();
            table.Borders.Width = 0.75;
            table.Borders.Color = Colors.LightGray;
            table.TopPadding = 3;
            table.BottomPadding = 3;

            var colWidth = contentWidth / columns.Sum(x => x.Tamaño);

            if (columns.Count == 0)
            {
                return;
            }

            for (int i = 0; i < columns.Count; i++)
            {
                Models.Col item = columns[i];
                MigraDoc.DocumentObjectModel.Tables.Column column = table.AddColumn(colWidth * item.Tamaño);
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
                cell.AddParagraph(columns[i].Texto);
            }

            if (data.Count > 0)
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

                            if (!string.IsNullOrWhiteSpace(format))
                            {
                                cell.AddParagraph(Convert.ToDouble(item[key]).ToString(format));
                            }
                            else
                            {
                                cell.AddParagraph(item[key].ToString());
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
                cell.Format.Alignment = ParagraphAlignment.Center;

                cell.AddParagraph("No hay registros");
            }

            section.Add(table);

            if (!seriesInfo.Any(x => x.SerieX) || data.Count == 0)
            {
                return;
            }

            section.AddSpace(25);

            Chart chart = new Chart();

            chart.Width = contentWidth;
            chart.Height = Unit.FromCentimeter(8);
            chart.Left = 0;

            chart.TopArea.AddLegend();

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
                series.Add(data.Select(x => Convert.ToDouble(x[serie.Serie])).ToArray());
                series.HasDataLabel = true;
                series.DataLabel.Format = serie.Formato;
                series.DataLabel.Position = DataLabelPosition.OutsideEnd;
                series.Name = serie.Serie;
            }

            var serieX = seriesInfo.FirstOrDefault(s => s.SerieX)?.Serie;

            XSeries xseries = chart.XValues.AddXSeries();
            xseries.Add(data.Select((x, i) => x[serieX].ToString() ?? "S-" + i).ToArray());

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