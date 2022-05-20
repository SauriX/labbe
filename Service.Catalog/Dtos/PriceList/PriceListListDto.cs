using Service.Catalog.Domain.Company;
using Service.Catalog.Domain.Price;
using System;
using System.Collections.Generic;

namespace Service.Catalog.Dtos.PriceList
{
    public class PriceListListDto
    {
        public Guid Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public bool? Visibilidad { get; set; }
        public bool Activo { get; set; }
        public virtual ICollection<Price_Promotion> Promocion { get; set; }
        public virtual ICollection<PriceListCompanyDto> Compañia { get; set; }
        public virtual ICollection<PriceListBranchDto> Sucursales { get; set; }
        public virtual ICollection<PriceListMedicDto> Medicos { get; set; }
        public virtual ICollection<PriceListStudyDto> Estudios { get; set; }
        public virtual ICollection<PriceList_Packet> Paquete { get; set; }
    }
}
