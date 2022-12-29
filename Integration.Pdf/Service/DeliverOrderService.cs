using Integration.Pdf.Dtos;
using Integration.Pdf.Dtos.DeliverOrder;
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
    public class DeliverOrderService
    {
        public static byte[] Generate(DeliverOrderdDto order)
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

        static Document CreateDocument(DeliverOrderdDto order)
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

            Format(section, order,order.Datos);

            return document;
        }

        static void Format(Section section, DeliverOrderdDto order, List<Dictionary<string, object>> data)
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
            var titledoc = new Col($"Formato orden de entrega", new Font("Calibri", 11) { Bold = true }, ParagraphAlignment.Center);
            section.AddText(titledoc);

            section.AddSpace();

            var titledestin = new Col($"DESTINATARIO", new Font("Calibri", 11) { Bold = true }, ParagraphAlignment.Center);
            section.AddText(titledestin);

            section.AddSpace();
            //tabla destino
            List<Col> columnsDestino = new List<Col>()
            {
                new Col("Destino", ParagraphAlignment.Left),
                new Col("Responsable de recibido", ParagraphAlignment.Left),
                new Col("Fecha y hora de entrega estimada", ParagraphAlignment.Left),
                new Col("Fecha y hora de entrega real", ParagraphAlignment.Left),
                new Col("Firma", ParagraphAlignment.Left)
,
            };
            var destino = new Dictionary<string, object>{
                { "Destino", order.Destino },
                { "Responsable de recibido", "" },
                { "Fecha y hora de entrega estimada",order.FechaEntestimada },
                { "Fecha y hora de entrega real", ""},
                { "Firma", ""},
            };

            var datadestino = new List <Dictionary<string, object>>();
            datadestino.Add(destino);

            Table tableDestino = new Table();
            tableDestino.Borders.Width = 0.75;
            tableDestino.Borders.Color = Colors.LightGray;
            tableDestino.TopPadding = 3;
            tableDestino.BottomPadding = 3;
            var contentWidthDestino = section.PageSetup.PageHeight - section.PageSetup.LeftMargin - section.PageSetup.RightMargin;
            var colWidthDestino = contentWidthDestino / columnsDestino.Sum(x => x.Tamaño);

            if (columnsDestino == null || columnsDestino.Count == 0)
            {
                return;
            }

            for (int i = 0; i < columnsDestino.Count; i++)
            {
                Col item = columnsDestino[i];
                Column column = tableDestino.AddColumn(colWidthDestino * item.Tamaño);
                column.Format.Alignment = item.Horizontal;
            }

            Row rowDestino = tableDestino.AddRow();

            for (int i = 0; i < columnsDestino.Count; i++)
            {
                rowDestino.Shading.Color = Colors.Gray;
                rowDestino.Format.Font.Color = Colors.White;

                Cell cellDestino = rowDestino.Cells[i];
                cellDestino.Borders.Left.Visible = false;
                cellDestino.Borders.Right.Visible = false;
                cellDestino.Borders.Bottom.Visible = false;
                cellDestino.AddParagraph(columnsDestino[i].Texto);
            }

            if (datadestino != null && datadestino.Count > 0)
            {
                foreach (var item in datadestino)
                {
                    rowDestino = tableDestino.AddRow();
                    for (int i = 0; i < columnsDestino.Count; i++)
                    {
                        var key = columnsDestino[i].Texto;

                        if (item.ContainsKey(key))
                        {
                            Cell cellDestino = rowDestino.Cells[i];
                            cellDestino.Borders.Left.Visible = false;
                            cellDestino.Borders.Right.Visible = false;

                            var format = columnsDestino[i].Formato;
                            var cellData = item[key].ToString();

                            if (!string.IsNullOrWhiteSpace(format))
                            {
                                if (cellData[0] == '[' && cellData[cellData.Length - 1] == ']')
                                {
                                    var datalist = JsonConvert.DeserializeObject<List<string>>(cellData);
                                    datalist.Where(x => x != null).ToList().ForEach(x => cellDestino.AddParagraph(x));
                                }
                                else
                                {
                                    cellDestino.AddParagraph(Convert.ToDouble(item[key]).ToString(format));
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
                                    datalist.Where(x => x != null).ToList().ForEach(x => cellDestino.AddParagraph(x));
                                }
                                else
                                {
                                    cellDestino.AddParagraph(cellData);
                                }
                            }
                        }
                    }
                }

                tableDestino.Rows.Alignment = RowAlignment.Center;
            }
            else
            {
                rowDestino = tableDestino.AddRow();

                Cell cell = rowDestino.Cells[0];
                cell.MergeRight = columnsDestino.Count - 1;
                cell.Borders.Left.Visible = false;
                cell.Borders.Right.Visible = false;
                cell.Borders.Bottom.Visible = false;
                cell.Format.Alignment = ParagraphAlignment.Center;

                cell.AddParagraph("No hay registros");
            }
            section.Add(tableDestino);
            section.AddSpace(15);
            //data origen
            var titleorigen = new Col($"ORIGEN", new Font("Calibri", 11) { Bold = true }, ParagraphAlignment.Center);
            List<Col> columnsOrigen = new List<Col>()
            {
                new Col("Origen", ParagraphAlignment.Left),
                new Col("Responsable de envío", ParagraphAlignment.Left),
                new Col("Nombre del transportista", ParagraphAlignment.Left),
                new Col("Medio de entrega", ParagraphAlignment.Left),
                new Col("Fecha y hora de envio", ParagraphAlignment.Left),
                new Col("Firma", ParagraphAlignment.Left)
,
            };
            var origen = new Dictionary<string, object>{
                { "Origen", order.Destino },
                { "Responsable de envío",order.ResponsableEnvio },
                { "Nombre del transportista",order.TransportistqName },
                { "Medio de entrega", "Transporte local"},
                { "Fecha y hora de envio", order.FechaEntestimada},
                { "Firma", ""},
            };

            var dataorigen = new List<Dictionary<string, object>>();
            dataorigen.Add(origen);

            Table tableOrigen = new Table();
            tableOrigen.Borders.Width = 0.75;
            tableOrigen.Borders.Color = Colors.LightGray;
            tableOrigen.TopPadding = 3;
            tableOrigen.BottomPadding = 3;
            var contentWidthOrigen = section.PageSetup.PageHeight - section.PageSetup.LeftMargin - section.PageSetup.RightMargin;
            var colWidthOrigen = contentWidthOrigen / columnsOrigen.Sum(x => x.Tamaño);

            if (columnsOrigen == null || columnsOrigen.Count == 0)
            {
                return;
            }

            for (int i = 0; i < columnsOrigen.Count; i++)
            {
                Col item = columnsOrigen[i];
                Column column = tableOrigen.AddColumn(colWidthOrigen * item.Tamaño);
                column.Format.Alignment = item.Horizontal;
            }

            Row rowOrigen = tableOrigen.AddRow();

            for (int i = 0; i < columnsOrigen.Count; i++)
            {
                rowOrigen.Shading.Color = Colors.Gray;
                rowOrigen.Format.Font.Color = Colors.White;

                Cell cellOrigen = rowOrigen.Cells[i];
                cellOrigen.Borders.Left.Visible = false;
                cellOrigen.Borders.Right.Visible = false;
                cellOrigen.Borders.Bottom.Visible = false;
                cellOrigen.AddParagraph(columnsOrigen[i].Texto);
            }

            if (dataorigen != null && dataorigen.Count > 0)
            {
                foreach (var item in dataorigen)
                {
                    rowOrigen = tableOrigen.AddRow();
                    for (int i = 0; i < columnsOrigen.Count; i++)
                    {
                        var key = columnsOrigen[i].Texto;

                        if (item.ContainsKey(key))
                        {
                            Cell cellOrigen = rowOrigen.Cells[i];
                            cellOrigen.Borders.Left.Visible = false;
                            cellOrigen.Borders.Right.Visible = false;

                            var format = columnsOrigen[i].Formato;
                            var cellData = item[key].ToString();

                            if (!string.IsNullOrWhiteSpace(format))
                            {
                                if (cellData[0] == '[' && cellData[cellData.Length - 1] == ']')
                                {
                                    var datalist = JsonConvert.DeserializeObject<List<string>>(cellData);
                                    datalist.Where(x => x != null).ToList().ForEach(x => cellOrigen.AddParagraph(x));
                                }
                                else
                                {
                                    cellOrigen.AddParagraph(Convert.ToDouble(item[key]).ToString(format));
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
                                    datalist.Where(x => x != null).ToList().ForEach(x => cellOrigen.AddParagraph(x));
                                }
                                else
                                {
                                    cellOrigen.AddParagraph(cellData);
                                }
                            }
                        }
                    }
                }

                tableOrigen.Rows.Alignment = RowAlignment.Center;
            }
            else
            {
                rowOrigen = tableOrigen.AddRow();

                Cell cell = rowOrigen.Cells[0];
                cell.MergeRight = columnsOrigen.Count - 1;
                cell.Borders.Left.Visible = false;
                cell.Borders.Right.Visible = false;
                cell.Borders.Bottom.Visible = false;
                cell.Format.Alignment = ParagraphAlignment.Center;

                cell.AddParagraph("No hay registros");
            }
            section.Add(tableOrigen);
            section.AddSpace(15);

            //tabla data
            List<Col> columns = order.Columnas;
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
                                var CelldataValidationInicial = ' ';
                                var CelldataValidationFinal = ' ' ;

                                if (cellData.Length > 0)
                                {
                                    CelldataValidationInicial = cellData[0];
                                    CelldataValidationFinal = cellData[cellData.Length - 1];
                                }

                                if (CelldataValidationInicial == '[' && CelldataValidationFinal == ']')
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
            section.AddSpace(15);

        }

    }
}
