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
                .OrderBy(x => x.FechaCreo)
                .AsQueryable();

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

            if (filter.Ciudad != null)
            {
                requests = requests.Where(x => x.Sucursal != null && x.Sucursal.Ciudad == filter.Ciudad);
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

            return last?.Serie;
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

        public Task<List<Request>> InvoiceCompanyFilter(InvoiceCompanyFilterDto filter)
        {
            var requests = _context.CAT_Solicitud
                .Include(x => x.Expediente)
                .Include(x => x.Compañia)
                .Include(x => x.Sucursal)
                .Include(x => x.FacturasCompañia)
                .Include(x => x.Estudios).ThenInclude(x => x.Estatus)
                .Include(x => x.Estudios).ThenInclude(x => x.Tapon)
                .Include(x => x.Pagos).ThenInclude(x => x.Estatus).OrderBy(x => x.FechaCreo)
                .OrderBy(x => x.FechaCreo)
                .Where(x => x.Procedencia == 2)
                .AsQueryable();

            if (filter.FechaInicial != null && filter.FechaFinal != null)
            {
                requests = requests.Where(x => ((DateTime)filter.FechaInicial).Date <= x.FechaCreo.Date && ((DateTime)filter.FechaFinal).Date >= x.FechaCreo.Date);
            }

            if (filter.Sucursales != null && filter.Sucursales.Any())
            {
                requests = requests.Where(x => filter.Sucursales.Contains(x.SucursalId));
            }

            if (filter.Companias != null && filter.Companias.Any())
            {
                requests = requests.Where(x => x.CompañiaId != null && filter.Companias.Contains((Guid)x.CompañiaId));
            }
            if (!string.IsNullOrWhiteSpace(filter.Buscar))
            {
                requests = requests.Where(x => x.Clave.ToLower().Contains(filter.Buscar)
                || x.ClavePatologica.ToLower().Contains(filter.Buscar)
                || (x.Expediente.NombrePaciente + " " + x.Expediente.PrimerApellido + " " + x.Expediente.SegundoApellido).ToLower().Contains(filter.Buscar));
            }
            if (filter.TipoFactura.Count() > 0)
            {
                if (filter.TipoFactura.Contains("facturadas"))
                {
                    requests = requests.Where(x => x.Pagos.FirstOrDefault().EstatusId != 3 && x.Pagos.Count() > 0);

                }
                if (filter.TipoFactura.Contains("noFacturadas"))
                {
                    requests = requests.Where(x => x.Pagos.Count() == 0);

                }
                if (filter.TipoFactura.Contains("canceladas"))
                {
                    requests = requests.Where(x => x.Pagos.FirstOrDefault().EstatusId == 3);

                }
            }
            return requests.ToListAsync();
        }

        public async Task CreateInvoiceCompanyData(InvoiceCompany invoiceCompnay, List<RequestInvoiceCompany> requestInvoiceCompany)
        {
            _context.Factura_Compania.Add(invoiceCompnay);

            await _context.SaveChangesAsync();

            var config = new BulkConfig() { SetOutputIdentity = true, PreserveInsertOrder = true };

            await _context.BulkInsertOrUpdateAsync(requestInvoiceCompany, config);


        }
    }
}
