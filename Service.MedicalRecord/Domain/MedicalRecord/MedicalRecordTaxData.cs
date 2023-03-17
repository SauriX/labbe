using System;

namespace Service.MedicalRecord.Domain.MedicalRecord
{
    public class MedicalRecordTaxData
    {
        public MedicalRecordTaxData() { }

        public MedicalRecordTaxData(Guid taxId, Guid recordId, Guid userId)
        {
            FacturaID = taxId;
            ExpedienteID = recordId;
            Activo = true;
            UsuarioCreoId = userId;
            FechaCreo = DateTime.Now;
        }
        public bool isDefaultTaxData { get; set; }
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
