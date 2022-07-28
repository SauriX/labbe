using System;

namespace Service.MedicalRecord.Dtos.Request
{
    public class RequestGeneralDto
    {
        public Guid Id { get; set; }
        public byte Procedencia { get; set; }
        public string Afiliacion { get; set; }
        public Guid CompañiaId { get; set; }
        public Guid MedicoId { get; set; }
        public byte Urgencia { get; set; }
        public string Observaciones { get; set; }
        public string EnvioCorreo { get; set; }
        public string EnvioWhatsApp { get; set; }
        public bool Activo { get; set; }
        public Guid UsuarioId { get; set; }
    }
}
