using ClosedXML.Excel;
using ClosedXML.Report;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Extensions
{
    public static class Extensions
    {
        public static async Task<bool> SaveFileAsync(this IFormFile file, string path, string name)
        {
            try
            {
                if (file != null && !string.IsNullOrWhiteSpace(path) && !string.IsNullOrWhiteSpace(name))
                {
                    Directory.CreateDirectory(path);
                    using Stream stream = new FileStream(Path.Combine(path, name), FileMode.Create);
                    await file.CopyToAsync(stream);

                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static bool In<T>(this T obj, params T[] args)
        {
            return args.Contains(obj);
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

        public static bool IsImage(this IFormFile postedFile)
        {
            //-------------------------------------------
            //  Check the image mime types
            //-------------------------------------------
            if (postedFile.ContentType.ToLower() != "image/jpg" &&
                        postedFile.ContentType.ToLower() != "image/jpeg" &&
                        postedFile.ContentType.ToLower() != "image/pjpeg" &&
                        postedFile.ContentType.ToLower() != "image/gif" &&
                        postedFile.ContentType.ToLower() != "image/x-png" &&
                        postedFile.ContentType.ToLower() != "image/png")
            {
                return false;
            }

            //-------------------------------------------
            //  Check the image extension
            //-------------------------------------------
            if (Path.GetExtension(postedFile.FileName).ToLower() != ".jpg"
                && Path.GetExtension(postedFile.FileName).ToLower() != ".png"
                && Path.GetExtension(postedFile.FileName).ToLower() != ".gif"
                && Path.GetExtension(postedFile.FileName).ToLower() != ".jpeg")
            {
                return false;
            }

            //-------------------------------------------
            //  Attempt to read the file and check the first bytes
            //-------------------------------------------
            try
            {
                if (!postedFile.OpenReadStream().CanRead)
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

            //-------------------------------------------
            //  Try to instantiate new Bitmap, if .NET will throw exception
            //  we can assume that it's not a valid image
            //-------------------------------------------
            try
            {
                using var bitmap = new System.Drawing.Bitmap(postedFile.OpenReadStream());
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                postedFile.OpenReadStream().Position = 0;
            }

            return true;
        }

        public static IEnumerable<IEnumerable<T>> ToChunks<T>(this IEnumerable<T> enumerable, int chunkSize)
        {
            int itemsReturned = 0;
            var list = enumerable.ToList();
            int count = list.Count;
            while (itemsReturned < count)
            {
                int currentChunkSize = Math.Min(chunkSize, count - itemsReturned);
                yield return list.GetRange(itemsReturned, currentChunkSize);
                itemsReturned += currentChunkSize;
            }
        }
    }
}
