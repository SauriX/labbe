using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Integration.WeeClinic.Models.Laboratorio_BusquedaFolios
{
    public class Laboratorio_BusquedaFolios
    {
        [JsonPropertyName("idOrden")]
        public string IdOrden { get; set; }
        public string FolioOrden { get; set; }
        [JsonPropertyName("xDateInsert")]
        public DateTime DateInsert { get; set; }
        public string FechaFolio { get; set; }
        [JsonPropertyName("idProducto")]
        public string IdProducto { get; set; }
        public string CorporativoNombre { get; set; }
        public string ProductoNombre { get; set; }
        public string NoPoliza { get; set; }
        [JsonPropertyName("idPersona")]
        public string IdPersona { get; set; }
        public string Nombre { get; set; }
        public string Paterno { get; set; }
        public string Materno { get; set; }
        public string NombreCompleto { get; set; }
        public string CURP { get; set; }
        [JsonPropertyName("codGenero")]
        public string CodGenero { get; set; }
        public string Genero { get; set; }
        public string RFC { get; set; }
        public int Edad { get; set; }
        public string Busqueda { get; set; }
        public string TPA { get; set; }
        [JsonPropertyName("isEstatus")]
        public int IsEstatus { get; set; }
        public string Copagos { get; set; }
        [JsonPropertyName("isTyC")]
        public int IsTyC { get; set; }
        public string NombreCompleto_Medico { get; set; }
        public int EstatusVigencia { get; set; }
    }
}
