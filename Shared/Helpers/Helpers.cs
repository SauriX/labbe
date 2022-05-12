using Shared.Dictionary;
using Shared.Error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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

        public static void Validateint(string text, out int guid)
        {
            var validGuid = int.TryParse(text, out guid);

            if (!validGuid)
            {
                throw new CustomException(HttpStatusCode.BadRequest, Responses.NotFound);
            }
        }
    }
}
