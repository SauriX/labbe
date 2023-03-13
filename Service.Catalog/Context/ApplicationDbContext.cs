using Microsoft.EntityFrameworkCore;
using Service.Catalog.Domain.Branch;
using Service.Catalog.Domain.Catalog;
using Service.Catalog.Domain.Company;
using Service.Catalog.Domain.Configuration;
using Service.Catalog.Domain.Constant;
using Service.Catalog.Domain.Indication;
using Service.Catalog.Domain.Loyalty;
using Service.Catalog.Domain.Maquila;
using Service.Catalog.Domain.Medics;
using Service.Catalog.Domain.Packet;
using Service.Catalog.Domain.Parameter;
using Service.Catalog.Domain.Price;
using Service.Catalog.Domain.Promotion;
using Service.Catalog.Domain.Provenance;
using Service.Catalog.Domain.Reagent;
using Service.Catalog.Domain.Route;
using Service.Catalog.Domain.Study;
using Service.Catalog.Domain.Tapon;
using Service.Catalog.Domain.Equipment;
using System.Reflection;
using Service.Catalog.Domain.EquipmentMantain;
using Service.Catalog.Domain.Series;

namespace Service.Catalog.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Configuration> CAT_Configuracion { get; set; }
        public DbSet<TaxConfiguration> CAT_Configuracion_Fiscal { get; set; }
        public DbSet<Area> CAT_Area { get; set; }
        public DbSet<InvoiceConcepts> CAT_Conceptos_Factura { get; set; }
        public DbSet<Budget> CAT_Presupuestos { get; set; }
        public DbSet<BudgetBranch> Relacion_Presupuesto_Sucursal { get; set; }
        public DbSet<Bank> CAT_Banco { get; set; }
        public DbSet<Clinic> CAT_Clinica { get; set; }
        public DbSet<Delivery> CAT_Paqueteria { get; set; }
        public DbSet<Department> CAT_Departamento { get; set; }
        public DbSet<Dimension> CAT_Dimension { get; set; }
        public DbSet<Field> CAT_Especialidad { get; set; }
        public DbSet<Indicator> CAT_Indicador { get; set; }
        public DbSet<Method> CAT_Metodo { get; set; }
        public DbSet<Payment> CAT_FormaPago { get; set; }
        public DbSet<PaymentMethod> CAT_MetodoPago { get; set; }
        public DbSet<SampleType> CAT_TipoMuestra { get; set; }
        public DbSet<UseOfCFDI> CAT_CFDI { get; set; }
        public DbSet<WorkList> CAT_ListaTrabajo { get; set; }
        public DbSet<Reagent> CAT_Reactivo_Contpaq { get; set; }
        public DbSet<Medics> CAT_Medicos { get; set; }
        public DbSet<Provenance> CAT_Procedencia { get; set; }
        public DbSet<Contact> CAT_Contacto { get; set; }
        public DbSet<MedicClinic> CAT_Medicos_Clinica { get; set; }
        public DbSet<Indication> CAT_Indicacion { get; set; }
        public DbSet<State> CAT_Estado { get; set; }
        public DbSet<City> CAT_Ciudad { get; set; }
        public DbSet<Colony> CAT_Colonia { get; set; }
        public DbSet<Branch> CAT_Sucursal { get; set; }
        public DbSet<BranchDepartment> CAT_Sucursal_Departamento { get; set; }
        public DbSet<Study> CAT_Estudio { get; set; }
        public DbSet<Company> CAT_Compañia { get; set; }
        public DbSet<Contact> CAT_CompañiaContacto { get; set; }
        public DbSet<BranchStudy> Relacion_Estudio_Sucursal { get; set; }
        public DbSet<IndicationStudy> Relacion_Estudio_Indicacion { get; set; }
        public DbSet<ParameterStudy> Relacion_Estudio_Parametro { get; set; }
        public DbSet<Parameter> CAT_Parametro { get; set; }
        public DbSet<ParameterValue> CAT_Tipo_Valor { get; set; }
        //De aqui
        public DbSet<PriceList> CAT_ListaPrecio { get; set; }
        public DbSet<Price_Company> CAT_ListaP_Compañia { get; set; }
        public DbSet<Price_Promotion> CAT_ListaP_Promocion { get; set; }
        public DbSet<Price_Branch> CAT_ListaP_Sucursal { get; set; }
        public DbSet<Price_Medics> CAT_ListaP_Medicos { get; set; }
        public DbSet<PriceList_Packet> Relacion_ListaP_Paquete { get; set; }
        public DbSet<PriceList_Study> Relacion_ListaP_Estudio { get; set; }
        //public DbSet<LoyaltyPriceList> Relacion_Loyalty_PrecioLista { get; set; }
        //hasta aqui es zona de pruebas
        public DbSet<Maquila> CAT_Maquilador { get; set; }
        public DbSet<Tag> CAT_Etiqueta { get; set; }
        public DbSet<StudyTag> Relacion_Estudio_Etiqueta { get; set; }
        public DbSet<Packet> CAT_Paquete { get; set; }
        public DbSet<PacketStudy> Relacion_Estudio_Paquete { get; set; }
        public DbSet<Loyalty> CAT_Lealtad { get; set; }
        public DbSet<Promotion> CAT_Promocion { get; set; }
        public DbSet<PromotionBranch> Relacion_Promocion_Sucursal { get; set; }
        public DbSet<PromotionStudy> Relacion_Promocion_Estudio { get; set; }
        public DbSet<PromotionPack> Relacion_Promocion_Paquete { get; set; }
        public DbSet<PromotionMedic> Relacion_Promocion_medicos { get; set; }
        public DbSet<Route> CAT_Rutas { get; set; }
        public DbSet<Units> CAT_Units { get; set; }
        public DbSet<BranchFolioConfig> CAT_Sucursal_Folio { get; set; }
        public DbSet<Equipos> CAT_Equipos { get; set; }
        public DbSet<EquipmentBranch> Relacion_Equipo_Sucursal { get; set; }
        public DbSet<Mantain> CAT_Mantenimiento_Equipo { get; set; }
        public DbSet<MantainImages> CAT_Mantenimiento_Equipo_Images { get; set; }
        public DbSet<Serie> CAT_Serie { get; internal set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
