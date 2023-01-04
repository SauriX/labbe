using System;

namespace Service.MedicalRecord.Domain.Invoice
{
    public class Series : BaseModel
    {
        public Guid Id { get; set; }
        public string Clave { get; set; }
        public string Descripcion { get; set; }
        public bool EsFacturaORecibo { get; set; }
        public Guid SucursalId { get; set; }
    }
}
