namespace Service.MedicalRecord.Dtos.Request
{
    public class RequestTagDto
    {
        public int Id { get; set; }
        public int EtiquetaId { get; set; }
        public int EstudioId { get; set; }
        public string Clave { get; set; }
        public string ClaveInicial { get; set; }
        public string Color { get; set; }
        public decimal Cantidad { get; set; }
        public int Orden { get; set; }
        public string Nombre { get; set; }
    }
}
