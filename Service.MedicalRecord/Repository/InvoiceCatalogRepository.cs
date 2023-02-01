using Microsoft.EntityFrameworkCore;
using Service.MedicalRecord.Context;
using Service.MedicalRecord.Dtos.InvoiceCatalog;
using Service.MedicalRecord.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Repository
{
    public class InvoiceCatalogRepository:IInvoiceCatalogRepository
    {
        private readonly ApplicationDbContext _context;

        public InvoiceCatalogRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Domain.Request.Request>> GetNotas(InvoiceCatalogSearch search) {
            var request = _context.CAT_Solicitud.Include(x=>x.Compañia).Include(x=>x.Sucursal).AsQueryable();
            if (search.Fecha != null ) {
                request = request.Where(x => x.FechaCreo.Date >= search.Fecha[0].Date && x.FechaCreo.Date <= search.Fecha[1].Date);

            }

            if (!string.IsNullOrEmpty(search.Buscar)) {
                request = request.Where(x=>x.SerieNumero == search.Buscar || x.Clave == search.Buscar);
            
            }
            if (search.Sucursal != null && search.Sucursal.Length > 0) {
                request = request.Where(x => search.Sucursal.Any(y => y == x.SucursalId.ToString()));
            }

            if (!string.IsNullOrEmpty(search.Ciudad)) {
                request = request.Where(x=>x.Sucursal.Ciudad == search.Ciudad);
            }
            var requestFilter = await request.ToListAsync();
            return requestFilter;
        }


        public async Task<List<Domain.Request.Request>> GetSolicitudbyclave(List<string> clave)
        {
            var request =await _context.CAT_Solicitud.Include(x => x.Compañia).Include(x => x.Sucursal).ToListAsync();
            var requestFilter = request.FindAll(x=> clave.Any (y=>x.Clave==y));
            
            return requestFilter;
        }

    }
}
