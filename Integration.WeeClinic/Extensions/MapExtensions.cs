using Integration.WeeClinic.Models.Laboratorio_BusquedaFolioLaboratorio;
using Newtonsoft.Json;
using Shared.Error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Integration.WeeClinic.Extensions
{
    public static class MapExtensions
    {
        public static T Transform<T>(this Dictionary<string, List<Dictionary<string, object>>> response)
        {
            var serializedResponse = System.Text.Json.JsonSerializer.Serialize(response);

            var folios = JsonConvert.DeserializeObject<T>(serializedResponse);

            return folios;
        }
    }
}
