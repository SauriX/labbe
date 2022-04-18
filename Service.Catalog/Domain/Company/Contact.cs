using System;

namespace Service.Catalog.Domain.Company
{
    public class Contact
    {
        public int IdContacto { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public int Telefono { get; set; }
        public string Correo { get; set; }
        public bool Activo { get; set; }
        public int UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public int UsuarioModId { get; set; }
        public DateTime FechaMod { get; set; }
    }
}