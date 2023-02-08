using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Extensions
{
    public static class ConversionExtensions
    {
        public static Dictionary<string, object> ToDictionary<T>(this T obj)
        {
            Dictionary<string, object> result = new();
            if (obj == null)
                return result;

            PropertyInfo[] properties = obj.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                try
                {
                    result.Add(property.Name, property.GetValue(obj, null));
                }
                catch (Exception)
                {
                    continue;
                }
            }
            return result;
        }
    }
}
