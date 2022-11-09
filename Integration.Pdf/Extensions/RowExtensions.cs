using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.DocumentObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Integration.Pdf.Extensions
{
    public static class RowExtensions
    {
        public static void AddText(this Row section, Models.Col[] cols, bool partialBold = false, int fontSize = 0, bool bold = false)
        {
            var cell = section.Cells[0];

            Table table = new Table();
            table.Borders.Visible = false;
            table.TopPadding = 0;
            table.BottomPadding = 0;

            float cellWidth = cell.Section.PageSetup.PageWidth - cell.Section.PageSetup.LeftMargin - cell.Section.PageSetup.RightMargin;
            float columnWidth = cellWidth / cols.Sum(x => x.Tamaño);

            for (int i = 0; i < cols.Length; i++)
            {
                Column column = table.AddColumn();
                column.LeftPadding = 0;
                column.RightPadding = 0;
                column.Width = columnWidth * cols[i].Tamaño;
                column.Format.Alignment = cols[i].Horizontal;
            }

            Row row = table.AddRow();
            row.VerticalAlignment = VerticalAlignment.Center;

            for (int i = 0; i < cols.Length; i++)
            {
                Models.Col col = cols[i];

                Paragraph paragraph = row.Cells[i].AddParagraph();
                string[] split = (col.Texto ?? "").Split(new[] { ':' }, 2);

                if (!col.EsImagen && partialBold && split.Length == 2)
                {
                    paragraph.AddFormattedText(split[0] + ": ", Models.Col.FONT_BOLD);
                    paragraph.AddFormattedText(split[1], fontSize == 0 ? Models.Col.FONT_DEFAULT : new Font("Calibri", fontSize));
                }
                else if (!col.EsImagen)
                {
                    var fp = paragraph.AddFormattedText(col.Texto ?? "", fontSize == 0 ? col.Fuente : new Font("Calibri", fontSize));
                    fp.Bold = col.Fuente.Bold || bold;
                }
                else
                {
                    string imageFilename = col.Imagen.MigraDocFilenameFromByteArray();
                    var image = paragraph.AddImage(imageFilename);
                    if (col.ImagenTamaño != null) image.Width = (Unit)col.ImagenTamaño;
                    image.LockAspectRatio = true;
                }

                if (col.Fill != null)
                {
                    paragraph.AddSpace(1);
                    paragraph.Format.TabStops.AddTabStop(columnWidth * cols[i].Tamaño - 5, (TabAlignment.Right), (TabLeader)col.Fill);
                    paragraph.AddTab();
                }
            }

            cell.Elements.Add(table);

            Paragraph p = cell.AddParagraph();
            p.Format.LineSpacingRule = LineSpacingRule.Exactly;
            p.Format.LineSpacing = 0;
            p.Format.SpaceBefore = Unit.FromPoint(5);
        }
    }
}