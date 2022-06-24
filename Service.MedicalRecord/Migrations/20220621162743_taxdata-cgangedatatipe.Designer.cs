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
    [Migration("20220621162743_taxdata-cgangedatatipe")]
    partial class taxdatacgangedatatipe
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.15")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

            modelBuilder.Entity("Service.MedicalRecord.Domain.TaxData.TaxData", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<int>("ColoniaId")
                        .HasColumnType("int");

                    b.Property<string>("Correo")
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

            modelBuilder.Entity("Service.MedicalRecord.Domain.MedicalRecord.MedicalRecord", b =>
                {
                    b.Navigation("TaxData");
                });
#pragma warning restore 612, 618
        }
    }
}
