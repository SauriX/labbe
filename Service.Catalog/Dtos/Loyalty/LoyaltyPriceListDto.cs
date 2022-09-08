using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Dtos.Loyalty
{
    public class LoyaltyPriceListDto
    {
        public Guid LealtadId { get; set; }
        public Guid PrecioListaId { get; set; }
        public string Nombre { get; set; }
    }
}
