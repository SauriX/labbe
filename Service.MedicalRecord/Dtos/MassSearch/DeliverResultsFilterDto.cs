using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.MassSearch
{
    public class DeliverResultsFilterDto
    {
        private string clave { get; set; }
        public string Clave { get => clave; set => clave = value?.ToLower(); }
        public List<Guid> Companias { get; set; }
        public List<int?> Departamentos { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public List<Guid> Medicos { get; set; }
        public List<string> MediosEntrega { get; set; }
        public List<int> Procedencias { get; set; }
        public List<int> Estatus { get; set; }
        public List<Guid> Sucursales { get; set; }
        public List<int> Ciudades { get; set; }
        public List<int?> Area { get; set; }
        public int TipoFecha { get; set; }
        public List<int> TipoSolicitud { get; set; }
        public List<Guid> SucursalesId { get; set; }
    }
}
