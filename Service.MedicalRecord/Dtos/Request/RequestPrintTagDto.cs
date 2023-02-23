namespace Service.MedicalRecord.Dtos.Request
{
    public class RequestPrintTagDto
    {
        public string Clave { get; set; }
        public string ClaveEtiqueta { get; set; }
        public string Paciente { get; set; }
        public string Estudios { get; set; }
        public string Ciudad { get; set; }
        public string Tipo { get; set; }
        public string EdadSexo { get; set; }
        public decimal Cantidad { get; set; }
        public string Medico { get; set; }
    }
}
