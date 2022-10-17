using System;

namespace Service.MedicalRecord.Domain
{
    public class ClinicResults
    {
        public Guid Id { get; set; }
        public Guid SolicitudId { get; set; }
        public virtual Request.Request Solicitud{ get; set; }
        public int SolicitudEstudioId { get; set; }
        public int EstudioId { get; set; }
        public virtual Request.RequestStudy SolicitudEstudio { get; set; }
        public Guid ParametroId { get; set; }
        public int TipoValorId { get; set; }
        public string Nombre { get; set; }
        public decimal? ValorInicial { get; set; }
        public decimal? ValorFinal { get; set; }
        public string Resultado { get; set; }
    }
}
