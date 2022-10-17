using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
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
        public List<InlineStyleRangesList> InlineStyleRangesLists { get; set; }
        public List<EntityRanges> EntityRanges { get; set; }
        public Data Data { get; set; }

    }
    public class InlineStyleRanges
    {
        public int Offset { get; set; }
        public int Length { get; set; }
        private int end;
        public int End { 
            get { return end; } 
            set {
                if (Offset == 0 && Length == 0)
                {
                    end = 0;
                }
                else
                {
                    end = Offset + Length -1;

                }
            } 
        } 
        public string Style { get; set; }
    }
    public class InlineStyleRangesList
    {
        public int Offset { get; set; }
        public int Length { get; set; }
        private int end;
        public int End
        {
            get { return end; }
            set
            {
                if (Offset == 0 && Length == 0)
                {
                    end = 0;
                }
                else
                {
                    end = Offset + Length -1;

                }
            }
        }
        //public int End { get; set; }
        public List<string> Style { get; set; }
    }
    public class EntityRanges
    {

    }

    public class Data
    {
        [JsonPropertyName("text-align")]
        public string TextAlign { get; set; }

    }

}