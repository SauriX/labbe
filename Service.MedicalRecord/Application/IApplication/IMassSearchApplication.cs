using Service.MedicalRecord.Dtos.MassSearch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Application.IApplication
{
    public interface IMassSearchApplication
    {
        Task<MassSearchInfoDto> GetByFilter(MassSearchFilterDto filter);
    }
}
