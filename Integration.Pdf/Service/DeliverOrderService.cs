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

            section.PageSetup.Orientation = Orientation.Portrait;
            section.PageSetup.PageFormat = PageFormat.A4;

            section.PageSetup.TopMargin = Unit.FromCentimeter(1);
            section.PageSetup.BottomMargin = Unit.FromCentimeter(1);
            section.PageSetup.LeftMargin = Unit.FromCentimeter(1);
            section.PageSetup.RightMargin = Unit.FromCentimeter(1);

            Format(section, order);

            return document;
        }

        static void Format(Section section, DeliverOrderdDto order)
        {
            var title = new Col($"Laboratorio Alfonso Ramos S.A. de C.V.", new Font("Calibri", 11) { Bold = true }, ParagraphAlignment.Right);
            section.AddText(title);

            section.AddSpace();
            var titledoc = new Col($"Formato orden de entrega", new Font("Calibri", 11) { Bold = true }, ParagraphAlignment.Right);
            section.AddText(titledoc);

            section.AddSpace();



            var line1 = new Col[]
            {
                  new Col($"DESTINATARIO", new Font("Calibri", 11) { Bold = true }, ParagraphAlignment.Center),
                new Col("Destino:", 3, ParagraphAlignment.Left),
                new Col($": {order.Destino}", 8, Col.FONT_BOLD, ParagraphAlignment.Left),
                new Col("", 1),
                new Col("Responsable de recibido:", 3, ParagraphAlignment.Left),
                new Col($": ", 4, Col.FONT_BOLD, ParagraphAlignment.Left),
                new Col("", 1),
                new Col("Fecha y hora de entrega estimada:", 1, ParagraphAlignment.Left),
                new Col($": {order.FechaEntestimada}", 3, Col.FONT_BOLD, ParagraphAlignment.Left),
                new Col("", 1),
                new Col("Fecha y hora de entrega real:", 1, ParagraphAlignment.Left),
                new Col($": ", 3, Col.FONT_BOLD, ParagraphAlignment.Left),
                new Col("", 1),
                new Col("Firma", 1, ParagraphAlignment.Left),
                new Col("", 1),
            };
            section.AddSpace();
            section.AddBorderedText(line1, right: true, left: true);

            var lineOrigen = new Col[]
            {
                  new Col($"ORIGEN", new Font("Calibri", 11) { Bold = true }, ParagraphAlignment.Center),
                new Col("Origen:", 3, ParagraphAlignment.Left),
                new Col($": {order.Destino}", 8, Col.FONT_BOLD, ParagraphAlignment.Left),
                new Col("", 1),
                new Col("Responsable de envío:", 3, ParagraphAlignment.Left),
                new Col($": {order.ResponsableEnvio}", 4, Col.FONT_BOLD, ParagraphAlignment.Left),
                new Col("", 1),
                new Col("Nombre del transportista:", 1, ParagraphAlignment.Left),
                new Col($": {order.TransportistqName}", 3, Col.FONT_BOLD, ParagraphAlignment.Left),
                new Col("", 1),
                new Col("Medio de entrega:", 1, ParagraphAlignment.Left),
                new Col($":Transporte local ", 3, Col.FONT_BOLD, ParagraphAlignment.Left),
                new Col("", 1),
                new Col("Fecha y hora de envio:", 1, ParagraphAlignment.Left),
                new Col($": {order.FechaEntestimada}", 3, Col.FONT_BOLD, ParagraphAlignment.Left),
                new Col("", 1),
                new Col("Firma", 1, ParagraphAlignment.Left),
                new Col("", 1),
            };
            section.AddSpace();
            section.AddBorderedText(line1, right: true, left: true);

            List<Col> columns = order.Columnas;
            var contentWidth = section.PageSetup.PageWidth - section.PageSetup.LeftMargin - section.PageSetup.RightMargin;

            Table table = new Table();
            table.Borders.Width = 0.75;
            table.Borders.Color = Colors.LightGray;
            table.TopPadding = 3;
            table.BottomPadding = 3;
            Row row = table.AddRow();
            if (order.Datos!= null && order.Datos.Count > 0)
            {
                foreach (var item in order.Datos)
                {
                    row = table.AddRow();
                    for (int i = 0; i < columns.Count; i++)
                    {
                        var key = columns[i].Texto;


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