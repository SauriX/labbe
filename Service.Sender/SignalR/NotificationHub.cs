using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Service.Sender.SignalR
{
    public class NotificationHub : Hub
    {
        private string GetEmail()
        {
            //return Context.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            return "admin";
        }

        public async Task Subscribe()
        {
            var user = GetEmail();
            await Groups.AddToGroupAsync(Context.ConnectionId, user);
        }
    }
}
