using Service.Report.Domain.Catalogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Domain.Request
{
    public class RequestPayment
    {
        public Guid Id { get; set; }
        public Guid SolicitudId { get; set; }
        public virtual Request Solicitud { get; set; }
        public string Factura { get; set; }
        public decimal ACuenta { get; set; }
        public decimal Efectivo { get; set; }
        public decimal TDC { get; set; }
        public decimal Transferecia { get; set; }
        public decimal Cheque { get; set; }
        public decimal TDD { get; set; }
        public decimal PP { get; set; }
        public decimal Total { get; set; }
        public decimal Saldo { get; set; }
        public DateTime Fecha { get; set; }
        public string UsuarioModifico { get; set; }
        public Guid EmpresaId { get; set; }
        public virtual Company Empresa { get; set; }
        public byte Estatus { get; set; }
    }
}
