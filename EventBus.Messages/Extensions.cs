using EventBus.Messages.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages
{
    public static class Extensions
    {
        public static string Serialize(this NotificationContract notification)
        {
            var serialized = JsonConvert.SerializeObject(notification, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            return serialized;
        }
    }
}
