using System;
using System.Collections.Generic;

namespace Service.Catalog.Domain.Tags
{
    public class Tag
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string NombreCorto { get; set; }
        public int DimensionesId { get; set; }
        public int Cantidad { get; set; }
        public bool Activo { get; set; }
        public string UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public string UsuarioModId { get; set; }
        public DateTime? FechaMod { get; set; }
        public virtual ICollection<TagStudy> Estudios { get; set; }
        public virtual ICollection<TagParameter> Parametros { get; set; }
    }
}
