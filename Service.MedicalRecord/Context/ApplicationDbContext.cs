using Microsoft.EntityFrameworkCore;
using Service.MedicalRecord.Domain.Appointments;
using Service.MedicalRecord.Domain.MedicalRecord;
using Service.MedicalRecord.Domain.PriceQuote;
using Service.MedicalRecord.Domain.Request;
using Service.MedicalRecord.Domain.TaxData;
using System.Reflection;

namespace Service.MedicalRecord.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Domain.MedicalRecord.MedicalRecord> CAT_Expedientes { get; set; }
        public DbSet<TaxData> CAT_Datos_Fiscales { get; set; }
        public DbSet<MedicalRecordTaxData> Relacion_Expediente_Factura { get; set; }
        public DbSet<Request> CAT_Solicitud { get; set; }
        public DbSet<AppointmentLab> CAT_Cita_Lab { get; set; }
        public DbSet<AppointmentDom> CAT_Cita_Dom { get; set; }
        public DbSet<PriceQuote> CAT_Cotizaciones { get; set; }
        public DbSet<CotizacionStudy> cotizacionStudies { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
