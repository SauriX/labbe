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
        public long? Telefono { set; get; }
        public string PaginaWeb { set; get; }
        public int CodigoPostal { get; set; }
        public int NumeroExterior { get; set; }
        public int? NumeroInterior { get; set; }
        public string Calle { get; set; }
        public int ColoniaId { get; set; }
        public virtual Colony Colonia { get; set; }
        public bool Activo { get; set; }
        public string UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public string UsuarioModId { get; set; }
        public DateTime? FechaMod { get; set; }
    }
}
