using Service.Sender.Domain.EmailConfiguration;
using System.Threading.Tasks;

namespace Service.Sender.Repository.IRepository
{
    public interface IEmailConfigurationRepository
    {
        Task<EmailConfiguration> GetEmail();
        Task UpdateEmail(EmailConfiguration newEmail);
    }
}
