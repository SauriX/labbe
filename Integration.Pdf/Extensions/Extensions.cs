using MigraDoc.DocumentObjectModel;
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

        public static void AddDivider(this Section section)
        {
            Paragraph p = section.AddParagraph();
            p.Format.LineSpacingRule = LineSpacingRule.Exactly;
            p.Format.LineSpacing = 0;
            p.Format.Borders.Bottom = new Border() { Width = Unit.FromPoint(1), Color = Colors.Black };
            p.Format.SpaceAfter = Unit.FromPoint(5);
        }
    }
}