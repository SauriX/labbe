using Microsoft.AspNetCore.SignalR;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Service.Sender.SignalR
{
    public class NotificationHub : Hub
    {
        private string GetId()
        {
            return Context.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        }

        public async Task Subscribe()
        {
            var userId = GetId();
            await Groups.AddToGroupAsync(Context.ConnectionId, userId);
        }

        public async Task SubscribeWithName(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task RemoveWithName(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }
    }
}
