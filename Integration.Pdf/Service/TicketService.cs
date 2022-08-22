using Integration.Pdf.Dtos;
using Integration.Pdf.Extensions;
using Integration.Pdf.Models;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using ZXing;

namespace Integration.Pdf.Service
{
    public class TicketService
    {
        public static byte[] Generate(RequestOrderDto order)
        {
            Document document = CreateDocument(order);

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

        static Document CreateDocument(RequestOrderDto order)
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

        static void Format(Section section, RequestOrderDto order)
        {
            var branchInfo = new Col("Laboratorio Alfonso Ramos, S.A. de C.V. Avenida Humberto Lobo #555 A, Col. del Valle C.P. 66220 San Pedro Garza García, Nuevo León.");
            section.AddText(branchInfo);

            var phoneInfo = new Col("Tel/WhatsApp: 81 4170 0769 RFC: LAR900731TL0");
            section.AddText(phoneInfo);

            var branchName = new Col("SUCURSAL MONTERREY", Col.FONT_BOLD);
            section.AddText(branchName);

            var folio = new Col("FOLIO: MT-7463");
            var date = new Col("FECHA: 2022-02-17");
            section.AddText(new[] { folio, date }, partialBold: true);

            section.AddDivider();

            var attendant = new Col($"QUIEN ATIENDE: {order.Atiende}", Col.FONT_BOLD, ParagraphAlignment.Left);
            section.AddText(attendant);

            section.AddDivider();

            var user = new Col($"PACIENTE: {order.Paciente}");
            var userId = new Col($"ID PACIENTE: {order.Clave}");
            section.AddText(new[] { user, userId });

            var birthdate = new Col($"FECHA NACIMIENTO: {order.FechaNacimiento}");
            section.AddText(birthdate, partialBold: true);

            var exp = new Col("EXPEDIENTE:2202178006");
            var code = new Col("CÓDIGO:2202178007");
            section.AddText(new[] { exp, code }, partialBold: true);

            var deliveryDate = new Col("FECHA ENTREGA:18-02-2022 05:00:00 PM");
            section.AddText(deliveryDate, partialBold: true);

            var doctor = new Col("MÉDICO:JUAN MANUEL PONCE");
            section.AddText(doctor, partialBold: true);

            section.AddDivider();

            var colDesc = new Col("DESC", 3, Col.FONT_BOLD);
            var colQty = new Col("CANT", 1, Col.FONT_BOLD);
            var colSub = new Col("SUB", 1, Col.FONT_BOLD);
            var colDisc = new Col("DES", 1, Col.FONT_BOLD);
            var colTotal = new Col("TOTAL", 1, Col.FONT_BOLD);

            section.AddText(new[] { colDesc, colQty, colSub, colDisc, colTotal });

            var studies = GetStudies();

            foreach (var study in studies)
            {
                section.AddText(
                    new[] {
                        new Col(study.Descripcion + " " + $"({study.Codigo})", 3, ParagraphAlignment.Left),
                        new Col(study.Cantidad.ToString(), 1),
                        new Col(study.SubTotal.ToString("F"), 1),
                        new Col(study.Descuento.ToString("F"), 1),
                        new Col(study.Total.ToString("F"), 1)
                    });
            }

            section.AddDivider();

            var paymentType = new Col("FORMA DE PAGO: TARJETA DE DEBITO", Col.FONT_BOLD);
            section.AddText(paymentType);

            section.AddText(new[] { new Col("SUBTOTAL", 1, ParagraphAlignment.Left), new Col("$ 150.80", 1, ParagraphAlignment.Right), new Col("") });
            section.AddText(new[] { new Col("DESCUENTO", 1, ParagraphAlignment.Left), new Col("$ 0.00", 1, ParagraphAlignment.Right), new Col("") });
            section.AddText(new[] { new Col("IVA", 1, ParagraphAlignment.Left), new Col("$ 24.14", 1, ParagraphAlignment.Right), new Col("") });
            section.AddText(new[] { new Col("TOTAL", 1, ParagraphAlignment.Left), new Col("$ 175.00", 1, ParagraphAlignment.Right), new Col("") });
            section.AddText(new[] { new Col("ANTICIPO", 1, ParagraphAlignment.Left), new Col("$ 175.00", 1, ParagraphAlignment.Right), new Col("") });
            section.AddText(new[] { new Col("SALDO", 1, ParagraphAlignment.Left), new Col("$ 0.00", 1, ParagraphAlignment.Right), new Col("") });

            var totalString = new Col("SON: CIENTO SETENTA Y CINCO PESOS 00/100 M.N");
            section.AddText(totalString);

            var mon = new Col("*Monedero Electrónico*", Col.FONT_BOLD);
            section.AddText(mon);

            var used = new Col("Utilizados: $ 00.00", ParagraphAlignment.Center);
            var gen = new Col("Generados: $ 75.25", ParagraphAlignment.Center);
            section.AddText(new[] { used, gen }, partialBold: true);

            var acc = new Col("Acumulado: $ 75.25", ParagraphAlignment.Center);
            section.AddText(acc, partialBold: true);

            section.AddDivider();

            BarcodeWriter<Bitmap> writer = new BarcodeWriter<Bitmap>()
            {
                Format = BarcodeFormat.CODE_128,
                Renderer = new ZXing.Rendering.BitmapRenderer()
            };

            var barHeight = 35;
            var barWidth = 150;

            writer.Options = new ZXing.Common.EncodingOptions { Width = barWidth, Height = barHeight, Margin = 0, PureBarcode = true };

            var bitmap = writer.Write("USUARIO:2202178006");

            ImageConverter converter = new ImageConverter();
            var image = (byte[])converter.ConvertTo(bitmap, typeof(byte[]));

            string imageFilename = MigraDocFilenameFromByteArray(image);

            var imgPar = section.AddParagraph();
            imgPar.Format.Alignment = ParagraphAlignment.Center;
            imgPar.AddImage(imageFilename);

            var page = new Col("Consulta tus resultados en línea en la página web: www.laboratoriosramos.com.mx con los siguientes datos:");
            section.AddText(page);

            var us = new Col("USUARIO: 2202178006", Col.FONT_BOLD, ParagraphAlignment.Center);
            var pass = new Col("CONTRASEÑA: F12D8", Col.FONT_BOLD, ParagraphAlignment.Center);
            section.AddText(new[] { us, pass });

            var wa = new Col("*También puedes solicitar tus resultados vía WhatsApp*", Col.FONT_BOLD);
            section.AddText(wa);

            var con = new Col("Contacto: 81 4170 0769", Col.FONT_BOLD);
            section.AddText(con);

            var inf = new Col("TE INVITAMOS A SOLICITAR U OBTENER TU COMPROBANTE FISCAL DURANTE EL MES EN EL QUE SE RELALIZA EL SERVICIO Y COMO MÁXIMO LOS PRIMEROS DOS DIAS NATURALES DEL SIGUIENTE MES, ESTE SE PODRA DESCARGAREN LA PAGINA WEB.", Col.FONT_BOLD, ParagraphAlignment.Justify);
            section.AddText(inf);

            var last = new Col("Laboratorio Alfonso Ramos S.A. de C.V. con domicilio matriz en Sinaloa No. 144 sur Col. Centro C.P. 85000 Ciudad Obregón, Sonora, así como las sucursales distribuidas en el territorio nacional utilizará sus datos personales recabados con los siguientes fines: Otorgar los servicios clínicos contratados, confirmar y/o asignar citas para servicios, tramites de facturación, actividades de archivo y respaldo de información, manejo de programas de lealtad, envío de comunicaciones y/o imágenes promocionales. Para más información sobre el tratamiento de sus datos personales usted puede ingresar en la siguiente página.", ParagraphAlignment.Justify);
            section.AddText(last);
        }

        static string MigraDocFilenameFromByteArray(byte[] image)
        {
            return "base64:" + Convert.ToBase64String(image);
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

        public static List<Info> GetStudies()
        {
            return new List<Info>
            {
                new Info
                {
                    Codigo = "ETX",
                    Descripcion = "TELERADIOGRAFIA DE TORAX",
                    Cantidad = 1,
                    SubTotal = 150.86m,
                    Impuestos = 15.45m,
                    Descuento = 0m,
                    IVA = 5.3m,
                    Total = 175,
                },
                new Info
                {
                    Codigo = "TEZ",
                    Descripcion = "TELERADIO GRAFIA DE TORAX",
                    Cantidad = 1,
                    SubTotal = 150.86m,
                    Impuestos = 15.45m,
                    Descuento = 0m,
                    IVA = 5.3m,
                    Total = 175,
                },
                new Info
                {
                    Codigo = "ETX",
                    Descripcion = "TELERADIOGRAFIA DE TORAX",
                    Cantidad = 1,
                    SubTotal = 150.86m,
                    Impuestos = 15.45m,
                    Descuento = 0m,
                    IVA = 5.3m,
                    Total = 175,
                },
                new Info
                {
                    Codigo = "TEZ",
                    Descripcion = "TELERADIO GRAFIA DE TORAX",
                    Cantidad = 1,
                    SubTotal = 150.86m,
                    Impuestos = 15.45m,
                    Descuento = 0m,
                    IVA = 5.3m,
                    Total = 175,
                },
                new Info
                {
                    Codigo = "ETX",
                    Descripcion = "TELERADIOGRAFIA DE TORAX",
                    Cantidad = 1,
                    SubTotal = 150.86m,
                    Impuestos = 15.45m,
                    Descuento = 0m,
                    IVA = 5.3m,
                    Total = 175,
                },
                new Info
                {
                    Codigo = "TEZ",
                    Descripcion = "TELERADIO GRAFIA DE TORAX",
                    Cantidad = 1,
                    SubTotal = 150.86m,
                    Impuestos = 15.45m,
                    Descuento = 0m,
                    IVA = 5.3m,
                    Total = 175,
                },
                new Info
                {
                    Codigo = "ETX",
                    Descripcion = "TELERADIOGRAFIA DE TORAX",
                    Cantidad = 1,
                    SubTotal = 150.86m,
                    Impuestos = 15.45m,
                    Descuento = 0m,
                    IVA = 5.3m,
                    Total = 175,
                },
                new Info
                {
                    Codigo = "TEZ",
                    Descripcion = "TELERADIO GRAFIA DE TORAX",
                    Cantidad = 1,
                    SubTotal = 150.86m,
                    Impuestos = 15.45m,
                    Descuento = 0m,
                    IVA = 5.3m,
                    Total = 175,
                },
            };
        }
    }
}