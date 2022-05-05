using Service.Catalog.Domain.Catalog;
using System;
using System.Collections.Generic;

namespace Service.Catalog.Domain.Parameter
{
    public class Parameter
    {
        public Guid Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public decimal ValorInicial { get; set; }
        public byte TipoValorId { get; set; }
        public string NombreCorto { get; set; }
        public decimal Unidades { get; set; }
        public string Formula { get; set; }
        public string Formato { get; set; }
        public int DepartamentoId { get; set; }
        public virtual Department Departmento { get; set; }
        public int AreaId { get; set; }
        public virtual Area Area { get; set; }
        public int FormatoImpresionId { get; set; }
        public virtual Format FormatoImpresion { get; set; }
        public Guid ReactivoId { get; set; }
        public virtual Reagent.Reagent Reactivo { get; set; }
        public string UnidadSi { get; set; } 
        public string FCSI { get; set; }
        public bool Activo { get; set; }
        public Guid UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public Guid UsuarioModificoId { get; set; }
        public DateTime FechaModifico { get; set; }
        public virtual ICollection<ParameterStudy> Estudios { get; set; }
    }
}
