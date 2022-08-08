using MassTransit;
using Microsoft.Extensions.Logging;
using Shared.Dictionary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Shared.Utils
{
    public class RabbitFaultLog<T> where T : class
    {
        public string GetLog(ConsumeContext<Fault<T>> context)
        {
            try
            {
                var contract = typeof(T);
                var messageId = (context.Message.FaultedMessageId ?? Guid.Empty).ToString();
                var messageEx = "";

                if (context.Message.Exceptions != null && context.Message.Exceptions.Any())
                {
                    messageEx = string.Join("\n", context.Message.Exceptions.Select(x => $"Exception: {x.Message}\nStackTrace: {x.StackTrace}"));
                }

                var messageData = "";
                if (context.Message.Message != null)
                {
                    messageData = JsonSerializer.Serialize(context.Message.Message);
                }

                var retry = context.GetRetryCount();

                var exMessage = Responses.RabbitMQError(
                    context.SourceAddress.OriginalString, retry, contract.FullName, messageId, messageData, messageEx);

                return exMessage;
            }
            catch (Exception)
            {
                return "Something happened while getting fault error";
            }

        }
    }
}
