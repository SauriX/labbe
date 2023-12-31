﻿using System;

namespace Service.Catalog.Dtos.Catalog
{
    public class CatalogFormDto
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }
        public Guid UsuarioId { get; set; }
    }
}
