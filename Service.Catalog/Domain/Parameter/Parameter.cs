using Service.Catalog.Domain.Catalog;
using System;
using System.Collections.Generic;

namespace Service.Catalog.Domain.Parameter
{
    public class Parameter
    {
        public Parameter()
        {
        }

        public Parameter(Guid id, string clave, string nombre, string nombreCorto, string tipoValor, string formula, int? areaId, int? departamentoId, int? unidadId, int? unidadSiId, string fcsi)
        {
            Id = id;
            Clave = clave;
            Nombre = nombre;
            NombreCorto = nombreCorto;
            TipoValor = tipoValor;
            Formula = formula;
            AreaId = areaId;
            DepartamentoId = departamentoId;
            UnidadId = unidadId;
            UnidadSiId = unidadSiId;
            FCSI = fcsi;
            Activo = true;
            FechaCreo = DateTime.Now;
        }

        public Guid Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string ValorInicial { get; set; }
        public string ValorFinal { get; set; }
        public string TipoValor { get; set; }
        public string NombreCorto { get; set; }
        public int? UnidadId { get; set; }
        public virtual Units Unidad { get; set; }
        public int? UnidadSiId { get; set; }
        public virtual Units UnidadSi { get; set; }
        public string Formula { get; set; }
        public int? DepartamentoId { get; set; }
        public virtual Department Departmento { get; set; }
        public int? AreaId { get; set; }
        public virtual Area Area { get; set; }
        public string FCSI { get; set; }
        public bool Activo { get; set; }
        public bool Requerido { get; set; }
        public bool DeltaCheck { get; set; }
        public bool MostrarFormato { get; set; }
        public Guid? UsuarioCreoId { get; set; }
        public DateTime? FechaCreo { get; set; }
        public Guid? UsuarioModificoId { get; set; }
        public DateTime? FechaModifico { get; set; }
        public virtual ICollection<ParameterStudy> Estudios { get; set; }
        public virtual ICollection<ParameterReagent> Reactivos { get; set; }
        public virtual ICollection<ParameterValue> TipoValores { get; set; }
    }
}
