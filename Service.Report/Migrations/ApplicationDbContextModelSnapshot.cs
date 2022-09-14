﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Service.Report.Context;

namespace Service.Report.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.15")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Service.Report.Domain.Catalogs.Branch", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Sucursal")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CAT_Sucursal");
                });

            modelBuilder.Entity("Service.Report.Domain.Catalogs.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte>("Convenio")
                        .HasColumnType("tinyint");

                    b.Property<string>("NombreEmpresa")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CAT_Compañia");
                });

            modelBuilder.Entity("Service.Report.Domain.Catalogs.Maquila", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CAT_Maquila");
                });

            modelBuilder.Entity("Service.Report.Domain.Catalogs.Medic", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ClaveMedico")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombreMedico")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CAT_Medico");
                });

            modelBuilder.Entity("Service.Report.Domain.MedicalRecord.MedicalRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Celular")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Correo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Edad")
                        .HasColumnType("int");

                    b.Property<string>("Expediente")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sexo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MedicalRecord");
                });

            modelBuilder.Entity("Service.Report.Domain.Request.Request", b =>
                {
                    b.Property<Guid>("SolicitudId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Cargo")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Clave")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Descuento")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("EmpresaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte>("EstatusId")
                        .HasColumnType("tinyint");

                    b.Property<Guid>("ExpedienteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("MedicoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Parcialidad")
                        .HasColumnType("bit");

                    b.Property<decimal>("Precio")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PrecioFinal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("SucursalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte>("Urgencia")
                        .HasColumnType("tinyint");

                    b.HasKey("SolicitudId");

                    b.HasIndex("EmpresaId");

                    b.HasIndex("ExpedienteId");

                    b.HasIndex("MedicoId");

                    b.HasIndex("SucursalId");

                    b.ToTable("Request");
                });

            modelBuilder.Entity("Service.Report.Domain.Request.RequestPack", b =>
                {
                    b.Property<Guid>("SolicitudId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("PaqueteId")
                        .HasColumnType("int");

                    b.Property<string>("Clave")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Descuento")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("DescuentoPorcentaje")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Precio")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PrecioFinal")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("SolicitudId", "PaqueteId");

                    b.ToTable("RequestPack");
                });

            modelBuilder.Entity("Service.Report.Domain.Request.RequestPayment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("ACuenta")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Cheque")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Efectivo")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("EmpresaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte>("Estatus")
                        .HasColumnType("tinyint");

                    b.Property<string>("Factura")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("PP")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Saldo")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("SolicitudId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("TDC")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TDD")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Transferecia")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("UsuarioModifico")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EmpresaId");

                    b.HasIndex("SolicitudId");

                    b.ToTable("RequestPayment");
                });

            modelBuilder.Entity("Service.Report.Domain.Request.RequestStatus", b =>
                {
                    b.Property<byte>("Id")
                        .HasColumnType("tinyint");

                    b.Property<string>("Estatus")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RequestStatus");
                });

            modelBuilder.Entity("Service.Report.Domain.Request.RequestStudy", b =>
                {
                    b.Property<Guid>("SolicitudId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Clave")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Descuento")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Duracion")
                        .HasColumnType("int");

                    b.Property<byte>("EstatusId")
                        .HasColumnType("tinyint");

                    b.Property<string>("Estudio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MaquilaId")
                        .HasColumnType("int");

                    b.Property<int?>("PaqueteId")
                        .HasColumnType("int");

                    b.Property<decimal>("Precio")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PrecioFinal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid?>("SucursalId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("SolicitudId", "Id");

                    b.HasIndex("EstatusId");

                    b.HasIndex("MaquilaId");

                    b.HasIndex("SucursalId");

                    b.HasIndex("SolicitudId", "PaqueteId");

                    b.ToTable("RequestStudy");
                });

            modelBuilder.Entity("Service.Report.Domain.Request.Request", b =>
                {
                    b.HasOne("Service.Report.Domain.Catalogs.Company", "Empresa")
                        .WithMany()
                        .HasForeignKey("EmpresaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Service.Report.Domain.MedicalRecord.MedicalRecord", "Expediente")
                        .WithMany()
                        .HasForeignKey("ExpedienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Service.Report.Domain.Catalogs.Medic", "Medico")
                        .WithMany()
                        .HasForeignKey("MedicoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Service.Report.Domain.Catalogs.Branch", "Sucursal")
                        .WithMany()
                        .HasForeignKey("SucursalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Empresa");

                    b.Navigation("Expediente");

                    b.Navigation("Medico");

                    b.Navigation("Sucursal");
                });

            modelBuilder.Entity("Service.Report.Domain.Request.RequestPack", b =>
                {
                    b.HasOne("Service.Report.Domain.Request.Request", "Solicitud")
                        .WithMany("Paquetes")
                        .HasForeignKey("SolicitudId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Solicitud");
                });

            modelBuilder.Entity("Service.Report.Domain.Request.RequestPayment", b =>
                {
                    b.HasOne("Service.Report.Domain.Catalogs.Company", "Empresa")
                        .WithMany()
                        .HasForeignKey("EmpresaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Service.Report.Domain.Request.Request", "Solicitud")
                        .WithMany("MetodoPago")
                        .HasForeignKey("SolicitudId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Empresa");

                    b.Navigation("Solicitud");
                });

            modelBuilder.Entity("Service.Report.Domain.Request.RequestStudy", b =>
                {
                    b.HasOne("Service.Report.Domain.Request.RequestStatus", "Estatus")
                        .WithMany()
                        .HasForeignKey("EstatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Service.Report.Domain.Catalogs.Maquila", "Maquila")
                        .WithMany()
                        .HasForeignKey("MaquilaId");

                    b.HasOne("Service.Report.Domain.Request.Request", "Solicitud")
                        .WithMany("Estudios")
                        .HasForeignKey("SolicitudId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Service.Report.Domain.Catalogs.Branch", "Sucursal")
                        .WithMany()
                        .HasForeignKey("SucursalId");

                    b.HasOne("Service.Report.Domain.Request.RequestPack", "Paquete")
                        .WithMany("Estudios")
                        .HasForeignKey("SolicitudId", "PaqueteId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Estatus");

                    b.Navigation("Maquila");

                    b.Navigation("Paquete");

                    b.Navigation("Solicitud");

                    b.Navigation("Sucursal");
                });

            modelBuilder.Entity("Service.Report.Domain.Request.Request", b =>
                {
                    b.Navigation("Estudios");

                    b.Navigation("MetodoPago");

                    b.Navigation("Paquetes");
                });

            modelBuilder.Entity("Service.Report.Domain.Request.RequestPack", b =>
                {
                    b.Navigation("Estudios");
                });
#pragma warning restore 612, 618
        }
    }
}
