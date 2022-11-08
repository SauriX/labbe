using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Integration.Pdf.Extensions
{
    public static class Extensions
    {
        public static void AddSpace(this Section section, double space = 5)
        {
            Paragraph p = section.AddParagraph();
            p.Format.LineSpacingRule = LineSpacingRule.Exactly;
            p.Format.LineSpacing = 0;
            p.Format.SpaceAfter = Unit.FromPoint(space);
        }

        public static void AddSpace(this Row row, double space = 5)
        {
            var cell = row.Cells[0];

            Paragraph p = cell.AddParagraph();
            p.Format.LineSpacingRule = LineSpacingRule.Exactly;
            p.Format.LineSpacing = 0;
            p.Format.SpaceAfter = Unit.FromPoint(space);
        }

        public static void AddSpace(this HeaderFooter headerFooter, double space = 5)
        {
            Paragraph p = headerFooter.AddParagraph();
            p.Format.LineSpacingRule = LineSpacingRule.Exactly;
            p.Format.LineSpacing = 0;
            p.Format.SpaceAfter = Unit.FromPoint(space);
        }

        public static void AddDivider(this Section section, int width = 1, Color? color = null)
        {
            Paragraph p = section.AddParagraph();
            p.Format.LineSpacingRule = LineSpacingRule.Exactly;
            p.Format.LineSpacing = 0;
            p.Format.Borders.Bottom = new Border() { Width = Unit.FromPoint(width), Color = color ?? Colors.Black };
            p.Format.SpaceAfter = Unit.FromPoint(5);
        }

        public static void AddDivider(this Row parentRow, int width = 1, Color? color = null)
        {
            var parentCell = parentRow.Cells[0];

            Table table = new Table();
            table.Borders.Visible = false;
            table.TopPadding = 0;
            table.BottomPadding = 0;

            float cellWidth = parentCell.Section.PageSetup.PageWidth - parentCell.Section.PageSetup.LeftMargin - parentCell.Section.PageSetup.RightMargin;

            Column column = table.AddColumn();
            column.LeftPadding = 0;
            column.RightPadding = 0;
            column.Width = cellWidth;

            var row = table.AddRow();

            var cell = row.Cells[0];

            Paragraph p = cell.AddParagraph();
            p.Format.LineSpacingRule = LineSpacingRule.Exactly;
            p.Format.LineSpacing = 0;
            p.Format.Borders.Bottom = new Border() { Width = Unit.FromPoint(width), Color = color ?? Colors.Black };
            p.Format.SpaceAfter = Unit.FromPoint(5);

            parentCell.Elements.Add(table);
        }

        public static void AddDivider(this HeaderFooter headerFooter, int width = 1, Color? color = null)
        {
            Paragraph p = headerFooter.AddParagraph();
            p.Format.LineSpacingRule = LineSpacingRule.Exactly;
            p.Format.LineSpacing = 0;
            p.Format.Borders.Bottom = new Border() { Width = Unit.FromPoint(width), Color = color ?? Colors.Black };
            p.Format.SpaceAfter = Unit.FromPoint(5);
        }

        public static string MigraDocFilenameFromByteArray(this byte[] image)
        {
            return "base64:" + Convert.ToBase64String(image);
        }

        public static IEnumerable<IEnumerable<T>> ToChunks<T>(this IEnumerable<T> enumerable, int chunkSize, bool fill = false)
        {
            int itemsReturned = 0;
            var list = enumerable.ToList();

            if (fill)
            {
                var missing = chunkSize - list.Count % chunkSize;
                if (missing > 0)
                    list.AddRange(Enumerable.Repeat<T>(default(T), missing));
            }

            int count = list.Count;
            while (itemsReturned < count)
            {
                int currentChunkSize = Math.Min(chunkSize, count - itemsReturned);
                yield return list.GetRange(itemsReturned, currentChunkSize);
                itemsReturned += currentChunkSize;
            }
        }
    }
}