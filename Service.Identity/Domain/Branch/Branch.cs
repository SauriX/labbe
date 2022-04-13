using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
namespace Service.Identity.Domain.Branch
{
    public class Branch
    {
        [Key]
        public Guid IdSucursal { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public int CodigoPostal { get; set; }
        public long? EstadoId { get; set; }
        public long? CiudadId { get; set; }
        public int NumeroExterior { get; set; }
        public int? NumeroInterior { get; set; }
        public string Calle { get; set; }
        public long ColoniaId { get; set; }
        public string Correo { get; set; }
        public long? Telefono { get; set; }
        public Guid PresupuestosId { get; set; }
        public Guid FacturaciónId { get; set; }
        public Guid ClinicosId { get; set; }
        public Guid ServicioId { get; set; }
        public bool Activo { get; set; }
        public Guid UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public Guid UsuarioModId { get; set; }
        public DateTime FechaMod { get; set; }
    }
}
