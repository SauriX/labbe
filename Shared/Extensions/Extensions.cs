using ClosedXML.Excel;
using ClosedXML.Report;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Extensions
{
    public static class Extensions
    {
        public static async Task SaveFileAsync(this IFormFile file, string path, string name)
        {
            if (file != null && !string.IsNullOrWhiteSpace(path) && !string.IsNullOrWhiteSpace(name))
            {
                using Stream stream = new FileStream(Path.Combine(path, name), FileMode.Create);
                await file.CopyToAsync(stream);
            }
        }

        public static void Format(this XLTemplate temp)
        {
            temp.Workbook.Worksheets.ToList().ForEach(x =>
            {
                x.Cells().Style.Alignment.SetWrapText(true);
                x.Cells().Style.Alignment.SetVertical(XLAlignmentVerticalValues.Top);
            });
        }

        public static byte[] ToByteArray(this XLTemplate temp)
        {
            MemoryStream stream = GetStream(temp);
            return stream.ToArray();
        }

        private static MemoryStream GetStream<T>(this T workbook)
        {
            MemoryStream ms = new();

            if (workbook is XLTemplate)
            {
                var book = workbook as XLTemplate;
                book.SaveAs(ms);
                ms.Position = 0;
            }

            return ms;
        }

        public static DataTable ToTable<T>(this T data, string name = "table")
        {
            DataTable table = new(name);
            DataRow row;

            if (data is IEnumerable<T>)
            {
                IList list = data as IList;

                foreach (var item in typeof(T).GetGenericArguments()[0].GetProperties().ToList())
                {
                    table.Columns.Add(item.Name.Replace("_", " "), Nullable.GetUnderlyingType(item.PropertyType) ?? item.PropertyType);
                }

                foreach (var prop in list)
                {
                    row = table.NewRow();

                    foreach (var item in prop.GetType().GetProperties())
                    {
                        row[item.Name.Replace("_", " ")] = prop.GetType().GetProperty(item.Name).GetValue(prop, null) ?? DBNull.Value;
                    }

                    table.Rows.Add(row);
                }
            }
            else
            {
                foreach (var item in typeof(T).GetProperties().ToList())
                {
                    table.Columns.Add(item.Name.Replace("_", " "), Nullable.GetUnderlyingType(item.PropertyType) ?? item.PropertyType);
                }

                row = table.NewRow();
                foreach (var item in data.GetType().GetProperties())
                {
                    row[item.Name.Replace("_", " ")] = data.GetType().GetProperty(item.Name).GetValue(data, null) ?? DBNull.Value;
                }
                table.Rows.Add(row);
            }

            return table;
        }
    }
}
