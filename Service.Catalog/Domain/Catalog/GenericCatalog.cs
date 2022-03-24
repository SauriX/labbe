﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Domain.Catalog
{
    public class GenericCatalog
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }
        public string UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public string UsuarioModificoId { get; set; }
        public DateTime FechaModifico { get; set; }
    }

    public class GenericCatalogDescription : GenericCatalog
    {
        public string Descripcion { get; set; }
    }
}
