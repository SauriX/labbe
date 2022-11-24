using System;

namespace Service.MedicalRecord.Domain.TaxData
{
    public class TaxData
    {
        public Guid Id { get; set; }
        public string RFC { get; set; }
        public string RazonSocial { get; set; }
        public string RegimenFiscal { get; set; }
        public string CodigoPostal { get; set; }
        public string Estado { get; set; }
        public string Ciudad { get; set; }
        public string Calle { get; set; }
        public int ColoniaId { get; set; }
        public string Correo { get; set; }
        public bool Activo { get; set; }
        public Guid UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public Guid? UsuarioModId { get; set; }
        public DateTime? FechaMod { get; set; }
    }
}
