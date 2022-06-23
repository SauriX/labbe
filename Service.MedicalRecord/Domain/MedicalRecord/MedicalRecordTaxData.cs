using System;

namespace Service.MedicalRecord.Domain.MedicalRecord
{
    public class MedicalRecordTaxData
    {
        public Guid ExpedienteID { get; set; }
        public virtual MedicalRecord Expediente { get; set; }
        public Guid FacturaID { get; set; }
        public virtual TaxData.TaxData Factura { get; set; }
        public bool Activo { get; set; }
        public Guid UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public Guid? UsuarioModId { get; set; }
        public DateTime? FechaMod { get; set; }
    }
}
