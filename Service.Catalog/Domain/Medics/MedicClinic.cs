using Service.Catalog.Domain.Catalog;
using Service.Catalog.Domain.Medics;
using System;

namespace Identidad.Api.Model.Medicos
{
    public class MedicClinic
    {
        public int MedicoId { get; set; }
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
