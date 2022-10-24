using System;

namespace Service.MedicalRecord.Dtos.ShipmentTracking
{
    public class ReciveShipmentStudyDto
    {
        public Guid Id { get; set; }
        public string Estudio { get; set; }
        public string Paciente { get; set; }
        public string Solicitud { get; set; }
        public bool ConfirmacionOrigen { get; set; }
        public bool ConfirmacionDestino { get; set; }
        public decimal Temperatura { get; set; }
    }
}
