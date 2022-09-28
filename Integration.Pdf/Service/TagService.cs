using Integration.Pdf.Dtos;
using Integration.Pdf.Extensions;
using Integration.Pdf.Models;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.Rendering;
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

            //document.UseCmykColor = true;
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

            for (int i = 0; i < tags.Count; i++)
            {
                RequestTagDto tag = tags[i];

                //BarcodeWriter<Bitmap> writer = new BarcodeWriter<Bitmap>()
                //{
                //    Format = BarcodeFormat.ITF,
                //    Renderer = new ZXing.Rendering.BitmapRenderer()
                //};

                //var barHeight = 20;
                //var barWidth = 35;

                //writer.Options = new ZXing.Common.EncodingOptions { Height = 50, Margin = 0, PureBarcode = true };

                //var bitmap = writer.Write("1405188002");

                //ImageConverter converter = new ImageConverter();
                //var image = (byte[])converter.ConvertTo(bitmap, typeof(byte[]));

                byte[] image;

                BarcodeWriterPixelData writer = new BarcodeWriterPixelData()
                {
                    Format = BarcodeFormat.ITF,
                    Options = new ZXing.Common.EncodingOptions
                    {
                        Width = 320,
                        Height = 60,
                        //Height = 90
                    }
                };
                var pixelData = writer.Write("1405188002");

                using (var bitmap = new Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb))
                {
                    using (var ms = new System.IO.MemoryStream())
                    {
                        var bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, pixelData.Width, pixelData.Height), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                        try
                        {
                            // we assume that the row stride of the bitmap is aligned to 4 byte multiplied by the width of the image   
                            System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
                        }
                        finally
                        {
                            bitmap.UnlockBits(bitmapData);
                        }

                        // PNG or JPEG or whatever you want
                        bitmap.SetResolution(200F, 200F);

                        ImageConverter converter = new ImageConverter();
                        image = (byte[])converter.ConvertTo(bitmap, typeof(byte[]));
                    }
                }

                string imageFilename = image.MigraDocFilenameFromByteArray();

                var imgPar = section.AddParagraph();
                imgPar.Format.Alignment = ParagraphAlignment.Center;
                var barcode = imgPar.AddImage(imageFilename);
                barcode.Width = Unit.FromCentimeter(5);

                var orderNo = new Col("1405188002", new Font("Arial", 10) { Bold = true });
                section.AddText(orderNo, spaceAfter: 0);

                var patient = new Col("MANUEL VIEJO GONZALEZ", new Font("Arial", 5), ParagraphAlignment.Left);
                section.AddText(patient, spaceAfter: 0);

                var studies = new Col("GLU 120 MIN, INSU 120 MIN", new Font("Arial", 5), ParagraphAlignment.Left);
                section.AddText(studies, spaceAfter: 0);

                var textFrame = section.AddTextFrame();
                textFrame.RelativeHorizontal = RelativeHorizontal.Page;
                textFrame.RelativeVertical = RelativeVertical.Page;

                textFrame.WrapFormat.DistanceLeft = Unit.FromCentimeter(2.81);
                textFrame.WrapFormat.DistanceTop = Unit.FromCentimeter(1.54);

                textFrame.Width = Unit.FromCentimeter(1);
                textFrame.Height = Unit.FromCentimeter(1);

                var textFramePar = textFrame.AddParagraph();
                textFramePar.AddFormattedText("ORI90\nMONTERREY\nSBAUTISTA\n07:53:49\nNormal\n49 años M", new Font("Arial", 4));

                //Paragraph paragraph = section.AddParagraph();
                //paragraph.AddFormattedText("123456789", new Font("Code39AzaleaRegular1")
                //{
                //    Size = Unit.FromCentimeter(1)
                //});

                //var barCode = section.Elements.AddBarcode();
                //barCode.Type = MigraDoc.DocumentObjectModel.Shapes.BarcodeType.Barcode128;
                //barCode.Code = "9005188002";
                //barCode.Text = true;
                //barCode.Width = Unit.FromCentimeter(3);
                //barCode.Height = Unit.FromCentimeter(1);
                //barCode.BearerBars = true;

                //var totalString = new Col("SON: CIENTO SETENTA Y CINCO PESOS 00/100 M.N SON: CIENTO SETENTA Y CINCO PESOS 00/100 M.N");
                //section.AddText(totalString);

                if (i < tags.Count - 1)
                {
                    section.AddPageBreak();
                }
            }
        }
    }
}