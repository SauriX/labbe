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
using MigraDoc.DocumentObjectModel.Shapes.Charts;

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
                section.PageSetup.TopMargin = Unit.FromCentimeter(8);
                section.PageSetup.HeaderDistance = 0.5;

            }
            else
            {
                section.PageSetup.TopMargin = Unit.FromCentimeter(5);
                section.PageSetup.HeaderDistance = 5;
            }
            section.PageSetup.BottomMargin = Unit.FromCentimeter(8);
            section.PageSetup.LeftMargin = Unit.FromCentimeter(0.5);
            section.PageSetup.RightMargin = Unit.FromCentimeter(0.5);

            Format(section, results);

            return document;
        }

        static void Format(Section section, ClinicResultsPdfDto results)
        {
            var fontText = new Font("calibri", 12);
            var fontParam = new Font("calibri", 12);
            var fontCritic = new Font("calibri", 12) { Bold = true };
            var fontTitle = new Font("calibri", 14) { Bold = true };
            const int CTG = 631;

            var contentWidth = section.PageSetup.PageWidth - section.PageSetup.LeftMargin - section.PageSetup.RightMargin;
            Chart chart = new Chart();
            chart.Left = 0;

            chart.Width = contentWidth;
            chart.Height = Unit.FromCentimeter(12);

            if (results.CapturaResultados != null)
            {

                RenderHeader(section, results, fontText);

                var study = results.CapturaResultados.Select(x => x.Estudio).Distinct().ToList();
                var studyParameter = results.CapturaResultados.GroupBy(x => x.Estudio).ToList();

                Series series = chart.SeriesCollection.AddSeries();
                series.ChartType = ChartType.Line;
                XSeries xseries = chart.XValues.AddXSeries();

                for (int i = 0; i < studyParameter.Count; i++)
                {
                    IGrouping<string, ClinicResultsCaptureDto> studyParam = studyParameter[i];
                    section.AddSpace(5);

                    var studyName = new Col("****" + studyParam.Key, 14, fontTitle, ParagraphAlignment.Left);
                    section.AddText(studyName);

                    var orderParams = studyParam.OrderBy(x => x.Orden);

                    foreach (var param in orderParams.Where(x => !string.IsNullOrWhiteSpace(x.Resultado)))
                    {
                        var checkResult = false;
                        var typeValueText = param.TipoValorId == TypeValue.Etiqueta || param.TipoValorId == TypeValue.Observacion;
                        var typeValueNumeric = param.TipoValorId == TypeValue.Numerico || param.TipoValorId == TypeValue.NumericoSexo || param.TipoValorId == TypeValue.NumericoEdad || param.TipoValorId == TypeValue.NumericoEdadSexo;
                        var fcsiExists = !string.IsNullOrEmpty(param.FCSI);

                        if (param.Resultado != null && typeValueNumeric)
                        {
                            if (param.ValorInicial != null && param.ValorFinal != null)
                                checkResult = decimal.Parse(param.Resultado) > decimal.Parse(param?.ValorFinal) || decimal.Parse(param.Resultado) < decimal.Parse(param?.ValorInicial);
                            else
                                checkResult = false;
                        }

                        List<Col> col = new List<Col>()
                        {
                            new Col(param.Nombre, 14, param.TipoValorId == TypeValue.Etiqueta ? fontCritic : fontParam, ParagraphAlignment.Left){
                                Fill = typeValueText ? TabLeader.Spaces : TabLeader.Dots
                            },
                            new Col(checkResult ? $"*{param.Resultado}" : param.Resultado, 7, checkResult ? fontCritic : fontParam, ParagraphAlignment.Center),
                            new Col(param.UnidadNombre, 6, fontParam, ParagraphAlignment.Center),
                            new Col(typeValueText ? "" : $"{param.ValorInicial} - {param.ValorFinal}", 6, fontParam, ParagraphAlignment.Center),
                        };

                        if (param.TipoValorId == TypeValue.Texto || param.TipoValorId == TypeValue.Parrafo || param.TipoValorId == TypeValue.Observacion)
                        {
                            col.RemoveAt(3);
                            col.RemoveAt(2);
                            col[1].Tamaño = 19;
                            col[1].Horizontal = ParagraphAlignment.Left;
                        }

                        var firstColumn = string.Join("\n", param.ValoresReferencia.Select(x => x.PrimeraColumna));
                        var secondColumn = string.Join("\n", param.ValoresReferencia.Select(x => x.SegundaColumna));
                        var thirdColumn = string.Join("\n", param.ValoresReferencia.Select(x => x.TerceraColumna));
                        var fourthColumn = string.Join("\n", param.ValoresReferencia.Select(x => x.CuartaColumna));
                        var fifthColumn = string.Join("\n", param.ValoresReferencia.Select(x => x.QuintaColumna));

                        var font2Column = new Font("calibri", 9);
                        var font3Column = new Font("calibri", 8);
                        var font4Column = new Font("calibri", 7);
                        var font5Column = new Font("calibri", 6);


                        if (param.TipoValorId == TypeValue.Numerico2Columna)
                        {
                            if (param.Resultado == "." && param.EstudioId == CTG)
                            {
                                col[2] = new Col(firstColumn, 6, font2Column, ParagraphAlignment.Center);
                                col[3] = new Col(secondColumn, 6, font2Column, ParagraphAlignment.Center);
                            }
                            else if (param.Resultado != ".")
                            {
                                col[2] = new Col(firstColumn, 6, font2Column, ParagraphAlignment.Center);
                                col[3] = new Col(secondColumn, 6, font2Column, ParagraphAlignment.Center);
                            }
                        }

                        else if (param.TipoValorId == TypeValue.Numerico3Columna)
                        {
                            col[2] = new Col(firstColumn + "\t" + secondColumn, 8, font3Column, ParagraphAlignment.Center);
                            col[3] = new Col(thirdColumn, 4, font3Column, ParagraphAlignment.Center);
                        }

                        else if (param.TipoValorId == TypeValue.Numerico4Columna)
                        {
                            col[2] = new Col(firstColumn + "\t" + secondColumn, 6, font4Column, ParagraphAlignment.Center);
                            col[3] = new Col(thirdColumn + "\t" + fourthColumn, 6, font4Column, ParagraphAlignment.Center);
                        }

                        else if (param.TipoValorId == TypeValue.Numerico5Columna)
                        {
                            col[2] = new Col(firstColumn + "\t" + secondColumn, 4, font5Column, ParagraphAlignment.Center);
                            col[3] = new Col(thirdColumn + "\t" + fourthColumn + "\t" + fifthColumn, 8, font5Column, ParagraphAlignment.Center);
                        }



                        var glucoseToleranceValues = param.TipoValorId == TypeValue.Numerico || param.TipoValorId == TypeValue.Numerico1Columna;

                        if (param.EstudioId == CTG && glucoseToleranceValues && param.Clave != "_OB_CTG")
                        {
                            try
                            {
                                var numericResult = Convert.ToDouble(param.Resultado);
                                series.Add(numericResult);

                                RenderGlucose(param.Clave, xseries);
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }

                        if (results.ImprimirPrevios) col.Insert(2, new Col(param.UltimoResultado != null ? param.UltimoResultado : "-", 6, Col.FONT_SUBTITLE_BOLD));
                        section.AddBorderedText(col.ToArray(), top: false, right: false, bottom: false, left: false);

                        var fcsiText = new Col(param.FCSI, 14, font2Column, ParagraphAlignment.Left);

                        if(fcsiExists)
                            section.AddText(fcsiText);
                    }

                    var methodExists = !string.IsNullOrEmpty(results.SolicitudInfo?.Metodo);
                    var methodText = new Col($"Método: {results.SolicitudInfo?.Metodo}", 18, fontText, ParagraphAlignment.Left);

                    if (methodExists)
                    {
                        section.AddSpace(2);
                        section.AddText(methodText);
                    }

                    if (studyParam.Any(x => x.EstudioId == CTG))
                    {
                        section.AddPageBreak();

                        var titleChart = new Col("Gráfica Curva de Tolerancia a Glucosa", 14, fontTitle, ParagraphAlignment.Left);
                        section.AddText(titleChart);
                        section.AddSpace(5);

                        chart.XAxis.MajorTickMark = TickMarkType.Outside;
                        chart.XAxis.Title.Caption = "Tiempo Post Carga de Glucosa min.";

                        chart.YAxis.MajorTickMark = TickMarkType.Outside;
                        chart.YAxis.Title.Caption = "Concentración de Glucosa mg/dl";
                        chart.YAxis.Title.Orientation = 90;
                        chart.YAxis.Title.VerticalAlignment = VerticalAlignment.Center;
                        chart.YAxis.HasMajorGridlines = true;

                        chart.PlotArea.LineFormat.Color = Colors.DarkGray;
                        chart.PlotArea.LineFormat.Width = 1;

                        section.Add(chart);
                    }

                    section.AddSpace(10);

                    if (results.ImprimirCriticos)
                    {
                        var notNumericTypeValue = studyParam.Where(x => x.Resultado != null &&
                        (x.TipoValorId != TypeValue.OpcionMultiple
                        && x.TipoValorId != TypeValue.Numerico1Columna
                        && x.TipoValorId != TypeValue.Numerico2Columna
                        && x.TipoValorId != TypeValue.Numerico3Columna
                        && x.TipoValorId != TypeValue.Numerico4Columna
                        && x.TipoValorId != TypeValue.Numerico5Columna
                        && x.TipoValorId != TypeValue.Texto
                        && x.TipoValorId != TypeValue.Parrafo
                        && x.TipoValorId != TypeValue.Etiqueta
                        && x.TipoValorId != TypeValue.Observacion));

                        var checkCritics = notNumericTypeValue.Any(x => decimal.Parse(x.Resultado) >= x.CriticoMaximo || decimal.Parse(x.Resultado) <= x.CriticoMinimo);
                        var criticTitle = new Col(checkCritics ? "VALORES CRÍTICOS" : "", 14, fontTitle, ParagraphAlignment.Left);

                        section.AddText(criticTitle);
                        section.AddSpace(5);

                        foreach (var param in notNumericTypeValue)
                        {
                            if (param.Resultado != null && (decimal.Parse(param.Resultado) >= param.CriticoMaximo || decimal.Parse(param.Resultado) <= param.CriticoMinimo))
                            {
                                var col = new Col[]
                                {
                                    new Col(param.Resultado, 7, fontCritic, ParagraphAlignment.Left),
                                };
                                section.AddBorderedText(col, top: false, right: false, bottom: false, left: false);
                            }
                        }
                    }

                    section.AddSpace(5);

                    if (i < studyParameter.Count - 1)
                    {
                        section.AddPageBreak();
                    }
                }

                RenderFooter(section, results);

            }
        }

        private static void RenderGlucose(string clave, XSeries xseries)
        {
            if (clave == "_GLU_SU")
            {
                xseries.Add("0");
            }
            if (clave == "_GLU_SU30")
            {
                xseries.Add("30");
            }
            if (clave == "_GLU_SU60")
            {
                xseries.Add("60");
            }
            if (clave == "_GLU_SU90")
            {
                xseries.Add("90");
            }
            if (clave == "_GLU_SU120")
            {
                xseries.Add("120");
            }
            if (clave == "_GLU_SU180")
            {
                xseries.Add("180");
            }
            if (clave == "_GLU_SU240")
            {
                xseries.Add("240");
            }
        }

        private static void RenderHeader(Section section, ClinicResultsPdfDto results, Font fontText)
        {
            var logoLab = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets\\LabRamosLogo.png");
            var logoISO = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets\\ISOLogo.png");

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

            var line1 = new Col[]
                {
                        new Col("Doctor (a)", 12, fontText, ParagraphAlignment.Left),
                        new Col($": {results.SolicitudInfo?.Medico}", 18, Col.FONT_SUBTITLE_BOLD, ParagraphAlignment.Left),
                        new Col("Expediente", 12, fontText, ParagraphAlignment.Left),
                        new Col($": {results.SolicitudInfo?.Expediente}", 18, Col.FONT_SUBTITLE_BOLD, ParagraphAlignment.Left)
                };
            header.AddBorderedText(line1, top: true, right: false, left: false);

            var line2 = new Col[]
            {
                        new Col("Paciente", 12, fontText, ParagraphAlignment.Left),
                        new Col($": {results.SolicitudInfo?.Paciente}", 18, Col.FONT_SUBTITLE_BOLD, ParagraphAlignment.Left),
                        new Col("Edad", 12, fontText, ParagraphAlignment.Left),
                        new Col($": {results.SolicitudInfo?.Edad}", 18, Col.FONT_SUBTITLE_BOLD, ParagraphAlignment.Left)
            };
            header.AddBorderedText(line2, right: false, left: false);

            var line3 = new Col[]
            {
                        new Col("No. Solicitud", 12, fontText, ParagraphAlignment.Left),
                        new Col($": {results.SolicitudInfo?.Clave}", 18, Col.FONT_SUBTITLE_BOLD, ParagraphAlignment.Left),
                        new Col("Sexo", 12, fontText, ParagraphAlignment.Left),
                        new Col($": {results.SolicitudInfo?.Sexo}", 18, Col.FONT_SUBTITLE_BOLD, ParagraphAlignment.Left),
            };
            header.AddBorderedText(line3, right: false, left: false);

            var line4 = new Col[]
            {
                        new Col("Fecha de Admisión", 12, fontText, ParagraphAlignment.Left),
                        new Col($": {results.SolicitudInfo?.FechaAdmision}", 18, Col.FONT_SUBTITLE_BOLD, ParagraphAlignment.Left),
                        new Col("Fecha de Entrega", 12, fontText, ParagraphAlignment.Left),
                        new Col($": {results.SolicitudInfo?.FechaEntrega}", 18, Col.FONT_SUBTITLE_BOLD, ParagraphAlignment.Left),
            };
            header.AddBorderedText(line4, right: false, left: false);

            var line5 = new Col[]
            {
                        new Col("Compañía", 12, fontText, ParagraphAlignment.Left),
                        new Col($": {(results.SolicitudInfo?.Compañia == null ? "Particulares" : results.SolicitudInfo.Compañia)}", 18, Col.FONT_SUBTITLE_BOLD, ParagraphAlignment.Left),
                        new Col("Impreso a las", 12, fontText, ParagraphAlignment.Left),
                        new Col($": {DateTime.Now.ToString("t")}", 18, Col.FONT_SUBTITLE_BOLD, ParagraphAlignment.Left),
            };
            header.AddBorderedText(line5, right: false, left: false);

            header.AddSpace(results.SolicitudInfo?.Paciente.Length > 25 ? 10 : 20);

            List<Col> studyHeader = new List<Col>()
                    {
                        new Col("EXAMEN", 14, Col.FONT_SUBTITLE_BOLD, ParagraphAlignment.Left),
                        new Col("RESULTADO", 7, Col.FONT_SUBTITLE_BOLD),
                        new Col("UNIDADES", 6, Col.FONT_SUBTITLE_BOLD),
                        new Col("REFERENCIA", 6, Col.FONT_SUBTITLE_BOLD),
                    };
            if (results.ImprimirPrevios) studyHeader.Insert(2, new Col("PREVIO", 6, Col.FONT_SUBTITLE_BOLD));
            header.AddBorderedText(studyHeader.ToArray(), top: true, right: false, bottom: true, left: false);
        }

        private static void RenderFooter(Section section, ClinicResultsPdfDto results)
        {
            var fontFooterTitle = new Font("Calibri", 10) { Bold = true };
            var fontFooterSubtitle = new Font("Calibri", 7) { Bold = true };
            var fontFooterText = new Font("Calibri", 6) { Bold = true };

            var firma = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets\\firmaEjemplo.png");
            var firmaImage = File.ReadAllBytes(firma);

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
                new Col("CD. OBREGÓN, SON.", 4, fontFooterTitle,  ParagraphAlignment.Center),
                        new Col("GUAYMAS, SON.", 1, fontFooterTitle,  ParagraphAlignment.Center),
                        new Col("HERMOSILLO, SON.", 3, fontFooterTitle,  ParagraphAlignment.Center),
            };

            footer.AddText(footerTitleBranches);

            var footerBranchesUnits = new Col[]
            {
                        new Col("UNIDAD CENTRO", 1, fontFooterSubtitle,  ParagraphAlignment.Center),
                        new Col("UNIDAD 200", 1, fontFooterSubtitle,  ParagraphAlignment.Center),
                        new Col("UNIDAD 300", 1, fontFooterSubtitle,  ParagraphAlignment.Center),
                        new Col("CENTRO MÉDICO SUR SONORA", 1, fontFooterSubtitle,  ParagraphAlignment.Center),
                        new Col("UNIDAD GUAYMAS", 1, fontFooterSubtitle,  ParagraphAlignment.Center),
                        new Col("CENTRO MÉDICO DEL RÍO", 1, fontFooterSubtitle,  ParagraphAlignment.Center),
                        new Col("MORELOS", 1, fontFooterSubtitle,  ParagraphAlignment.Center),
                        new Col("NAVARRETE", 1, fontFooterSubtitle,  ParagraphAlignment.Center),
            };

            footer.AddText(footerBranchesUnits);

            var footerBranchesAddress = new Col[]
            {
                        new Col("Sinaloa No. 144 Sur Col. CentroTels. (664) 415-16-92, 414-08-41, 414-08-42, 415-31-39 y 415-31-40", 1, fontFooterText,  ParagraphAlignment.Center),
                        new Col("Calle 200 Casi Esq. con Michoacán Tels. (644)412-31-56 y 416-14-07", 1, fontFooterText,  ParagraphAlignment.Center),
                        new Col("Jalisco No. 2250 Esq. Calle 300 Plaza Perisur Cd. Obregón, Son. Tel. (644) 444-66-69", 1, fontFooterText,  ParagraphAlignment.Center),
                        new Col("Calle Norte No. 749 Ote. Esq. con Sonora Tel(644) 415-06-66", 1, fontFooterText ,  ParagraphAlignment.Center),
                        new Col("Calzada Agustín García López Local 6 esq. Paseo de las Villas Fracc. Las Villas Tel. (622) 218-9183", 1, fontFooterText,  ParagraphAlignment.Center),
                        new Col("Reforma No. 273 Sur Tel. (662) 213-6866", 1, fontFooterText,  ParagraphAlignment.Center),
                        new Col("Blvd. Morelos No. 357 Col. El Bachoco Tels. (662) 267-8635 y 37", 1, fontFooterText,  ParagraphAlignment.Center),
                        new Col("Blvd. Navarrete No. 292 Col. Raquet Club Tel. (662) 216-3342", 1, fontFooterText,  ParagraphAlignment.Center),
            };

            footer.AddText(footerBranchesAddress);
            section.AddSpace(3);

            var footerNoPage = new Col[]
            {
                        new Col("Página {pageNumber}/{pageCount}", 1, fontFooterTitle, ParagraphAlignment.Right),
            };
            footer.AddText(footerNoPage);
        }

        public class TypeValue
        {
            public const byte Numerico = 1;
            public const byte NumericoSexo = 2;
            public const byte NumericoEdad = 3;
            public const byte NumericoEdadSexo = 4;
            public const byte OpcionMultiple = 5;
            public const byte Numerico1Columna = 6;
            public const byte Texto = 7;
            public const byte Parrafo = 8;
            public const byte Etiqueta = 9;
            public const byte Observacion = 10;
            public const byte Numerico2Columna = 11;
            public const byte Numerico3Columna = 12;
            public const byte Numerico4Columna = 13;
            public const byte Numerico5Columna = 14;

        }

    }
}