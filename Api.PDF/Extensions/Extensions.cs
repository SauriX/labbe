using Api.PDF.Models;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.PDF.Extensions
{
    public static class Extensions
    {
        public static void AddDivider(this Section section)
        {
            Paragraph p = section.AddParagraph();
            p.Format.LineSpacingRule = LineSpacingRule.Exactly;
            p.Format.LineSpacing = "0mm";
            p.Format.Borders.Bottom = new Border() { Width = "1pt", Color = Colors.Black };
            p.Format.SpaceAfter = Unit.FromPoint(5);
        }

        public static void AddText(this Section section, Col col, bool partialBold = false, bool inverted = false)
        {
            Paragraph paragraph = section.AddParagraph();
            paragraph.Format.Alignment = col.Horizontal;

            string[] split = col.Texto.Split(new[] { ':' }, 2);

            if (partialBold && split.Length == 2)
            {
                paragraph.AddFormattedText(split[0] + ": ", Col.FONT_BOLD);
                paragraph.AddFormattedText(split[1], Col.FONT_DEFAULT);
            }
            else
            {
                paragraph.AddFormattedText(col.Texto, col.Fuente);
            }

            if (inverted)
            {
                paragraph.Format.Font.Color = Colors.White;
                paragraph.Format.Shading.Color = Colors.Gray;
            }

            Paragraph p = section.AddParagraph();
            p.Format.LineSpacingRule = LineSpacingRule.Exactly;
            p.Format.LineSpacing = "0mm";
            p.Format.SpaceBefore = Unit.FromPoint(5);
        }

        public static void AddText(this Section section, Col[] cols, bool partialBold = false)
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
                Col col = cols[i];

                Paragraph paragraph = row.Cells[i].AddParagraph();
                string[] split = col.Texto.Split(new[] { ':' }, 2);

                if (partialBold && split.Length == 2)
                {
                    paragraph.AddFormattedText(split[0] + ": ", Col.FONT_BOLD);
                    paragraph.AddFormattedText(split[1], Col.FONT_DEFAULT);
                }
                else
                {
                    paragraph.AddFormattedText(col.Texto, col.Fuente);
                }
            }

            Paragraph p = section.AddParagraph();
            p.Format.LineSpacingRule = LineSpacingRule.Exactly;
            p.Format.LineSpacing = "0mm";
            p.Format.SpaceBefore = Unit.FromPoint(5);
        }
    }
}