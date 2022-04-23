using System;

namespace Service.Catalog.Domain.Parameter
{
    public class Format
    {
        public string NombreFormato { get; set; }
        public bool Activo { get; set; }
        public Guid UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public Guid UsuarioModId { get; set; }
        public DateTime FechaMod { get; set; }
    }
}
