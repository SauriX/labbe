using Service.Sender.Dtos;
using System.Threading.Tasks;

namespace Service.Sender.Service.IService
{
    public interface IConfigurationService
    {
        Task<ConfigurationEmailDTO> GetEmail(bool pass = false);
    }
}
