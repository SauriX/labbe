using Api.PDF.Models;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Api.PDF.Extensions
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

        public static void AddDivider(this Section section)
        {
            Paragraph p = section.AddParagraph();
            p.Format.LineSpacingRule = LineSpacingRule.Exactly;
            p.Format.LineSpacing = 0;
            p.Format.Borders.Bottom = new Border() { Width = Unit.FromPoint(1), Color = Colors.Black };
            p.Format.SpaceAfter = Unit.FromPoint(5);
        }

        public static void AddText(this Section section, Models.Col col, bool partialBold = false, bool inverted = false)
        {
            Paragraph paragraph = section.AddParagraph();
            paragraph.Format.Alignment = col.Horizontal;

            string[] split = col.Texto.Split(new[] { ':' }, 2);

            if (partialBold && split.Length == 2)
            {
                paragraph.AddFormattedText(split[0] + ": ", Models.Col.FONT_BOLD);
                paragraph.AddFormattedText(split[1], Models.Col.FONT_DEFAULT);
            }
            else
            {
                paragraph.AddFormattedText(col.Texto, col.Fuente);
            }

            if (inverted)
            {
                paragraph.Format.Font.Color = Colors.White;
                paragraph.Format.Shading.Color = Colors.DarkGray;
            }

            Paragraph p = section.AddParagraph();
            p.Format.LineSpacingRule = LineSpacingRule.Exactly;
            p.Format.LineSpacing = 0;
            p.Format.SpaceBefore = Unit.FromPoint(5);
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

            paragraph.AddFormattedText(col.Texto, col.Fuente);

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
                string[] split = col.Texto.Split(new[] { ':' }, 2);

                if (partialBold && split.Length == 2)
                {
                    paragraph.AddFormattedText(split[0] + ": ", Models.Col.FONT_BOLD);
                    paragraph.AddFormattedText(split[1], Models.Col.FONT_DEFAULT);
                }
                else
                {
                    paragraph.AddFormattedText(col.Texto, col.Fuente);
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
                //string[] split = col.Texto.Split(new[] { ':' }, 2);
                //if (Regex.IsMatch(col.Texto, @"REPEAT\(([^,]*,([ ]|)[\d]*)\)"))
                //{
                //    char[] splitChar = new[] { ',' };
                //    var param = Regex.Match(col.Texto, @"\(\s*([^)]+?)\s*\)");

                //    if (param.Success)
                //    {
                //        var funcText = param.ToString().Substring(1, param.ToString().Length - 2);
                //        var textToRep = funcText.Split(splitChar, 2)[0];
                //        var qtyOk = int.TryParse(funcText.Split(splitChar, 2)[1].Trim(), out int qty);
                //        if (qtyOk)
                //        {
                //            var text = new StringBuilder(textToRep.Length * qty).Insert(0, textToRep, qty).ToString();
                //            paragraph.AddFormattedText(text, col.Fuente);
                //        }
                //    }
                //}
                //else
                //{
                paragraph.AddFormattedText(col.Texto, col.Fuente);
                //}
            }
        }
    }
}