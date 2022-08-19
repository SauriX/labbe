using Microsoft.EntityFrameworkCore;
using Service.MedicalRecord.Context;
using Service.MedicalRecord.Dictionary;
using Service.MedicalRecord.Domain.Request;
using Service.MedicalRecord.Dtos.Sampling;
using Service.MedicalRecord.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Repository
{
    public class SamplingRepository:ISamplingRepository
    {
        private readonly ApplicationDbContext _context;

        public SamplingRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Request>> GetAll(SamplingSearchDto search)
        {
            var citasLab = _context.CAT_Solicitud.Include(x=>x.Estudios).Include(x=>x.Expediente).Include(x=>x.Sucursal).Include(x=>x.Compañia).AsQueryable();
            if (search.Sucursal != null && search.Sucursal.Count > 0)
            {
                citasLab = citasLab.Where(x => search.Sucursal.Contains(x.SucursalId.ToString()));
                
            }
             if (!string.IsNullOrEmpty(search.Buscar)) {
                 citasLab = citasLab.Where(x => x.Clave.Contains(search.Buscar)|| x.Expediente.NombreCompleto.Contains(search.Buscar));
             }
            if (search.Status != null && search.Status.Count > 0)
            {
                citasLab = citasLab.Where(x => x.Estudios.Any(y=> search.Status.Contains(y.EstatusId)));

            }
            if (search.Fecha != null && search.Fecha.Count() > 0 && search.Fecha[0].Date != DateTime.Now.Date && search.Fecha[1].Date != DateTime.Now.Date)
            {
                citasLab = citasLab.Where(x => x.FechaCreo.Date > search.Fecha[0].Date && x.FechaCreo.Date < search.Fecha[1].Date);

            }
            if (search.Procedencia != null && search.Procedencia.Count > 0)
            {
                citasLab = citasLab.Where(x => search.Procedencia.Contains(x.Procedencia));

            }
             if (search.Departamento != null && search.Departamento.Count > 0)
             {
                 citasLab = citasLab.Where(x => x.Estudios.Any(y=>search.Departamento.Contains(y.DepartamentoId)));

             }
            if (search.Ciudad != null && search.Ciudad.Count > 0)
            {
                citasLab = citasLab.Where(x => search.Ciudad.Contains(x.Expediente.Expediente.ToString()));

            }
            if (search.TipoSolicitud!= null && search.TipoSolicitud.Count > 0)
            {
                citasLab = citasLab.Where(x => search.TipoSolicitud.Contains(x.Urgencia));

            }

            if (search.Area!= null && search.Area.Count > 0)
            {
                citasLab = citasLab.Where(x => x.Estudios.Any(y => search.Area.Contains(y.AreaId)));

            }
            if (search.Medico != null && search.Medico.Count > 0)
            {
                citasLab = citasLab.Where(x => search.Medico.Contains(x.MedicoId.ToString()));

            }
            if (search.Compañia != null && search.Compañia.Count > 0)
            {
                citasLab = citasLab.Where(x => search.Compañia.Contains(x.CompañiaId.ToString()));

            }
            return await citasLab.ToListAsync();
        }


        public async Task UpdateStatus(UpdateDto dates) {
            var data = dates.Id.ToArray();

            foreach(var id in data){
                var studio = await _context.Relacion_Solicitud_Estudio.FirstOrDefaultAsync(x => x.EstudioId==id);
                if (studio.EstatusId == 1)
                {
                    studio.EstatusId = Status.RequestStudy.TomaDeMuestra;
                }
                else {
                    if (studio.EstatusId == 2)
                    {
                        studio.EstatusId = Status.RequestStudy.Pendiente;
                    }
                }

                _context.Update(studio);
                await _context.SaveChangesAsync();
            }
        
        }

    }
}
