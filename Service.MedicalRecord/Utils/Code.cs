using Shared.Error;
using System;
using System.Net;

namespace Service.MedicalRecord.Utils
{
    public static class Code
    {
        public static int GetCode(string codeRange, string lastCode)
        {
            var date = DateTime.Now.ToString("ddMMyy");
            var ranges = codeRange?.Split("-");

            if (ranges.Length != 2)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "Sucursal no configurada");
            }

            var initOk = int.TryParse(ranges[1], out int init);
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
    }
}
