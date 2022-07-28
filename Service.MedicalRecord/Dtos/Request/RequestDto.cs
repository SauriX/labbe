using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.Request
{
    public class RequestDto
    {
        public Guid? Id { get; set; }
        public Guid ExpedienteId { get; set; }
        public Guid SucursalId { get; set; }
        public string Clave { get; set; }
        public string ClavePatologica { get; set; }
        public string Registro { get; set; }
        public bool EsNuevo { get; set; }
        public Guid UsuarioId { get; set; }
    }
}
