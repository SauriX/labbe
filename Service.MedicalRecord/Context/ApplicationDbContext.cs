using Microsoft.EntityFrameworkCore;
using Service.MedicalRecord.Domain;
using Service.MedicalRecord.Domain.Appointments;
using Service.MedicalRecord.Domain.MedicalRecord;
using Service.MedicalRecord.Domain.PriceQuote;
using Service.MedicalRecord.Domain.Request;
using Service.MedicalRecord.Domain.RouteTracking;
using Service.MedicalRecord.Domain.TaxData;
using Service.MedicalRecord.Domain.TrackingOrder;
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
        public DbSet<RequestStudy> Relacion_Solicitud_Estudio { get; set; }
        public DbSet<ClinicResults> ClinicResults { get; set; }
        public DbSet<RequestPack> Relacion_Solicitud_Paquete { get; set; }
        public DbSet<RequestImage> Relacion_Solicitud_Imagen { get; set; }
        public DbSet<AppointmentLab> CAT_Cita_Lab { get; set; }
        public DbSet<AppointmentDom> CAT_Cita_Dom { get; set; }
        public DbSet<PriceQuote> CAT_Cotizaciones { get; set; }
        public DbSet<CotizacionStudy> cotizacionStudies { get; set; }
        public DbSet<RequestStatus> Estatus_Solicitud { get; set; }
        public DbSet<RequestStudyStatus> Estatus_Solicitud_Estudio { get; set; }
        public DbSet<TrackingOrder> CAT_Seguimiento_Ruta { get; set; }
        public DbSet<TrackingOrderDetail> Relacion_Seguimiento_Solicitud { get; set; }
        public DbSet<RouteTracking> Cat_PendientesDeEnviar { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
