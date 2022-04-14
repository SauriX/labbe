﻿using Service.Catalog.Dtos.Catalog;
using Service.Catalog.Dtos.Study;
using System.Collections.Generic;

namespace Service.Catalog.Dtos.Indication
{
    public class IndicationFormDto
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
        public string UsuarioId { get; set; }
        public IEnumerable<StudyListDto> Estudios { get; set; }
    }
}