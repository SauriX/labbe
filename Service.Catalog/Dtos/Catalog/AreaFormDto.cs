﻿using System;

namespace Service.Catalog.Dtos.Catalog
{
    public class AreaFormDto
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }
        public int DepartamentoId { get; set; }
        public string Departamento { get; set; }
        public Guid UsuarioId { get; set; }
    }
}
