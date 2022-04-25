using System;

namespace Service.Catalog.Domain.Company
{
    public class Contact
    {
        public int Id { get; set; }
        public int CompañiaId { get; set; }
        public virtual Company Compañia { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public long? Telefono { get; set; }
        public string Correo { get; set; }
        public bool Activo { get; set; }
        public int UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public int? UsuarioModId { get; set; }
        public DateTime? FechaMod { get; set; }
    }
}