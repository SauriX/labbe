using System;

namespace Service.MedicalRecord.Domain.Request
{
    public class RequestStudyWee
    {
        public RequestStudyWee()
        {
        }

        public RequestStudyWee(string idNodo, string idServicio, string cubierto, decimal totalPaciente, decimal totalAseguradora, int isAvailable, int restanteDays, int vigencia, int isCancel)
        {
            IdNodo = idNodo;
            IdServicio = idServicio;
            Cubierto = cubierto;
            TotalPaciente = totalPaciente;
            TotalAseguradora = totalAseguradora;
            IsAvailable = isAvailable;
            RestanteDays = restanteDays;
            Vigencia = vigencia;
            IsCancel = isCancel;
        }

        public int Id { get; set; }
        public int SolicitudEstudioId { get; set; }
        public virtual RequestStudy SolicitudEstudio { get; set; }
        public string IdNodo { get; set; }
        public string IdServicio { get; set; }
        public string Cubierto { get; set; }
        public decimal TotalPaciente { get; set; }
        public decimal TotalAseguradora { get; set; }
        public decimal Total => TotalPaciente + TotalAseguradora;
        public int IsAvailable { get; set; }
        public int RestanteDays { get; set; }
        public int Vigencia { get; set; }
        public int IsCancel { get; set; }
        public bool Asignado { get; set; }
    }
}
