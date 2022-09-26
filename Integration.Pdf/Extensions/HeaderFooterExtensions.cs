using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Integration.Pdf.Extensions
{
    public static class HeaderFooterExtensions
    {
        public static void AddText(this HeaderFooter headerFooter, Models.Col col, bool partialBold = false, bool inverted = false)
        {
            Paragraph paragraph = headerFooter.AddParagraph();
            paragraph.Format.Alignment = col.Horizontal;

            string[] split = (col.Texto ?? "").Split(new[] { ':' }, 2);

            if (partialBold && split.Length == 2)
            {
                paragraph.AddFormattedText(split[0] + ": ", Models.Col.FONT_BOLD);
                paragraph.AddFormattedText(split[1], Models.Col.FONT_DEFAULT);
            }
            else
            {
                paragraph.AddFormattedText(col.Texto ?? "", col.Fuente);
            }

            if (inverted)
            {
                paragraph.Format.Font.Color = Colors.White;
                paragraph.Format.Shading.Color = Colors.DarkGray;
            }

            Paragraph p = headerFooter.AddParagraph();
            p.Format.LineSpacingRule = LineSpacingRule.Exactly;
            p.Format.LineSpacing = 0;
            p.Format.SpaceBefore = Unit.FromPoint(5);
        }

        public static void AddBorderedText(this HeaderFooter headerFooter, Models.Col col, bool all = true, bool top = false, bool right = false, bool bottom = false, bool left = false)
        {
            Paragraph paragraph = headerFooter.AddParagraph();
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

            paragraph.AddFormattedText(col.Texto ?? "", col.Fuente);

            Paragraph p = headerFooter.AddParagraph();
            p.Format.LineSpacingRule = LineSpacingRule.Exactly;
            p.Format.LineSpacing = 0;
            p.Format.SpaceBefore = Unit.FromPoint(5);
        }

        public static void AddText(this HeaderFooter headerFooter, Models.Col[] cols, bool partialBold = false)
        {
            Table table = headerFooter.AddTable();
            table.Borders.Visible = false;
            table.TopPadding = 0;
            table.BottomPadding = 0;

            var section = headerFooter.Section;
            float sectionWidth = section.PageSetup.PageWidth - section.PageSetup.LeftMargin - section.PageSetup.RightMargin;
            float columnWidth = sectionWidth / cols.Sum(x => x.Tamaño);

            for (int i = 0; i < cols.Length; i++)
            {
                MigraDoc.DocumentObjectModel.Tables.Column column = table.AddColumn();
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

                if (partialBold && split.Length == 2)
                {
                    paragraph.AddFormattedText(split[0] + ": ", Models.Col.FONT_BOLD);
                    paragraph.AddFormattedText(split[1], Models.Col.FONT_DEFAULT);
                }
                else
                {
                    paragraph.AddFormattedText(col.Texto ?? "", col.Fuente);
                }
            }

            Paragraph p = headerFooter.AddParagraph();
            p.Format.LineSpacingRule = LineSpacingRule.Exactly;
            p.Format.LineSpacing = 0;
            p.Format.SpaceBefore = Unit.FromPoint(5);
        }

        public static void AddBorderedText(this HeaderFooter headerFooter, Models.Col[] cols, bool all = false, bool top = false, bool right = false, bool bottom = false, bool left = false)
        {
            Table table = headerFooter.AddTable();
            if (!all && !top && !right && !bottom && !left)
            {
                table.Borders.Visible = false;
            }

            table.TopPadding = Unit.FromPoint(2.5);
            table.RightPadding = Unit.FromPoint(2.5);
            table.BottomPadding = Unit.FromPoint(2.5);
            table.LeftPadding = Unit.FromPoint(2.5);

            var section = headerFooter.Section;
            float sectionWidth = section.PageSetup.PageWidth - section.PageSetup.LeftMargin - section.PageSetup.RightMargin;
            float columnWidth = sectionWidth / cols.Sum(x => x.Tamaño);

            for (int i = 0; i < cols.Length; i++)
            {
                MigraDoc.DocumentObjectModel.Tables.Column column = table.AddColumn();
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
                paragraph.AddFormattedText(col.Texto ?? "", col.Fuente);
            }
        }
    }
}