using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class InicialRestaurada : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CAT_Compañia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Compañia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CAT_Datos_Fiscales",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RFC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RazonSocial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodigoPostal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ciudad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Calle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ColoniaId = table.Column<int>(type: "int", nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Datos_Fiscales", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CAT_Expedientes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Expediente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NombrePaciente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimerApellido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SegundoApellido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Genero = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaDeNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Edad = table.Column<int>(type: "int", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodigoPostal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ciudad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Celular = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Calle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ColoniaId = table.Column<int>(type: "int", nullable: false),
                    Monedero = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MonederoActivo = table.Column<bool>(type: "bit", nullable: false),
                    FechaActivacionMonedero = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdSucursal = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Expedientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CAT_Medico",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Medico", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CAT_Seguimiento_Ruta",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SucursalDestinoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SucursalOrigenId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaquiladorId = table.Column<int>(type: "int", nullable: false),
                    RutaId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaRecoleccion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MuestraId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EscaneoCodigoBarras = table.Column<bool>(type: "bit", nullable: false),
                    Temperatura = table.Column<double>(type: "float", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Seguimiento_Ruta", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CAT_Sucursal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Clinicos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodigoPostal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CiudadId = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Sucursal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CAT_Tipo_Tapon",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Tipo_Tapon", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Estatus_Solicitud",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estatus_Solicitud", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Estatus_Solicitud_Estudio",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estatus_Solicitud_Estudio", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CAT_Cita_Dom",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExpedienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NombrePaciente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estatus_Cita = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Celular = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Genero = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WhatsApp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Indicaciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cita = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    FechaCita = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HoraCita = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Cita_Dom", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CAT_Cita_Dom_CAT_Expedientes_ExpedienteId",
                        column: x => x.ExpedienteId,
                        principalTable: "CAT_Expedientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CAT_Cita_Lab",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExpedienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NombrePaciente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Edad = table.Column<int>(type: "int", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Procedencia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompaniaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicoID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Genero = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Cita = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WhatsApp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SucursalID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCita = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HoraCita = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Cita_Lab", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CAT_Cita_Lab_CAT_Expedientes_ExpedienteId",
                        column: x => x.ExpedienteId,
                        principalTable: "CAT_Expedientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CAT_Cotizaciones",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Procedencia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NombrePaciente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaNac = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompaniaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Afiliacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Whatsapp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaPropuesta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    Estatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpedienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Genero = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Edad = table.Column<int>(type: "int", nullable: false),
                    Cargo = table.Column<int>(type: "int", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Cotizaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CAT_Cotizaciones_CAT_Expedientes_ExpedienteId",
                        column: x => x.ExpedienteId,
                        principalTable: "CAT_Expedientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Relacion_Expediente_Factura",
                columns: table => new
                {
                    ExpedienteID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacturaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relacion_Expediente_Factura", x => new { x.FacturaID, x.ExpedienteID });
                    table.ForeignKey(
                        name: "FK_Relacion_Expediente_Factura_CAT_Datos_Fiscales_FacturaID",
                        column: x => x.FacturaID,
                        principalTable: "CAT_Datos_Fiscales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Relacion_Expediente_Factura_CAT_Expedientes_ExpedienteID",
                        column: x => x.ExpedienteID,
                        principalTable: "CAT_Expedientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CAT_Solicitud",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExpedienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SucursalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClavePatologica = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstatusId = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)1),
                    Procedencia = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)2),
                    Afiliacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompañiaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MedicoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Urgencia = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)1),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnvioCorreo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnvioWhatsApp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RutaOrden = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RutaINE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RutaINEReverso = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Parcialidad = table.Column<bool>(type: "bit", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    EsNuevo = table.Column<bool>(type: "bit", nullable: false),
                    TotalEstudios = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Descuento = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DescuentoTipo = table.Column<byte>(type: "tinyint", nullable: false),
                    Cargo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CargoTipo = table.Column<byte>(type: "tinyint", nullable: false),
                    Copago = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CopagoTipo = table.Column<byte>(type: "tinyint", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Saldo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UsuarioCreo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModificoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaModifico = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Solicitud", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CAT_Solicitud_CAT_Compañia_CompañiaId",
                        column: x => x.CompañiaId,
                        principalTable: "CAT_Compañia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CAT_Solicitud_CAT_Expedientes_ExpedienteId",
                        column: x => x.ExpedienteId,
                        principalTable: "CAT_Expedientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CAT_Solicitud_CAT_Medico_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "CAT_Medico",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CAT_Solicitud_CAT_Sucursal_SucursalId",
                        column: x => x.SucursalId,
                        principalTable: "CAT_Sucursal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cotizacionStudies",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CotizacionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ListaPrecioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PromocionId = table.Column<int>(type: "int", nullable: true),
                    EstudioId = table.Column<int>(type: "int", nullable: true),
                    PaqueteId = table.Column<int>(type: "int", nullable: true),
                    EstatusId = table.Column<byte>(type: "tinyint", nullable: false),
                    Descuento = table.Column<bool>(type: "bit", nullable: false),
                    Cargo = table.Column<bool>(type: "bit", nullable: false),
                    Copago = table.Column<bool>(type: "bit", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PrecioFinal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AppointmentDomId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AppointmentLabId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PriceQuoteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cotizacionStudies", x => x.id);
                    table.ForeignKey(
                        name: "FK_cotizacionStudies_CAT_Cita_Dom_AppointmentDomId",
                        column: x => x.AppointmentDomId,
                        principalTable: "CAT_Cita_Dom",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_cotizacionStudies_CAT_Cita_Lab_AppointmentLabId",
                        column: x => x.AppointmentLabId,
                        principalTable: "CAT_Cita_Lab",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_cotizacionStudies_CAT_Cotizaciones_PriceQuoteId",
                        column: x => x.PriceQuoteId,
                        principalTable: "CAT_Cotizaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cat_PendientesDeEnviar",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SegumientoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RutaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SucursalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaDeEntregaEstimada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SolicitudId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HoraDeRecoleccion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioModificoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaModifico = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cat_PendientesDeEnviar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cat_PendientesDeEnviar_CAT_Solicitud_SolicitudId",
                        column: x => x.SolicitudId,
                        principalTable: "CAT_Solicitud",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Relacion_Seguimiento_Solicitud",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SeguimientoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EstudioId = table.Column<int>(type: "int", nullable: false),
                    Estudio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SolicitudId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExpedienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NombrePaciente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Temperatura = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Escaneado = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relacion_Seguimiento_Solicitud", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Relacion_Seguimiento_Solicitud_CAT_Seguimiento_Ruta_SeguimientoId",
                        column: x => x.SeguimientoId,
                        principalTable: "CAT_Seguimiento_Ruta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Relacion_Seguimiento_Solicitud_CAT_Solicitud_SolicitudId",
                        column: x => x.SolicitudId,
                        principalTable: "CAT_Solicitud",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Relacion_Solicitud_Imagen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SolicitudId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ruta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModificoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaModifico = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relacion_Solicitud_Imagen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Relacion_Solicitud_Imagen_CAT_Solicitud_SolicitudId",
                        column: x => x.SolicitudId,
                        principalTable: "CAT_Solicitud",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Relacion_Solicitud_Paquete",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SolicitudId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaqueteId = table.Column<int>(type: "int", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ListaPrecioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ListaPrecio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PromocionId = table.Column<int>(type: "int", nullable: true),
                    Promocion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartamentoId = table.Column<int>(type: "int", nullable: false),
                    AreaId = table.Column<int>(type: "int", nullable: false),
                    AplicaDescuento = table.Column<bool>(type: "bit", nullable: false),
                    AplicaCargo = table.Column<bool>(type: "bit", nullable: false),
                    AplicaCopago = table.Column<bool>(type: "bit", nullable: false),
                    Dias = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Horas = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Descuento = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DescuentoPorcentaje = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PrecioFinal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModificoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaModifico = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relacion_Solicitud_Paquete", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Relacion_Solicitud_Paquete_CAT_Solicitud_SolicitudId",
                        column: x => x.SolicitudId,
                        principalTable: "CAT_Solicitud",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Relacion_Solicitud_Estudio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SolicitudId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EstudioId = table.Column<int>(type: "int", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaqueteId = table.Column<int>(type: "int", nullable: true),
                    ListaPrecioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ListaPrecio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PromocionId = table.Column<int>(type: "int", nullable: true),
                    Promocion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartamentoId = table.Column<int>(type: "int", nullable: false),
                    AreaId = table.Column<int>(type: "int", nullable: false),
                    EstatusId = table.Column<byte>(type: "tinyint", nullable: false),
                    Dias = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Horas = table.Column<int>(type: "int", nullable: false),
                    FechaEntrega = table.Column<DateTime>(type: "smalldatetime", nullable: false, defaultValueSql: "getdate()"),
                    AplicaDescuento = table.Column<bool>(type: "bit", nullable: false),
                    AplicaCargo = table.Column<bool>(type: "bit", nullable: false),
                    AplicaCopago = table.Column<bool>(type: "bit", nullable: false),
                    TaponId = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Descuento = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DescuentoPorcentaje = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PrecioFinal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModificoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaModifico = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relacion_Solicitud_Estudio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Relacion_Solicitud_Estudio_CAT_Solicitud_SolicitudId",
                        column: x => x.SolicitudId,
                        principalTable: "CAT_Solicitud",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Relacion_Solicitud_Estudio_CAT_Tipo_Tapon_TaponId",
                        column: x => x.TaponId,
                        principalTable: "CAT_Tipo_Tapon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Relacion_Solicitud_Estudio_Estatus_Solicitud_Estudio_EstatusId",
                        column: x => x.EstatusId,
                        principalTable: "Estatus_Solicitud_Estudio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Relacion_Solicitud_Estudio_Relacion_Solicitud_Paquete_PaqueteId",
                        column: x => x.PaqueteId,
                        principalTable: "Relacion_Solicitud_Paquete",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Cita_Dom_ExpedienteId",
                table: "CAT_Cita_Dom",
                column: "ExpedienteId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Cita_Lab_ExpedienteId",
                table: "CAT_Cita_Lab",
                column: "ExpedienteId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Cotizaciones_ExpedienteId",
                table: "CAT_Cotizaciones",
                column: "ExpedienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Cat_PendientesDeEnviar_SolicitudId",
                table: "Cat_PendientesDeEnviar",
                column: "SolicitudId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Solicitud_CompañiaId",
                table: "CAT_Solicitud",
                column: "CompañiaId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Solicitud_ExpedienteId",
                table: "CAT_Solicitud",
                column: "ExpedienteId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Solicitud_MedicoId",
                table: "CAT_Solicitud",
                column: "MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Solicitud_SucursalId",
                table: "CAT_Solicitud",
                column: "SucursalId");

            migrationBuilder.CreateIndex(
                name: "IX_cotizacionStudies_AppointmentDomId",
                table: "cotizacionStudies",
                column: "AppointmentDomId");

            migrationBuilder.CreateIndex(
                name: "IX_cotizacionStudies_AppointmentLabId",
                table: "cotizacionStudies",
                column: "AppointmentLabId");

            migrationBuilder.CreateIndex(
                name: "IX_cotizacionStudies_PriceQuoteId",
                table: "cotizacionStudies",
                column: "PriceQuoteId");

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Expediente_Factura_ExpedienteID",
                table: "Relacion_Expediente_Factura",
                column: "ExpedienteID");

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Seguimiento_Solicitud_SeguimientoId",
                table: "Relacion_Seguimiento_Solicitud",
                column: "SeguimientoId");

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Seguimiento_Solicitud_SolicitudId",
                table: "Relacion_Seguimiento_Solicitud",
                column: "SolicitudId");

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Solicitud_Estudio_EstatusId",
                table: "Relacion_Solicitud_Estudio",
                column: "EstatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Solicitud_Estudio_PaqueteId",
                table: "Relacion_Solicitud_Estudio",
                column: "PaqueteId");

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Solicitud_Estudio_SolicitudId",
                table: "Relacion_Solicitud_Estudio",
                column: "SolicitudId");

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Solicitud_Estudio_TaponId",
                table: "Relacion_Solicitud_Estudio",
                column: "TaponId");

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Solicitud_Imagen_SolicitudId",
                table: "Relacion_Solicitud_Imagen",
                column: "SolicitudId");

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Solicitud_Paquete_SolicitudId",
                table: "Relacion_Solicitud_Paquete",
                column: "SolicitudId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cat_PendientesDeEnviar");

            migrationBuilder.DropTable(
                name: "cotizacionStudies");

            migrationBuilder.DropTable(
                name: "Estatus_Solicitud");

            migrationBuilder.DropTable(
                name: "Relacion_Expediente_Factura");

            migrationBuilder.DropTable(
                name: "Relacion_Seguimiento_Solicitud");

            migrationBuilder.DropTable(
                name: "Relacion_Solicitud_Estudio");

            migrationBuilder.DropTable(
                name: "Relacion_Solicitud_Imagen");

            migrationBuilder.DropTable(
                name: "CAT_Cita_Dom");

            migrationBuilder.DropTable(
                name: "CAT_Cita_Lab");

            migrationBuilder.DropTable(
                name: "CAT_Cotizaciones");

            migrationBuilder.DropTable(
                name: "CAT_Datos_Fiscales");

            migrationBuilder.DropTable(
                name: "CAT_Seguimiento_Ruta");

            migrationBuilder.DropTable(
                name: "CAT_Tipo_Tapon");

            migrationBuilder.DropTable(
                name: "Estatus_Solicitud_Estudio");

            migrationBuilder.DropTable(
                name: "Relacion_Solicitud_Paquete");

            migrationBuilder.DropTable(
                name: "CAT_Solicitud");

            migrationBuilder.DropTable(
                name: "CAT_Compañia");

            migrationBuilder.DropTable(
                name: "CAT_Expedientes");

            migrationBuilder.DropTable(
                name: "CAT_Medico");

            migrationBuilder.DropTable(
                name: "CAT_Sucursal");
        }
    }
}
