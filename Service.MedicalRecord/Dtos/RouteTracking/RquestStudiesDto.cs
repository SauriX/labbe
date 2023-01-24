namespace Service.MedicalRecord.Dtos.RouteTracking
{
    public class RquestStudiesDto
    {
        public int Id { get; set; } 
        public string Clave { get; set; }
        public string Estudio { get; set; }
        public string Estatus { get; set; }
        public string Dias { get; set; }
        public string  Fecha { get; set; }
        public int EstatusId { get; set; }
    }
}
