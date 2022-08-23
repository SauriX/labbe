using Shared.Dictionary;
using Shared.Error;
using System;
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
    }
}
