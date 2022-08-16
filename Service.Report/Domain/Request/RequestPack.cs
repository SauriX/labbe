using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Domain.Request
{
    public class RequestPack
    {
        public Guid SolicitudId { get; set; }
        public virtual Request Solicitud { get; set; }
        public int PaqueteId { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public decimal Descuento { get; set; }
        public decimal DescuentoPorcentaje { get; set; }
        public decimal PrecioFinal { get; set; }
        public virtual ICollection<RequestStudy> Estudios { get; set; }
    }
}
