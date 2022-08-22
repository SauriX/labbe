﻿using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Service.MedicalRecord.Context;
using Service.MedicalRecord.Domain.Request;
using Service.MedicalRecord.Dtos.Request;
using Service.MedicalRecord.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Repository
{
    public class RequestRepository : IRequestRepository
    {
        private readonly ApplicationDbContext _context;

        public RequestRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Request> FindAsync(Guid id)
        {
            var request = await _context.CAT_Solicitud.FindAsync(id);

            return request;
        }

        public async Task<List<Request>> GetByFilter(RequestFilterDto filter)
        {
            var requests = _context.CAT_Solicitud
                .Include(x => x.Expediente)
                .Include(x => x.Compañia)
                .Include(x => x.Sucursal)
                .Include(x => x.Estudios).ThenInclude(x => x.Estatus)
                .Include(x => x.Estudios).ThenInclude(x => x.Tapon)
                .AsQueryable();

            if (filter.TipoFecha == 1)
            {
                requests = requests.Where(x => filter.FechaInicial.Date <= x.FechaCreo.Date && filter.FechaFinal.Date >= x.FechaCreo.Date);
            }

            if (filter.TipoFecha == 2)
            {
                requests = requests.Where(x => x.Estudios.Any(x => filter.FechaInicial.Date <= x.FechaEntrega && filter.FechaFinal.Date >= x.FechaEntrega));
            }

            if (!string.IsNullOrWhiteSpace(filter.Clave))
            {
                requests = requests.Where(x => string.Equals(x.Clave, filter.Clave, StringComparison.CurrentCultureIgnoreCase)
                || string.Equals(x.ClavePatologica, filter.Clave, StringComparison.CurrentCultureIgnoreCase));
            }

            if (filter.Sucursales.Any())
            {
                requests = requests.Where(x => filter.Sucursales.Contains(x.SucursalId));
            }

            if (filter.Compañias.Any())
            {
                requests = requests.Where(x => x.CompañiaId != null && filter.Compañias.Contains((Guid)x.CompañiaId));
            }

            if (filter.Medicos.Any())
            {
                requests = requests.Where(x => x.MedicoId != null && filter.Medicos.Contains((Guid)x.MedicoId));
            }

            if (filter.Procedencias.Any())
            {
                requests = requests.Where(x => filter.Procedencias.Contains(x.Procedencia));
            }

            if (filter.Urgencias.Any())
            {
                requests = requests.Where(x => filter.Urgencias.Contains(x.Urgencia));
            }

            if (filter.Estatus.Any())
            {
                requests = requests.Where(x => x.Estudios.Any(y => filter.Estatus.Contains(y.EstatusId)));
            }

            if (filter.Departamentos.Any())
            {
                requests = requests.Where(x => x.Estudios.Any(y => filter.Departamentos.Contains(y.DepartamentoId)));
            }

            return await requests.ToListAsync();
        }

        public async Task<Request> GetById(Guid id)
        {
            var request = await _context.CAT_Solicitud
                .Include(x => x.Expediente)
                .Include(x => x.Sucursal)
                .Include(x => x.Compañia)
                .Include(x => x.Medico)
                .Include(x => x.Estudios.Where(x => x.PaqueteId == null))
                .Include(x => x.Paquetes).ThenInclude(x => x.Estudios)
                .FirstOrDefaultAsync(x => x.Id == id);

            return request;
        }

        public async Task<string> GetLastCode(Guid branchId, string date)
        {
            var lastRequest = await _context.CAT_Solicitud
                .OrderBy(x => x.FechaCreo)
                .LastOrDefaultAsync(x => x.SucursalId == branchId && x.Clave.EndsWith(date));

            return lastRequest?.Clave;
        }

        public async Task<RequestStudy> GetStudyById(Guid requestId, int studyId)
        {
            var study = await _context.Relacion_Solicitud_Estudio
                .FirstOrDefaultAsync(x => x.SolicitudId == requestId && x.EstudioId == studyId);

            return study;
        }

        public async Task<List<RequestStudy>> GetStudyById(Guid requestId, IEnumerable<int> studiesIds)
        {
            var studies = await _context.Relacion_Solicitud_Estudio
                .Where(x => x.SolicitudId == requestId && studiesIds.Contains(x.EstudioId))
                .ToListAsync();

            return studies;
        }

        public async Task<List<RequestStudy>> GetAllStudies(Guid requestId)
        {
            var studies = await _context.Relacion_Solicitud_Estudio
                .Include(x => x.Paquete)
                .Where(x => x.SolicitudId == requestId)
                .ToListAsync();

            return studies;
        }

        public async Task<List<RequestStudy>> GetStudiesByRequest(Guid requestId)
        {
            var studies = await _context.Relacion_Solicitud_Estudio
                .Include(x => x.Paquete)
                .Include(x => x.Tapon)
                .Include(x => x.Estatus)
                .Where(x => x.SolicitudId == requestId && x.PaqueteId == null)
                .ToListAsync();

            return studies;
        }

        public async Task<List<RequestPack>> GetPacksByRequest(Guid requestId)
        {
            var studies = await _context.Relacion_Solicitud_Paquete
                .Include(x => x.Estudios).ThenInclude(x => x.Tapon)
                .Include(x => x.Estudios).ThenInclude(x => x.Estatus)
                .Where(x => x.SolicitudId == requestId)
                .ToListAsync();

            return studies;
        }

        public async Task<RequestImage> GetImage(Guid requestId, int imageId)
        {
            var image = await _context.Relacion_Solicitud_Imagen.FirstOrDefaultAsync(x => x.SolicitudId == requestId && x.Id == imageId);

            return image;
        }

        public async Task Create(Request request)
        {
            _context.CAT_Solicitud.Add(request);

            await _context.SaveChangesAsync();
        }

        public async Task Update(Request request)
        {
            _context.CAT_Solicitud.Update(request);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateImage(RequestImage requestImage)
        {
            if (requestImage.Id == 0)
            {
                _context.Relacion_Solicitud_Imagen.Add(requestImage);
            }
            else
            {
                _context.Relacion_Solicitud_Imagen.Update(requestImage);
            }

            await _context.SaveChangesAsync();
        }

        public async Task UpdateStudy(RequestStudy study)
        {
            _context.Relacion_Solicitud_Estudio.Update(study);

            await _context.SaveChangesAsync();
        }

        public async Task BulkUpdatePacks(Guid requestId, List<RequestPack> packs)
        {
            var config = new BulkConfig();
            config.SetSynchronizeFilter<RequestPack>(x => x.SolicitudId == requestId);

            await _context.BulkInsertOrUpdateAsync(packs, config);
        }

        public async Task BulkUpdateDeletePacks(Guid requestId, List<RequestPack> packs)
        {
            var config = new BulkConfig();
            config.SetSynchronizeFilter<RequestPack>(x => x.SolicitudId == requestId);

            await _context.BulkInsertOrUpdateOrDeleteAsync(packs, config);
        }

        public async Task BulkUpdateStudies(Guid requestId, List<RequestStudy> studies)
        {
            var config = new BulkConfig();
            config.SetSynchronizeFilter<RequestStudy>(x => x.SolicitudId == requestId);

            await _context.BulkInsertOrUpdateAsync(studies, config);
        }

        public async Task BulkUpdateDeleteStudies(Guid requestId, List<RequestStudy> studies)
        {
            var config = new BulkConfig();
            config.SetSynchronizeFilter<RequestStudy>(x => x.SolicitudId == requestId);

            await _context.BulkInsertOrUpdateOrDeleteAsync(studies, config);
        }
    }
}
