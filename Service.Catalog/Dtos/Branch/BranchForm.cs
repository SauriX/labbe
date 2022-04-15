using Service.Catalog.Domain.Constant;
using Service.Catalog.Dtos.Study;
using System.Collections.Generic;

namespace Service.Catalog.Dtos.Branch
{
    public class BranchForm
    {
         public string idSucursal {get; set;}
         public string clave {get; set;}
         public string nombre {get; set;} 
         public string calle {get; set;}
         public string correo {get; set;}
         public long telefono {get; set;}
         public int numeroExt {get; set;}
         public int numeroInt {get; set;}
         public string presupuestosId {get; set;}
         public string facturaciónId {get; set;}
         public string clinicosId {get; set;}
         public bool activo {get; set;}
        public string estado { get; set; }
        public string ciudad { get; set; }
        public int coloniaId { get; set; }
        public string codigoPostal { get; set; }
        public IEnumerable<StudyListDto> estudios { get; set; }
    }
}
