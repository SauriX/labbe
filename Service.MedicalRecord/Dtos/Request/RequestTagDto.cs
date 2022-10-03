namespace Service.MedicalRecord.Dtos.Request
{
    public class RequestTagDto
    {
        public string Clave { get; set; }
        public string Paciente { get; set; }
        public string Estudios { get; set; }
        public string ClaveEtiqueta { get; set; }
        public string Ciudad { get; set; }
        public string NombreInfo { get; set; }
        public string Tipo { get; set; }
        public string EdadSexo { get; set; }
        public int Cantidad { get; set; }
    }
}
