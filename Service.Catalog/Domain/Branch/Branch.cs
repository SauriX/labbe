using Service.Catalog.Domain.Constant;
using Service.Catalog.Domain.Series;
using System;
using System.Collections.Generic;

namespace Service.Catalog.Domain.Branch
{
    public class Branch
    {
        public Branch()
        {
        }

        public Branch(Guid id, string codigo, string clave, string nombre, int matriz, string clinicos, string telefono, string correo, string calle, string numExterior, string numInterior, string cp, int coloniaId, string municipio, string estado)
        {
            Id = id;
            Codigo = codigo;
            Clave = clave;
            Nombre = nombre;
            Clinicos = clinicos;
            Telefono = telefono;
            Correo = correo;
            Calle = calle;
            NumeroExterior = numExterior;
            NumeroInterior = numInterior;
            Codigopostal = cp;
            ColoniaId = coloniaId;
            Ciudad = municipio;
            Estado = estado;
            Matriz = matriz == 1;
            Activo = true;
            FechaCreo = DateTime.Now;
        }

        public Guid Id { get; set; }
        public string Codigo { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
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
        public Guid? PresupuestosId { get; set; }
        public Guid? FacturaciónId { get; set; }
        public string Clinicos { get; set; }
        public Guid? ServicioId { get; set; }
        public bool Activo { get; set; }
        public Guid? UsuarioCreoId { get; set; }
        public DateTime? FechaCreo { get; set; }
        public Guid? UsuarioModificoId { get; set; }
        public DateTime? FechaModifico { get; set; }
        public bool Matriz { get; set; }
        public virtual IEnumerable<BranchDepartment> Departamentos { get; set; }
        public virtual ICollection<Serie> Series { get; set; }
    }
}
