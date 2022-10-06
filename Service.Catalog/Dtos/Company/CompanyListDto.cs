using System;
using System.Collections.Generic;

namespace Service.Catalog.Dtos.Company
{
    public class CompanyListDto
    {
        public Guid Id { get; set; }
        public string Clave { get; set; }
        public string Contrasena { get; set; }
        public string NombreComercial { get; set; }
        public string Procedencia { get; set; }
        public int ProcedenciaId { get; set; }
        public Guid? PrecioListaId { get; set; }
        public string PrecioLista { get; set; }
        public bool Activo { get; set; }
        public IEnumerable<ContactListDto> Contacts { get; set; }

    }
}