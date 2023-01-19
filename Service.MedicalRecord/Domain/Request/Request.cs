using Service.MedicalRecord.Domain.Catalogs;
using Service.MedicalRecord.Domain.Invoice;
using Service.MedicalRecord.Domain.Status;
using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Domain.Request
{
    public class Request : BaseModel
    {
        public Guid Id { get; set; }
        public Guid ExpedienteId { get; set; }
        public virtual MedicalRecord.MedicalRecord Expediente { get; set; }
        public Guid SucursalId { get; set; }
        public virtual Branch Sucursal { get; set; }
        public string Clave { get; set; }
        public string ClavePatologica { get; set; }
        public string Serie { get; set; }
        public string SerieNumero { get; set; }
        public byte EstatusId { get; set; }
        public virtual StatusRequest Estatus { get; set; }
        public byte Procedencia { get; set; }
        public string Afiliacion { get; set; }
        public Guid? CompañiaId { get; set; }
        public virtual Company Compañia { get; set; }
        public Guid? MedicoId { get; set; }
        public virtual Medic Medico { get; set; }
        public byte Urgencia { get; set; }
        public string Observaciones { get; set; }
        public string EnvioCorreo { get; set; }
        public string EnvioWhatsApp { get; set; }
        public string RutaOrden { get; set; }
        public string RutaINE { get; set; }
        public string RutaINEReverso { get; set; }
        public bool Parcialidad { get; set; }
        public bool Activo { get; set; }
        public bool EsNuevo { get; set; }
        public decimal TotalEstudios { get; set; }
        public decimal Descuento { get; set; }
        public decimal Cargo { get; set; }
        public decimal Copago { get; set; }
        public decimal Total { get; set; }
        public decimal Saldo { get; set; }
        public string UsuarioCreo { get; set; }

        public string FolioWeeClinic { get; set; }
        public bool TokenValidado { get; set; }
        public string IdOrden { get; set; }
        public string IdPersona { get; set; }

        public bool EsWeeClinic => !string.IsNullOrWhiteSpace(FolioWeeClinic);
        public virtual ICollection<InvoiceCompany> FacturasCompañia { get; set; }
        public virtual ICollection<RequestPayment> Pagos { get; set; }
        public virtual ICollection<RequestStudy> Estudios { get; set; }
        public virtual ICollection<RequestPack> Paquetes { get; set; }
        public virtual ICollection<RequestImage> Imagenes { get; set; }
    }
}