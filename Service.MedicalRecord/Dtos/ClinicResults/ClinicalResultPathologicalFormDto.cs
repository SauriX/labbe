using System;

namespace Service.MedicalRecord.Dtos.ClinicResults
{
    public class ClinicalResultPathologicalFormDto
    {
        public Guid Id { get; set; }
        public Guid SolicitudId { get; set; }
        public int EstudioId { get; set; }
        public int RequestStudyId  { get; set; }
        public string DescripcionMacroscopica { get; set; }
        public string DescripcionMicroscopica { get; set; }
        public string ImagenPatologica { get; set; }
        public string Diagnostico { get; set; }
        public string MuestraRecibida { get; set; }
        public Guid? MedicoId { get; set; }
    }
}
