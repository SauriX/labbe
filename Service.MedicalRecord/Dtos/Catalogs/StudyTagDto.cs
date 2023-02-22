namespace Service.MedicalRecord.Dtos.Catalogs
{
    public class StudyTagDto
    {
        public string DestinoId { get; set; }
        public string Destino { get; set; }
        public byte DestinoTipo { get; set; }
        public int EtiquetaId { get; set; }
        public int EstudioId { get; set; }
        public string ClaveEtiqueta { get; set; }
        public string ClaveInicial { get; set; }
        public string Color { get; set; }
        public decimal Cantidad { get; set; }
        public int Orden { get; set; }
        public string NombreEtiqueta { get; set; }
        public string NombreEstudio { get; set; }
    }
}
