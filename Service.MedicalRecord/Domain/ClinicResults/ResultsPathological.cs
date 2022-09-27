using Service.MedicalRecord.Domain.Catalogs;
using System;

namespace Service.MedicalRecord.Domain.ClinicResults
{
    public class ClinicalResultsPathological
    {
        public Guid Id { get; set; }
        public Guid SolicitudId { get; set; }
        public virtual Request.Request Solicitud { get; set; }
        public int EstudioId { get; set; }
        public virtual Request.RequestStudy Estudio { get; set; }
        public string Descripcion_Macroscopica { get; set; }
        public string Descripcion_Microscopica { get; set; }
        public string Imagen_Patologica { get; set; }
        public string Diagnostico { get; set; }
        public string Muestra_Recibida { get; set; }
        public Guid? MedicoId { get; set; }
        public virtual Medic Medico { get; set; }

    }
}
