﻿using Service.Catalog.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Domain.Branch
{
    public class BranchDepartment
    {
        public Guid SucursalId { get; set; }
        public virtual Branch Sucursal { get; set; }
        public int DepartamentoId { get; set; }
        public virtual Department Departamento { get; set; }
        public Guid? UsuarioCreoId { get; set; }
        public DateTime? FechaCreo { get; set; }
    }
}
