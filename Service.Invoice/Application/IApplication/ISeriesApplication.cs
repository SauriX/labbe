using Service.Billing.Dto.Series;
using Service.Billing.Dtos.Series;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Billing.Application.IApplication
{
    public interface ISeriesApplication
    {
        Task<IEnumerable<SeriesListDto>> GetByBranch(Guid branchId, byte type);
        Task<IEnumerable<SeriesListDto>> GetByFilter(SeriesFilterDto filter);
        Task<SeriesDto> GetByNewForm(SeriesNewDto newSerie);
        Task<SeriesDto> GetById(int id, byte tipo);
        Task CreateInvoice(SeriesDto serie);
        Task UpdateTicket(TicketDto ticket);
        Task CreateTicket(TicketDto ticket);
        Task UpdateInvoice(SeriesDto serie);
        Task<(byte[] file, string fileName)> ExportSeriesList(SeriesFilterDto search);
    }
}
