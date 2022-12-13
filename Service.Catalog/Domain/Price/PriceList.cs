using System;
using System.Collections.Generic;

namespace Service.Catalog.Domain.Price
{
    public class PriceList
    {
        public PriceList()
        {
        }

        public PriceList(Guid id, string clave, string nombre, bool? visibilidad, bool activo)
        {
            Id = id;
            Clave = clave;
            Nombre = nombre;
            Visibilidad = visibilidad;
            Activo = activo;
        }

        public Guid Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public bool? Visibilidad { get; set; }
        public bool Activo { get; set; }
        public Guid UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public Guid UsuarioModificoId { get; set; }
        public DateTime? FechaModifico { get; set; }
        public virtual ICollection<Price_Promotion> Promocion { get; set; }
        public virtual ICollection<Price_Company> Compañia { get; set; }
        public virtual ICollection<Price_Branch> Sucursales { get; set; }
        public virtual ICollection<Price_Medics> Medicos { get; set; }
        public virtual ICollection<PriceList_Study> Estudios { get; set; }
        public virtual ICollection<PriceList_Packet> Paquete { get; set; }
    }
}
