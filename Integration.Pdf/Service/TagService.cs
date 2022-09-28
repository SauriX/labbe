using Integration.Pdf.Dtos;
using Integration.Pdf.Extensions;
using Integration.Pdf.Models;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using System.Collections.Generic;
using System.IO;

namespace Integration.Pdf.Service
{
    public class TagService
    {
        public static byte[] Generate(List<RequestTagDto> tags)
        {
            Document document = CreateDocument(tags);

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

        static Document CreateDocument(List<RequestTagDto> tags)
        {
            Document document = new Document();

            Section section = document.AddSection();

            section.PageSetup = document.DefaultPageSetup.Clone();

            section.PageSetup.Orientation = Orientation.Portrait;
            section.PageSetup.PageWidth = Unit.FromCentimeter(3.81);
            section.PageSetup.PageHeight = Unit.FromCentimeter(2.54);

            section.PageSetup.TopMargin = Unit.FromMillimeter(1);
            section.PageSetup.BottomMargin = Unit.FromMillimeter(1);
            section.PageSetup.LeftMargin = Unit.FromMillimeter(1);
            section.PageSetup.RightMargin = Unit.FromMillimeter(1);

            Format(section, tags);

            return document;
        }

        static void Format(Section section, List<RequestTagDto> tags)
        {
            for (int i = 0; i < tags.Count; i++)
            {
                RequestTagDto tag = tags[i];

                Paragraph paragraph = section.AddParagraph();
                paragraph.AddFormattedText("123456789", new Font("Code39AzaleaRegular1")
                {
                    Size = Unit.FromCentimeter(1)
                });

                var barCode = section.Elements.AddBarcode();
                barCode.Type = MigraDoc.DocumentObjectModel.Shapes.BarcodeType.Barcode128;
                barCode.Code = "9005188002";
                barCode.Text = true;
                barCode.Width = Unit.FromCentimeter(3);
                barCode.Height = Unit.FromCentimeter(1);
                barCode.BearerBars = true;

                var totalString = new Col("SON: CIENTO SETENTA Y CINCO PESOS 00/100 M.N SON: CIENTO SETENTA Y CINCO PESOS 00/100 M.N");
                section.AddText(totalString);

                if (i < tags.Count - 1)
                {
                    section.AddPageBreak();
                }
            }
        }
    }
}