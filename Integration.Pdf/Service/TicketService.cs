using Integration.Pdf.Dtos;
using Integration.Pdf.Extensions;
using Integration.Pdf.Models;
using Integration.Pdf.Utils;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using ZXing;

namespace Integration.Pdf.Service
{
    public class TicketService
    {
        public static byte[] Generate(RequestTicketDto order)
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

        static Document CreateDocument(RequestTicketDto order)
        {
            Document document = new Document();

            Section section = document.AddSection();

            section.PageSetup = document.DefaultPageSetup.Clone();

            section.PageSetup.Orientation = Orientation.Portrait;
            section.PageSetup.PageWidth = Unit.FromCentimeter(7.2);
            section.PageSetup.PageHeight = Unit.FromCentimeter(29.7);

            section.PageSetup.TopMargin = 0;
            section.PageSetup.BottomMargin = Unit.FromMillimeter(5);
            section.PageSetup.LeftMargin = 0;
            section.PageSetup.RightMargin = 0;

            Format(section, order);

            return document;
        }

        static void Format(Section section, RequestTicketDto ticket)
        {
            section.AddSpace();

            var logo = new Col(Assets.GetLogoBytes()) { ImagenTamaño = Unit.FromCentimeter(4), };
            section.AddText(logo);

            var branchInfo = new Col($"Laboratorio Alfonso Ramos, S.A. de C.V. {ticket.DireccionSucursal}");
            section.AddText(branchInfo);

            var phoneInfo = new Col(ticket.Contacto);
            section.AddText(phoneInfo);

            var branchName = new Col(ticket.Sucursal, Col.FONT_BOLD);
            section.AddText(branchName);

            var folio = new Col($"FOLIO: {ticket.Folio}");
            var date = new Col($"FECHA: {ticket.Fecha}");
            section.AddText(new[] { folio, date }, partialBold: true);

            section.AddDivider();

            var attendant = new Col($"QUIEN ATIENDE: {ticket.Atiende}", Col.FONT_BOLD, ParagraphAlignment.Left);
            section.AddText(attendant);

            section.AddDivider();

            var user = new Col($"PACIENTE: {ticket.Paciente}");
            var userId = new Col($"ID PACIENTE: {ticket.Expediente}");
            section.AddText(new[] { user, userId });

            var birthdate = new Col($"FECHA NACIMIENTO: {ticket.FechaNacimiento}");
            section.AddText(birthdate, partialBold: true);

            var exp = new Col($"EXPEDIENTE: {ticket.Expediente}");
            var code = new Col($"CÓDIGO: {ticket.Solicitud}");
            section.AddText(new[] { exp, code }, partialBold: true);

            var deliveryDate = new Col($"FECHA ENTREGA: {ticket.FechaEntrega}");
            section.AddText(deliveryDate, partialBold: true);

            var doctor = new Col($"MÉDICO: {ticket.Medico}");
            section.AddText(doctor, partialBold: true);

            section.AddDivider();

            var detailHeader = new Col[]
            {
                new Col("DESC", 3, Col.FONT_BOLD),
                new Col("CANT", 1, Col.FONT_BOLD),
                new Col("SUB", 1, Col.FONT_BOLD),
                new Col("DES", 1, Col.FONT_BOLD),
                new Col("TOTAL", 1, Col.FONT_BOLD)
            };
            section.AddText(detailHeader);

            foreach (var study in ticket.Estudios)
            {
                section.AddText(
                    new[] {
                        new Col(study.Estudio + " " + $"({study.Clave})", 3, ParagraphAlignment.Left),
                        new Col(study.Cantidad, 1),
                        new Col(study.Precio, 1),
                        new Col(study.Descuento, 1),
                        new Col(study.Total, 1)
                    });
            }

            section.AddDivider();

            var paymentType = new Col($"FORMA DE PAGO: {ticket.FormaPago}", Col.FONT_BOLD);
            section.AddText(paymentType);

            section.AddText(new[] { new Col(""), new Col("SUBTOTAL", 3, ParagraphAlignment.Left), new Col(ticket.Subtotal, 2, ParagraphAlignment.Right) });
            section.AddText(new[] { new Col(""), new Col("DESCUENTO", 3, ParagraphAlignment.Left), new Col(ticket.Descuento, 2, ParagraphAlignment.Right) });
            section.AddText(new[] { new Col(""), new Col("IVA", 3, ParagraphAlignment.Left), new Col(ticket.IVA, 2, ParagraphAlignment.Right) });
            section.AddText(new[] { new Col(""), new Col("TOTAL", 3, ParagraphAlignment.Left), new Col(ticket.Total, 2, ParagraphAlignment.Right) });
            //section.AddText(new[] { new Col("ANTICIPO", 1, ParagraphAlignment.Left), new Col(ticket.Anticipo, 1, ParagraphAlignment.Right), new Col("") });
            foreach (var payment in ticket.Pagos.OrderBy(x => x.FormaPago))
            {
                section.AddText(new[] { new Col(""), new Col(payment.FormaPago, 3, ParagraphAlignment.Left), new Col(payment.Cantidad, 2, ParagraphAlignment.Right) });
            }
            section.AddText(new[] { new Col(""), new Col("SALDO", 3, ParagraphAlignment.Left), new Col(ticket.Saldo, 2, ParagraphAlignment.Right) });

            //var totalString = new Col("SON: CIENTO SETENTA Y CINCO PESOS 00/100 M.N");
            //section.AddText(totalString);

            var mon = new Col("*Monedero Electrónico*", Col.FONT_BOLD);
            section.AddText(mon);

            var used = new Col($"Utilizados: {ticket.MonederoUtilizado}", ParagraphAlignment.Center);
            var gen = new Col($"Generados: {ticket.MonederoGenerado}", ParagraphAlignment.Center);
            section.AddText(new[] { used, gen }, partialBold: true);

            var acc = new Col($"Acumulado: {ticket.MonederoAcumulado}", ParagraphAlignment.Center);
            section.AddText(acc, partialBold: true);

            section.AddDivider();

            //byte[] barcodeImage = BarCode.Generate(ticket.CodigoPago, 150, 28, BarcodeFormat.CODE_128);
            //string imageFilename = barcodeImage.MigraDocFilenameFromByteArray();

            //var imgPar = section.AddParagraph();
            //imgPar.Format.Alignment = ParagraphAlignment.Center;
            //var barcode = imgPar.AddImage(imageFilename);
            //barcode.Width = Unit.FromCentimeter(7);

            BarcodeWriter<Bitmap> writer = new BarcodeWriter<Bitmap>()
            {
                Format = BarcodeFormat.CODE_128,
                Renderer = new ZXing.Rendering.BitmapRenderer()
            };

            var barHeight = 35;
            var barWidth = 150;

            writer.Options = new ZXing.Common.EncodingOptions { Width = barWidth, Height = barHeight, Margin = 0, PureBarcode = true };

            var bitmap = writer.Write(ticket.CodigoPago);

            ImageConverter converter = new ImageConverter();
            var image = (byte[])converter.ConvertTo(bitmap, typeof(byte[]));

            string imageFilename = image.MigraDocFilenameFromByteArray();

            var imgPar = section.AddParagraph();
            imgPar.Format.Alignment = ParagraphAlignment.Center;
            imgPar.AddImage(imageFilename);

            var page = new Col("Consulta tus resultados en línea en la página web: www.laboratoriosramos.com.mx con los siguientes datos:");
            section.AddText(page);

            var us = new Col($"USUARIO: {ticket.Usuario}", Col.FONT_BOLD, ParagraphAlignment.Center);
            var pass = new Col($"CONTRASEÑA: {ticket.Contraseña}", Col.FONT_BOLD, ParagraphAlignment.Center);
            section.AddText(new[] { us, pass });

            var wa = new Col("*También puedes solicitar tus resultados vía WhatsApp*", Col.FONT_BOLD);
            section.AddText(wa);

            var con = new Col($"Contacto: {ticket.ContactoTelefono}", Col.FONT_BOLD);
            section.AddText(con);

            var inf = new Col("TE INVITAMOS A SOLICITAR U OBTENER TU COMPROBANTE FISCAL DURANTE EL MES EN EL QUE SE RELALIZA EL SERVICIO Y COMO MÁXIMO LOS PRIMEROS DOS DIAS NATURALES DEL SIGUIENTE MES, ESTE SE PODRA DESCARGAREN LA PAGINA WEB.", Col.FONT_BOLD, ParagraphAlignment.Justify);
            section.AddText(inf);

            var last = new Col("Laboratorio Alfonso Ramos S.A. de C.V. con domicilio matriz en Sinaloa No. 144 sur Col. Centro C.P. 85000 Ciudad Obregón, Sonora, así como las sucursales distribuidas en el territorio nacional utilizará sus datos personales recabados con los siguientes fines: Otorgar los servicios clínicos contratados, confirmar y/o asignar citas para servicios, tramites de facturación, actividades de archivo y respaldo de información, manejo de programas de lealtad, envío de comunicaciones y/o imágenes promocionales. Para más información sobre el tratamiento de sus datos personales usted puede ingresar en la siguiente página.", ParagraphAlignment.Justify);
            section.AddText(last);
        }

        public class Info
        {
            public string Codigo { get; set; }
            public string Descripcion { get; set; }
            public int Cantidad { get; set; }
            public decimal SubTotal { get; set; }
            public decimal Impuestos { get; set; }
            public decimal Descuento { get; set; }
            public decimal IVA { get; set; }
            public decimal Total { get; set; }
        }
    }
}