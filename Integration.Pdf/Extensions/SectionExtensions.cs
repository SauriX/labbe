using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using System.Linq;

namespace Integration.Pdf.Extensions
{
    public static class SectionExtensions
    {
        public static void AddText(this Section section, Models.Col col, bool partialBold = false, bool inverted = false, int spaceAfter = 5)
        {
            Paragraph paragraph = section.AddParagraph();
            paragraph.Format.Alignment = col.Horizontal;

            string[] split = (col.Texto ?? "").Split(new[] { ':' }, 2);

            if (!col.EsImagen && partialBold && split.Length == 2)
            {
                paragraph.AddFormattedText(split[0] + ": ", Models.Col.FONT_BOLD);
                paragraph.AddFormattedText(split[1], Models.Col.FONT_DEFAULT);
            }
            else if (!col.EsImagen)
            {
                paragraph.AddFormattedText(col.Texto ?? "", col.Fuente);
            }
            else
            {
                string imageFilename = col.Imagen.MigraDocFilenameFromByteArray();
                var image = paragraph.AddImage(imageFilename);
                if (col.ImagenTamaño != null) image.Width = (Unit)col.ImagenTamaño;
                image.LockAspectRatio = true;
            }

            if (inverted)
            {
                paragraph.Format.Font.Color = Colors.White;
                paragraph.Format.Shading.Color = Colors.DarkGray;
            }

            if (col.Fill != null)
            {
                float sectionWidth = section.PageSetup.PageWidth - section.PageSetup.LeftMargin - section.PageSetup.RightMargin;
                paragraph.AddSpace(1);
                paragraph.Format.TabStops.AddTabStop(sectionWidth - 5, (TabAlignment.Right), (TabLeader)col.Fill);
                paragraph.AddTab();
            }

            Paragraph p = section.AddParagraph();
            p.Format.LineSpacingRule = LineSpacingRule.Exactly;
            p.Format.LineSpacing = 0;
            p.Format.SpaceBefore = Unit.FromPoint(spaceAfter);
        }

        public static void AddBorderedText(this Section section, Models.Col col, bool all = true, bool top = false, bool right = false, bool bottom = false, bool left = false)
        {
            Paragraph paragraph = section.AddParagraph();
            if (all)
            {
                paragraph.Format.Borders.Width = Unit.FromPoint(1);
                paragraph.Format.Borders.Color = Colors.Black;
            }
            if (top && !all)
            {
                paragraph.Format.Borders.Top = new Border() { Width = Unit.FromPoint(1), Color = Colors.Black };
            }
            if (right && !all)
            {
                paragraph.Format.Borders.Right = new Border() { Width = Unit.FromPoint(1), Color = Colors.Black };
            }
            if (bottom && !all)
            {
                paragraph.Format.Borders.Bottom = new Border() { Width = Unit.FromPoint(1), Color = Colors.Black };
            }
            if (left && !all)
            {
                paragraph.Format.Borders.Left = new Border() { Width = Unit.FromPoint(1), Color = Colors.Black };
            }
            paragraph.Format.Alignment = col.Horizontal;

            if (!col.EsImagen)
            {
                paragraph.AddFormattedText(col.Texto ?? "", col.Fuente);
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
                float sectionWidth = section.PageSetup.PageWidth - section.PageSetup.LeftMargin - section.PageSetup.RightMargin;
                paragraph.AddSpace(1);
                paragraph.Format.TabStops.AddTabStop(sectionWidth - 5, (TabAlignment.Right), (TabLeader)col.Fill);
                paragraph.AddTab();
            }

            Paragraph p = section.AddParagraph();
            p.Format.LineSpacingRule = LineSpacingRule.Exactly;
            p.Format.LineSpacing = 0;
            p.Format.SpaceBefore = Unit.FromPoint(5);
        }

        public static void AddText(this Section section, Models.Col[] cols, bool partialBold = false)
        {
            Table table = section.AddTable();
            table.Borders.Visible = false;
            table.TopPadding = 0;
            table.BottomPadding = 0;

            float sectionWidth = section.PageSetup.PageWidth - section.PageSetup.LeftMargin - section.PageSetup.RightMargin;
            float columnWidth = sectionWidth / cols.Sum(x => x.Tamaño);

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
                    paragraph.AddFormattedText(split[1], Models.Col.FONT_DEFAULT);
                }
                else if (!col.EsImagen)
                {
                    paragraph.AddFormattedText(col.Texto ?? "", col.Fuente);
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

            Paragraph p = section.AddParagraph();
            p.Format.LineSpacingRule = LineSpacingRule.Exactly;
            p.Format.LineSpacing = 0;
            p.Format.SpaceBefore = Unit.FromPoint(5);
        }

        public static void AddBorderedText(this Section section, Models.Col[] cols, bool all = false, bool top = false, bool right = false, bool bottom = false, bool left = false)
        {
            Table table = section.AddTable();
            if (!all && !top && !right && !bottom && !left)
            {
                table.Borders.Visible = false;
            }

            table.TopPadding = Unit.FromPoint(2.5);
            table.RightPadding = Unit.FromPoint(2.5);
            table.BottomPadding = Unit.FromPoint(2.5);
            table.LeftPadding = Unit.FromPoint(2.5);

            float sectionWidth = section.PageSetup.PageWidth - section.PageSetup.LeftMargin - section.PageSetup.RightMargin;
            float columnWidth = sectionWidth / cols.Sum(x => x.Tamaño);

            for (int i = 0; i < cols.Length; i++)
            {
                Column column = table.AddColumn();
                column.Width = columnWidth * cols[i].Tamaño;
                column.Format.Alignment = cols[i].Horizontal;
            }

            Row row = table.AddRow();
            row.VerticalAlignment = VerticalAlignment.Center;

            for (int i = 0; i < cols.Length; i++)
            {
                Models.Col col = cols[i];
                var cell = row.Cells[i];

                if (all)
                {
                    cell.Borders.Width = Unit.FromPoint(1);
                    cell.Borders.Color = Colors.Black;
                }
                else
                {
                    if (top)
                    {
                        cell.Borders.Top = new Border() { Width = Unit.FromPoint(1), Color = Colors.Black };
                    }
                    if (right && i == cols.Length - 1)
                    {
                        cell.Borders.Right = new Border() { Width = Unit.FromPoint(1), Color = Colors.Black };
                    }
                    if (bottom)
                    {
                        cell.Borders.Bottom = new Border() { Width = Unit.FromPoint(1), Color = Colors.Black };
                    }
                    if (left && i == 0)
                    {
                        cell.Borders.Left = new Border() { Width = Unit.FromPoint(1), Color = Colors.Black };
                    }
                }

                Paragraph paragraph = cell.AddParagraph();

                if (!col.EsImagen)
                {
                    paragraph.AddFormattedText(col.Texto ?? "", col.Fuente);
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
        }
    }
}