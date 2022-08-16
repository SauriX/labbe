using System;

namespace Service.MedicalRecord.Dtos
{
    public class TaxDataDto
    {
        public Guid Id { get; set; }
        public Guid? ExpedienteId { get; set; }
        public string Rfc { get; set; }
        public string RazonSocial { get; set; }
        public string Cp { get; set; }
        public string Estado { get; set; }
        public string Municipio { get; set; }
        public string Correo { get; set; }
        public string Calle { get; set; }
        public int Colonia { get; set; }
        public Guid UsuarioId { get; set; }
    }
}
