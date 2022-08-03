using System;

namespace Service.Catalog.Dtos.PriceList
{
    public class PriceListInfoFilterDto
    {
        public int? EstudioId { get; set; }
        public int? PaqueteId { get; set; }
        public Guid SucursalId { get; set; }
        public Guid? MedicoId { get; set; }
        public Guid? CompañiaId { get; set; }
    }
}
