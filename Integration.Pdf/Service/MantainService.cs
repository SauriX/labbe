using Integration.Pdf.Dtos;
using Integration.Pdf.Extensions;
using Integration.Pdf.Models;
using MigraDoc.DocumentObjectModel;
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

            Format(section, order);

            return document;
        }

        static void Format(Section section, MantainDto order)
        {
            var title = new Col("Laboratorio Alfonso Ramos S.A. de C.V. (HERMOSILLO)", new Font("Calibri", 11) { Bold = true }, ParagraphAlignment.Right);
            section.AddText(title);

            section.AddSpace();

             
            var line1 = new Col[]
            {
                new Col("Clave:", 3, ParagraphAlignment.Left),
                new Col($": {order.Clave}", 8, Col.FONT_BOLD, ParagraphAlignment.Left),
                new Col("", 1),
                new Col("Nombre:", 3, ParagraphAlignment.Left),
                new Col($": {order.Fecha}", 4, Col.FONT_BOLD, ParagraphAlignment.Left),
                new Col("", 1),
                new Col("Serie:", 1, ParagraphAlignment.Left),
                new Col($": {order.No_serie}", 3, Col.FONT_BOLD, ParagraphAlignment.Left)
            };
            section.AddSpace();
            section.AddBorderedText(line1, right: true, left: true);
            var observationTitle = new Col("Observaciones", new Font("Calibri", 11) { Bold = true }, ParagraphAlignment.Center);
            section.AddText(observationTitle);
            var observation = new Col(order.Descripcion, new Font("Calibri", 11) { Bold = true }, ParagraphAlignment.Center);
            section.AddText(observation);
            var images = new Col("Imagenes", new Font("Calibri", 11) { Bold = true }, ParagraphAlignment.Center);
            section.AddText(observation);



            if (order.imagenUrl.Count()>0) {
                foreach (var image in order.imagenUrl) {
                    var img = $"wwwroot/images/mantain{image.ImagenUrl}";
                    var path = AppDomain.CurrentDomain.BaseDirectory;
                    path = path.ToString().Replace("\\Integration.Pdf\\", "\\Service.Catalog\\");
                    var logo = $"{path}{img}";

                    var webClient = new WebClient();
                    byte[] imageBytes = webClient.DownloadData(logo);

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