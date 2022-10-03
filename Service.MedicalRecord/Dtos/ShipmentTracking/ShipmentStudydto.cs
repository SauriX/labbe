namespace Service.MedicalRecord.Dtos.ShipmentTracking
{
    public class ShipmentStudydto
    {
        public string Estudio { get; set; }
        public string Paciente { get; set; }
        public string Solicitud { get; set; }
        public bool ConfirmacionOrigen { get; set; }
        public bool ConfirmacionDestino { get; set; }
    }
}
