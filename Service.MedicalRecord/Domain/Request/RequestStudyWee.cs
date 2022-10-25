using System;

namespace Service.MedicalRecord.Domain.Request
{
    public class RequestStudyWee
    {
        public RequestStudyWee()
        {
        }

        public RequestStudyWee(string idOrden, string idNodo, string idServicio, string cubierto, int isAvailable, int restanteDays, int vigencia, int isCancel)
        {
            IdOrden = idOrden;
            IdNodo = idNodo;
            IdServicio = idServicio;
            Cubierto = cubierto;
            IsAvailable = isAvailable;
            RestanteDays = restanteDays;
            Vigencia = vigencia;
            IsCancel = isCancel;
        }

        public int Id { get; set; }
        public int SolicitudEstudioId { get; set; }
        public virtual RequestStudy SolicitudEstudio { get; set; }
        public string IdOrden { get; set; }
        public string IdNodo { get; set; }
        public string IdServicio { get; set; }
        public string Cubierto { get; set; }
        public int IsAvailable { get; set; }
        public int RestanteDays { get; set; }
        public int Vigencia { get; set; }
        public int IsCancel { get; set; }
    }
}
