using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Service.MedicalRecord.Context;
using Service.MedicalRecord.Domain.Appointments;
using Service.MedicalRecord.Domain.Quotation;
using Service.MedicalRecord.Dtos.Appointment;
using Service.MedicalRecord.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Repository
{
    public class AppointmentRepository: IAppointmentResposiotry
    {
        private readonly ApplicationDbContext _context;

        public AppointmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<List<AppointmentLab>> GetAllLab(SearchAppointment search)
        {
            var citasLab = _context.CAT_Cita_Lab.Include(x => x.Expediente).Include(x=> x.Estudios).AsQueryable();
            if (!string.IsNullOrEmpty(search.nombre) || search.fecha.Length > 0)
            {
                citasLab = citasLab.Where(x => x.NombrePaciente.Contains(search.nombre) || x.FechaCita.Date <= search.fecha[1].Date && x.FechaCreo.Date >= search.fecha[0].Date);
            }

            if (search.expediente != null || !string.IsNullOrEmpty(search.expediente)) {
                citasLab = citasLab.Where(x=> x.Expediente.Expediente==search.expediente);
            }
            return await citasLab.ToListAsync();
        }
        public async Task<List<AppointmentDom>> GetAllDom(SearchAppointment search)
        {
            var citasDom = _context.CAT_Cita_Dom.Include(x=>x.Expediente).AsQueryable();
            if (!string.IsNullOrEmpty(search.nombre) || search.fecha.Length > 0)
            {
                citasDom = citasDom.Where(x => x.NombrePaciente.Contains(search.nombre) || x.FechaCita.Date <= search.fecha[1].Date && x.FechaCreo.Date >= search.fecha[0].Date);
            }
            if (search.expediente != null || !string.IsNullOrEmpty(search.expediente))
            {
                citasDom = citasDom.Where(x => x.Expediente.Expediente == search.expediente);
            }
            return await citasDom.ToListAsync();
        }

        public async Task<AppointmentLab> GetByIdLab(string id) {

            var citas= await _context.CAT_Cita_Lab.Include(x=>x.Expediente).FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));
            var estudios = await _context.Relacion_Cotizacion_Estudio.Where(x => x.CotizacionId == citas.Id).ToListAsync();



            citas.Estudios = estudios;

            return citas;
        }
        public async Task<AppointmentDom> GetByIdDom(string id)
        {

            var citas = await _context.CAT_Cita_Dom.Include(x => x.Expediente).FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));
            var estudios =await  _context.Relacion_Cotizacion_Estudio.Where(x => x.CotizacionId == citas.Id).ToListAsync();
            citas.Estudios = estudios;
            return citas;
        }
        public async Task CreateLab(AppointmentLab appointmentLab) {
            var estudios = appointmentLab.Estudios.ToList();
            appointmentLab.Estudios = null;
            _context.CAT_Cita_Lab.Add(appointmentLab);
            await _context.SaveChangesAsync();
            var config = new BulkConfig();
            config.SetSynchronizeFilter<QuotationStudy>(x => x.CotizacionId == appointmentLab.Id);
            estudios.ForEach(x => x.CotizacionId = appointmentLab.Id);
            await _context.BulkInsertOrUpdateOrDeleteAsync(estudios, config);
        }

        public async Task CreateDom(AppointmentDom appointmentDom)
        {

            var estudios = appointmentDom.Estudios.ToList();
            appointmentDom.Estudios = null;
            _context.CAT_Cita_Dom.Add(appointmentDom);
            await _context.SaveChangesAsync();
            var config = new BulkConfig();
            config.SetSynchronizeFilter<QuotationStudy>(x => x.CotizacionId == appointmentDom.Id);
            estudios.ForEach(x => x.CotizacionId = appointmentDom.Id);
            await _context.BulkInsertOrUpdateOrDeleteAsync(estudios, config);

        }

        public async Task UpdateLab(AppointmentLab appointmentLab) {
            var estudios = appointmentLab.Estudios.ToList();
            appointmentLab.Estudios = null;
            _context.CAT_Cita_Lab.Update(appointmentLab);
            await _context.SaveChangesAsync();

            var config = new BulkConfig();
            config.SetSynchronizeFilter<QuotationStudy>(x => x.CotizacionId == appointmentLab.Id);
            estudios.ForEach(x => x.CotizacionId = appointmentLab.Id);
            await _context.BulkInsertOrUpdateOrDeleteAsync(estudios, config);
        }

        public async Task UpdateDom(AppointmentDom appointmentDom)
        {

            var estudios = appointmentDom.Estudios.ToList();
            appointmentDom.Estudios = null;
            _context.CAT_Cita_Dom.Update(appointmentDom);
            await _context.SaveChangesAsync();
            var config = new BulkConfig();
            config.SetSynchronizeFilter<QuotationStudy>(x => x.CotizacionId == appointmentDom.Id);
            estudios.ForEach(x => x.CotizacionId = appointmentDom.Id);
            await _context.BulkInsertOrUpdateOrDeleteAsync(estudios, config);

        }

        public async Task<string> GetLastCode(string date)
        {
            var lastRequest = await _context.CAT_Cotizacion
                .OrderByDescending(x => x.FechaCreo)
                .FirstOrDefaultAsync(x => x.Clave.StartsWith(date));

            return lastRequest?.Clave;
        }
    }
}
