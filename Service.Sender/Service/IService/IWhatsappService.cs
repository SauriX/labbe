using System;
using System.Threading.Tasks;

namespace Service.Sender.Service.IService
{
    public interface IWhatsappService
    {
        Task Send(string phone, string message);
        Task SendFile(string phone, Uri filePath, string fileName, string message);
    }
}
