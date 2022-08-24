using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Dtos.RequestedStudy
{
    public class RequestedStudySearchDto
    {
        public List<Guid> SucursalId { get; set; }
        public List<Guid> MedicoId { get; set; }
        public List<Guid> CompañiaId { get; set; }
        public List<DateTime> Fecha { get; set; }
        public string Buscar { get; set; }
        public List<int> Departamento { get; set; }
        public List<int> Area { get; set; }
        public List<byte> TipoSolicitud { get; set; }
        public List<int> Procedencia { get; set; }
        public List<int> Estatus { get; set; }
    }
}
