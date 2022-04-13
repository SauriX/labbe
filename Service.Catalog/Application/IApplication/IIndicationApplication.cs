
using Service.Catalog.Dtos.Indication;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Application.IApplication
{
    public interface IIndicationApplication
    {
        Task<IndicationFormDto> GetById(int Id);
        Task<IndicationFormDto> Create(IndicationFormDto indicacion);
        Task<IndicationFormDto> Update(IndicationFormDto indication);
        Task<IEnumerable<IndicationListDto>> GetAll(string search = null);
        Task<byte[]> ExportListIndication(string search = null);
        Task<byte[]> ExportFormIndication(int id);
    }
}
