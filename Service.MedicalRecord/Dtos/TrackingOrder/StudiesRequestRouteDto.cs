namespace Service.MedicalRecord.Dtos.TrackingOrder
{
    public class StudiesRequestRouteDto
    {
        public string Clave { get; set; }
        public string Estudio { get; set; }
        public string Solicitud { get; set; }
        public string Paciente { get; set; }
        public bool Escaneado { get; set; }
        public decimal Temperatura { get; set; }
        public string TaponNombre { get; set; }
    }
}
