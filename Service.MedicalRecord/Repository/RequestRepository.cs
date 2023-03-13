using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using MoreLinq;
using Service.MedicalRecord.Context;
using Service.MedicalRecord.Domain.Invoice;
using Service.MedicalRecord.Domain.Request;
using Service.MedicalRecord.Dtos.InvoiceCompany;
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
                .Include(x => x.Medico)
                .OrderBy(x => x.FechaCreo)
                .AsQueryable();

            if ((string.IsNullOrWhiteSpace(filter.Clave)) && (filter.Sucursales == null || !filter.Sucursales.Any()))
            {
                requests = requests.Where(x => filter.SucursalesId.Contains(x.SucursalId));
            }

            if (string.IsNullOrWhiteSpace(filter.Clave) && filter.TipoFecha != null && filter.TipoFecha == 1 && filter.FechaInicial != null && filter.FechaFinal != null)
            {
                requests = requests.Where(x => ((DateTime)filter.FechaInicial).Date <= x.FechaCreo.Date && ((DateTime)filter.FechaFinal).Date >= x.FechaCreo.Date);
            }

            if (string.IsNullOrWhiteSpace(filter.Clave) && filter.TipoFecha != null && filter.TipoFecha == 2 && filter.FechaInicial != null && filter.FechaFinal != null)
            {
                requests = requests.Where(x => x.Estudios.Any(x => ((DateTime)filter.FechaInicial).Date <= x.FechaEntrega.Date && ((DateTime)filter.FechaFinal).Date >= x.FechaEntrega.Date));
            }

            if (!string.IsNullOrWhiteSpace(filter.Clave))
            {
                requests = requests.Where(x => x.Clave.Contains(filter.Clave)
                || x.ClavePatologica.ToLower().Contains(filter.Clave.ToLower())
                || (x.Expediente.NombrePaciente + " " + x.Expediente.PrimerApellido + " " + x.Expediente.SegundoApellido).ToLower().Contains(filter.Clave));
            }

            if (filter.Ciudad != null && filter.Ciudad.Any())
            {
                requests = requests.Where(x => x.Sucursal != null && filter.Ciudad.Contains(x.Sucursal.Ciudad));
            }

            if (filter.Sucursales != null && filter.Sucursales.Any())
            {
                requests = requests.Where(x => filter.Sucursales.Contains(x.SucursalId));
            }

            if (filter.Compañias != null && filter.Compañias.Any())
            {
                requests = requests.Where(x => x.CompañiaId != null && filter.Compañias.Contains((Guid)x.CompañiaId));
            }

            if (filter.Medicos != null && filter.Medicos.Any())
            {
                requests = requests.Where(x => x.MedicoId != null && filter.Medicos.Contains((Guid)x.MedicoId));
            }

            if (filter.Procedencias != null && filter.Procedencias.Any())
            {
                requests = requests.Where(x => filter.Procedencias.Contains(x.Procedencia));
            }

            if (filter.Urgencias != null && filter.Urgencias.Any())
            {
                requests = requests.Where(x => filter.Urgencias.Contains(x.Urgencia));
            }

            if (filter.Estatus != null && filter.Estatus.Any())
            {
                requests = requests.Where(x => x.Estudios.Any(y => filter.Estatus.Contains(y.EstatusId)));
            }

            if (filter.Departamentos != null && filter.Departamentos.Any())
            {
                requests = requests.Where(x => x.Estudios.Any(y => filter.Departamentos.Contains(y.DepartamentoId)));
            }

            if (filter.Expediente != null)
            {
                requests = requests.Where(x => x.Expediente.Expediente == filter.Expediente);
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
                .OrderByDescending(x => x.FechaCreo)
                .FirstOrDefaultAsync(x => x.SucursalId == branchId && x.Clave.StartsWith(date));

            return lastRequest?.Clave;
        }

        public async Task<string> GetLastTagCode(string date)
        {
            var lastTag = await _context.Relacion_Solicitud_Etiquetas
                .Include(x => x.Solicitud)
                //.OrderByDescending(x => x.Fecha)
                //.FirstOrDefaultAsync(x => x.Clave.Contains(date));
                .FirstOrDefaultAsync();

            return lastTag?.ClaveEtiqueta;
        }

        public async Task<List<RequestTag>> GetTags(Guid requestId)
        {
            var tags = await _context.Relacion_Solicitud_Etiquetas
                .Include(x => x.Estudios)
                .Where(x => x.SolicitudId == requestId)
                .ToListAsync();

            return tags;
        }

        public async Task<string> GetLastPathologicalCode(Guid branchId, string date, string type)
        {
            var lastRequest = await _context.CAT_Solicitud
                .OrderBy(x => x.FechaCreo)
                .Where(x => x.SucursalId == branchId)
                .ToListAsync();

            var last = lastRequest
                .LastOrDefault(x => x.ClavePatologica != null
                && x.ClavePatologica.Contains(type)
                && x.ClavePatologica.Split(",").All(y => y.EndsWith(date)));

            if (last == null) return null;

            var code = last.ClavePatologica.Split(",").FirstOrDefault(x => x.Contains(type));

            return code;
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
                .Include(x => x.EstudioWeeClinic)
                .Where(x => x.SolicitudId == requestId && studiesIds.Contains(x.Id))
                .ToListAsync();

            return studies;
        }

        public async Task<List<RequestStudy>> GetAllStudies(Guid requestId)
        {
            var studies = await _context.Relacion_Solicitud_Estudio
                .Include(x => x.Paquete)
                .Include(x => x.Tapon)
                .Include(x => x.Estatus)
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
                .Include(x => x.EstudioWeeClinic)
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

        public async Task<RequestImage> GetImage(Guid requestId, string code)
        {
            var image = await _context.Relacion_Solicitud_Imagen.FirstOrDefaultAsync(x => x.SolicitudId == requestId && x.Clave == code);

            return image;
        }

        public async Task<List<RequestImage>> GetImages(Guid requestId)
        {
            var images = await _context.Relacion_Solicitud_Imagen.Where(x => x.SolicitudId == requestId).ToListAsync();

            return images;
        }

        public async Task<List<RequestPayment>> GetPayments(Guid requestId)
        {
            var payments = await _context.Relacion_Solicitud_Pago
                .Where(x => x.SolicitudId == requestId)
                .OrderBy(x => x.FechaCreo)
                .ToListAsync();

            return payments;
        }

        public async Task<string> GetLastPaymentCode(string serie, string year)
        {
            var last = await _context.CAT_Solicitud
                .Where(x => x.Serie == serie && x.SerieNumero.StartsWith(year))
                .OrderBy(x => Convert.ToInt32(x.SerieNumero ?? "0"))
                .LastOrDefaultAsync();

            return last?.SerieNumero;
        }

        public async Task Create(Request request)
        {
            _context.CAT_Solicitud.Add(request);

            await _context.SaveChangesAsync();
        }

        public async Task CreatePayment(RequestPayment request)
        {
            _context.Relacion_Solicitud_Pago.Add(request);

            await _context.SaveChangesAsync();
        }

        public async Task Update(Request request)
        {
            _context.CAT_Solicitud.Update(request);

            await _context.SaveChangesAsync();

            _context.ChangeTracker.Clear();
        }

        public async Task Delete(Request request)
        {
            _context.CAT_Solicitud.Remove(request);

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

        public async Task UpdatePayment(RequestPayment payment)
        {
            _context.Relacion_Solicitud_Pago.Update(payment);

            await _context.SaveChangesAsync();
        }

        public async Task BulkInsertUpdatePacks(Guid requestId, List<RequestPack> packs)
        {
            var config = new BulkConfig();
            config.SetSynchronizeFilter<RequestPack>(x => x.SolicitudId == requestId);

            await _context.BulkInsertOrUpdateAsync(packs, config);
        }

        public async Task BulkUpdateDeletePacks(Guid requestId, List<RequestPack> packs)
        {
            var config = new BulkConfig();
            config.SetSynchronizeFilter<RequestPack>(x => x.SolicitudId == requestId);
            config.SetOutputIdentity = true;

            await _context.BulkInsertOrUpdateOrDeleteAsync(packs, config);
        }

        public async Task BulkInsertUpdateStudies(Guid requestId, List<RequestStudy> studies)
        {
            var config = new BulkConfig();
            config.SetSynchronizeFilter<RequestStudy>(x => x.SolicitudId == requestId);

            await _context.BulkInsertOrUpdateAsync(studies, config);
        }

        public async Task BulkInsertUpdateDeleteTags(Guid requestId, List<RequestTag> tags)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var config = new BulkConfig();
                config.SetSynchronizeFilter<RequestTag>(x => x.SolicitudId == requestId);
                config.SetOutputIdentity = true;

                var tagsToCreate = tags.Where(x => x.Id == 0).ToList();
                await _context.BulkInsertAsync(tagsToCreate, config);

                config.SetOutputIdentity = false;
                await _context.BulkInsertOrUpdateOrDeleteAsync(tags, config);

                var configStudies = new BulkConfig();
                configStudies.SetSynchronizeFilter<RequestTagStudy>(x => tags.Select(t => t.Id).Contains(x.SolicitudEtiquetaId));
                configStudies.SetOutputIdentity = true;

                var studies = tags.SelectMany(t => t.Estudios.Select(x => { x.SolicitudEtiquetaId = t.Id; return x; })).ToList();
                var studiesToCreate = studies.Where(x => x.Id == 0).ToList();
                await _context.BulkInsertAsync(studiesToCreate, configStudies);

                configStudies.SetOutputIdentity = false;
                await _context.BulkInsertOrUpdateOrDeleteAsync(studies, configStudies);

                //foreach (var tag in tags)
                //{
                //    tag.Estudios = studies.Where(x => x.SolicitudEtiquetaId == tag.Id).ToList();
                //}

                transaction.Commit();
            }
            catch (System.Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task BulkUpdatePayments(Guid requestId, List<RequestPayment> payments)
        {
            var config = new BulkConfig();
            config.SetSynchronizeFilter<RequestStudy>(x => x.SolicitudId == requestId);

            await _context.BulkUpdateAsync(payments, config);
        }

        public async Task BulkUpdateDeleteStudies(Guid requestId, List<RequestStudy> studies)
        {
            var config = new BulkConfig();
            config.SetSynchronizeFilter<RequestStudy>(x => x.SolicitudId == requestId);
            config.PropertiesToExclude = new List<string>
            {
                nameof(RequestStudy.FechaTomaMuestra), nameof(RequestStudy.UsuarioTomaMuestra),
                nameof(RequestStudy.FechaSolicitado), nameof(RequestStudy.UsuarioSolicitado),
                nameof(RequestStudy.FechaCaptura), nameof(RequestStudy.UsuarioCaptura),
                nameof(RequestStudy.FechaValidacion), nameof(RequestStudy.UsuarioValidacion),
                nameof(RequestStudy.FechaLiberado), nameof(RequestStudy.UsuarioLiberado),
                nameof(RequestStudy.FechaEnviado), nameof(RequestStudy.UsuarioEnviado),
            };
            config.SetOutputIdentity = true;

            await _context.BulkInsertOrUpdateOrDeleteAsync(studies, config);
        }

        public async Task BulkUpdateWeeStudies(Guid requestId, List<RequestStudyWee> studies)
        {
            await _context.BulkUpdateAsync(studies);
        }

        public async Task DeleteImage(Guid requestId, string code)
        {
            var image = await _context.Relacion_Solicitud_Imagen.FirstOrDefaultAsync(x => x.SolicitudId == requestId && x.Clave == code);

            if (image != null)
            {
                _context.Relacion_Solicitud_Imagen.Remove(image);

                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Domain.Request.RequestStudy>> GetRequestsStudyByListId(List<Guid> solicitudesId)
        {

            return await _context.Relacion_Solicitud_Estudio
                .Where(x => solicitudesId.Contains(x.SolicitudId))
                .ToListAsync();

        }
        public async Task<List<Domain.Request.Request>> GetRequestsByListId(List<Guid> solicitudesId)
        {

            return await _context.CAT_Solicitud
                .Where(x => solicitudesId.Contains(x.Id))
                .ToListAsync();

        }
    }
}
