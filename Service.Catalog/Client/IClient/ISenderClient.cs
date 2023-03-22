using Service.Catalog.Dtos.Notifications;
using System.Threading.Tasks;

namespace Service.Catalog.Client.IClient
{
    public interface ISenderClient
    {
        Task Notify(NotificationDto notification);
    }
}
