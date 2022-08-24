using Service.Catalog.Dtos.Equipmentmantain;
using System.Threading.Tasks;

namespace Service.Catalog.Application
{
    public interface IPdfClient
    {
        Task<byte[]> GenerateOrder(MantainFormDto order);
    }
}