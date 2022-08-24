using System;

namespace Service.Catalog.Domain.EquipmentMantain
{
    public class MantainImages
    {
        public Guid Id { get; set; }
        public string UrlImg { get; set; }

        public Guid MantainId { get; set; }
        public virtual Mantain Mantain { get; set; }
    }
}
