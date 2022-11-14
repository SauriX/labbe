using Service.MedicalRecord.Domain.Catalogs;
using Service.MedicalRecord.Domain.Status;
using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Domain.PriceQuote
{
    public class PriceQuote : BaseModel
    {
        public Guid Id { get; set; }
        public Guid? ExpedienteId { get; set; }
        public virtual MedicalRecord.MedicalRecord Expediente { get; set; }
        public Guid SucursalId { get; set; }
        public virtual Branch Sucursal { get; set; }
        public string Clave { get; set; }
        public byte EstatusId { get; set; }
        public virtual StatusPriceQuote Estatus { get; set; }
        public byte Procedencia { get; set; }
        public Guid? CompañiaId { get; set; }
        public virtual Company Compañia { get; set; }
        public Guid? MedicoId { get; set; }
        public virtual Medic Medico { get; set; }
        public string EnvioCorreo { get; set; }
        public string EnvioWhatsapp { get; set; }
        public string Observaciones { get; set; }
        public bool Activo { get; set; }
        public decimal TotalEstudios { get; set; }
        public decimal Cargo { get; set; }
        public byte CargoTipo { get; set; }
        public decimal Total { get; set; }

        public virtual ICollection<PriceQuoteStudy> Estudios { get; set; }
        public virtual ICollection<PriceQuotePack> Paquetes { get; set; }
    }
}