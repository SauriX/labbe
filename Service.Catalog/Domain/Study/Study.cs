using Service.Catalog.Domain.Catalog;
using Service.Catalog.Domain.Indication;
using Service.Catalog.Domain.Parameter;
using System;
using System.Collections.Generic;

namespace Service.Catalog.Domain.Study
{
    public class Study
    {
        public Study    ()
        {
        }

        public Study(int id, string clave, string nombre, int orden, string titulo, string corto, bool visible, int dias, int? areaId, int? departamentoId, int? maquiladorId, int? metodoId)
        {
            Id = id;
            Clave = clave;
            Nombre = nombre;
            Orden = orden;
            Titulo = titulo;
            NombreCorto = corto;
            Visible = visible;
            DiasResultado = dias;
            Dias = dias;
            TiempoResultado = dias * 24;
            AreaId = areaId;
            DepartamentoId = departamentoId;
            MaquiladorId = maquiladorId;
            MetodoId = metodoId;
            Cantidad = 1;
            Activo = true;
            FechaCreo = DateTime.Now;
        }

        public int Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public int Orden { get; set; }
        public string Titulo { get; set; }
        public string NombreCorto { get; set; }
        public string WorkList { get; set; }
        public bool Visible { get; set; }
        public decimal DiasResultado { get; set; }
        public int Dias { get; set; }
        public int TiempoResultado { get; set; }
        public int? AreaId { get; set; }
        public virtual Area Area { get; set; }
        public int? DepartamentoId { get; set; }
        public int? MaquiladorId { get; set; }
        public virtual Domain.Maquila.Maquila Maquilador { get; set; }
        public int? MetodoId { get; set; }
        public virtual Method Metodo { get; set; }
        public int? SampleTypeId { get; set; }
        public virtual SampleType SampleType { get; set; }
        public int? TaponId { get; set; }
        public virtual Domain.Tapon.Tag Tapon { get; set; }
        public int Cantidad { get; set; }
        public bool Prioridad { get; set; }
        public bool Urgencia { get; set; }
        public bool Activo { get; set; }
        public Guid? UsuarioCreoId { get; set; }
        public DateTime? FechaCreo { get; set; }
        public Guid? UsuarioModificoId { get; set; }
        public DateTime? FechaModifico { get; set; }
        public string Instrucciones { get; set; }

        public int DiasEstabilidad { get; set; }
        public int DiasRefrigeracion { get; set; }
        public virtual ICollection<ParameterStudy> Parameters { get; set; }
        public virtual ICollection<Domain.Study.WorkListStudy> WorkLists { get; set; }
        public virtual ICollection<IndicationStudy> Indications { get; set; }
        public virtual ICollection<Domain.Study.ReagentStudy> Reagents { get; set; }
        public virtual ICollection<PacketStudy> Packets { get; set; }
        public virtual ICollection<StudyTag> Etiquetas { get; set; }
    }
}
