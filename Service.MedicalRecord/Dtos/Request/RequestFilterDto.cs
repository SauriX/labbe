using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.Request
{
    public class RequestFilterDto
    {
        public byte TipoFecha { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public string Clave { get; set; }
        public string ClavePatologica { get; set; }
        public List<byte> Procedencias { get; set; }
        public List<byte> Estatus { get; set; }
        public List<int> Departamentos { get; set; }
        public List<int> Ciudades { get; set; }
        public List<Guid> Sucursales { get; set; }
        public List<Guid> Compañias { get; set; }
        public List<Guid> Medicos { get; set; }
    }
}
