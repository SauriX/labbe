using Service.Catalog.Dto.Series;
using Service.Catalog.Dtos.Series;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Application.IApplication
{
    public interface ISeriesApplication
    {
        Task<IEnumerable<SeriesListDto>> GetByBranch(Guid branchId, byte type);
        Task<IEnumerable<SeriesListDto>> GetByFilter(SeriesFilterDto filter);
        Task<SeriesDto> GetByNewForm(SeriesNewDto newSerie);
        Task<SeriesDto> GetById(int id, byte tipo);
        Task<ExpeditionPlaceDto> GetBranch(string branchId);
        Task CreateInvoice(SeriesDto serie);
        Task UpdateTicket(TicketDto ticket);
        Task CreateTicket(TicketDto ticket);
        Task UpdateInvoice(SeriesDto serie);
        Task<(byte[] file, string fileName)> ExportSeriesList(SeriesFilterDto search);
    }
}
