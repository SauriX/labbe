using Service.Catalog.Dtos.Series;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Application.IApplication
{
    public interface ISeriesApplication
    {
        Task<IEnumerable<SeriesListDto>> GetByBranch(Guid branchId, byte type);
    }
}
