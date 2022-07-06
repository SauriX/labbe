using Microsoft.EntityFrameworkCore;
using Service.Catalog.Context;
using Service.Report.Domain.Request;
using Service.Report.Dtos.Request;
using Service.Report.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Repository
{
    public class RequestRepository : IRequestRepository
    {
        private readonly ApplicationDbContext _context;

        public RequestRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Request>> GetRequestByCount()
        {
            var report = await _context.Request.ToListAsync();

            return report;
        }


        public async Task<List<Report.Domain.Request.Request>> GetFilter(RequestSearchDto search)
        {
            var report = _context.Request
                .AsQueryable();

            if (!string.IsNullOrEmpty(search.ciudad))
            {
                report = report.Where(x => x.Ciudad.ToString() == search.ciudad);
            }
            if (!string.IsNullOrEmpty(search.sucursal))
            {
                report = report.Where(x => x.SucursalId == Guid.Parse(search.sucursal));
            }
            //if (search.Fecha != )
            //{
            //   report = report.Where(x => x.Fecha == search.Fecha);
            //}
            return report.ToList();

        }

    }
}
