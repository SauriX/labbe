﻿using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Service.MedicalRecord.Context;
using Service.MedicalRecord.Domain.RouteTracking;
using Service.MedicalRecord.Domain.TrackingOrder;
using Service.MedicalRecord.Dtos.PendingRecive;
using Service.MedicalRecord.Dtos.RouteTracking;
using Service.MedicalRecord.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Service.MedicalRecord.Dictionary.Status;

namespace Service.MedicalRecord.Repository
{
    public class RouteTrackingRepository:IRouteTrackingRepository
    {
        private readonly ApplicationDbContext _context;

        public RouteTrackingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<TrackingOrder>> GetAll(RouteTrackingSearchDto search) {
            var routeTrackingList = _context.CAT_Seguimiento_Ruta.Include(x => x.Estudios).ThenInclude(x=>x.Solicitud.Sucursal).AsQueryable();

            if (search.Fechas != null && search.Fechas.Length != 0)
            {
                routeTrackingList = routeTrackingList.
                    Where(x => x.FechaCreo.Date >= search.Fechas.First().Date && x.FechaCreo.Date <= search.Fechas.Last().Date);
            }

            if (!string.IsNullOrEmpty(search.Sucursal))
            {
                routeTrackingList = routeTrackingList.Where(x => search.Sucursal.Contains(x.SucursalOrigenId));
            }

            if (!string.IsNullOrEmpty(search.Buscar))
            {
                routeTrackingList = routeTrackingList.Where(x => search.Buscar.Contains(x.Clave));
            }
            return await routeTrackingList.ToListAsync();
        }
        public async Task<TrackingOrder> getById(Guid Id) {
            var route =await  _context.CAT_Seguimiento_Ruta.Include(x => x.Estudios).ThenInclude(x=>x.Solicitud.Sucursal).AsQueryable().FirstOrDefaultAsync(x=>x.Id==Id);
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

            var routeTrackingList = _context.CAT_Seguimiento_Ruta.Include(x => x.Estudios).ThenInclude(x => x.Solicitud.Sucursal).AsQueryable();

            if (search.Fecha != null)
            {
                routeTrackingList = routeTrackingList.
                    Where(x => x.FechaCreo.Date == search.Fecha.Date );
            }

            if (search.Sucursal!=null && search.Sucursal.Count >0)
            {
                routeTrackingList = routeTrackingList.Where(x => search.Sucursal.Contains(x.SucursalOrigenId) && x.SucursalDestinoId==search.Sucursaldest);
            }

            if (!string.IsNullOrEmpty(search.Busqueda))
            {
                routeTrackingList = routeTrackingList.Where(x => search.Busqueda.Contains(x.Clave));
            }
            return await routeTrackingList.ToListAsync();
        }

        public async Task<RouteTracking> GetTracking(Guid Id) {
            var routeTracking =  _context.Cat_PendientesDeEnviar.Include(x => x.Solicitud.Sucursal).FirstOrDefault(x=>x.SegumientoId==Id);
            return routeTracking;
        
        }

    }
}
