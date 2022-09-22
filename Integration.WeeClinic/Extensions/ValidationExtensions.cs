using MassTransit;
using Shared.Error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Integration.WeeClinic.Extensions
{
    public static class ValidationExtensions
    {
        public static void ValidateNotEmpty(this Dictionary<string, List<Dictionary<string, object>>> response, params string[] keys)
        {
            if (keys.Any() && !keys.All(response.ContainsKey))
            {
                throw new CustomException(HttpStatusCode.FailedDependency, "La respuesta no contiene el parametro Datos");
            }

            if (keys.All(x => response[x].Count == 0))
            {
                throw new CustomException(HttpStatusCode.FailedDependency, "La respuesta no contiene datos");
            }
        }
    }
}
