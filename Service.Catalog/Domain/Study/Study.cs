﻿using Service.Catalog.Domain.Catalog;
using Service.Catalog.Domain.Indication;
using Service.Catalog.Domain.Parameter;
using System;
using System.Collections.Generic;

namespace Service.Catalog.Domain.Study
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
        public int Dias { get; set; }
        public int TiempoResultado { get; set; }
        public int AreaId { get; set; }
        public virtual Area Area { get; set; }
        public int DepartamentoId { get; set; }
        public int FormatoId { get; set; }
        public virtual Format Formato { get; set; }
        public int MaquiladorId { get; set; }
        public virtual Domain.Maquila.Maquila Maquilador { get; set; }
        public int MetodoId { get; set; }
        public virtual Method Metodo { get; set; }
        public int SampleTypeId { get; set; }
        public virtual SampleType SampleType { get; set; }
        public int TaponId { get; set; }
        public virtual Domain.Tapon.Tapon Tapon { get; set; }
        public int Cantidad { get; set; }
        public bool Prioridad { get; set; }
        public bool Urgencia { get; set; }
        public bool Activo { get; set; }
        public Guid UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public Guid UsuarioModId { get; set; }
        public DateTime FechaMod { get; set; }
        public virtual ICollection<ParameterStudy> Parameters { get; set; }
        public virtual ICollection<Domain.Study.WorkListStudy> WorkLists { get; set; }
        public virtual ICollection<IndicationStudy> Indications { get; set; }
        public virtual ICollection<Domain.Study.ReagentStudy> Reagents { get; set; }
        public virtual ICollection<PacketStudy> Packets { get; set; }
    }
}
