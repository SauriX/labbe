using System;

namespace Service.MedicalRecord.Dtos.ClinicResults
{
    public class ClinicalResultPathologicalFormDto
    {
        public Guid Id { get; set; }
        public Guid SolicitudId { get; set; }
        public int EstudioId { get; set; }
        public string Descripcion_Macroscopica { get; set; }
        public string Descripcion_Microscopica { get; set; }
        public string Imagen_Patologica { get; set; }
        public string Diagnostico { get; set; }
        public string Muestra_Recibida { get; set; }
        public Guid? MedicoId { get; set; }
    }
}
