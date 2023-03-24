using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Domain.MedicalRecord
{
    public class RequestRegister
    {
        public Guid Id { get; set; }
        public Guid SolicitudId { get; set; }
        public string Solicitud { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string NombreCompleto { get; set; }
        public Guid CompañiaId { get; set; }
        public string Compañia { get; set; }
        public decimal Saldo { get; set; }
        public decimal Efectivo { get; set; }
        public decimal TDC { get; set; }
        public decimal Transferencia { get; set; }
        public decimal Cheque { get; set; }
        public decimal TDD { get; set; }
        public decimal PP { get; set; }
        public decimal OtroMetodo { get; set; }
        public decimal Total { get; set; }
        public int FormaPagoId { get; set; }
        public string FormaPago { get; set; }
        public string NumeroCuenta { get; set; }
        public Guid FacturaId { get; set; }
        public string Factura { get; set; }
        public decimal Cantidad { get; set; }
        public string Serie { get; set; }
        public string Numero { get; set; }
        public DateTime FechaPago { get; set; }
        public string UsuarioRegistra { get; set; }
        public byte EstatusId { get; set; }
        public string Estatus { get; set; }
    }
}
