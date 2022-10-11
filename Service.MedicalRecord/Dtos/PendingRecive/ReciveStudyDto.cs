using System;

namespace Service.MedicalRecord.Dtos.PendingRecive
{
    public class ReciveStudyDto
    {
        public string Id { get; set; }
        public string Estudio { get; set; }
        public string Solicitud { get; set; }
        public DateTime Horarecoleccion { get; set; }
        public DateTime Check { get; set; }
    }
}
