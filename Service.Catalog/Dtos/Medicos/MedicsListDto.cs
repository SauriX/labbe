using Service.Catalog.Dtos.Catalog;
using System;
using System.Collections.Generic;

namespace Service.Catalog.Dtos.Medicos
{
    public class MedicsListDto
    {
        public int IdMedico { get; set; }
        public string Clave { get; set; }
        public string NombreCompleto { get; set; }
        public long EspecialidadId { get; set; }
        public string Observaciones { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }
        public long? Celular { get; set; }
        public long? Telefono { get; set; }
        public string ActivoDescripcion => Activo ? "Si" : "No";
        public bool Activo { get; set; }
        public IEnumerable<CatalogListDto> Clinicas { get; set; }
    }
}
