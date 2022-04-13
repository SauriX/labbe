using Service.Catalog.Domain.Catalog;
using System;
using System.Collections.Generic;

namespace Service.Catalog.Domain.Indication
{
    public class Indication : GenericCatalogDescription
    {
        public virtual ICollection<IndicationStudy> Estudios { get; set; }

    }
}
