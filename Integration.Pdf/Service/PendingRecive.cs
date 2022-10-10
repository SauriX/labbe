using Integration.Pdf.Dtos.PendingRecive;
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
namespace Integration.Pdf.Service
{
    public class PendingRecive
    {
        public static byte[] Generate(List<PendingReciveDto> order)
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

        static Document CreateDocument(List<PendingReciveDto> order)
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

        static void Format(Section section,  List<PendingReciveDto> data)
        {
            var title = new Col("Laboratorio Alfonso Ramos S.A. de C.V. (HERMOSILLO)", new Font("Calibri", 11) { Bold = true }, ParagraphAlignment.Right);
            section.AddText(title);
            section.AddSpace(10);

            List<Col> columns = new List<Col>();
            columns.Add(new Col {Texto="#Numero de seguimiento" });
            columns.Add(new Col { Texto = "Clave de ruta" });
            columns.Add(new Col { Texto = "Sucursal de procedencia" });
            columns.Add(new Col { Texto = "Fecha de entrega" });
            columns.Add(new Col { Texto = "Hora de entrega" });
            columns.Add(new Col { Texto = "Fecha y hora real" });

            var contentWidth = section.PageSetup.PageWidth - section.PageSetup.LeftMargin - section.PageSetup.RightMargin;

            Table table = new Table();
            table.Borders.Width = 0.75;
            table.Borders.Color = Colors.LightGray;
            table.TopPadding = 3;
            table.BottomPadding = 3;
            Row row = table.AddRow();
            if (data != null && data.Count > 0)
            {
                foreach (var item in data)
                {
                    row = table.AddRow();
                    for (int i = 0; i < columns.Count; i++)
                    {
                        var key = columns[i].Texto;

                            
                            Cell cell = row.Cells[i];
                            cell.Borders.Left.Visible = false;
                            cell.Borders.Right.Visible = false;

                            var format = columns[i].Formato;
                            var cellData ="";

                            if (!string.IsNullOrWhiteSpace(format))
                            {
                                if (cellData[0] == '[' && cellData[cellData.Length - 1] == ']')
                                {
                                    var datalist = JsonConvert.DeserializeObject<List<string>>(cellData);
                                    datalist.Where(x => x != null).ToList().ForEach(x => cell.AddParagraph(x));
                                }
                                else
                                {
                                    cell.AddParagraph(Convert.ToDouble("").ToString(format));
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

                    if (item.Study.Count>0)
                    {
                        row = table.AddRow();
                        Cell cell = row.Cells[0];
                        cell.MergeRight = columns.Count - 1;

                        Table childrenTable = new Table();
                        childrenTable.Borders.Visible = false;
                        childrenTable.Borders.Width = 0;

                        cell.Borders.Visible = false;

                        var childrenData = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>("");

                        var noCol = childrenData.Max(x => x.Count);

                        for (int i = 0; i < noCol; i++)
                        {
                            Column column = childrenTable.AddColumn(contentWidth / noCol);
                        }

                        Row childrenRow = childrenTable.AddRow();
                        Cell titleCell = childrenRow.Cells[0];
                        titleCell.Borders.Visible = false;
                        titleCell.MergeRight = noCol - 1;
                        var paragraph = titleCell.AddParagraph();
                        paragraph.AddFormattedText("Estudio");

                        for (int i = 0; i < childrenData.Count; i++)
                        {
                            childrenRow = childrenTable.AddRow();
                            childrenRow.Borders.Visible = false;

                            var values = childrenData[i].Values.ToList();

                            for (int j = 0; j < values.Count; j++)
                            {
                                Cell childrenCell = childrenRow.Cells[j];
                                childrenCell.Borders.Visible = false;

                                childrenCell.AddParagraph(values[j].ToString());
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




            //  section.AddImage(imageFilename);



        }
    }
}