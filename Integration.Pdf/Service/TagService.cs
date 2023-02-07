using Integration.Pdf.Dtos;
using Integration.Pdf.Extensions;
using Integration.Pdf.Models;
using Integration.Pdf.Utils;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.Rendering;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using ZXing;
using Font = MigraDoc.DocumentObjectModel.Font;

namespace Integration.Pdf.Service
{
    public class TagService
    {
        public static byte[] Generate(List<RequestTagDto> tags)
        {
            Document document = CreateDocument(tags);

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

        static Document CreateDocument(List<RequestTagDto> tags)
        {
            Document document = new Document();

            Section section = document.AddSection();

            section.PageSetup = document.DefaultPageSetup.Clone();

            section.PageSetup.Orientation = Orientation.Portrait;
            section.PageSetup.PageWidth = Unit.FromCentimeter(3.81);
            section.PageSetup.PageHeight = Unit.FromCentimeter(2.54);

            section.PageSetup.TopMargin = Unit.FromMillimeter(1.8);
            section.PageSetup.BottomMargin = Unit.FromMillimeter(0);
            section.PageSetup.LeftMargin = Unit.FromMillimeter(2);
            section.PageSetup.RightMargin = Unit.FromMillimeter(2);

            section.PageSetup.FooterDistance = 0;

            Format(section, tags);

            return document;
        }

        static void Format(Section section, List<RequestTagDto> tags)
        {
            var footer = section.Footers.Primary;
            footer.AddText(new Col("Laboratorio Ramos", new Font("Arial", 4) { Italic = true, Bold = true }, ParagraphAlignment.Left), spaceAfter: 0);

            var date = DateTime.Now.ToString("HH:mm:ss");

            for (int i = 0; i < tags.Count; i++)
            {
                RequestTagDto tag = tags[i];

                for (int j = 0; j < tag.Cantidad; j++)
                {
                    byte[] barcodeImage = BarCode.Generate(tag.Clave.Substring(0, 4), 320, 60);
                    string imageFilename = barcodeImage.MigraDocFilenameFromByteArray();

                    var imgPar = section.AddParagraph();
                    imgPar.Format.Alignment = ParagraphAlignment.Center;
                    var barcode = imgPar.AddImage(imageFilename);
                    barcode.Width = Unit.FromCentimeter(4.8);

                    var orderNo = new Col(tag.Clave, new Font("Arial", 10) { Bold = true });
                    section.AddText(orderNo, spaceAfter: 0);

                    //var patient = new Col(tag.Paciente, new Font("Arial", 5) { Bold = true }, ParagraphAlignment.Left);
                    //section.AddText(patient, spaceAfter: 0);

                    //var studies = new Col(tag.Estudios, new Font("Arial", 5) { Bold = true }, ParagraphAlignment.Left);
                    //section.AddText(studies, spaceAfter: 0);

                    var textFrame1 = section.AddTextFrame();
                    textFrame1.RelativeHorizontal = RelativeHorizontal.Page;
                    textFrame1.RelativeVertical = RelativeVertical.Page;

                    textFrame1.WrapFormat.DistanceLeft = Unit.FromMillimeter(2);
                    textFrame1.WrapFormat.DistanceTop = Unit.FromCentimeter(1.54);

                    textFrame1.Width = Unit.FromCentimeter(2.81);
                    textFrame1.Height = Unit.FromCentimeter(1);

                    var textFrame1Par = textFrame1.AddParagraph();
                    textFrame1Par.AddFormattedText(tag.Paciente, new Font("Arial", 4) { Bold = true });
                    textFrame1Par.AddLineBreak();
                    textFrame1Par.AddFormattedText(tag.Estudios, new Font("Arial", 4) { Bold = true });

                    var textFrame = section.AddTextFrame();
                    textFrame.RelativeHorizontal = RelativeHorizontal.Page;
                    textFrame.RelativeVertical = RelativeVertical.Page;

                    textFrame.WrapFormat.DistanceLeft = Unit.FromCentimeter(2.81);
                    textFrame.WrapFormat.DistanceTop = Unit.FromCentimeter(1.54);

                    textFrame.Width = Unit.FromCentimeter(1);
                    textFrame.Height = Unit.FromCentimeter(1);

                    var textFramePar = textFrame.AddParagraph();
                    textFramePar.AddFormattedText($"ORI90\n{tag.Ciudad}\nSBAUTISTA\n{date}\nNormal\n{tag.EdadSexo}", new Font("Arial", 4) { Bold = true });

                    if (i < tags.Count - 1)
                    {
                        section.AddPageBreak();
                    }
                }
            }
        }
    }
}