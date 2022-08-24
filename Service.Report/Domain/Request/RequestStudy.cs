using Service.Report.Domain.Catalogs;
using System;

namespace Service.Report.Domain.Request
{
    public class RequestStudy
    {
        public Guid Id { get; set; }
        public Guid SolicitudId { get; set; }
        public virtual Request Solicitud { get; set; }
        public string Estudio { get; set; }
        public string Clave { get; set; }
        public int? PaqueteId { get; set; }
        public virtual RequestPack Paquete { get; set; }
        public byte EstatusId { get; set; }
        public virtual RequestStatus Estatus { get; set; }
        public int Duracion { get; set; }
        public decimal Descuento { get; set; }
        public decimal Precio { get; set; }
        public decimal PrecioFinal { get; set; }
        public Guid? SucursalId { get; set; }
        public virtual Branch Sucursal { get; set; }
        public int? MaquilaId { get; set; }
        public virtual Maquila Maquila { get; set; }
    }
}
