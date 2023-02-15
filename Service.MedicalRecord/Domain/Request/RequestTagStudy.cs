namespace Service.MedicalRecord.Domain.Request
{
    public class RequestTagStudy : BaseModel
    {
        public int Id { get; set; }
        public int SolicitudEtiquetaId { get; set; }
        public RequestTag SolicitudEtiqueta { get; set; }
        public int EtiquetaId { get; set; }
        public int EstudioId { get; set; }
        public int Cantidad { get; set; }
        public int Orden { get; set; }
        public bool Manual { get; set; }
        public bool Borrado { get; set; }
        public string Nombre { get; set; }
        public string Identificador { get; set; }
        public string IdentificadorEtiqueta { get; set; }
    }
}
