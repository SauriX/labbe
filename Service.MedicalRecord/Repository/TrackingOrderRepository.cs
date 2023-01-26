﻿using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Service.MedicalRecord.Context;
using Service.MedicalRecord.Dictionary;
using Service.MedicalRecord.Domain.TrackingOrder;
using Service.MedicalRecord.Dtos.TrackingOrder;
using Service.MedicalRecord.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Service.MedicalRecord.Dictionary.Status;
//using Service.MedicalRecord.Domain.Request;

namespace Service.MedicalRecord.Repository
{
    public class TrackingOrderRepository : ITrackingOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public TrackingOrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Update(TrackingOrder order)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {

                var estudios = order.Estudios.ToList();

                order.Estudios = null;

                _context.CAT_Seguimiento_Ruta.Update(order);

                await _context.SaveChangesAsync();

                estudios.ForEach(estudio =>
                {
                    estudio.SeguimientoId = order.Id;
                    estudio.Id = Guid.NewGuid();
                });

                var config = new BulkConfig();

                config.SetSynchronizeFilter<TrackingOrderDetail>(x => x.SeguimientoId == order.Id);

                await _context.BulkInsertOrUpdateOrDeleteAsync(estudios, config);

                transaction.Commit();

            }
            catch (System.Exception)
            {
                transaction.Rollback();
                throw;
            }

        }
        public async Task Create(TrackingOrder order)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var numberClave = _context.CAT_Seguimiento_Ruta.Count();

                order.Clave = $"{order.Clave}{numberClave + 1}";


                var estudios = order.Estudios.ToList();

                order.Estudios = null;

                _context.CAT_Seguimiento_Ruta.Add(order);

                await _context.SaveChangesAsync();

                estudios.ForEach(estudio => { estudio.SeguimientoId = order.Id; estudio.Id = Guid.NewGuid(); });

                var config = new BulkConfig();

                config.SetSynchronizeFilter<TrackingOrderDetail>(x => x.SeguimientoId == order.Id);

                await _context.BulkInsertOrUpdateOrDeleteAsync(estudios, config);

                transaction.Commit();

            }
            catch (System.Exception)
            {
                transaction.Rollback();
                throw;
            }
        }
        public async Task<List<Domain.Request.RequestStudy>> FindStudiesRequest(string Solicitud) {
            var listaEstudio = _context.Relacion_Solicitud_Estudio
            .Include(x => x.Solicitud).ThenInclude(x => x.Expediente)
            .Include(x => x.Solicitud)
            .Include(x => x.Tapon)
            .Include(x=>x.Estatus)
           .AsQueryable();
            
            listaEstudio = listaEstudio.Where(x=>x.Solicitud.Clave == Solicitud && x.EstatusId == Status.RequestStudy.TomaDeMuestra);
            return listaEstudio.ToList()    ;
            
        }
        public async Task<List<Domain.Request.RequestStudy>> FindEstudios(List<int> estudios)
        {
            var listaEstudio = _context.Relacion_Solicitud_Estudio
                .Include(x => x.Solicitud).ThenInclude(x => x.Expediente)
                .Include(x => x.Solicitud)
                .Include(x => x.Tapon)
                .AsQueryable();
            var ordenes = _context.CAT_Seguimiento_Ruta.Include(x => x.Estudios);
            List<Domain.Request.RequestStudy> newlistestudios = new List<Domain.Request.RequestStudy>();

            foreach (var estudio in listaEstudio)
            {
                if (!ordenes.Any(x => x.Estudios.Any(y => y.EstudioId == estudio.EstudioId && y.SolicitudId == estudio.SolicitudId)))
                {

                    newlistestudios.Add(estudio);
                }
            }

            newlistestudios = newlistestudios.FindAll(x => estudios.Contains(x.EstudioId));
            newlistestudios = newlistestudios.FindAll(x => x.EstatusId == Status.RequestStudy.TomaDeMuestra);

            return newlistestudios;
        }
        public async Task<List<Domain.Request.RequestStudy>> FindAllEstudios(List<int> estudios,string solicitud)
        {
            var listaEstudio = _context.Relacion_Solicitud_Estudio
                .Include(x => x.Solicitud).ThenInclude(x => x.Expediente)
                .Include(x => x.Solicitud)
                .Include(x => x.Tapon)
                .AsQueryable();

            listaEstudio = listaEstudio.Where(x=>x.Solicitud.Clave == solicitud);
            var ordenes = _context.CAT_Seguimiento_Ruta.Include(x => x.Estudios);
            List<Domain.Request.RequestStudy> newlistestudios = new List<Domain.Request.RequestStudy>();

            foreach (var estudio in listaEstudio)
            {
                if (!ordenes.Any(x => x.Estudios.Any(y => y.EstudioId == estudio.EstudioId && y.SolicitudId == estudio.SolicitudId)))
                {

                    newlistestudios.Add(estudio);
                }
            }

            newlistestudios = newlistestudios.FindAll(x => estudios.Contains(x.EstudioId));
            newlistestudios = newlistestudios.FindAll(x => x.EstatusId == Status.RequestStudy.TomaDeMuestra);

            return newlistestudios;
        }
        public async Task<bool> ConfirmarRecoleccion(Guid seguimientoId)
        {
            var solicitudes = await _context.Relacion_Seguimiento_Solicitud.Include(x=>x.Solicitud.Estudios).Where(x => x.SeguimientoId == seguimientoId).ToListAsync();
            var order =  _context.CAT_Seguimiento_Ruta.Include(x=>x.Estudios).FirstOrDefault(x=>x.Id== seguimientoId);
            var lstSolicitudes = order.Estudios;
  
            foreach (var requesty in solicitudes) {

                var listaEstudio = requesty.Solicitud.Estudios.AsQueryable();

                listaEstudio = listaEstudio.Where(x => lstSolicitudes.Any(y => y.EstudioId == x.EstudioId && y.SolicitudId== requesty.SolicitudId));

                var estudiosEncontrados = listaEstudio.ToList();

                estudiosEncontrados.ForEach(x => x.EstatusId = Status.RequestStudy.EnRuta);

                var config = new BulkConfig();

                config.SetSynchronizeFilter<Domain.Request.RequestStudy>(x => x.SolicitudId == requesty.SolicitudId);

                await _context.BulkInsertOrUpdateAsync(estudiosEncontrados, config);


            }
            



            return true;
        }
        public async Task<string> GetLastCode(Guid branchId, string date)
        {
            var lastRequest = await _context.CAT_Expedientes
                .OrderByDescending(x => x.FechaCreo)
                .FirstOrDefaultAsync(x => x.IdSucursal == branchId && x.Expediente.StartsWith(date));

            return lastRequest?.Expediente;
        }
        public async Task<bool> CancelarRecoleccion(Guid seguimientoId)
        {
            var solicitudes = await _context.Relacion_Seguimiento_Solicitud.Include(x => x.Solicitud.Estudios).Where(x => x.SeguimientoId == seguimientoId).ToListAsync();
            var order = _context.CAT_Seguimiento_Ruta.Include(x => x.Estudios).FirstOrDefault(x => x.Id == seguimientoId);
            var lstSolicitudes = order.Estudios;

            foreach (var requesty in solicitudes)
            {

                var listaEstudio = requesty.Solicitud.Estudios.AsQueryable();

                listaEstudio = listaEstudio.Where(x => lstSolicitudes.Any(y => y.EstudioId == x.EstudioId && y.SolicitudId == requesty.SolicitudId));

                var estudiosEncontrados = listaEstudio.ToList();

                estudiosEncontrados.ForEach(x => x.EstatusId = Status.RequestStudy.TomaDeMuestra);

                var config = new BulkConfig();

                config.SetSynchronizeFilter<Domain.Request.RequestStudy>(x => x.SolicitudId == requesty.SolicitudId);

                await _context.BulkInsertOrUpdateAsync(estudiosEncontrados, config);


            }




            return true;
        }

        public async Task<TrackingOrder> FindAsync(Guid orderId)
        {
            return await _context.CAT_Seguimiento_Ruta
                .Where(x => x.Id == orderId)
                .Include(x => x.Estudios)
                .ThenInclude(x => x.SolicitudEstudio.Tapon)
                .Include(x => x.Estudios)
                .ThenInclude(x=>x.Solicitud.Expediente)
                .FirstOrDefaultAsync();
        }


    }
}
