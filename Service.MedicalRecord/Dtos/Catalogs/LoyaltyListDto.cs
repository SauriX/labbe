using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Dtos.Catalogs
{
    public class LoyaltyListDto
    {
        public Guid Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public decimal CantidadDescuento { get; set; }
        public string TipoDescuento { get; set; }
        public string Fecha { get; set; }
        public List<Guid> PrecioListaId { get; set; }
        public List<string> PrecioLista { get; set; }
        public string ListaPrecio => string.Join(", ", PrecioLista);
        public bool Activo { get; set; }
    }

    public class LoyaltyDto
    {
        public DateTime Fecha { get; set; }
        public Guid ListaPrecioId { get; set; }
    }
}
