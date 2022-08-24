using System;
using System.Collections.Generic;

namespace Service.Catalog.Domain.EquipmentMantain
{
    public class Mantain
    {

        public Guid Id { get; set; }
        public string clave { get; set; }
        public DateTime Fecha_Prog { get; set; }
        public string Descrip {get;set;}    
        public string Num_Serie { get; set; }
        public Guid EquipoId { get; set; }
        public Equipment.EquipmentBranch Equipo { get; set; }
        public bool Activo { get; set; }
        public Guid? UsuarioCreoId { get; set; }
        public DateTime? FechaCreo { get; set; }
        public Guid? UsuarioModId { get; set; }
        public DateTime? FechaMod { get; set; }

        public virtual ICollection<MantainImages> images { get; set; }
       
    }
}
