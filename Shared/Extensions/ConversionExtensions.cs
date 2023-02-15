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
        public static Dictionary<string, object> ToDictionary<T>(this T obj, bool lowercaseFirstLetter = false)
        {
            Dictionary<string, object> result = new();
            if (obj == null)
                return result;

            PropertyInfo[] properties = obj.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                try
                {
                    string key = lowercaseFirstLetter ? char.ToLower(property.Name[0]) + property.Name[1..] : property.Name;
                    var value = property.GetValue(obj, null);
                    result.Add(key, value);
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
