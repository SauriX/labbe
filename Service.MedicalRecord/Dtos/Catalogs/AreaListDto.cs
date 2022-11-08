using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Dtos.Catalogs
{
    public class AreaListDto
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }
        public int DepartamentoId { get; set; }
        public string Departamento { get; set; }
        public Guid UsuarioId { get; set; }
    }
}
