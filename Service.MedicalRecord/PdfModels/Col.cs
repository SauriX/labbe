namespace Service.MedicalRecord.PdfModels
{
    public class Col
    {
        public string Texto { get; set; }
        public int Tamaño { get; set; }
        public string Formato { get; set; }
        public ParagraphAlignment Horizontal { get; set; }

        public Col() { }

        public Col(string texto)
        {
            Texto = texto;
            Tamaño = 1;
            Horizontal = ParagraphAlignment.Center;
        }

        public Col(string texto, int tamaño)
        {
            Texto = texto;
            Tamaño = tamaño;
            Horizontal = ParagraphAlignment.Center;
        }

        public Col(string texto, ParagraphAlignment horizontal)
        {
            Texto = texto;
            Tamaño = 1;
            Horizontal = horizontal;
        }

        public Col(string texto, ParagraphAlignment horizontal, string formato)
        {
            Texto = texto;
            Tamaño = 1;
            Horizontal = horizontal;
            Formato = formato;
        }

        public Col(string texto, int tamaño, ParagraphAlignment horizontal)
        {
            Texto = texto;
            Tamaño = tamaño;
            Horizontal = horizontal;
        }

        public Col(string texto, int tamaño, ParagraphAlignment horizontal, string formato)
        {
            Texto = texto;
            Tamaño = tamaño;
            Horizontal = horizontal;
            Formato = formato;
        }
    }
}
