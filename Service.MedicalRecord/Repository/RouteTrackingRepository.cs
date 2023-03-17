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
    public class RouteTrackingRepository : IRouteTrackingRepository
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
        public async Task<List<TrackingOrder>> GetAll(RouteTrackingSearchDto search)
        {
            var routeTrackingList = _context.CAT_Seguimiento_Ruta.Where(x => x.Estudios.Any(y => y.Solicitud.Estudios.Any(m => m.EstatusId == Status.RequestStudy.TomaDeMuestra || m.EstatusId == Status.RequestStudy.EnRuta)))
                .Include(x => x.Estudios)
                .ThenInclude(x => x.Solicitud.Sucursal)
                .Include(x => x.Estudios)
                .ThenInclude(x => x.Solicitud.Estudios)
                .ThenInclude(x => x.Estatus)
                .Include(x => x.Etiquetas)
                .ThenInclude(x => x.Estudios)
            .AsQueryable();

            if (search.Fechas != null && search.Fechas.Length != 0)
            {
                routeTrackingList = routeTrackingList.
                    Where(x => x.FechaCreo.Date >= search.Fechas.First().Date && x.FechaCreo.Date <= search.Fechas.Last().Date);
            }

            if (!string.IsNullOrEmpty(search.Origen))
            {
                routeTrackingList = routeTrackingList.Where(x => search.Destino.Contains(x.OrigenId));
            }

            if (!string.IsNullOrEmpty(search.Destino))
            {
                routeTrackingList = routeTrackingList.Where(x => search.Destino.Contains(x.DestinoId));
            }

            if (!string.IsNullOrEmpty(search.Buscar))
            {
                routeTrackingList = routeTrackingList.Where(x => search.Buscar.Contains(x.Clave));
            }

            return await routeTrackingList.ToListAsync();
        }

        public async Task<List<RequestTag>> GetTagsByOrigin()
        {
            var tags = await _context.Relacion_Solicitud_Etiquetas
                .Include(x => x.Estudios)
                .Include(x => x.Solicitud)
                .ThenInclude(x => x.Estudios)
                .ToListAsync();

            return tags;
        }

        public async Task<TrackingOrder> GetById(Guid Id)
        {
            var route = await _context.CAT_Seguimiento_Ruta
                .Include(x => x.Estudios)
                .ThenInclude(x => x.Solicitud)
                .Include(x => x.Etiquetas)
                .AsQueryable().FirstOrDefaultAsync(x => x.Id == Id);
            return route;
        }

        public async Task CreateOrder(TrackingOrder order)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var tags = order.Estudios.ToList();

                order.Estudios = null;

                _context.CAT_Seguimiento_Ruta.Add(order);

                await _context.SaveChangesAsync();

                tags.ForEach(x => x.SeguimientoId = order.Id);

                var config = new BulkConfig();
                config.SetSynchronizeFilter<TrackingOrderDetail>(x => x.SeguimientoId == order.Id);

                await _context.BulkInsertOrUpdateAsync(tags, config);

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task Update(RouteTracking route)
        {
            _context.Update(route);
            await _context.SaveChangesAsync();
        }
        public async Task Create(RouteTracking route)
        {
            await _context.AddAsync(route);
            await _context.SaveChangesAsync();
        }
        public async Task<List<TrackingOrder>> GetAllRecive(PendingSearchDto search)
        {

            var routeTrackingList = _context.CAT_Seguimiento_Ruta.Include(x => x.Estudios)
                .ThenInclude(x => x.Solicitud.Sucursal)
                .Include(x => x.Estudios)
                .ThenInclude(x => x.Solicitud.Estatus)
                .Include(x => x.Estudios)
                .ThenInclude(x => x.Solicitud.Estudios)
                .ThenInclude(x => x.Estatus)
                .Include(x => x.Estudios)
                .ThenInclude(x => x.Solicitud.Expediente)
                .Include(x => x.Etiquetas)
                .ThenInclude(x => x.Estudios)
                .AsQueryable();
            if (search.Sucursal != null && search.Sucursal.Count > 0)
            {
                routeTrackingList = routeTrackingList.Where(x => search.Sucursal.Contains(x.OrigenId));
            }
            if (!string.IsNullOrEmpty(search.Busqueda))
            {
                routeTrackingList = routeTrackingList.Where(x => search.Busqueda.Contains(x.Clave));
            }
            routeTrackingList = routeTrackingList.Where(x => x.DestinoId == search.Sucursaldest);
            return await routeTrackingList.ToListAsync();
        }
        public async Task<RouteTracking> GetTracking(Guid Id)
        {
            var routeTracking = _context.Cat_PendientesDeEnviar.Include(x => x.Solicitud.Sucursal).FirstOrDefault(x => x.SegumientoId == Id);
            return routeTracking;
        }

        public async Task<IEnumerable<RequestTag>> GetAllTags(string search)
        {
            var tags = _context.Relacion_Solicitud_Etiquetas
                .Include(x => x.Estudios)
                .AsQueryable();

            search = search.Trim().ToLower();

            if (!string.IsNullOrWhiteSpace(search) && search != "all")
            {
                tags = tags.Where(x => x.ClaveEtiqueta.ToLower().Contains(search));
            }

            return await tags.ToListAsync();

        }

        public async Task<IEnumerable<RequestTag>> FindTags(string routeId)
        {
            var tags = await _context.Relacion_Solicitud_Etiquetas
                .Where(x => x.Destino == routeId)
                .Include(x => x.Estudios)
                .ToListAsync();

            return tags;
        }

        public async Task<IEnumerable<RequestStudy>> FindStudies(List<int> tagsId, Guid requestId)
        {
            var studyTags = await _context.Relacion_Etiqueta_Estudio
                .Where(x => tagsId.Contains(x.SolicitudEtiquetaId))
                .Include(x => x.SolicitudEtiqueta)
                .Select(x => x.EstudioId)
                .ToListAsync();

            var studies = await _context.Relacion_Solicitud_Estudio
                .Where(x => studyTags.Contains(x.EstudioId) && x.SolicitudId == requestId)
                .Where(x => x.EstatusId == Status.RequestStudy.TomaDeMuestra || x.EstatusId == Status.RequestStudy.EnRuta)
                .ToListAsync();

            return studies;
        }

        public async Task<string> GetLastCode(string date)
        {
            var lastOrder = await _context.Relacion_Seguimiento_Solicitud
                .Include(x => x.Solicitud)
                .Include(x => x.Etiqueta)
                .FirstOrDefaultAsync();

            return lastOrder?.Clave;
        }
    }
}
