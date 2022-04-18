﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Service.Catalog.Context;

namespace Service.Catalog.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220414194026_sucursalerelations")]
    partial class sucursalerelations
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.15")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Identidad.Api.Model.Medicos.MedicClinic", b =>
                {
                    b.Property<int>("MedicoId")
                        .HasColumnType("int");

                    b.Property<int>("ClinicaId")
                        .HasColumnType("int");

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaCreo")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaMod")
                        .HasColumnType("datetime2");

                    b.Property<long>("UsuarioCreoId")
                        .HasColumnType("bigint");

                    b.Property<string>("UsuarioModId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MedicoId", "ClinicaId");

                    b.HasIndex("ClinicaId");

                    b.ToTable("CAT_Medico_Clinica");
                });

            modelBuilder.Entity("Identidad.Api.ViewModels.Menu.Medics", b =>
                {
                    b.Property<int>("IdMedico")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Calle")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<long?>("Celular")
                        .HasColumnType("bigint");

                    b.Property<long?>("CiudadId")
                        .HasMaxLength(15)
                        .HasColumnType("bigint");

                    b.Property<string>("Clave")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<int>("CodigoPostal")
                        .HasMaxLength(15)
                        .HasColumnType("int");

                    b.Property<long>("ColoniaId")
                        .HasMaxLength(15)
                        .HasColumnType("bigint");

                    b.Property<string>("Correo")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<long>("EspecialidadId")
                        .HasColumnType("bigint");

                    b.Property<long?>("EstadoId")
                        .HasMaxLength(15)
                        .HasColumnType("bigint");

                    b.Property<DateTime>("FechaCreo")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaMod")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("NumeroExterior")
                        .HasMaxLength(100)
                        .HasColumnType("int");

                    b.Property<int?>("NumeroInterior")
                        .HasMaxLength(100)
                        .HasColumnType("int");

                    b.Property<string>("Observaciones")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("PrimerApellido")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("SegundoApellido")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<long?>("Telefono")
                        .HasColumnType("bigint");

                    b.Property<int>("UsuarioCreoId")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioModId")
                        .HasColumnType("int");

                    b.HasKey("IdMedico");

                    b.ToTable("CAT_Medico");
                });

            modelBuilder.Entity("Service.Catalog.Domain.Branch.Branch", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Calle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Clave")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ClinicosId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ColoniaId")
                        .HasColumnType("int");

                    b.Property<string>("Correo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("FacturaciónId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("FechaCreo")
                        .HasColumnType("smalldatetime");

                    b.Property<DateTime>("FechaModifico")
                        .HasColumnType("smalldatetime");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumeroExterior")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumeroInterior")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("PresupuestosId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ServicioId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long?>("Telefono")
                        .HasColumnType("bigint");

                    b.Property<Guid>("UsuarioCreoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UsuarioModificoId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ColoniaId");

                    b.ToTable("CAT_Sucursal");
                });

            modelBuilder.Entity("Service.Catalog.Domain.Branch.BranchStudy", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<Guid>("BranchId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("EstudioId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaCreo")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaMod")
                        .HasColumnType("datetime2");

                    b.Property<string>("UsuarioCreoId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioModId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Relacion_Estudio_Sucursal");
                });

            modelBuilder.Entity("Service.Catalog.Domain.Catalog.Area", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Clave")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DepartamentoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaCreo")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaModifico")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioCreoId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioModificoId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DepartamentoId");

                    b.ToTable("CAT_Area");
                });

            modelBuilder.Entity("Service.Catalog.Domain.Catalog.Bank", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Clave")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaCreo")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaModifico")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioCreoId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioModificoId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CAT_Banco");
                });

            modelBuilder.Entity("Service.Catalog.Domain.Catalog.Clinic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Clave")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<DateTime>("FechaCreo")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaModifico")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("UsuarioCreoId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioModificoId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CAT_Clinica");
                });

            modelBuilder.Entity("Service.Catalog.Domain.Catalog.Delivery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Clave")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaCreo")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaModifico")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioCreoId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioModificoId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CAT_Paqueteria");
                });

            modelBuilder.Entity("Service.Catalog.Domain.Catalog.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Clave")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaCreo")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaModifico")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioCreoId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioModificoId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CAT_Departamento");
                });

            modelBuilder.Entity("Service.Catalog.Domain.Catalog.Dimension", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<byte>("Ancho")
                        .HasColumnType("tinyint");

                    b.Property<string>("Clave")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaCreo")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaModifico")
                        .HasColumnType("datetime2");

                    b.Property<byte>("Largo")
                        .HasColumnType("tinyint");

                    b.Property<string>("UsuarioCreoId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioModificoId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CAT_Dimension");
                });

            modelBuilder.Entity("Service.Catalog.Domain.Catalog.Field", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Clave")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaCreo")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaModifico")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioCreoId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioModificoId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CAT_Especialidad");
                });

            modelBuilder.Entity("Service.Catalog.Domain.Catalog.Indicator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Clave")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaCreo")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaModifico")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioCreoId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioModificoId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CAT_Indicador");
                });

            modelBuilder.Entity("Service.Catalog.Domain.Catalog.Method", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Clave")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaCreo")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaModifico")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioCreoId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioModificoId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CAT_Metodo");
                });

            modelBuilder.Entity("Service.Catalog.Domain.Catalog.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Clave")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaCreo")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaModifico")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioCreoId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioModificoId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CAT_FormaPago");
                });

            modelBuilder.Entity("Service.Catalog.Domain.Catalog.PaymentMethod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Clave")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaCreo")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaModifico")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioCreoId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioModificoId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CAT_MetodoPago");
                });

            modelBuilder.Entity("Service.Catalog.Domain.Catalog.SampleType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Clave")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaCreo")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaModifico")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioCreoId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioModificoId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CAT_TipoMuestra");
                });

            modelBuilder.Entity("Service.Catalog.Domain.Catalog.UseOfCFDI", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Clave")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaCreo")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaModifico")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioCreoId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioModificoId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CAT_CFDI");
                });

            modelBuilder.Entity("Service.Catalog.Domain.Catalog.WorkList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Clave")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaCreo")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaModifico")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioCreoId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioModificoId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CAT_ListaTrabajo");
                });

            modelBuilder.Entity("Service.Catalog.Domain.Constant.City", b =>
                {
                    b.Property<short>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Ciudad")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("EstadoId")
                        .HasColumnType("tinyint");

                    b.HasKey("Id");

                    b.HasIndex("EstadoId");

                    b.ToTable("CAT_Ciudad");
                });

            modelBuilder.Entity("Service.Catalog.Domain.Constant.Colony", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<short>("CiudadId")
                        .HasColumnType("smallint");

                    b.Property<string>("CodigoPostal")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Colonia")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CiudadId");

                    b.ToTable("CAT_Colonia");
                });

            modelBuilder.Entity("Service.Catalog.Domain.Constant.State", b =>
                {
                    b.Property<byte>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Estado")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CAT_Estado");
                });

            modelBuilder.Entity("Service.Catalog.Domain.Indication.Indication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Clave")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Descripcion")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime>("FechaCreo")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaModifico")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("UsuarioCreoId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioModificoId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CAT_Indicacion");
                });

            modelBuilder.Entity("Service.Catalog.Domain.Indication.IndicationStudy", b =>
                {
                    b.Property<int>("EstudioId")
                        .HasColumnType("int");

                    b.Property<int>("IndicacionId")
                        .HasColumnType("int");

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaCreo")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaMod")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("UsuarioCreoId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UsuarioModId")
                        .HasColumnType("int");

                    b.HasKey("EstudioId", "IndicacionId");

                    b.HasIndex("IndicacionId");

                    b.ToTable("Relacion_Estudio_Indicacion");
                });

            modelBuilder.Entity("Service.Catalog.Domain.Reagent.Reagent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Clave")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("ClaveSistema")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<DateTime>("FechaCreo")
                        .HasColumnType("smalldatetime");

                    b.Property<DateTime?>("FechaModifico")
                        .HasColumnType("smalldatetime");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("NombreSistema")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UsuarioCreoId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioModificoId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CAT_Reactivo_Contpaq");
                });

            modelBuilder.Entity("Service.Catalog.Domain.Study", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<int>("AreaId")
                        .HasColumnType("int");

                    b.Property<string>("Clave")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DepartamentoId")
                        .HasColumnType("int");

                    b.Property<int>("DiasResultado")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaCreo")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaMod")
                        .HasColumnType("datetime2");

                    b.Property<int>("FormatoId")
                        .HasColumnType("int");

                    b.Property<int>("MaquiladorId")
                        .HasColumnType("int");

                    b.Property<int>("MetodoId")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombreCorto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Orden")
                        .HasColumnType("int");

                    b.Property<string>("Prioridad")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TiempoRespuesta")
                        .HasColumnType("int");

                    b.Property<int>("TipoMuestraId")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UsuarioCreoId")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioModId")
                        .HasColumnType("int");

                    b.Property<bool>("Visible")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("AreaId");

                    b.ToTable("CAT_Estudio");
                });

            modelBuilder.Entity("Identidad.Api.Model.Medicos.MedicClinic", b =>
                {
                    b.HasOne("Service.Catalog.Domain.Catalog.Clinic", "Clinica")
                        .WithMany()
                        .HasForeignKey("ClinicaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Identidad.Api.ViewModels.Menu.Medics", "Medico")
                        .WithMany("Clinicas")
                        .HasForeignKey("MedicoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Clinica");

                    b.Navigation("Medico");
                });

            modelBuilder.Entity("Service.Catalog.Domain.Branch.Branch", b =>
                {
                    b.HasOne("Service.Catalog.Domain.Constant.Colony", "Colonia")
                        .WithMany()
                        .HasForeignKey("ColoniaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Colonia");
                });

            modelBuilder.Entity("Service.Catalog.Domain.Catalog.Area", b =>
                {
                    b.HasOne("Service.Catalog.Domain.Catalog.Department", "Departamento")
                        .WithMany()
                        .HasForeignKey("DepartamentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Departamento");
                });

            modelBuilder.Entity("Service.Catalog.Domain.Constant.City", b =>
                {
                    b.HasOne("Service.Catalog.Domain.Constant.State", "Estado")
                        .WithMany()
                        .HasForeignKey("EstadoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Estado");
                });

            modelBuilder.Entity("Service.Catalog.Domain.Constant.Colony", b =>
                {
                    b.HasOne("Service.Catalog.Domain.Constant.City", "Ciudad")
                        .WithMany()
                        .HasForeignKey("CiudadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ciudad");
                });

            modelBuilder.Entity("Service.Catalog.Domain.Indication.IndicationStudy", b =>
                {
                    b.HasOne("Service.Catalog.Domain.Study", "Estudio")
                        .WithMany()
                        .HasForeignKey("EstudioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Service.Catalog.Domain.Indication.Indication", "Indicacion")
                        .WithMany("Estudios")
                        .HasForeignKey("IndicacionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Estudio");

                    b.Navigation("Indicacion");
                });

            modelBuilder.Entity("Service.Catalog.Domain.Study", b =>
                {
                    b.HasOne("Service.Catalog.Domain.Catalog.Area", "Area")
                        .WithMany()
                        .HasForeignKey("AreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Area");
                });

            modelBuilder.Entity("Identidad.Api.ViewModels.Menu.Medics", b =>
                {
                    b.Navigation("Clinicas");
                });

            modelBuilder.Entity("Service.Catalog.Domain.Indication.Indication", b =>
                {
                    b.Navigation("Estudios");
                });
#pragma warning restore 612, 618
        }
    }
}