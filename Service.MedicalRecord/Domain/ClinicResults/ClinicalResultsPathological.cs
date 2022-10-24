using Service.MedicalRecord.Domain.Catalogs;
using System;

namespace Service.MedicalRecord.Domain
{
    public class ClinicalResultsPathological
    {
        public Guid Id { get; set; }
        public Guid SolicitudId { get; set; }
        public virtual Request.Request Solicitud { get; set; }
        public int SolicitudEstudioId { get; set; }
        public virtual Request.RequestStudy SolicitudEstudio { get; set; }
        public int RequestStudyId { get; set; }
        public string DescripcionMacroscopica { get; set; }
        public string DescripcionMicroscopica { get; set; }
        public string ImagenPatologica { get; set; }
        public string Diagnostico { get; set; }
        public string MuestraRecibida { get; set; }
        public Guid? MedicoId { get; set; }
        public virtual Medic Medico { get; set; }

    }
}
