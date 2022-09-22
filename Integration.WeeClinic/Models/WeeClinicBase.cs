using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Integration.WeeClinic.Models
{
    public class WeeClinicBase
    {
        public bool IsOnline { get; set; }
        public bool IsActionPermitted { get; set; }
        public bool IsExists { get; set; }
        public bool IsOk { get; set; }
        public int MensajeID { get; set; }
        public string Mensaje { get; set; }
        public DateTime Fecha { get; set; }
        public string URL { get; set; }
        public int NoFilas { get; set; }
        public string Permiso { get; set; }
        [JsonPropertyName("dsRespuesta")]
        public Dictionary<string, List<Dictionary<string, object>>> DsRespuesta { get; set; }
        public string Nota { get; set; }
    }
}
