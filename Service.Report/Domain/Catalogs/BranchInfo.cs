using System.Collections.Generic;
using System;

namespace Service.Report.Domain.Catalogs
{
    public class BranchInfo
    {
        public string IdSucursal { get; set; }
        public string Codigo { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Calle { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string NumeroExt { get; set; }
        public string NumeroInt { get; set; }
        public string PresupuestosId { get; set; }
        public string FacturaciónId { get; set; }
        public string ClinicosId { get; set; }
        public bool Activo { get; set; }
        public string Estado { get; set; }
        public string Ciudad { get; set; }
        public int ColoniaId { get; set; }
        public string CodigoPostal { get; set; }
        public string Colonia { get; set; }
        public Guid UsuarioId { get; set; }
        public bool Matriz { get; set; }
        public IEnumerable<BranchDepartmentDto> Departamentos { get; set; }
    }

    public class BranchDepartmentDto
    {
        public string SucursalId { get; set; }
        public int DepartamentoId { get; set; }
        public string Departamento { get; set; }
    }
}
