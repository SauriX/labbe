using Integration.Pdf.Dtos;
using Integration.Pdf.Extensions;
using Integration.Pdf.Models;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using System;
using System.Collections.Generic;

using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using ZXing;

namespace Integration.Pdf.Service
{ 
    public class MantainService
    {
        public static byte[] Generate(MantainDto order)
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

        static Document CreateDocument(MantainDto order)
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

            Format(section, order,order.Header);

            return document;
        }

        static void Format(Section section, MantainDto order, HeaderData Header)
        {
 var fontTitle = new Font("calibri", 14);
            var fontSubtitle = new Font("calibri", 12);
            var fontTitleChart = new Font("calibri", 11) { Bold = true };
            var fontText = new Font("calibri", 10);

            var contentWidth = section.PageSetup.PageHeight - section.PageSetup.LeftMargin - section.PageSetup.RightMargin;

            var title = "Formato Mantenimiento";
            var branchType = "Sucursal " + Header.Sucursal;
            

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
            headerParagraph.AddFormattedText(title + "\n", fontTitle);
            headerParagraph.AddFormattedText(branchType + "\n", fontSubtitle);
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



            var line1 = new Col[]
            {
                new Col($"Clave: {order.Clave}", 3, ParagraphAlignment.Left),
                new Col("", 1),
                new Col($"Fecha:{order.Fecha}", 3, ParagraphAlignment.Left),
                new Col($"Serie:{order.No_serie}", 1, ParagraphAlignment.Left),
            };
            section.AddText(line1);
            section.AddSpace();
            var observationTitle = new Col("Observaciones", new Font("Calibri", 11) { Bold = true }, ParagraphAlignment.Center);
            section.AddText(observationTitle);
            var observation = new Col(order.Descripcion, new Font("Calibri", 11) { Bold = true }, ParagraphAlignment.Justify);
            section.AddText(observation);
            var images = new Col("Imagenes", new Font("Calibri", 11) { Bold = true }, ParagraphAlignment.Center);
            section.AddText(observation);



            if (order.imagenUrl.Count()>0) {
                foreach (var image in order.imagenUrl) {


                    var webClient = new WebClient();
                    byte[] imageBytes = webClient.DownloadData(image.ImagenUrl);

                    /* System.Drawing.ImageConverter converter = new System.Drawing.ImageConverter();
                     var image = (byte[])converter.ConvertTo(new System.Drawing.Bitmap(imageBytes), typeof(byte[]));*/

                    string imageFilename = MigraDocFilenameFromByteArray(imageBytes);

                    var imgPar = section.AddParagraph();
                    imgPar.Format.Alignment = ParagraphAlignment.Center;
                    imgPar.AddImage(imageFilename);
                }

            }

           

          //  section.AddImage(imageFilename);



        }
        static string MigraDocFilenameFromByteArray(byte[] image)
        {
            return "base64:" + Convert.ToBase64String(image);
        }
    }
}