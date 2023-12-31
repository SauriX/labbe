﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Dtos.Parameter
{
    public class ParameterValueStudyDto
    {
        public string Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string NombreCorto { get; set; }
        public string Area { get; set; }
        public string Departamento { get; set; }
        public bool Activo { get; set; }
        public bool Requerido { get; set; }
        public bool DeltaCheck { get; set; }
        public bool ValoresCriticos { get; set; }
        public bool MostrarFormato { get; set; }
        public int? Unidades { get; set; }
        public string UnidadNombre { get; set; }
        public string TipoValor { get; set; }
        public string ValorInicial { get; set; }
        public string ValorFinal { get; set; }
        public decimal CriticoMinimo { get; set; }
        public decimal CriticoMaximo { get; set; }
        public int EstudioId { get; set; }
        public int SolicitudEstudioId { get; set; }
        public string Formula { get; set; }
        public string FCSI { get; set; }
        public List<ParameterValueDto> TipoValores { get; set; }
    }
}
