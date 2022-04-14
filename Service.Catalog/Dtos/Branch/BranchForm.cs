using Service.Catalog.Dtos.Study;
using System.Collections.Generic;

namespace Service.Catalog.Dtos.Branch
{
    public class BranchForm
    {
         public string IdSucursal {get; set;}
         public string Clave {get; set;}
         public string Nombre {get; set;} 
         public string Calle {get; set;}
         public string Correo {get; set;}
         public int Telefono {get; set;}
         public int NumeroExt {get; set;}
         public int NumeroInt {get; set;}
         public string PresupuestosId {get; set;}
         public string FacturaciónId {get; set;}
         public string ClinicosId {get; set;}
         public bool Activo {get; set;}
         public string estado {get; set;}
         public string ciudad {get; set;}
         public int coloniaId {get; set;}
         public string codigoPostal {get; set;}
         public List<StudyListDto> estudios { get; set; }
    }
}
