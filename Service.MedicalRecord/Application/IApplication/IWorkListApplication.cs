using System.Threading.Tasks;
using System;
using Service.MedicalRecord.Dtos.WorkList;

namespace Service.MedicalRecord.Application.IApplication
{
    public interface IWorkListApplication
    {
        Task<byte[]> PrintWorkList(WorkListFilterDto filter);
    }
}
