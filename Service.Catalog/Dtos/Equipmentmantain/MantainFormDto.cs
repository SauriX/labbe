using System;

namespace Service.Catalog.Dtos.Equipmentmantain
{
    public class MantainFormDto
    {
        public string Id { get; set; }
        public Guid IdEquipo { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public Guid? IdUser { get; set; }
        public string Clave { get; set; }
        public string No_serie { get; set; }
        public bool Ativo { get; set; }
        public string imagenUrl { get; set; }
        public int ide { get; set; }
    }
}
