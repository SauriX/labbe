﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Service.Billing.Context;

namespace Service.Billing.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230120211216_SeriesBilling")]
    partial class SeriesBilling
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.15")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Service.Billing.Domain.Invoice.Invoice", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("ConNombre")
                        .HasColumnType("bit");

                    b.Property<bool>("Desglozado")
                        .HasColumnType("bit");

                    b.Property<string>("EnvioCorreo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EnvioWhatsapp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Estatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Expediente")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ExpedienteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FacturapiId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaCreo")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaModifico")
                        .HasColumnType("datetime2");

                    b.Property<string>("FormaPago")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MetodoPago")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Paciente")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RFC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RegimenFiscal")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Serie")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SerieNumero")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Solicitud")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("SolicitudId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UsoCFDI")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UsuarioCreoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UsuarioModificoId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("CAT_Factura");
                });

            modelBuilder.Entity("Service.Billing.Domain.Invoice.InvoiceCompany", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Compañia")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("CompañiaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("ConNombre")
                        .HasColumnType("bit");

                    b.Property<bool>("Desglozado")
                        .HasColumnType("bit");

                    b.Property<string>("EnvioCorreo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EnvioWhatsapp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Estatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FacturapiId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaCreo")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaModifico")
                        .HasColumnType("datetime2");

                    b.Property<string>("FormaPago")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MetodoPago")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RFC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RegimenFiscal")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsoCFDI")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UsuarioCreoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UsuarioModificoId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("CAT_Factura_Companias");
                });

            modelBuilder.Entity("Service.Billing.Domain.Invoice.InvoiceCompanyRequests", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("FechaCreo")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaModifico")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("InvoiceCompanyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SolicitudId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UsuarioCreoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UsuarioModificoId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceCompanyId");

                    b.ToTable("Relacion_Factura_Solicitudes");
                });

            modelBuilder.Entity("Service.Billing.Domain.Series.Series", b =>
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

                    b.Property<Guid>("SucursalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte>("TipoSerie")
                        .HasColumnType("tinyint");

                    b.Property<Guid>("UsuarioCreoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UsuarioModificoId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("CAT_Serie");
                });

            modelBuilder.Entity("Service.Billing.Domain.Invoice.InvoiceCompanyRequests", b =>
                {
                    b.HasOne("Service.Billing.Domain.Invoice.InvoiceCompany", "InvoiceCompany")
                        .WithMany("Solicitudes")
                        .HasForeignKey("InvoiceCompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("InvoiceCompany");
                });

            modelBuilder.Entity("Service.Billing.Domain.Invoice.InvoiceCompany", b =>
                {
                    b.Navigation("Solicitudes");
                });
#pragma warning restore 612, 618
        }
    }
}
