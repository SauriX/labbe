namespace Service.MedicalRecord.Dtos.WeeClinic
{
    public class WeeServiceAssignmentDto
    {
        public string IdServicio { get; set; }
        public string Estatus { get; set; }
        public string Mensaje { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public bool Asignado => Estatus == "1";
    }
}
