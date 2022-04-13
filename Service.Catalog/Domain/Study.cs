using Service.Catalog.Domain.Catalog;
using System;

namespace Service.Catalog.Domain
{
    public class Study
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public int Orden { get; set; }
        public string Titulo { get; set; }
        public string NombreCorto { get; set; }
        public bool Visible { get; set; }
        public int DiasResultado { get; set; }
        public int AreaId { get; set; }
        public virtual Area Area { get; set; }
        public int DepartamentoId { get; set; }
        public int FormatoId { get; set; }
        public int MaquiladorId { get; set; }
        public int MetodoId { get; set; }
        public int TipoMuestraId { get; set; }
        public int TiempoRespuesta { get; set; }
        public string Prioridad { get; set; }
        public bool Activo { get; set; }
        public int UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public int UsuarioModId { get; set; }
        public DateTime FechaMod { get; set; }
       
    }
}
