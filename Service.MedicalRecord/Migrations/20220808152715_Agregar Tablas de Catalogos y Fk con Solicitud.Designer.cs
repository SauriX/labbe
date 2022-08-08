﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Service.MedicalRecord.Context;

namespace Service.MedicalRecord.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220808152715_Agregar Tablas de Catalogos y Fk con Solicitud")]
    partial class AgregarTablasdeCatalogosyFkconSolicitud
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.15")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Service.MedicalRecord.Domain.Appointments.AppointmentDom", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Celular")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cita")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Direccion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Estatus_Cita")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ExpedienteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("FechaCita")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaCreo")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaMod")
                        .HasColumnType("datetime2");

                    b.Property<string>("Genero")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("HoraCita")
                        .HasColumnType("datetime2");

                    b.Property<string>("Indicaciones")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombrePaciente")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid>("UsuarioCreoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UsuarioModId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("WhatsApp")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ExpedienteId");

                    b.ToTable("CAT_Cita_Dom");
                });

            modelBuilder.Entity("Service.MedicalRecord.Domain.Appointments.AppointmentLab", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Cita")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("CompaniaID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Direccion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Edad")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ExpedienteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("FechaCita")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaCreo")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaMod")
                        .HasColumnType("datetime2");

                    b.Property<string>("Genero")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("HoraCita")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("MedicoID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("NombrePaciente")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Procedencia")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid>("SucursalID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UsuarioCreoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UsuarioModId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("WhatsApp")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ExpedienteId");

                    b.ToTable("CAT_Cita_Lab");
                });

            modelBuilder.Entity("Service.MedicalRecord.Domain.Catalogs.Branch", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<short>("CiudadId")
                        .HasColumnType("smallint");

                    b.Property<string>("Clave")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Clinicos")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CodigoPostal")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CAT_Sucursal");
                });

            modelBuilder.Entity("Service.MedicalRecord.Domain.Catalogs.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Clave")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CAT_Compañia");
                });

            modelBuilder.Entity("Service.MedicalRecord.Domain.Catalogs.Medic", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Clave")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CAT_Medico");
                });

            modelBuilder.Entity("Service.MedicalRecord.Domain.MedicalRecord.MedicalRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Calle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Celular")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ciudad")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CodigoPostal")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ColoniaId")
                        .HasColumnType("int");

                    b.Property<string>("Correo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Edad")
                        .HasColumnType("int");

                    b.Property<string>("Estado")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Expediente")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaCreo")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaDeNacimiento")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaMod")
                        .HasColumnType("datetime2");

                    b.Property<string>("Genero")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("IdSucursal")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Monedero")
                        .HasColumnType("int");

                    b.Property<string>("NombrePaciente")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PrimerApellido")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SegundoApellido")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefono")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UsuarioCreoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UsuarioModId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("CAT_Expedientes");
                });

            modelBuilder.Entity("Service.MedicalRecord.Domain.MedicalRecord.MedicalRecordTaxData", b =>
                {
                    b.Property<Guid>("FacturaID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ExpedienteID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaCreo")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaMod")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UsuarioCreoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UsuarioModId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("FacturaID", "ExpedienteID");

                    b.HasIndex("ExpedienteID");

                    b.ToTable("Relacion_Expediente_Factura");
                });

            modelBuilder.Entity("Service.MedicalRecord.Domain.PriceQuote.CotizacionStudy", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AppointmentDomId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AppointmentLabId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Cargo")
                        .HasColumnType("bit");

                    b.Property<string>("Clave")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Copago")
                        .HasColumnType("bit");

                    b.Property<Guid>("CotizacionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Descuento")
                        .HasColumnType("bit");

                    b.Property<byte>("EstatusId")
                        .HasColumnType("tinyint");

                    b.Property<int?>("EstudioId")
                        .HasColumnType("int");

                    b.Property<Guid>("ListaPrecioId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("PaqueteId")
                        .HasColumnType("int");

                    b.Property<decimal>("Precio")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PrecioFinal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid?>("PriceQuoteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("PromocionId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("AppointmentDomId");

                    b.HasIndex("AppointmentLabId");

                    b.HasIndex("PriceQuoteId");

                    b.ToTable("cotizacionStudies");
                });

            modelBuilder.Entity("Service.MedicalRecord.Domain.PriceQuote.PriceQuote", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Afiliacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Cargo")
                        .HasColumnType("int");

                    b.Property<Guid>("CompaniaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Correo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Edad")
                        .HasColumnType("int");

                    b.Property<string>("Estatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ExpedienteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("FechaCreo")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaMod")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaNac")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaPropuesta")
                        .HasColumnType("datetime2");

                    b.Property<string>("Genero")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("MedicoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("NombrePaciente")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Observaciones")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Procedencia")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Tipo")
                        .HasColumnType("int");

                    b.Property<Guid>("UsuarioCreoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UsuarioModId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Whatsapp")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ExpedienteId");

                    b.ToTable("CAT_Cotizaciones");
                });

            modelBuilder.Entity("Service.MedicalRecord.Domain.Request.Request", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Afiliacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Clave")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClavePatologica")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("CompañiaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EnvioCorreo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EnvioWhatsApp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EsNuevo")
                        .HasColumnType("bit");

                    b.Property<Guid>("ExpedienteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("FechaCreo")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaModifico")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("MedicoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Observaciones")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Parcialidad")
                        .HasColumnType("bit");

                    b.Property<byte>("Procedencia")
                        .HasColumnType("tinyint");

                    b.Property<string>("RutaFormato")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RutaINE")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RutaOrden")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("SucursalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte>("Urgencia")
                        .HasColumnType("tinyint");

                    b.Property<string>("UsuarioCreo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UsuarioCreoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UsuarioModificoId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CompañiaId");

                    b.HasIndex("ExpedienteId");

                    b.HasIndex("MedicoId");

                    b.HasIndex("SucursalId");

                    b.ToTable("CAT_Solicitud");
                });

            modelBuilder.Entity("Service.MedicalRecord.Domain.Request.RequestPack", b =>
                {
                    b.Property<Guid>("SolicitudId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("PaqueteId")
                        .HasColumnType("int");

                    b.Property<bool>("AplicaCargo")
                        .HasColumnType("bit");

                    b.Property<bool>("AplicaCopago")
                        .HasColumnType("bit");

                    b.Property<bool>("AplicaDescuento")
                        .HasColumnType("bit");

                    b.Property<int>("AreaId")
                        .HasColumnType("int");

                    b.Property<string>("Clave")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DepartamentoId")
                        .HasColumnType("int");

                    b.Property<decimal>("Descuento")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("DescuentoPorcentaje")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Dias")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("FechaCreo")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaModifico")
                        .HasColumnType("datetime2");

                    b.Property<int>("Horas")
                        .HasColumnType("int");

                    b.Property<string>("ListaPrecio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ListaPrecioId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Precio")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PrecioFinal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Promocion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PromocionId")
                        .HasColumnType("int");

                    b.Property<Guid>("UsuarioCreoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UsuarioModificoId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("SolicitudId", "PaqueteId");

                    b.ToTable("Relacion_Solicitud_Paquete");
                });

            modelBuilder.Entity("Service.MedicalRecord.Domain.Request.RequestStudy", b =>
                {
                    b.Property<Guid>("SolicitudId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("EstudioId")
                        .HasColumnType("int");

                    b.Property<bool>("AplicaCargo")
                        .HasColumnType("bit");

                    b.Property<bool>("AplicaCopago")
                        .HasColumnType("bit");

                    b.Property<bool>("AplicaDescuento")
                        .HasColumnType("bit");

                    b.Property<int>("AreaId")
                        .HasColumnType("int");

                    b.Property<string>("Clave")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DepartamentoId")
                        .HasColumnType("int");

                    b.Property<decimal>("Descuento")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("DescuentoPorcentaje")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Dias")
                        .HasColumnType("decimal(18,2)");

                    b.Property<byte>("EstatusId")
                        .HasColumnType("tinyint");

                    b.Property<DateTime>("FechaCreo")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaModifico")
                        .HasColumnType("datetime2");

                    b.Property<int>("Horas")
                        .HasColumnType("int");

                    b.Property<string>("ListaPrecio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ListaPrecioId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PaqueteId")
                        .HasColumnType("int");

                    b.Property<decimal>("Precio")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PrecioFinal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Promocion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PromocionId")
                        .HasColumnType("int");

                    b.Property<Guid>("UsuarioCreoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UsuarioModificoId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("SolicitudId", "EstudioId");

                    b.HasIndex("SolicitudId", "PaqueteId");

                    b.ToTable("Relacion_Solicitud_Estudio");
                });

            modelBuilder.Entity("Service.MedicalRecord.Domain.TaxData.TaxData", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Calle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ciudad")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CodigoPostal")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ColoniaId")
                        .HasColumnType("int");

                    b.Property<string>("Correo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Estado")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaCreo")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaMod")
                        .HasColumnType("datetime2");

                    b.Property<string>("RFC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RazonSocial")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UsuarioCreoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UsuarioModId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("CAT_Datos_Fiscales");
                });

            modelBuilder.Entity("Service.MedicalRecord.Domain.Appointments.AppointmentDom", b =>
                {
                    b.HasOne("Service.MedicalRecord.Domain.MedicalRecord.MedicalRecord", "Expediente")
                        .WithMany()
                        .HasForeignKey("ExpedienteId");

                    b.Navigation("Expediente");
                });

            modelBuilder.Entity("Service.MedicalRecord.Domain.Appointments.AppointmentLab", b =>
                {
                    b.HasOne("Service.MedicalRecord.Domain.MedicalRecord.MedicalRecord", "Expediente")
                        .WithMany()
                        .HasForeignKey("ExpedienteId");

                    b.Navigation("Expediente");
                });

            modelBuilder.Entity("Service.MedicalRecord.Domain.MedicalRecord.MedicalRecordTaxData", b =>
                {
                    b.HasOne("Service.MedicalRecord.Domain.MedicalRecord.MedicalRecord", "Expediente")
                        .WithMany("TaxData")
                        .HasForeignKey("ExpedienteID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Service.MedicalRecord.Domain.TaxData.TaxData", "Factura")
                        .WithMany()
                        .HasForeignKey("FacturaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Expediente");

                    b.Navigation("Factura");
                });

            modelBuilder.Entity("Service.MedicalRecord.Domain.PriceQuote.CotizacionStudy", b =>
                {
                    b.HasOne("Service.MedicalRecord.Domain.Appointments.AppointmentDom", null)
                        .WithMany("Estudios")
                        .HasForeignKey("AppointmentDomId");

                    b.HasOne("Service.MedicalRecord.Domain.Appointments.AppointmentLab", null)
                        .WithMany("Estudios")
                        .HasForeignKey("AppointmentLabId");

                    b.HasOne("Service.MedicalRecord.Domain.PriceQuote.PriceQuote", null)
                        .WithMany("Estudios")
                        .HasForeignKey("PriceQuoteId");
                });

            modelBuilder.Entity("Service.MedicalRecord.Domain.PriceQuote.PriceQuote", b =>
                {
                    b.HasOne("Service.MedicalRecord.Domain.MedicalRecord.MedicalRecord", "Expediente")
                        .WithMany()
                        .HasForeignKey("ExpedienteId");

                    b.Navigation("Expediente");
                });

            modelBuilder.Entity("Service.MedicalRecord.Domain.Request.Request", b =>
                {
                    b.HasOne("Service.MedicalRecord.Domain.Catalogs.Company", "Compañia")
                        .WithMany()
                        .HasForeignKey("CompañiaId");

                    b.HasOne("Service.MedicalRecord.Domain.MedicalRecord.MedicalRecord", "Expediente")
                        .WithMany()
                        .HasForeignKey("ExpedienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Service.MedicalRecord.Domain.Catalogs.Medic", "Medico")
                        .WithMany()
                        .HasForeignKey("MedicoId");

                    b.HasOne("Service.MedicalRecord.Domain.Catalogs.Branch", "Sucursal")
                        .WithMany()
                        .HasForeignKey("SucursalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Compañia");

                    b.Navigation("Expediente");

                    b.Navigation("Medico");

                    b.Navigation("Sucursal");
                });

            modelBuilder.Entity("Service.MedicalRecord.Domain.Request.RequestPack", b =>
                {
                    b.HasOne("Service.MedicalRecord.Domain.Request.Request", "Solicitud")
                        .WithMany("Paquetes")
                        .HasForeignKey("SolicitudId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Solicitud");
                });

            modelBuilder.Entity("Service.MedicalRecord.Domain.Request.RequestStudy", b =>
                {
                    b.HasOne("Service.MedicalRecord.Domain.Request.Request", "Solicitud")
                        .WithMany("Estudios")
                        .HasForeignKey("SolicitudId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Service.MedicalRecord.Domain.Request.RequestPack", "Paquete")
                        .WithMany("Estudios")
                        .HasForeignKey("SolicitudId", "PaqueteId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Paquete");

                    b.Navigation("Solicitud");
                });

            modelBuilder.Entity("Service.MedicalRecord.Domain.Appointments.AppointmentDom", b =>
                {
                    b.Navigation("Estudios");
                });

            modelBuilder.Entity("Service.MedicalRecord.Domain.Appointments.AppointmentLab", b =>
                {
                    b.Navigation("Estudios");
                });

            modelBuilder.Entity("Service.MedicalRecord.Domain.MedicalRecord.MedicalRecord", b =>
                {
                    b.Navigation("TaxData");
                });

            modelBuilder.Entity("Service.MedicalRecord.Domain.PriceQuote.PriceQuote", b =>
                {
                    b.Navigation("Estudios");
                });

            modelBuilder.Entity("Service.MedicalRecord.Domain.Request.Request", b =>
                {
                    b.Navigation("Estudios");

                    b.Navigation("Paquetes");
                });

            modelBuilder.Entity("Service.MedicalRecord.Domain.Request.RequestPack", b =>
                {
                    b.Navigation("Estudios");
                });
#pragma warning restore 612, 618
        }
    }
}
