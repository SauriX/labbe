using Api.PDF.Extensions;
using Api.PDF.Models;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Api.PDF.Service
{
    public class TicketService
    {
        public static byte[] Generate()
        {
            Document document = CreateDocument();

            document.UseCmykColor = true;
            const bool unicode = false;

            DocumentRenderer renderer = new DocumentRenderer(document);
            renderer.PrepareDocument();

            RenderInfo[] info = renderer.GetRenderInfoFromPage(1);
            int index = info.Length - 1;

            double stop = info[index].LayoutInfo.ContentArea.Y.Millimeter + info[index].LayoutInfo.ContentArea.Height.Millimeter + 10; //add more if you have bottom page margin, borders on the last table etc.
            var section = document.LastSection;
            section.PageSetup.PageHeight = Unit.FromMillimeter(stop);

            PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(unicode)
            {
                Document = document
            };

            pdfRenderer.RenderDocument();
            //var filename = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "/test.pdf";

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

        static Document CreateDocument()
        {
            Document document = new Document();

            Section section = document.AddSection();

            section.PageSetup = document.DefaultPageSetup.Clone();

            section.PageSetup.Orientation = Orientation.Portrait;
            section.PageSetup.PageWidth = Unit.FromInch(2.83);
            section.PageSetup.PageHeight = Unit.FromInch(50.00);

            section.PageSetup.BottomMargin = 0;
            section.PageSetup.LeftMargin = 0;
            section.PageSetup.RightMargin = 0;
            section.PageSetup.TopMargin = 0;

            Format(section);

            return document;
        }

        static void Format(Section section)
        {
            var branchInfo = new Col("Laboratorio Alfonso Ramos, S.A. de C.V. Avenida Humberto Lobo #555 A, Col. del Valle C.P. 66220 San Pedro Garza García, Nuevo León.");
            section.AddText(branchInfo);

            var phoneInfo = new Col("Tel/WhatsApp: 81 4170 0769 RFC: LAR900731TL0");
            section.AddText(phoneInfo);

            var branchName = new Col("SUCURSAL MONTERREY", Col.FONT_BOLD);
            section.AddText(branchName);

            var folio = new Col("FOLIO: MT-7463");
            var date = new Col("FECHA: 2022-02-17");
            section.AddText(new[] { folio, date }, true);

            section.AddDivider();

            var attendant = new Col("QUIEN ATIENDE: PERLA MARÍA SUAREZ MARTINEZ", Col.FONT_BOLD, ParagraphAlignment.Left);
            section.AddText(attendant);

            section.AddDivider();
        }
    }
}