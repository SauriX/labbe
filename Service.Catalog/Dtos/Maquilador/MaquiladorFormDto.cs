using System;

namespace Service.Catalog.Dtos.Maquilador
{
    public class MaquiladorFormDto
    {
        public int Id { set; get; }
        public string Clave { set; get; }
        public string Nombre { set; get; }
        public string? Correo { set; get; }
        public long? Telefono { set; get; }
        public string? PaginaWeb { set; get; }
        public int CodigoPostal { get; set; }
        public long? EstadoId { get; set; }
        public long? CiudadId { get; set; }
        public int NumeroExterior { get; set; }
        public int? NumeroInterior { get; set; }
        public string Calle { get; set; }
        public long ColoniaId { get; set; }
        public bool Activo { get; set; }
        public int UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public int UsuarioModId { get; set; }
    }
}
