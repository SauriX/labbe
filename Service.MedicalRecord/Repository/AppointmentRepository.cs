using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Service.MedicalRecord.Context;
using Service.MedicalRecord.Domain.Appointments;
using Service.MedicalRecord.Domain.PriceQuote;
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
        public async Task<List<AppointmentLab>> GetAllLab()
        {
            var citasLab = _context.CAT_Cita_Lab.AsQueryable();

            return await citasLab.ToListAsync();
        }
        public async Task<List<AppointmentDom>> GetAllDom()
        {
            var citasDom = _context.CAT_Cita_Dom.AsQueryable();

            return await citasDom.ToListAsync();
        }
        public async Task CreateLab(AppointmentLab appointmentLab) {
            var estudios = appointmentLab.Estudios.ToList();
            appointmentLab.Estudios = null;
            _context.CAT_Cita_Lab.Add(appointmentLab);
            await _context.SaveChangesAsync();
            var config = new BulkConfig();
            config.SetSynchronizeFilter<CotizacionStudy>(x => x.CotizacionId == appointmentLab.Id);
            estudios.ForEach(x => x.CotizacionId = appointmentLab.Id);
            estudios.ForEach(x => x.id = Guid.NewGuid());
            await _context.BulkInsertOrUpdateOrDeleteAsync(estudios, config);
        }

        public async Task CreateDom(AppointmentDom appointmentDom)
        {

            var estudios = appointmentDom.Estudios.ToList();
            appointmentDom.Estudios = null;
            _context.CAT_Cita_Dom.Add(appointmentDom);
            await _context.SaveChangesAsync();
            var config = new BulkConfig();
            config.SetSynchronizeFilter<CotizacionStudy>(x => x.CotizacionId == appointmentDom.Id);
            estudios.ForEach(x => x.CotizacionId = appointmentDom.Id);
            estudios.ForEach(x => x.id = Guid.NewGuid());
            await _context.BulkInsertOrUpdateOrDeleteAsync(estudios, config);

        }

        public async Task UpdateLab(AppointmentLab appointmentLab) {
            var estudios = appointmentLab.Estudios.ToList();
            appointmentLab.Estudios = null;
            _context.CAT_Cita_Lab.Update(appointmentLab);
            await _context.SaveChangesAsync();

            var config = new BulkConfig();
            config.SetSynchronizeFilter<CotizacionStudy>(x => x.CotizacionId == appointmentLab.Id);
            estudios.ForEach(x => x.CotizacionId = appointmentLab.Id);
            estudios.ForEach(x => x.id = Guid.NewGuid());
            await _context.BulkInsertOrUpdateOrDeleteAsync(estudios, config);
        }

        public async Task UpdateDom(AppointmentDom appointmentDom)
        {

            var estudios = appointmentDom.Estudios.ToList();
            appointmentDom.Estudios = null;
            _context.CAT_Cita_Dom.Update(appointmentDom);
            await _context.SaveChangesAsync();
            var config = new BulkConfig();
            config.SetSynchronizeFilter<CotizacionStudy>(x => x.CotizacionId == appointmentDom.Id);
            estudios.ForEach(x => x.CotizacionId = appointmentDom.Id);
            estudios.ForEach(x => x.id = Guid.NewGuid());
            await _context.BulkInsertOrUpdateOrDeleteAsync(estudios, config);

        } 
    }
}
