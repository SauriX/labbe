using System;

namespace Service.MedicalRecord.Domain.Request
{
    public class Request : BaseModel
    {
        public Guid Id { get; set; }
        public Guid ExpedienteId { get; set; }
        public Guid SucursalId { get; set; }
        public string Clave { get; set; }
        public string ClavePatologica { get; set; }
        public byte Procedencia { get; set; }
        public string Afiliacion { get; set; }
        public Guid CompañiaId { get; set; }
        public Guid MedicoId { get; set; }
        public byte Urgencia { get; set; }
        public string Observaciones { get; set; }
        public string EnvioCorreo { get; set; }
        public string EnvioWhatsApp { get; set; }
        public bool Activo { get; set; }
    }
}
