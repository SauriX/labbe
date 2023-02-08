using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Domain.Request
{
    public class RequestTag
    {
        public int Id { get; set; }
        public Guid SolicitudId { get; set; }
        public virtual Request Solicitud { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public int Suero { get; set; }
        public string Tapon { get; set; }
        public decimal Cantidad { get; set; }
        public DateTime Fecha { get; set; }
        public int Orden { get; set; }
    }
}
