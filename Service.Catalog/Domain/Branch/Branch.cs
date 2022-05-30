using Service.Catalog.Domain.Catalog;
using Service.Catalog.Domain.Constant;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Domain.Branch
{
    public class Branch
    {
        public Guid Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        //public string CodigoPostal { get; set; }
        public int ColoniaId { get; set; }
        public virtual Colony Colonia { get; set; }
        public string Ciudad { get; set; }
        public string Estado { get; set; }
        public string Codigopostal { get; set; }
        public string NumeroExterior { get; set; }
        public string NumeroInterior { get; set; }
        public string Calle { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public Guid PresupuestosId { get; set; }
        public Guid FacturaciónId { get; set; }
        public Guid ClinicosId { get; set; }
        public Guid ServicioId { get; set; }
        public bool Activo { get; set; }
        public Guid? UsuarioCreoId { get; set; }
        public DateTime? FechaCreo { get; set; }
        public Guid? UsuarioModificoId { get; set; }
        public DateTime? FechaModifico { get; set; }

        public virtual IEnumerable<BranchDepartment> Departamentos { get; set; }
    }
}
