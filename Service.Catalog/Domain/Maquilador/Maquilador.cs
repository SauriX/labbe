using Service.Catalog.Domain.Constant;
using System;

namespace Service.Catalog.Domain.Maquilador
{
    public class Maquilador
    {
        public int Id { set; get; }
        public string Clave { set; get; }
        public string Nombre { set; get; }
        public string Correo { set; get; }
        public string Telefono { set; get; }
        public string PaginaWeb { set; get; }
        public int CodigoPostal { get; set; }
        public string NumeroExterior { get; set; }
        public string NumeroInterior { get; set; }
        public string Calle { get; set; }
        public int ColoniaId { get; set; }
        public virtual Colony Colonia { get; set; }
        public string Estado { get; set; }
        public string Ciudad { get; set; }
        public bool Activo { get; set; }
        public string UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public string UsuarioModId { get; set; }
        public DateTime? FechaMod { get; set; }
    }
}
