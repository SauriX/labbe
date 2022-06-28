using Service.Catalog.Domain.Constant;
using Service.Catalog.Dtos.Catalog;
using Service.Catalog.Dtos.Study;
using System;
using System.Collections.Generic;

namespace Service.Catalog.Dtos.Branch
{
    public class BranchFormDto
    {
         public string idSucursal {get; set;}
         public string clave {get; set;}
         public string nombre {get; set;} 
         public string calle {get; set;}
         public string correo {get; set;}
         public string telefono {get; set;}
         public string numeroExt {get; set;}
         public string numeroInt {get; set;}
         public string presupuestosId {get; set;}
         public string facturaciónId {get; set;}
         public string clinicosId {get; set;}
         public bool activo {get; set;}
        public string estado { get; set; }
        public string ciudad { get; set; }
        public int coloniaId { get; set; }
        public string codigoPostal { get; set; }
        public Guid UsuarioId { get; set; }
        public bool Matriz { get; set; }
        public IEnumerable<BranchDepartmentDto> departamentos { get; set; }
    }
}
