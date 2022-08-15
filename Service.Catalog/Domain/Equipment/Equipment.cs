using System;

namespace Service.Catalog.Domain.Equipment
{
    public class Equipment
    {
        public Guid Id { get; set; }
        public string NombreCorto { get; set; }
        public string NombreLargo { get; set; }
        public string Categoria { get; set; }
        public string Clave { get; set; }
        public bool Activo { get; set; }
        public Guid? UsuarioCreoId { get; set; }
        public DateTime? FechaCreo { get; set; }
        public Guid? UsuarioModId { get; set; }
        public DateTime? FechaMod { get; set; }
    }
}
