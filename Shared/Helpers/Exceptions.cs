using Shared.Error;
using System;

namespace Shared.Helpers
{
    public class Exceptions
    {
        public static Exception GetException(ClientException error)
        {
            //var message = error.ExceptionMessage + " / " + error.Message;
            //if (!string.IsNullOrWhiteSpace(error.StackTrace))
            //{
            //    message = string.Concat(message, Environment.NewLine, error.StackTrace.Trim().Split(Environment.NewLine)[0]);
            //}

            //string innerMessage;
            //if (error.InnerException != null)
            //{
            //    innerMessage = error.InnerException.ExceptionMessage + " / " + error.InnerException.Message;

            //    if (!string.IsNullOrWhiteSpace(error.InnerException.StackTrace))
            //    {
            //        innerMessage = string.Concat(innerMessage, Environment.NewLine, error.InnerException.StackTrace.Trim().Split(Environment.NewLine)[0]);
            //    }

            //    return new Exception(message, new Exception(innerMessage));
            //}

            return new Exception("");
        }

        public static string GetMessage(Exception ex)
        {
            var message = string.Concat("Exception: ", ex.Message);
            if (!string.IsNullOrWhiteSpace(ex.StackTrace))
            {
                var stackList = ex.StackTrace; //.Split(Environment.NewLine);
                var stackMessage = stackList; //.FirstOrDefault(x => x.Contains("LaboratorioRamos"));
                if (!string.IsNullOrWhiteSpace(stackMessage))
                {
                    message = string.Concat(message, Environment.NewLine, "StackTrace: ", stackMessage.Trim());
                }
            }

            string innerMessage;
            if (ex.InnerException != null)
            {
                innerMessage = string.Concat(Environment.NewLine, "InnerException: ", ex.InnerException.Message);

                if (!string.IsNullOrWhiteSpace(ex.InnerException.StackTrace))
                {
                    var stackList = ex.InnerException.StackTrace; //.Split(Environment.NewLine);
                    var stackMessage = stackList; //.FirstOrDefault(x => x.Contains("LaboratorioRamos"));
                    if (!string.IsNullOrWhiteSpace(stackMessage))
                    {
                        innerMessage = string.Concat(innerMessage, Environment.NewLine, "StackTrace: ", stackMessage.Trim());
                    }
                }

                return string.Concat(message, innerMessage);
            }

            return message;
        }
    }
}
