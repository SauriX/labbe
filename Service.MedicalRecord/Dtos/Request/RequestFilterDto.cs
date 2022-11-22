using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.Request
{
    public class RequestFilterDto
    {
        private string clave;

        public byte? TipoFecha { get; set; }
        public DateTime? FechaInicial { get; set; }
        public DateTime? FechaFinal { get; set; }
        public string Clave { get => clave; set => clave = value?.ToLower(); }
        public List<byte> Procedencias { get; set; } = new List<byte>();
        public List<byte> Estatus { get; set; } = new List<byte>();
        public List<byte> Urgencias { get; set; } = new List<byte>();
        public List<int?> Departamentos { get; set; } = new List<int?>();
        public List<Guid> Sucursales { get; set; } = new List<Guid>();
        public List<Guid> Compañias { get; set; } = new List<Guid>();
        public List<Guid> Medicos { get; set; } = new List<Guid>();
        public string? Expediente { get; set; }
    }
}
