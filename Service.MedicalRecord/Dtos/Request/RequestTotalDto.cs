using System;

namespace Service.MedicalRecord.Dtos.Request
{
    public class RequestTotalDto
    {
        public Guid Id { get; set; }
        public decimal TotalEstudios { get; set; }
        public decimal Descuento { get; set; }
        public byte DescuentoTipo { get; set; }
        public decimal Cargo { get; set; }
        public byte CargoTipo { get; set; }
        public decimal Copago { get; set; }
        public byte CopagoTipo { get; set; }
        public decimal Total { get; set; }
        public decimal Saldo { get; set; }
        public Guid UsuarioId { get; set; }
    }
}
