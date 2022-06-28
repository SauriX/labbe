using System;

namespace Service.MedicalRecord.Domain.Request
{
    public class Request
    {
        public Guid Id { get; set; }
        public Guid SucursalId { get; set; }
        public string Clave { get; set; }
        public string ClavePatologica { get; set; }
        public byte Procedencia { get; set; }
        public Guid MedicoId { get; set; }
    }
}
