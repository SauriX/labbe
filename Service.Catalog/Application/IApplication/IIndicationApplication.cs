
using Service.Catalog.Dtos.Indication;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Application.IApplication
{
    public interface IIndicationApplication
    {
        Task<IEnumerable<IndicationListDto>> GetAll(string search);
        Task<IndicationFormDto> GetById(int Id);
        Task<IndicationListDto> Create(IndicationFormDto indicacion);
        Task<IndicationListDto> Update(IndicationFormDto indication);
        Task<(byte[] file, string fileName)> ExportList(string search);
        Task<(byte[] file, string fileName)> ExportForm(int id);
    }
}
