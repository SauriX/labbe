using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Integration.WeeClinic.Dtos
{
    public class WeePatientInfoDto
    {
        public string IdOrden { get; set; }
        public string FolioOrden { get; set; }
        public string DateInsert { get; set; }
        public string FechaFolio { get; set; }
        public string IdProducto { get; set; }
        public string CorporativoNombre { get; set; }
        public string ProductoNombre { get; set; }
        public string NoPoliza { get; set; }
        public string IdPersona { get; set; }
        public string Nombre { get; set; }
        public string Paterno { get; set; }
        public string Materno { get; set; }
        public string NombreCompleto { get; set; }
        public string CURP { get; set; }
        public string CodGenero { get; set; }
        public string Genero { get; set; }
        public string RFC { get; set; }
        public int Edad { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Busqueda { get; set; }
        public string TPA { get; set; }
        public int IsEstatus { get; set; }
        public string Copagos { get; set; }
        public int IsTyC { get; set; }
        public string NombreCompleto_Medico { get; set; }
        public int EstatusVigencia { get; set; }
        public IEnumerable<WeePatientInfoStudyDto> Estudios { get; set; }
    }

    public class WeePatientInfoStudyDto
    {
        public string IdServicio { get; set; }
        public string IdNodo { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string DescripcionWeeClinic { get; set; }
        public int Cantidad { get; set; }
    }
}
