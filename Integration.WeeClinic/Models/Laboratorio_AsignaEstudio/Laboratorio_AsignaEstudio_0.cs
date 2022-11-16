using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Integration.WeeClinic.Models.Laboratorio_AsignaEstudio
{
    public class Laboratorio_AsignaEstudio_0
    {
        [JsonPropertyName("idServicio")]
        public string IdServicio { get; set; }
        public string Estatus { get; set; }
        public string Mensaje { get; set; }
    }
}
