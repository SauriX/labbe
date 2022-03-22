using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Error
{
    public class CustomException : Exception
    {
        public HttpStatusCode Code { get; }
        public object Errors { get; }

        public CustomException(HttpStatusCode code, string message = null)
        {
            Code = code;
            Errors = new { error = message };
        }
    }
}
