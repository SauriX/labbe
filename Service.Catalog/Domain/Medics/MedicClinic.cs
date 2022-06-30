using Service.Catalog.Domain.Catalog;
using System;

namespace Service.Catalog.Domain.Medics
{
    public class MedicClinic
    {
        public Guid MedicoId { get; set; }
        public virtual Medics Medico { get; set; }
        public int ClinicaId { get; set; }
        public virtual Clinic Clinica { get; set; }
        public bool Activo { get; set; }
        public Guid? UsuarioCreoId { get; set; }
        public DateTime? FechaCreo { get; set; }
        public Guid? UsuarioModId { get; set; }
        public DateTime? FechaMod { get; set; }
    }
}
