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

            //////float posY = 50;
            //////float posYDato = 55;

            //////var code = new Col("9005188002", new Font("Calibri", 10, FontStyle.Bold));

            //////TicketUtil.AddString(graphics, code, ref posY);

            //////var name = new Col("MANUEL VIEJO GONZALEZ", new Font("Calibri", 5, FontStyle.Regular), StringAlignment.Near);

            //////TicketUtil.AddString(graphics, name, ref posY);

            //////var test = new Col("GLU BASAL ORINA", new Font("Calibri", 5, FontStyle.Regular), StringAlignment.Near);

            //////TicketUtil.AddString(graphics, test, ref posY);

            //////posY += 5;

            //////var lab = new Col("Laboratorio Ramos", new Font("Calibri", 4, FontStyle.Italic), StringAlignment.Near);

            //////TicketUtil.AddString(graphics, lab, ref posY);

            //////float posX = 115;

            //////var type = new Col("ORI90", new Font("Calibri", 4, FontStyle.Regular), StringAlignment.Near);

            //////TicketUtil.AddString(graphics, type, posX, ref posYDato);

            //////var place = new Col("MONTERREY", new Font("Calibri", 4, FontStyle.Regular), StringAlignment.Near);

            //////TicketUtil.AddString(graphics, place, posX, ref posYDato);

            //////var nam = new Col("SBAUTISTA", new Font("Calibri", 4, FontStyle.Regular), StringAlignment.Near);

            //////TicketUtil.AddString(graphics, nam, posX, ref posYDato);

            //////var time = new Col("07:53:49", new Font("Calibri", 4, FontStyle.Regular), StringAlignment.Near);

            //////TicketUtil.AddString(graphics, time, posX, ref posYDato);

            //////var nom = new Col("Normal", new Font("Calibri", 4, FontStyle.Regular), StringAlignment.Near);

            //////TicketUtil.AddString(graphics, nom, posX, ref posYDato);

            //////var age = new Col("48 años M", new Font("Calibri", 4, FontStyle.Regular), StringAlignment.Near);

            //////TicketUtil.AddString(graphics, age, posX, ref posYDato);

            //var branchInfo = new Col("Laboratorio Alfonso Ramos, S.A. de C.V. Avenida Humberto Lobo #555 A, Col. del Valle C.P. 66220 San Pedro Garza García, Nuevo León.");
            //section.AddText(branchInfo);

            //var phoneInfo = new Col("Tel/WhatsApp: 81 4170 0769 RFC: LAR900731TL0");
            //section.AddText(phoneInfo);

            //var branchName = new Col("SUCURSAL MONTERREY", Col.FONT_BOLD);
            //section.AddText(branchName);

            //var folio = new Col("FOLIO: MT-7463");
            //var date = new Col("FECHA: 2022-02-17");
            //section.AddText(new[] { folio, date }, partialBold: true);

            //section.AddDivider();

            //var attendant = new Col($"QUIEN ATIENDE: {tag.Atiende}", Col.FONT_BOLD, ParagraphAlignment.Left);
            //section.AddText(attendant);

            //section.AddDivider();

            //var user = new Col($"PACIENTE: {tag.Paciente}");
            //var userId = new Col($"ID PACIENTE: {tag.Clave}");
            //section.AddText(new[] { user, userId });

            //var birthdate = new Col($"FECHA NACIMIENTO: {tag.FechaNacimiento}");
            //section.AddText(birthdate, partialBold: true);

            //var exp = new Col("EXPEDIENTE:2202178006");
            //var code = new Col("CÓDIGO:2202178007");
            //section.AddText(new[] { exp, code }, partialBold: true);

            //var deliveryDate = new Col("FECHA ENTREGA:18-02-2022 05:00:00 PM");
            //section.AddText(deliveryDate, partialBold: true);

            //var doctor = new Col("MÉDICO:JUAN MANUEL PONCE");
            //section.AddText(doctor, partialBold: true);

            //section.AddDivider();

            //var colDesc = new Col("DESC", 3, Col.FONT_BOLD);
            //var colQty = new Col("CANT", 1, Col.FONT_BOLD);
            //var colSub = new Col("SUB", 1, Col.FONT_BOLD);
            //var colDisc = new Col("DES", 1, Col.FONT_BOLD);
            //var colTotal = new Col("TOTAL", 1, Col.FONT_BOLD);

            //section.AddText(new[] { colDesc, colQty, colSub, colDisc, colTotal });

            //var studies = GetStudies();

            //foreach (var study in studies)
            //{
            //    section.AddText(
            //        new[] {
            //            new Col(study.Descripcion + " " + $"({study.Codigo})", 3, ParagraphAlignment.Left),
            //            new Col(study.Cantidad.ToString(), 1),
            //            new Col(study.SubTotal.ToString("F"), 1),
            //            new Col(study.Descuento.ToString("F"), 1),
            //            new Col(study.Total.ToString("F"), 1)
            //        });
            //}

            //section.AddDivider();

            //var paymentType = new Col("FORMA DE PAGO: TARJETA DE DEBITO", Col.FONT_BOLD);
            //section.AddText(paymentType);

            //section.AddText(new[] { new Col("SUBTOTAL", 1, ParagraphAlignment.Left), new Col("$ 150.80", 1, ParagraphAlignment.Right), new Col("") });
            //section.AddText(new[] { new Col("DESCUENTO", 1, ParagraphAlignment.Left), new Col("$ 0.00", 1, ParagraphAlignment.Right), new Col("") });
            //section.AddText(new[] { new Col("IVA", 1, ParagraphAlignment.Left), new Col("$ 24.14", 1, ParagraphAlignment.Right), new Col("") });
            //section.AddText(new[] { new Col("TOTAL", 1, ParagraphAlignment.Left), new Col("$ 175.00", 1, ParagraphAlignment.Right), new Col("") });
            //section.AddText(new[] { new Col("ANTICIPO", 1, ParagraphAlignment.Left), new Col("$ 175.00", 1, ParagraphAlignment.Right), new Col("") });
            //section.AddText(new[] { new Col("SALDO", 1, ParagraphAlignment.Left), new Col("$ 0.00", 1, ParagraphAlignment.Right), new Col("") });

            //var totalString = new Col("SON: CIENTO SETENTA Y CINCO PESOS 00/100 M.N");
            //section.AddText(totalString);

            //var mon = new Col("*Monedero Electrónico*", Col.FONT_BOLD);
            //section.AddText(mon);

            //var used = new Col("Utilizados: $ 00.00", ParagraphAlignment.Center);
            //var gen = new Col("Generados: $ 75.25", ParagraphAlignment.Center);
            //section.AddText(new[] { used, gen }, partialBold: true);

            //var acc = new Col("Acumulado: $ 75.25", ParagraphAlignment.Center);
            //section.AddText(acc, partialBold: true);

            //section.AddDivider();

            ////BarcodeWriter<Bitmap> writer = new BarcodeWriter<Bitmap>()
            ////{
            ////    Format = BarcodeFormat.CODE_128,
            ////    Renderer = new ZXing.Rendering.BitmapRenderer()
            ////};

            ////var barHeight = 48;
            ////var barWidth = 150;

            ////writer.Options = new ZXing.Common.EncodingOptions { Width = barWidth, Height = barHeight, Margin = 0 };

            ////graphics.DrawImage(writer.Write("USUARIO:2202178006"), (TicketUtil.TICKET_WIDTH - barWidth) / 2, posY, barWidth, barHeight);

            ////posY += barHeight;

            ////posY += 12;

            //var page = new Col("Consulta tus resultados en línea en la página web: www.laboratoriosramos.com.mx con los siguientes datos:");
            //section.AddText(page);

            ////posY += 3;

            //var us = new Col("USUARIO: 2202178006", Col.FONT_BOLD, ParagraphAlignment.Center);
            //var pass = new Col("CONTRASEÑA: F12D8", Col.FONT_BOLD, ParagraphAlignment.Center);
            //section.AddText(new[] { us, pass });

            ////posY += 6;

            //var wa = new Col("*También puedes solicitar tus resultados vía WhatsApp*", Col.FONT_BOLD);
            //section.AddText(wa);

            ////posY += 6;

            //var con = new Col("Contacto: 81 4170 0769", Col.FONT_BOLD);
            //section.AddText(con);

            //var inf = new Col("TE INVITAMOS A SOLICITAR U OBTENER TU COMPROBANTE FISCAL DURANTE EL MES EN EL QUE SE RELALIZA EL SERVICIO Y COMO MÁXIMO LOS PRIMEROS DOS DIAS NATURALES DEL SIGUIENTE MES, ESTE SE PODRA DESCARGAREN LA PAGINA WEB.", Col.FONT_BOLD, ParagraphAlignment.Justify);
            //section.AddText(inf);

            //var last = new Col("Laboratorio Alfonso Ramos S.A. de C.V. con domicilio matriz en Sinaloa No. 144 sur Col. Centro C.P. 85000 Ciudad Obregón, Sonora, así como las sucursales distribuidas en el territorio nacional utilizará sus datos personales recabados con los siguientes fines: Otorgar los servicios clínicos contratados, confirmar y/o asignar citas para servicios, tramites de facturación, actividades de archivo y respaldo de información, manejo de programas de lealtad, envío de comunicaciones y/o imágenes promocionales. Para más información sobre el tratamiento de sus datos personales usted puede ingresar en la siguiente página.", ParagraphAlignment.Justify);
            //section.AddText(last);
        }
    }
}