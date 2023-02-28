using Service.Billing.Dtos.Notification;
using System.Threading.Tasks;

namespace Service.Billing.Client.IClient
{
    public interface ISenderClient
    {
        Task Notify(NotificationDto notification);
    }
}
