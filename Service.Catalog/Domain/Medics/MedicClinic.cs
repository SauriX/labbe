using Identidad.Api.ViewModels.Menu;
using System;
using System.ComponentModel.DataAnnotations;

namespace Identidad.Api.Model.Medicos
{
    public class MedicClinic
    {
        public int IdMedico_Clinica { get; set; }
        public long MedicoId { get; set; }
        public virtual Medics Medico { get; set; }
        public long ClinicaId { get; set; }
        public bool Activo { get; set; }
        public long UsuarioCreoId { get; set; }
        public string UsuarioCreo  { get; set; }
        public DateTime FechaCreo { get; set; }
        public string UsuarioModId { get; set; }
        public DateTime FechaMod { get; set; }
        
    }
}
