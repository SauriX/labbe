using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Service.MedicalRecord.Context;
using Service.MedicalRecord.Dictionary;
using Service.MedicalRecord.Domain.RouteTracking;
using Service.MedicalRecord.Domain.TrackingOrder;
using Service.MedicalRecord.Dtos.PendingRecive;
using Service.MedicalRecord.Dtos.RouteTracking;
using Service.MedicalRecord.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Service.MedicalRecord.Domain.Request;
namespace Service.MedicalRecord.Repository
{
    public class RouteTrackingRepository:IRouteTrackingRepository
    {
        private readonly ApplicationDbContext _context;
        public RouteTrackingRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Request> FindAsync(Guid id)
        {
            var report = _context.CAT_Solicitud.Include(x => x.Estudios);
            var request = await _context.CAT_Solicitud.FindAsync(id);
            return request;
        }
        public async Task<List<RequestStudy>> GetStudyById(Guid requestId, IEnumerable<int> studiesIds)
        {
            var studies = await _context.Relacion_Solicitud_Estudio
                .Where(x => x.SolicitudId == requestId && studiesIds.Contains(x.EstudioId))
                .ToListAsync();
            return studies;
        }
        public async Task BulkUpdateStudies(Guid requestId, List<RequestStudy> studies)
        {
            var config = new BulkConfig();
            config.SetSynchronizeFilter<RequestStudy>(x => x.SolicitudId == requestId);
            await _context.BulkUpdateAsync(studies, config);
        }
        public async Task<List<TrackingOrder>> GetAll(RouteTrackingSearchDto search) {
            var routeTrackingList = _context.CAT_Seguimiento_Ruta.Where(x => x.Estudios.Any(y => y.Solicitud.Estudios.Any(m=>m.EstatusId==Status.RequestStudy.TomaDeMuestra|| m.EstatusId == Status.RequestStudy.EnRuta)))
                .Include(x => x.Estudios)
                .ThenInclude(x=>x.Solicitud.Sucursal)
                .Include(x => x.Estudios)
                .ThenInclude(x=>x.Solicitud.Estudios)
                .ThenInclude(x=>x.Estatus)
                .Include(x=>x.Estudios)
            .ThenInclude(x=>x.SolicitudEstudio).AsQueryable();
            if (search.Fechas != null && search.Fechas.Length != 0)
            {
                routeTrackingList = routeTrackingList.
                    Where(x => x.FechaCreo.Date >= search.Fechas.First().Date && x.FechaCreo.Date <= search.Fechas.Last().Date);
            }
            if (!string.IsNullOrEmpty(search.Sucursal))
            {
                routeTrackingList = routeTrackingList.Where(x => search.Sucursal.Contains(x.SucursalDestinoId));
            }
            if (!string.IsNullOrEmpty(search.Buscar))
            {
                routeTrackingList = routeTrackingList.Where(x => search.Buscar.Contains(x.Clave));
            }
            return await routeTrackingList.ToListAsync();
        }
        public async Task<TrackingOrder> getById(Guid Id) {
            var route = await _context.CAT_Seguimiento_Ruta.Include(x => x.Estudios)
                .ThenInclude(x => x.Solicitud.Sucursal)
                .Include(x => x.Estudios)
                .ThenInclude(x => x.Solicitud.Estatus)
                .Include(x => x.Estudios).ThenInclude(x => x.Solicitud.Estudios)
                .ThenInclude(x => x.Estatus)
                .Include(x => x.Estudios)
                .ThenInclude(x => x.Solicitud.Expediente).AsQueryable().FirstOrDefaultAsync(x=>x.Id==Id);
            return route;
        }
        public async Task Update(RouteTracking route) {
             _context.Update(route);
            await _context.SaveChangesAsync();
        }
        public async Task Create(RouteTracking route) {
            await _context.AddAsync(route);
            await _context.SaveChangesAsync();
        }
        public async Task<List<TrackingOrder>> GetAllRecive(PendingSearchDto search) {

            var routeTrackingList = _context.CAT_Seguimiento_Ruta.Include(x => x.Estudios)
                .ThenInclude(x => x.Solicitud.Sucursal)
                .Include(x => x.Estudios)
                .ThenInclude(x => x.Solicitud.Estatus)
                .Include(x => x.Estudios)
                .ThenInclude(x => x.Solicitud.Estudios)
                .ThenInclude(x => x.Estatus)
                .Include(x => x.Estudios)
                .ThenInclude(x => x.Solicitud.Expediente)
                .Include(x => x.Estudios)
                .ThenInclude(x=>x.SolicitudEstudio)
                .AsQueryable();
            if (search.Sucursal!=null && search.Sucursal.Count >0)
            {
                routeTrackingList = routeTrackingList.Where(x => search.Sucursal.Contains(x.SucursalOrigenId));
            }
            if (!string.IsNullOrEmpty(search.Busqueda))
            {
                routeTrackingList = routeTrackingList.Where(x => search.Busqueda.Contains(x.Clave));
            }
            routeTrackingList = routeTrackingList.Where(x =>  x.SucursalDestinoId == search.Sucursaldest);
            return await routeTrackingList.ToListAsync();
        }
        public async Task<RouteTracking> GetTracking(Guid Id) {
            var routeTracking =   _context.Cat_PendientesDeEnviar.Include(x => x.Solicitud.Sucursal).FirstOrDefault(x=>x.SegumientoId==Id);
            return routeTracking;
        }
    }
}
