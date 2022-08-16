using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Dtos
{
    public class CashRegisterDto
    {
        public Guid Id { get; set; }
        public string Solicitud { get; set; }
        public string Paciente { get; set; }
        public string Factura { get; set; }
        public decimal ACuenta { get; set; }
        public decimal Efectivo { get; set; }
        public decimal TDC { get; set; }
        public decimal Transferencia { get; set; }
        public decimal Cheque { get; set; }
        public decimal TDD { get; set; }
        public decimal PP { get; set; }
        public decimal Total { get; set; }
        public decimal Saldo { get; set; }
        public string Fecha { get; set; }
        public string UsuarioModifico { get; set; }
        public string Empresa { get; set; }
        public byte Estatus { get; set; }
    }
}
