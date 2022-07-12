﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Service.Catalog.Context;

namespace Service.Report.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220712170745_Tabla Estadisitca Pacientes")]
    partial class TablaEstadisitcaPacientes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.15")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Service.Report.Domain.MedicalRecord.MedicalRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Expediente")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MedicalRecord");
                });

            modelBuilder.Entity("Service.Report.Domain.PatientStats.PatientStats", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ExpendienteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<string>("NombrePaciente")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Solicitudes")
                        .HasColumnType("int");

                    b.Property<string>("Sucursal")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("SucursalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("ExpendienteId");

                    b.ToTable("PatientStats");
                });

            modelBuilder.Entity("Service.Report.Domain.Request.Request", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Ciudad")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ExpedienteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ExpedienteNombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaFinal")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Precio")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Sucursal")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("SucursalId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ExpedienteId");

                    b.ToTable("Request");
                });

            modelBuilder.Entity("Service.Report.Domain.PatientStats.PatientStats", b =>
                {
                    b.HasOne("Service.Report.Domain.MedicalRecord.MedicalRecord", "Expendiente")
                        .WithMany()
                        .HasForeignKey("ExpendienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Expendiente");
                });

            modelBuilder.Entity("Service.Report.Domain.Request.Request", b =>
                {
                    b.HasOne("Service.Report.Domain.MedicalRecord.MedicalRecord", "Expediente")
                        .WithMany()
                        .HasForeignKey("ExpedienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Expediente");
                });
#pragma warning restore 612, 618
        }
    }
}
