using System;

namespace Service.MedicalRecord.Domain.Request
{
    public class RequestStudyWeeClinic
    {
        public int Id { get; set; }
        public int SolicitudEstudioId { get; set; }
        public Guid IdOrden { get; set; }
        public Guid IdNodo { get; set; }
        public Guid IdServicio { get; set; }
        public string Cubierto { get; set; }
        public int IsAvailable { get; set; }
        public int RestanteDays { get; set; }
        public int Vigencia { get; set; }
        public int isCancel { get; set; }
    }
}
