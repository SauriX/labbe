﻿using System;
using System.Collections.Generic;

namespace Service.Catalog.Dtos.Equipment
{
    public class EquipmentFormDto
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Categoria { get; set; }
        public bool Activo { get; set; }
        public string CategoriaText { get; set; }
        public Guid UsuarioId { get; set; }
        public IEnumerable<EquipmentBrancheDto> valores { get; set; }
    }
}
