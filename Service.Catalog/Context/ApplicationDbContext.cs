﻿using Identidad.Api.Model.Medicos;
using Identidad.Api.ViewModels.Menu;
using Microsoft.EntityFrameworkCore;
using Service.Catalog.Domain;
using Service.Catalog.Domain.Branch;
using Service.Catalog.Domain.Catalog;
using Service.Catalog.Domain.Company;
using Service.Catalog.Domain.Constant;
using Service.Catalog.Domain.Indication;
using Service.Catalog.Domain.Maquila;
using Service.Catalog.Domain.Parameter;
using Service.Catalog.Domain.Price;
using Service.Catalog.Domain.Provenance;
using Service.Catalog.Domain.Reagent;
using Service.Catalog.Domain.Study;
using Service.Catalog.Domain.Tapon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Service.Catalog.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Area> CAT_Area { get; set; }
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
        public DbSet<Parameter> CAT_Parametro { get; set; }
        public DbSet<ParameterValue> CAT_Tipo_Valor { get; set; }
        //De aqui
        public DbSet<Price> CAT_ListaPrecio { get; set; }
        public DbSet<Price_Company> CAT_ListaP_Compañia { get; set; }
        public DbSet<Price_Company> CAT_ListaP_Promocion { get; set; }
        public DbSet<Provenance> CAT_Procedencia { get; set; }
        public DbSet<Contact> CAT_Contacto { get; set; }
        //hasta aqui es zona de pruebas
        public DbSet<Maquila> CAT_Maquilador { get; set; }
        public DbSet<Tapon> CAT_Tipo_Tapon { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
