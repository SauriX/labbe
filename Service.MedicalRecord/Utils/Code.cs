using FluentValidation.Validators;
using Shared.Error;
using Shared.Helpers;
using System;
using System.Linq;
using System.Net;

namespace Service.MedicalRecord.Utils
{
    public static class Codes
    {
        public static int GetCode(string codeRange, string lastCode)
        {
            var date = DateTime.Now.ToString("ddMMyy");
            var ranges = codeRange?.Split("-");

            if (ranges == null || ranges.Length != 2)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "Sucursal no configurada");
            }

            var initOk = int.TryParse(ranges[0], out int init);
            var limitOk = int.TryParse(ranges[1], out int limit);

            if (!initOk || !limitOk)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "Sucursal no configurada");
            }

            if (lastCode == null)
            {
                return init;
            }

            var currentCode = Convert.ToInt32(lastCode[..lastCode.IndexOf(date)]) + 1;

            if (currentCode > limit)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "Limite diario alcanzado");
            }

            return currentCode;
        }

        public static string GetPathologicalCode(string type, string lastCode)
        {
            var date = DateTime.Now.ToString("yy");

            if (lastCode == null)
            {
                return $"{(type == "C" ? "C" : "LR")}-{1:D4}-{date}";
            }

            var parts = lastCode.Split("-");

            var consecutive = Convert.ToInt32(parts[1]) + 1;

            return $"{(type == "C" ? "C" : "LR")}-{consecutive:D4}-{date}";
        }
    }
}
