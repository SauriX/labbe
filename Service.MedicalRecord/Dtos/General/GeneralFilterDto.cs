using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Dtos.General
{
    public class GeneralFilterDto
    {
        private string buscar;

        public List<Guid> SucursalId { get; set; }
        public List<Guid> SucursalesId { get; set; }
        public List<Guid?> MedicoId { get; set; }
        public List<Guid?> CompañiaId { get; set; }
        public List<DateTime> Fecha { get; set; }
        public string Buscar { get => buscar; set => buscar = value?.ToLower(); }
        public List<int?> Departamento { get; set; }
        public List<int?> Area { get; set; }
        public List<string> Ciudad { get; set; }
        public List<byte?> TipoSolicitud { get; set; }
        public List<int?> Procedencia { get; set; }

        public List<byte?> Estatus { get; set; }
        public List<int?> Estudio { get; set; }
        public List<Guid?> SolicitudId { get; set; }
        public string Expediente { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public List<DateTime> FechaAlta { get; set; }
        public DateTime? FechaAInicial { get; set; }
        public DateTime? FechaAFinal { get; set; }
        public DateTime? FechaNInicial { get; set; }
        public DateTime? FechaNFinal { get; set; }
        public byte? TipoFecha { get; set; }
        public DateTime? FechaInicial { get; set; }
        public DateTime? FechaFinal { get; set; }
        public string NombreArea { get; set; }
        public List<string> MediosEntrega { get; set; }
    }
}
