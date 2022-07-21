using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Sender.Service.IService
{
    public interface IEmailService
    {
        Task Send(string to, string subject, string title, string content);
        Task Send(IEnumerable<string> to, string subject, string title, string content);
    }
}
