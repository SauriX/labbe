using Identidad.Api.Model.Medicos;
using Service.Catalog.Domain.Catalog;
using Service.Catalog.Domain.Constant;
using System;
using System.Collections.Generic;

namespace Service.Catalog.Domain.Medics

{
    public class Medics
    {
        public Guid IdMedico { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public int EspecialidadId { get; set; }
        public virtual Field Especialidad { get; set; }
        public string Observaciones { get; set; }
        public int CodigoPostal { get; set; }
        public long? EstadoId { get; set; }
        public long? CiudadId { get; set; }
        public string NumeroExterior { get; set; }
        public string NumeroInterior { get; set; }
        public string Calle { get; set; }
        public int ColoniaId { get; set; }
        public virtual Colony Colonia { get; set; }
        public string Correo { get; set; }
        public string Celular { get; set; }
        public string Telefono { get; set; }
        public bool Activo { get; set; }
        public Guid? UsuarioCreoId { get; set; }
        public DateTime? FechaCreo { get; set; }
        public Guid? UsuarioModId { get; set; }
        public DateTime? FechaMod { get; set; }

        public virtual ICollection<MedicClinic> Clinicas { get; set; }
    }
}