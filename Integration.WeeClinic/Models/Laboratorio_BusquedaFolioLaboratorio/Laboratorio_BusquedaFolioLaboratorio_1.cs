using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Integration.WeeClinic.Models.Laboratorio_BusquedaFolioLaboratorio
{
    public class Laboratorio_BusquedaFolioLaboratorio_1
    {
        [JsonPropertyName("isVigente")]
        public int IsVigente { get; set; }
        public string Poliza { get; set; }
        public int CodEstatus { get; set; }
        public string Estatus { get; set; }
        public string Cliente { get; set; }
        [JsonPropertyName("idServicio")]
        public string IdServicio { get; set; }
        [JsonPropertyName("idNodo")]
        public string IdNodo { get; set; }
        [JsonPropertyName("idCliente")]
        public string IdCliente { get; set; }
        [JsonPropertyName("idServicioParent")]
        public string IdServicioParent { get; set; }
        [JsonPropertyName("idNodoParent")]
        public string IdNodoParent { get; set; }
        public string FolioSolicitud { get; set; }
        public int CODTIPOSERVICIO { get; set; }
        public string NoReferencia { get; set; }
        public int EdadPersona { get; set; }
        public string IvaSucursal { get; set; }
    }
}