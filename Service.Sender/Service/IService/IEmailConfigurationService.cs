using Service.Sender.Dtos;
using System.Threading.Tasks;

namespace Service.Sender.Service.IService
{
    public interface IEmailConfigurationService
    {
        Task<EmailConfigurationDto> GetEmail();
        Task UpdateEmail(EmailConfigurationDto email);
    }
}
