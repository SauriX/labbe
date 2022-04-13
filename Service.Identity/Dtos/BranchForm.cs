using System;

namespace Service.Identity.Dtos
{
    public class BranchForm
    {
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
        public string PresupuestosId { get; set; }
        public string FacturaciónId { get; set; }
        public string  ClinicosId { get; set; }
        public string ServicioId { get; set; }
        public bool Activo { get; set; }
    }
}
