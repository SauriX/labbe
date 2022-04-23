﻿using Service.Catalog.Domain.Catalog;
using System;

namespace Service.Catalog.Domain.Parameter
{
    public class Parameters
    {
        public Guid IdParametro { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string ValorInicial { get; set; }
        public string NombreCorto { get; set; }
        public double Unidades { get; set; }
        public string Formula { get; set; }
        public string Formato { get; set; }
        public int DepartamentId { get; set; }
        public virtual Department Department { get; set; }
        public int AreaId { get; set; }
        public virtual Area Area { get; set; }
        public string NombreFormato { get; set; }
        public int ReagentId { get; set; }
        public virtual Reagent.Reagent Reagent { get; set; }
        public string UnidadSi { get; set; } 
        public string FCSI { get; set; }
        public bool Activo { get; set; }
        public Guid UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public Guid UsuarioModId { get; set; }
        public DateTime FechaMod { get; set; }
    }
}
