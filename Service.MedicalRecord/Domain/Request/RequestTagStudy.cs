namespace Service.MedicalRecord.Domain.Request
{
    public class RequestTagStudy : BaseModel
    {
        public int Id { get; set; }
        public int SolicitudEtiquetaId { get; set; }
        public virtual RequestTag SolicitudEtiqueta { get; set; }
        public int EstudioId { get; set; }
        public decimal Cantidad { get; set; }
        public int Orden { get; set; }
        public string NombreEstudio { get; set; }
    }
}