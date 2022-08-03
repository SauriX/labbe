using Service.Report.Domain.Catalogs;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Service.Report.Domain.Request
{
    public class Request
    {
        public Guid SolicitudId { get; set; }
        public string Clave { get; set; }
        public Guid SucursalId { get; set; }
        public virtual Branch Sucursal { get; set; }
        public Guid ExpedienteId { get; set; }
        public virtual MedicalRecord.MedicalRecord Expediente { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Precio { get; set; }
        public decimal Descuento { get; set; }
        public decimal Cargo { get; set; }
        public decimal PrecioFinal { get; set; }
        public Guid MedicoId { get; set; }
        public virtual Medic Medico { get; set; }
        public Guid EmpresaId { get; set; }
        public virtual Company.Company Empresa { get; set; }
        public byte EstatusId { get; set; }
        public virtual ICollection<RequestStudy> Estudios { get; set; }
        public bool Parcialidad { get; set; }
        public byte Urgencia { get; set; }
    }
}