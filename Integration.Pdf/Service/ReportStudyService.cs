using Integration.Pdf.Dtos;
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
    public class ReportStudyService
    {
        private static TextFormat fontTitleChart;

        public static byte[] Generate(List<ReportRequestListDto> order)
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

        static Document CreateDocument(List<ReportRequestListDto> order)
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

            Format(section, order);

            return document;
        }

        static void Format(Section section, List<ReportRequestListDto> order)
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
            var titledoc = new Col($"Listado de  Solicitudes del día", new Font("Calibri", 11) { Bold = true }, ParagraphAlignment.Center);
            section.AddText(titledoc);

            section.AddSpace();

            var titlefecha = new Col($"DEL {order.First().Fechas.First().Date} AL {order.First().Fechas.Last().Date}", new Font("Calibri", 11) { Bold = true }, ParagraphAlignment.Center);
            section.AddText(titlefecha);


            section.AddSpace();

            List<Col> columns = new List<Col>()
            {
                new Col("Solicitud", ParagraphAlignment.Left),
                new Col("Nombre del paciente", ParagraphAlignment.Left),
                new Col("Edad", ParagraphAlignment.Left),
                new Col("Sexo", ParagraphAlignment.Left),
                new Col("Nombre del medico", ParagraphAlignment.Left),
                new Col("Fecha de entrega", ParagraphAlignment.Left),
            };
            var data = order.Select(x => new Dictionary<string, object>
            {
                { "Solicitud", x.Solicitud},
                { "Nombre del paciente", x.Paciente },
                { "Edad", x.Edad },
                { "Sexo", x.Sexo},
                { "Nombre del medico", x.Medico },
                { "Fecha de entrega", x.Entrega },
                { "Children",  JsonConvert.SerializeObject( x.Estudios.Select(y => new Dictionary<string, object> {
                        { "Clave", y.Clave},{"Estudio",y.Nombre},{"Estatus",y.Estatus}, {"Fecha de entrega",y.Fecha },{"Sucursal",x.Sucursal }
                    }))
      
                },
            }).ToList();

            //tabla data

            Table table = new Table();
            table.Borders.Width = 0.75;
            table.Borders.Color = Colors.LightGray;
            table.TopPadding = 3;
            table.BottomPadding = 3;
            var contentWidth = section.PageSetup.PageHeight - section.PageSetup.LeftMargin - section.PageSetup.RightMargin;
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

                        cell.Borders.Visible = false;

                        var childrenData = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(item["Children"].ToString());

                        var noCol = childrenData.Max(x => x.Count);
                        var cellSize = (int)contentWidth / noCol;

                        for (int i = 0; i < noCol; i++)
                        {
                            Column column = childrenTable.AddColumn(contentWidth / noCol + 1);
                        }

                        Dictionary<string, object> titles = new Dictionary<string, object>
                        {
                            {"Clave", "Clave"},
                            {"Estudio", "Estudio"},
                            {"Estatus", "Estatus"},
                            {"Fecha de entrega", "Fecha de entrega"},
                            {"Sucursal", "Sucursal"},
                        };

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



        }

    }
}