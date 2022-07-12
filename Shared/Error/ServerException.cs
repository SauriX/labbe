using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Error
{
    public class ServerException
    {
        private string stackTrace;
        private string message;
        private string exceptionMessage;
        private string exceptionType;

        public ServerException() { }

        public ServerException(Exception exception)
        {
            Message = exception.Message;
            ExceptionMessage = exception.Message;
            ExceptionType = exception.GetType().ToString();
            StackTrace = exception.StackTrace;
            InnerException = exception.InnerException == null ? null :
                new ServerException
                {
                    Message = exception.InnerException.Message,
                    ExceptionMessage = exception.InnerException.Message,
                    ExceptionType = exception.InnerException.GetType().ToString(),
                    StackTrace = exception.InnerException.StackTrace,
                };
        }

        public string Message { get => message; set => message = value?.Trim(); }
        public string ExceptionMessage { get => exceptionMessage; set => exceptionMessage = value?.Trim(); }
        public string ExceptionType { get => exceptionType; set => exceptionType = value?.Trim(); }
        public string StackTrace
        {
            get { return stackTrace; }
            set { stackTrace = value; }
            //set { stackTrace = value?.Split(Environment.NewLine)?.FirstOrDefault(x => x.Contains("LaboratorioRamos"))?.Trim(); }
        }

        public ServerException InnerException { get; set; }
    }
}
