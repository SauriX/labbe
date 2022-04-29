namespace Service.Catalog.Dtos.Parameters
{
    public class ValorTipeForm
    {
        public string id { get; set; }
        public string idParametro { get; set; }
        public string nombre { get; set; }
        public int valorInicial { get; set; }
        public int valorFinal { get; set; }
        public int valorInicialNumerico { get; set; } 
        public int valorFinalNumerico { get; set; }
        public int rangoEdadInicial { get; set; }
        public int rangoEdadFinal { get; set; }
        public int hombreValorInicial { get; set; }
        public int hombreValorFinal { get; set; }
        public int mujerValorInicial { get; set; }
        public int mujerValorFinal { get; set; }
        public int medidaTiempo { get; set; }
        public string opcion { get; set; }
        public string descripcionTexto { get; set; }
        public string descripcionParrafo { get; set; }
    }
}
