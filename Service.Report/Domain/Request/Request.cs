using System;
using System.Collections;
using System.Collections.Generic;

namespace Service.Report.Domain.Request
{
    public class Request : Base
    {
        public Guid SolicitudId { get; set; }
        public string Clave { get; set; }
        public Guid SucursalId { get; set; }
        public virtual Branch.Branch Sucursal { get; set; }
        public Guid ExpedienteId { get; set; }
        public virtual MedicalRecord.MedicalRecord Expediente { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Precio { get; set; }
        public decimal Descuento { get; set; }
        public decimal Cargo { get; set; }
        public decimal PrecioFinal { get; set; }
        public Guid MedicoId { get; set; }
        public virtual Medic.Medic Medico { get; set; }
        public Guid EmpresaId { get; set; }
        public virtual Company.Company Empresa { get; set; }
        public byte EstatusId { get; set; }
        public virtual ICollection<RequestStudy> Estudios { get; set; }
    }
}