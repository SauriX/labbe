using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Utils
{
    public class MergeGenerator
    {
        public static string Build<T>(string tableName, IEnumerable<T> data, string key, params string[] columns)
        {
            if (columns == null || columns.Length == 0)
            {
                throw new ArgumentNullException(nameof(columns));
            }

            var values = string.Join(", " + Environment.NewLine,
                       data.Select(x =>
                       {
                           var ty = x.GetType();
                           StringBuilder value = new($"({GetMergeValue(x, ty, key)}, ");
                           var colVal = string.Join(", ", columns.Select(c => $"{GetMergeValue(x, ty, c)}"));
                           value.Append(colVal);
                           value.Append(")");
                           return value.ToString();
                       }));

            string script = $"MERGE {tableName} AS Target" +
                $"\nUSING (VALUES {values}) AS Source({key}, {string.Join(", ", columns)})" +
                $"\n\tON Source.{key} = Target.{key}" +
                $"\nWHEN NOT MATCHED BY Target THEN" +
                $"\n\tINSERT ({key}, {string.Join(", ", columns)})" +
                $"\n\tVALUES (Source.{key}, {string.Join(", ", columns.Select(x => $"Source.{x}"))})" +
                $"\nWHEN MATCHED THEN UPDATE SET" +
                $"\n{string.Join("\n,", columns.Select(x => $"Target.{x} = Source.{x}"))};";

            return script;
        }

        private static string GetMergeValue<T>(T obj, Type ty, string col)
        {
            var propertyInfo = ty.GetProperty(col);
            var type = propertyInfo.PropertyType;
            var value = propertyInfo.GetValue(obj, null);

            if (value == null) return "NULL";

            if (type == typeof(string) || type == typeof(Guid) || type == typeof(Guid?))
            {
                return $"'{value}'";
            }
            else if (type == typeof(bool) || type == typeof(bool?))
            {
                return $"{((bool)value ? "1" : "0")}";
            }
            else if (type == typeof(decimal) || type == typeof(byte) || type == typeof(short) || type == typeof(int) || type == typeof(long) ||
                type == typeof(decimal?) || type == typeof(byte?) || type == typeof(short?) || type == typeof(int?) || type == typeof(long?))
            {
                return $"{value}";
            }
            else if (type == typeof(DateTime) || type == typeof(DateTime?))
            {
                return $"'{(DateTime)value:yyyyMMdd HHmmss}'";
            }

            return "NULL";
        }
    }
}
