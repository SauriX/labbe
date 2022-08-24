using Shared.Dictionary;
using Shared.Error;
using System;
using System.Linq;
using System.Net;

namespace Shared.Helpers
{
    public class Helpers
    {
        public static void ValidateGuid(string text, out Guid guid)
        {
            var validGuid = Guid.TryParse(text, out guid);

            if (!validGuid)
            {
                throw new CustomException(HttpStatusCode.BadRequest, Responses.NotFound);
            }
        }

        static readonly Random random = new();
        public static string GenerateRandomHex(int digits = 5)
        {
            byte[] buffer = new byte[digits / 2];
            random.NextBytes(buffer);
            string result = string.Concat(buffer.Select(x => x.ToString("X2")).ToArray());

            if (digits % 2 == 0)
            {
                return result;
            }

            return result + random.Next(16).ToString("X");
        }
    }
}
