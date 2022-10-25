using System;
using System.Net;

namespace Shared.Error
{
    public class CustomException : Exception
    {
        public HttpStatusCode Code { get; }
        public string Errors { get; }

        public CustomException(HttpStatusCode code, string message = null)
        {
            Code = code;
            Errors = message;
        }
    }
}
