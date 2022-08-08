using Service.MedicalRecord.Domain.Catalogs;
using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Domain.Request
{
    public class Request : BaseModel
    {
        public Guid Id { get; set; }
        public Guid ExpedienteId { get; set; }
        public virtual MedicalRecord.MedicalRecord Expediente { get; set; }
        public Guid SucursalId { get; set; }
        public virtual Branch Sucursal { get; set; }
        public string Clave { get; set; }
        public string ClavePatologica { get; set; }
        public byte Procedencia { get; set; }
        public string Afiliacion { get; set; }
        public Guid? CompañiaId { get; set; }
        public virtual Company Compañia { get; set; }
        public Guid? MedicoId { get; set; }
        public virtual Medic Medico { get; set; }
        public byte Urgencia { get; set; }
        public string Observaciones { get; set; }
        public string EnvioCorreo { get; set; }
        public string EnvioWhatsApp { get; set; }
        public string RutaOrden { get; set; }
        public string RutaINE { get; set; }
        public string RutaFormato { get; set; }
        public bool Parcialidad { get; set; }
        public bool Activo { get; set; }
        public bool EsNuevo { get; set; }
        public string UsuarioCreo { get; set; }

        public virtual ICollection<RequestStudy> Estudios { get; set; }
        public virtual ICollection<RequestPack> Paquetes { get; set; }
    }
}
