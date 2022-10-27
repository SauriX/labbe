using Integration.Pdf.Dtos;
using Integration.Pdf.Extensions;
using Integration.Pdf.Models;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;
namespace Integration.Pdf.Service
{
    public class LabResultsService
    {
        public static byte[] Generate(ClinicResultsPdfDto results)
        {
            Document document = CreateDocument(results);

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

        static Document CreateDocument(ClinicResultsPdfDto results)
        {
            Document document = new Document();

            Section section = document.AddSection();

            section.PageSetup = document.DefaultPageSetup.Clone();

            section.PageSetup.Orientation = Orientation.Portrait;
            section.PageSetup.PageFormat = PageFormat.A4;
            if (results.ImprimrLogos)
            {
                section.PageSetup.TopMargin = Unit.FromCentimeter(4);

            }
            else
            {
                section.PageSetup.TopMargin = Unit.FromCentimeter(1);
            }
            section.PageSetup.BottomMargin = Unit.FromCentimeter(2);
            section.PageSetup.LeftMargin = Unit.FromCentimeter(0.5);
            section.PageSetup.RightMargin = Unit.FromCentimeter(0.5);

            section.PageSetup.HeaderDistance = 0;

            Format(section, results);

            return document;
        }

        static void Format(Section section, ClinicResultsPdfDto results)
        {
            var fontText = new Font("calibri", 12);
            var fontParam = new Font("calibri", 12);
            var fontTitle = new Font("calibri", 14);

            var logoLab = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets\\LabRamosLogo.png");
            var logoISO = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets\\ISOLogo.png");
            var firma = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets\\firmaEjemplo.png");
            var firmaImage = File.ReadAllBytes(firma);

            var LabRamosImage = File.ReadAllBytes(logoLab);
            var ISOImage = File.ReadAllBytes(logoISO);

            var headerParagraph = "ALFONSO RAMOS SALAZAR, QBP, MSC, DBC UNIVERSIDAD Y HOSPITAL GENERAL DE TORONTO CED. DGP No. 703973 REG. S.S.A. 10-86 DGP F-370, No. REG. 0111";

            var header = section.Headers.Primary;

            if (results.ImprimrLogos)
            {
                var headerInfo = new Col[]
            {
                new Col(LabRamosImage, 6, ParagraphAlignment.Left)
                {
                    ImagenTamaño = Unit.FromCentimeter(6)
                },
                new Col(headerParagraph, 4, new Font("Calibri", 10), ParagraphAlignment.Center),
                new Col(ISOImage, 4, ParagraphAlignment.Right)
                {
                    ImagenTamaño = Unit.FromCentimeter(3)
                },
            };

                header.AddText(headerInfo);

                var textFrame = header.AddTextFrame();
                textFrame.RelativeHorizontal = RelativeHorizontal.Page;
                textFrame.RelativeVertical = RelativeVertical.Page;

                textFrame.WrapFormat.DistanceLeft = (section.PageSetup.PageWidth / 2) - 15;
                textFrame.WrapFormat.DistanceTop = Unit.FromCentimeter(2.7);

                textFrame.Width = 100;
                textFrame.Height = Unit.FromCentimeter(1);

                var textFramePar = textFrame.AddParagraph();
                textFramePar.Format.Alignment = ParagraphAlignment.Center;
                textFramePar.AddFormattedText("www.laboratorioramos.com.mx", new Font("Calibri", 10) { Bold = true });
            }

            section.AddSpace(5);

            var line1 = new Col[]
            {
                new Col("Doctor (a)", 12, fontText, ParagraphAlignment.Left),
                new Col($": {results.SolicitudInfo?.Medico}", 18, Col.FONT_SUBTITLE_BOLD, ParagraphAlignment.Left),
                new Col("Expediente", 12, fontText, ParagraphAlignment.Left),
                new Col($": {results.SolicitudInfo?.Clave}", 18, Col.FONT_SUBTITLE_BOLD, ParagraphAlignment.Left)
            };
            section.AddBorderedText(line1, top: true, right: false, left: false);

            var line2 = new Col[]
            {
                new Col("Paciente", 12, fontText, ParagraphAlignment.Left),
                new Col($": {results.SolicitudInfo?.Paciente}", 18, Col.FONT_SUBTITLE_BOLD, ParagraphAlignment.Left),
                new Col("Edad", 12, fontText, ParagraphAlignment.Left),
                new Col($": {results.SolicitudInfo?.Edad}", 18, Col.FONT_SUBTITLE_BOLD, ParagraphAlignment.Left)
            };
            section.AddBorderedText(line2, right: false, left: false);

            var line3 = new Col[]
            {
                new Col("Paciente Número", 12, fontText, ParagraphAlignment.Left),
                new Col($": {results.SolicitudInfo?.Expediente}", 18, Col.FONT_SUBTITLE_BOLD, ParagraphAlignment.Left),
                new Col("Sexo", 12, fontText, ParagraphAlignment.Left),
                new Col($": {results.SolicitudInfo?.Sexo}", 18, Col.FONT_SUBTITLE_BOLD, ParagraphAlignment.Left),
            };
            section.AddBorderedText(line3, right: false, left: false);

            var line4 = new Col[]
            {
                new Col("Fecha de Admisión", 12, fontText, ParagraphAlignment.Left),
                new Col($": {results.SolicitudInfo?.FechaAdmision}", 18, Col.FONT_SUBTITLE_BOLD, ParagraphAlignment.Left),
                new Col("Fecha de Entrega", 12, fontText, ParagraphAlignment.Left),
                new Col($": {results.SolicitudInfo?.FechaEntrega}", 18, Col.FONT_SUBTITLE_BOLD, ParagraphAlignment.Left),
            };
            section.AddBorderedText(line4, right: false, left: false);

            var line5 = new Col[]
            {
                new Col("Compañía", 12, fontText, ParagraphAlignment.Left),
                new Col($": {(results.SolicitudInfo?.Compañia == null ? "Particulares" : results.SolicitudInfo.Compañia)}", 18, Col.FONT_SUBTITLE_BOLD, ParagraphAlignment.Left),
                new Col("Impreso a las", 12, fontText, ParagraphAlignment.Left),
                new Col($": {DateTime.Now.ToString("t")}", 18, Col.FONT_SUBTITLE_BOLD, ParagraphAlignment.Left),
            };
            section.AddBorderedText(line5, right: false, left: false);

            section.AddSpace(25);

            var studyHeader = new Col[]
            {
                new Col("EXAMEN", 14, Col.FONT_SUBTITLE_BOLD, ParagraphAlignment.Left),
                new Col("RESULTADO", 7, Col.FONT_SUBTITLE_BOLD),
                new Col("UNIDADES", 6, Col.FONT_SUBTITLE_BOLD),
                new Col("REFERENCIA", 6, Col.FONT_SUBTITLE_BOLD),
            };
            section.AddBorderedText(studyHeader, top: true, right: false, bottom: true, left: false);
            section.AddSpace(5);

            if (results.CapturaResultados != null)
            {
                var study = results.CapturaResultados.Select(x => x.Estudio).Distinct().ToList();

                var studyParameter = results.CapturaResultados.GroupBy(x => x.Estudio).ToList();

                foreach (var studyParam in studyParameter)
                {
                    var studyName = new Col("****" + studyParam.Key, 14, fontTitle, ParagraphAlignment.Left);
                    section.AddText(studyName);

                    foreach (var param in studyParam)
                    {

                        var checkResult = false;
                        var typeValueText = param.TipoValorId == 9 || param.TipoValorId == 10;

                        if (param.Resultado != null && param.TipoValorId == 1)
                        {
                            checkResult = decimal.Parse(param.Resultado) > decimal.Parse(param.ValorFinal) || decimal.Parse(param.Resultado) < decimal.Parse(param.ValorInicial);
                        }

                        var col = new Col[]
                        {
                    new Col(param.Nombre, 12, fontParam, ParagraphAlignment.Left){
                        Fill = typeValueText ? TabLeader.Spaces : TabLeader.Dots
                    },
                    new Col(checkResult ? $"*{param.Resultado}" : param.Resultado, 7, fontParam, ParagraphAlignment.Center),
                    new Col(param.UnidadNombre, 6, fontParam, ParagraphAlignment.Center),
                    new Col(typeValueText ? "" : $"{param.ValorInicial} - {param.ValorFinal}", 6, fontParam, ParagraphAlignment.Center),
                        };
                        section.AddBorderedText(col, top: false, right: false, bottom: false, left: false);
                    }
                }

                section.AddSpace(5);
            }

            var footer = section.Footers.Primary;

            var firmadoPor = "MSc. Alfonso Ramos Salazar\nCED. PROF. 703973";

            var footerFirmaLibera = new Col[]
            {
                 new Col("Toma de Muestra: " + results.SolicitudInfo?.FechaAdmision + " " + " " + results.SolicitudInfo?.User + "\nLiberó: " + results.SolicitudInfo?.FechaEntrega + " " + " " + results.SolicitudInfo?.User, 5, ParagraphAlignment.Left),
                 new Col(firmaImage, 4)
                 {
                     ImagenTamaño = Unit.FromCentimeter(3)
                 },
            };

            var footerFirmadoPor = new Col[]
            {
                new Col("", 5, ParagraphAlignment.Left),
                new Col(firmadoPor, 4)
            };

            footer.AddText(footerFirmaLibera);
            footer.AddText(footerFirmadoPor);
            section.AddSpace(5);

            var footerTitleBranches = new Col[]
            {
                new Col("CD. OBREGÓN, SON.", 4, new Font("Calibri", 10) { Bold = true },  ParagraphAlignment.Center),
                new Col("GUAYMAS, SON.", 1, new Font("Calibri", 10) { Bold = true },  ParagraphAlignment.Center),
                new Col("HERMOSILLO, SON.", 3, new Font("Calibri", 10) { Bold = true },  ParagraphAlignment.Center),
            };

            footer.AddText(footerTitleBranches);

            var footerBranchesUnits = new Col[]
            {
                new Col("UNIDAD CENTRO", 1,new Font("Calibri", 7) { Bold = true },  ParagraphAlignment.Center),
                new Col("UNIDAD 200", 1, new Font("Calibri", 7) { Bold = true },  ParagraphAlignment.Center),
                new Col("UNIDAD 300", 1, new Font("Calibri", 7) { Bold = true },  ParagraphAlignment.Center),
                new Col("CENTRO MÉDICO SUR SONORA", 1, new Font("Calibri", 7) { Bold = true },  ParagraphAlignment.Center),
                new Col("UNIDAD GUAYMAS", 1, new Font("Calibri", 7) { Bold = true },  ParagraphAlignment.Center),
                new Col("CENTRO MÉDICO DEL RÍO", 1, new Font("Calibri", 7) { Bold = true },  ParagraphAlignment.Center),
                new Col("MORELOS", 1, new Font("Calibri", 7) { Bold = true },  ParagraphAlignment.Center),
                new Col("NAVARRETE", 1, new Font("Calibri", 7) { Bold = true },  ParagraphAlignment.Center),
            };

            footer.AddText(footerBranchesUnits);

            var footerBranchesAddress = new Col[]
            {
                new Col("Sinaloa No. 144 Sur Col. CentroTels. (664) 415-16-92, 414-08-41, 414-08-42, 415-31-39 y 415-31-40", 1,new Font("Calibri", 6),  ParagraphAlignment.Center),
                new Col("Calle 200 Casi Esq. con Michoacán Tels. (644)412-31-56 y 416-14-07", 1, new Font("Calibri", 6),  ParagraphAlignment.Center),
                new Col("Jalisco No. 2250 Esq. Calle 300 Plaza Perisur Cd. Obregón, Son. Tel. (644) 444-66-69", 1, new Font("Calibri", 6),  ParagraphAlignment.Center),
                new Col("Calle Norte No. 749 Ote. Esq. con Sonora Tel(644) 415-06-66", 1, new Font("Calibri", 6) ,  ParagraphAlignment.Center),
                new Col("Calzada Agustín García López Local 6 esq. Paseo de las Villas Fracc. Las Villas Tel. (622) 218-9183", 1, new Font("Calibri", 6),  ParagraphAlignment.Center),
                new Col("Reforma No. 273 Sur Tel. (662) 213-6866", 1, new Font("Calibri", 6),  ParagraphAlignment.Center),
                new Col("Blvd. Morelos No. 357 Col. El Bachoco Tels. (662) 267-8635 y 37", 1, new Font("Calibri", 6),  ParagraphAlignment.Center),
                new Col("Blvd. Navarrete No. 292 Col. Raquet Club Tel. (662) 216-3342", 1, new Font("Calibri", 6),  ParagraphAlignment.Center),
            };

            footer.AddText(footerBranchesAddress);
        }
    }
}