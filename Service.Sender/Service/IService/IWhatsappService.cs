using System.Threading.Tasks;

namespace Service.Sender.Service.IService
{
    public interface IWhatsappService
    {
        Task Send(string phone, string message);
    }
}
