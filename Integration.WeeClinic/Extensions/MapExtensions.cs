using Newtonsoft.Json;
using System.Collections.Generic;

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
