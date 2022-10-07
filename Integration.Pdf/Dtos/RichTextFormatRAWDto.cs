using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Integration.Pdf.Dtos
{
    public class RichTextFormatRAWDto
    {
        public List<Blocks> blocks { get; set; }
    }
    public class Blocks
    {
        public string Key { get; set; }
        public string Text { get; set; }
        public string Type { get; set; }
        public int Depth { get; set; }
        public List<InlineStyleRanges> InlineStyleRanges { get; set; }
        public List<EntityRanges> EntityRanges { get; set; }
        public Data Data { get; set; }

    }
    public class InlineStyleRanges
    {
        public int Offset { get; set; }
        public int Length { get; set; }
        public string Style { get; set; }
    }
    public class EntityRanges
    {

    }

    public class Data
    {
        public string TextAlign { get; set; }
    }

}