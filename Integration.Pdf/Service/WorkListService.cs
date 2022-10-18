using Integration.Pdf.Dtos;
using Integration.Pdf.Extensions;
using Integration.Pdf.Models;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using System;
using System.IO;
using System.Linq;
using System.Text;
using ZXing.OneD;

namespace Integration.Pdf.Service
{
    public class WorkListService
    {
        public static byte[] Generate(WorkListDto workList)
        {
            Document document = CreateDocument(workList);

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

        static Document CreateDocument(WorkListDto workList)
        {
            Document document = new Document();

            Section section = document.AddSection();

            section.PageSetup = document.DefaultPageSetup.Clone();

            section.PageSetup.Orientation = Orientation.Portrait;
            section.PageSetup.PageFormat = PageFormat.Letter;

            section.PageSetup.TopMargin = Unit.FromCentimeter(5.2);
            section.PageSetup.BottomMargin = Unit.FromCentimeter(2.5);
            section.PageSetup.LeftMargin = Unit.FromCentimeter(1);
            section.PageSetup.RightMargin = Unit.FromCentimeter(1);

            Format(section, workList);

            return document;
        }

        static void Format(Section section, WorkListDto workList)
        {
            var header = section.Headers.Primary;
            var headerFont = new Font("Calibri", 14) { Bold = true };

            var title = new Col("Laboratorio Alfonso Ramos S.A. de C.V.", headerFont);
            header.AddText(title);

            header.AddSpace(10);

            var wl = new Col($"HOJA DE TRABAJO ({workList.HojaTrabajo})", headerFont);
            header.AddText(wl);

            var branches = new Col($"SUCURSAL ({workList.Sucursal})");
            header.AddText(branches);

            var date = new Col($"FECHA ({workList.Fecha} {workList.HoraInicio}) AL ({workList.Fecha} {workList.HoraFin})");
            header.AddText(date);

            var headers = new Col[]
            {
                new Col("SOILICTUD", 1, Col.FONT_BOLD, ParagraphAlignment.Left),
                new Col("HORA", 1, Col.FONT_BOLD, ParagraphAlignment.Center),
                new Col("NOMBRE DEL PACIENTE", 4, Col.FONT_BOLD, ParagraphAlignment.Left),
                new Col("ESTATUS", 1, Col.FONT_BOLD, ParagraphAlignment.Center),
                new Col("HORA", 1, Col.FONT_BOLD, ParagraphAlignment.Center),
            };
            header.AddBorderedText(headers, top: true, bottom: true);

            for (int i = 0; i < workList.Solicitudes.Count; i++)
            {
                WorkListRequestDto request = workList.Solicitudes[i];
                var requestInfo = new Col[]
                {
                    new Col(request.Solicitud, 1, ParagraphAlignment.Left),
                    new Col(request.HoraSolicitud, 1, ParagraphAlignment.Center),
                    new Col(request.Paciente, 4, ParagraphAlignment.Left),
                    new Col(request.Estatus, 1, ParagraphAlignment.Center),
                    new Col(request.HoraEstatus, 1, ParagraphAlignment.Center),
                };
                section.AddText(requestInfo);

                foreach (var study in request.Estudios)
                {
                    var studyName = new Col(study.Estudio, Col.FONT_BOLD, ParagraphAlignment.Left);
                    section.AddText(studyName);

                    foreach (var parameters in study.ListasTrabajo.ToChunks(8, true))
                    {
                        var param = parameters.Select(x => new Col(x) { Fill = string.IsNullOrEmpty(x) ? (TabLeader?)null : TabLeader.Lines }).ToArray();
                        section.AddText(param);
                    }
                }

                if (i < workList.Solicitudes.Count - 1)
                {
                    section.AddSpace(8);
                    section.AddDivider(2, Colors.DarkGray);
                    section.AddSpace(8);
                }
            }

            var footer = section.Footers.Primary;
            var footerFont = new Font("Calibri", 8) { Italic = true };

            var fotterPageNo = new Col[]
            {
                new Col($"TRAMOS {DateTime.Now:dd/MM/yyyy HH:mm}", 1, footerFont, ParagraphAlignment.Left),
                new Col("Página {pageNumber}/{pageCount}", 1, footerFont, ParagraphAlignment.Right),
            };
            footer.AddText(fotterPageNo);

            var fotterClaimer = new Col($"Este informe no podrá ser reproducido total o parcialmente.", 1, footerFont, ParagraphAlignment.Left);
            footer.AddText(fotterClaimer);
        }
    }
}