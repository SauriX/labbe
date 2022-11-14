using Service.MedicalRecord.Domain.Catalogs;
using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Domain.PriceQuote
{
    public class PriceQuote
    {
        public Guid Id { get; set; }
        public Guid? ExpedienteId { get; set; }
        public virtual MedicalRecord.MedicalRecord Expediente { get; set; }
        public Guid? SucursalId { get; set; }
        public virtual Branch Sucursal { get; set; }
        public string Clave { get; set; }
        public string Procedencia { get; set; }
        public string NombrePaciente { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public Guid? CompañiaId { get; set; }
        public virtual Company Compañia { get; set; }
        public Guid? MedicoId { get; set; }
        public virtual Medic Medico { get; set; }
        public string EnvioCorreo { get; set; }
        public string EnvioWhatsapp { get; set; }
        public string Observaciones { get; set; }
        public DateTime FechaPropuesta { get; set; }
        public bool Activo { get; set; }
        public string Estatus { get; set; }
        public Guid UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public Guid? UsuarioModId { get; set; }
        public DateTime? FechaMod { get; set; }
        public string Genero { get; set; }
        public int Edad { get; set; }
        public int Cargo { get; set; }
        public int Tipo { get; set; }
        public virtual ICollection<PriceQuoteStudy> Estudios { get; set; }
    }
}
