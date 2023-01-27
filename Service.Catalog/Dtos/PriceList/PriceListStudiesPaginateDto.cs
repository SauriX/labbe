using System;

namespace Service.Catalog.Dtos.PriceList
{
    public class PriceListStudiesPaginateDto
    {
        public Guid id { get; set; }
        public int skip { get; set; }
        public int take { get; set; }
    }
}
