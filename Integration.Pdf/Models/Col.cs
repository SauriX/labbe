using MigraDoc.DocumentObjectModel;

namespace Integration.Pdf.Models
{
    public class Col
    {
        public readonly static Font FONT_DEFAULT = new Font("Calibri", 9);
        public readonly static Font FONT_BOLD = new Font("Calibri", 9)
        {
            Bold = true
        };

        public string Texto { get; set; }
        public int Tamaño { get; set; }
        public string Formato { get; set; }
        public Font Fuente { get; set; }
        public ParagraphAlignment Horizontal { get; set; }

        public Col() { }

        public Col(string texto)
        {
            Texto = texto;
            Tamaño = 1;
            Fuente = FONT_DEFAULT;
            Horizontal = ParagraphAlignment.Center;
        }

        public Col(string texto, Font fuente)
        {
            Texto = texto;
            Tamaño = 1;
            Fuente = fuente;
            Horizontal = ParagraphAlignment.Center;
        }

        public Col(string texto, int tamaño)
        {
            Texto = texto;
            Tamaño = tamaño;
            Fuente = FONT_DEFAULT;
            Horizontal = ParagraphAlignment.Center;
        }

        public Col(string texto, ParagraphAlignment horizontal)
        {
            Texto = texto;
            Tamaño = 1;
            Fuente = FONT_DEFAULT;
            Horizontal = horizontal;
        }

        public Col(string texto, ParagraphAlignment horizontal, string formato)
        {
            Texto = texto;
            Tamaño = 1;
            Fuente = FONT_DEFAULT;
            Horizontal = horizontal;
            Formato = formato;
        }

        public Col(string texto, Font fuente, ParagraphAlignment horizontal)
        {
            Texto = texto;
            Tamaño = 1;
            Fuente = fuente;
            Horizontal = horizontal;
        }

        public Col(string texto, int tamaño, ParagraphAlignment horizontal)
        {
            Texto = texto;
            Tamaño = tamaño;
            Fuente = FONT_DEFAULT;
            Horizontal = horizontal;
        }

        public Col(string texto, int tamaño, Font fuente)
        {
            Texto = texto;
            Tamaño = tamaño;
            Fuente = fuente;
            Horizontal = ParagraphAlignment.Center;
        }

        public Col(string texto, int tamaño, Font fuente, ParagraphAlignment horizontal)
        {
            Texto = texto;
            Tamaño = tamaño;
            Fuente = fuente;
            Horizontal = horizontal;
        }

        public Col(string texto, int tamaño, ParagraphAlignment horizontal, string formato)
        {
            Texto = texto;
            Tamaño = tamaño;
            Fuente = FONT_DEFAULT;
            Horizontal = horizontal;
            Formato = formato;
        }
    }
}