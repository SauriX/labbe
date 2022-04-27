using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CAT_Banco",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModificoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModifico = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Banco", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CAT_CFDI",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModificoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModifico = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_CFDI", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CAT_Clinica",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clave = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModificoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModifico = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Clinica", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CAT_Departamento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModificoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModifico = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Departamento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CAT_Dimension",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Largo = table.Column<byte>(type: "tinyint", nullable: false),
                    Ancho = table.Column<byte>(type: "tinyint", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModificoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModifico = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Dimension", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CAT_Especialidad",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModificoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModifico = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Especialidad", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CAT_Estado",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Estado", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CAT_FormaPago",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModificoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModifico = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_FormaPago", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cat_Formato",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModificoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModifico = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cat_Formato", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CAT_Indicacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clave = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModificoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModifico = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Indicacion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CAT_Indicador",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModificoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModifico = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Indicador", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CAT_ListaTrabajo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModificoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModifico = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_ListaTrabajo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CAT_ListPrecio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clave = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Visibilidad = table.Column<bool>(type: "bit", maxLength: 100, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModificoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModifico = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_ListPrecio", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CAT_Medico",
                columns: table => new
                {
                    IdMedico = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clave = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PrimerApellido = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SegundoApellido = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EspecialidadId = table.Column<long>(type: "bigint", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CodigoPostal = table.Column<int>(type: "int", maxLength: 15, nullable: false),
                    EstadoId = table.Column<long>(type: "bigint", maxLength: 15, nullable: true),
                    CiudadId = table.Column<long>(type: "bigint", maxLength: 15, nullable: true),
                    NumeroExterior = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    NumeroInterior = table.Column<int>(type: "int", maxLength: 100, nullable: true),
                    Calle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ColoniaId = table.Column<long>(type: "bigint", maxLength: 15, nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Celular = table.Column<long>(type: "bigint", nullable: true),
                    Telefono = table.Column<long>(type: "bigint", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<int>(type: "int", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModId = table.Column<int>(type: "int", nullable: false),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Medico", x => x.IdMedico);
                });

            migrationBuilder.CreateTable(
                name: "CAT_Metodo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModificoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModifico = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Metodo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CAT_MetodoPago",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModificoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModifico = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_MetodoPago", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CAT_Paqueteria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModificoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModifico = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Paqueteria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CAT_Procedencia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clave = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModificoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModifico = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Procedencia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CAT_Reactivo_Contpaq",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clave = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ClaveSistema = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    NombreSistema = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    UsuarioModificoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModifico = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Reactivo_Contpaq", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CAT_TipoMuestra",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModificoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModifico = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_TipoMuestra", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Promotion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoDeDescuento = table.Column<bool>(type: "bit", nullable: false),
                    CantidadDescuento = table.Column<float>(type: "real", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFinal = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Visibilidad = table.Column<bool>(type: "bit", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModificoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModifico = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promotion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Relacion_Estudio_Sucursal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EstudioId = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relacion_Estudio_Sucursal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CAT_Area",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartamentoId = table.Column<int>(type: "int", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModificoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModifico = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Area", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CAT_Area_CAT_Departamento_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "CAT_Departamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CAT_Ciudad",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EstadoId = table.Column<byte>(type: "tinyint", nullable: false),
                    Ciudad = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Ciudad", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CAT_Ciudad_CAT_Estado_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "CAT_Estado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CAT_Medico_Clinica",
                columns: table => new
                {
                    MedicoId = table.Column<int>(type: "int", nullable: false),
                    ClinicaId = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<long>(type: "bigint", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Medico_Clinica", x => new { x.MedicoId, x.ClinicaId });
                    table.ForeignKey(
                        name: "FK_CAT_Medico_Clinica_CAT_Clinica_ClinicaId",
                        column: x => x.ClinicaId,
                        principalTable: "CAT_Clinica",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CAT_Medico_Clinica_CAT_Medico_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "CAT_Medico",
                        principalColumn: "IdMedico",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CAT_Compañia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clave = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Contrasena = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EmailEmpresarial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NombreComercial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProcedenciaId = table.Column<int>(type: "int", nullable: false),
                    ListaPrecioId = table.Column<int>(type: "int", nullable: true),
                    PromocionesId = table.Column<long>(type: "bigint", nullable: true),
                    RFC = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CodigoPostal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ciudad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RazonSocial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MetodoDePagoId = table.Column<int>(type: "int", nullable: false),
                    FormaDePagoId = table.Column<int>(type: "int", nullable: true),
                    LimiteDeCredito = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiasCredito = table.Column<int>(type: "int", nullable: true),
                    CFDIId = table.Column<int>(type: "int", nullable: true),
                    NumeroDeCuenta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BancoId = table.Column<int>(type: "int", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<int>(type: "int", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModId = table.Column<int>(type: "int", nullable: true),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Compañia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CAT_Compañia_CAT_Procedencia_ProcedenciaId",
                        column: x => x.ProcedenciaId,
                        principalTable: "CAT_Procedencia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CAT_ListaP_Promocion",
                columns: table => new
                {
                    PrecioId = table.Column<int>(type: "int", nullable: false),
                    PromocionId = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<long>(type: "bigint", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PriceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_ListaP_Promocion", x => new { x.PrecioId, x.PromocionId });
                    table.ForeignKey(
                        name: "FK_CAT_ListaP_Promocion_CAT_ListPrecio_PrecioId",
                        column: x => x.PrecioId,
                        principalTable: "CAT_ListPrecio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CAT_ListaP_Promocion_CAT_ListPrecio_PriceId",
                        column: x => x.PriceId,
                        principalTable: "CAT_ListPrecio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CAT_ListaP_Promocion_Promotion_PromocionId",
                        column: x => x.PromocionId,
                        principalTable: "Promotion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CAT_Estudio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Orden = table.Column<int>(type: "int", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NombreCorto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Visible = table.Column<bool>(type: "bit", nullable: false),
                    DiasResultado = table.Column<int>(type: "int", nullable: false),
                    AreaId = table.Column<int>(type: "int", nullable: false),
                    DepartamentoId = table.Column<int>(type: "int", nullable: false),
                    FormatoId = table.Column<int>(type: "int", nullable: false),
                    MaquiladorId = table.Column<int>(type: "int", nullable: false),
                    MetodoId = table.Column<int>(type: "int", nullable: false),
                    TipoMuestraId = table.Column<int>(type: "int", nullable: false),
                    TiempoRespuesta = table.Column<int>(type: "int", nullable: false),
                    Prioridad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<int>(type: "int", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModId = table.Column<int>(type: "int", nullable: false),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Estudio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CAT_Estudio_CAT_Area_AreaId",
                        column: x => x.AreaId,
                        principalTable: "CAT_Area",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CAT_Parametro",
                columns: table => new
                {
                    IdParametro = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ValorInicial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NombreCorto = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Unidades = table.Column<double>(type: "float", nullable: false),
                    Formula = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Formato = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DepartamentId = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: true),
                    AreaId = table.Column<int>(type: "int", nullable: false),
                    FormatId = table.Column<int>(type: "int", nullable: false),
                    ReagentId = table.Column<int>(type: "int", nullable: false),
                    UnidadSi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FCSI = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Parametro", x => x.IdParametro);
                    table.ForeignKey(
                        name: "FK_CAT_Parametro_CAT_Area_AreaId",
                        column: x => x.AreaId,
                        principalTable: "CAT_Area",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CAT_Parametro_CAT_Departamento_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "CAT_Departamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CAT_Parametro_Cat_Formato_FormatId",
                        column: x => x.FormatId,
                        principalTable: "Cat_Formato",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CAT_Parametro_CAT_Reactivo_Contpaq_ReagentId",
                        column: x => x.ReagentId,
                        principalTable: "CAT_Reactivo_Contpaq",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CAT_Colonia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CiudadId = table.Column<short>(type: "smallint", nullable: false),
                    Colonia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodigoPostal = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Colonia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CAT_Colonia_CAT_Ciudad_CiudadId",
                        column: x => x.CiudadId,
                        principalTable: "CAT_Ciudad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CAT_CompañiaContacto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompañiaId = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Apellidos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefono = table.Column<long>(type: "bigint", nullable: true),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<int>(type: "int", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModId = table.Column<int>(type: "int", nullable: true),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_CompañiaContacto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CAT_CompañiaContacto_CAT_Compañia_CompañiaId",
                        column: x => x.CompañiaId,
                        principalTable: "CAT_Compañia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CAT_ListaP_Compañia",
                columns: table => new
                {
                    PrecioId = table.Column<int>(type: "int", nullable: false),
                    CompañiaId = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<long>(type: "bigint", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_ListaP_Compañia", x => new { x.PrecioId, x.CompañiaId });
                    table.ForeignKey(
                        name: "FK_CAT_ListaP_Compañia_CAT_Compañia_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "CAT_Compañia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CAT_ListaP_Compañia_CAT_ListPrecio_PrecioId",
                        column: x => x.PrecioId,
                        principalTable: "CAT_ListPrecio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Relacion_Estudio_Indicacion",
                columns: table => new
                {
                    IndicacionId = table.Column<int>(type: "int", nullable: false),
                    EstudioId = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModId = table.Column<int>(type: "int", nullable: true),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relacion_Estudio_Indicacion", x => new { x.EstudioId, x.IndicacionId });
                    table.ForeignKey(
                        name: "FK_Relacion_Estudio_Indicacion_CAT_Estudio_EstudioId",
                        column: x => x.EstudioId,
                        principalTable: "CAT_Estudio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Relacion_Estudio_Indicacion_CAT_Indicacion_IndicacionId",
                        column: x => x.IndicacionId,
                        principalTable: "CAT_Indicacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CAT_Tipo_Valor",
                columns: table => new
                {
                    IdTipo_Valor = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdParametro = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    parametresIdParametro = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValorInicial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValorFinal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValorInicialNumerico = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValorFinalNumerico = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RangoEdadInicial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RangoEdadFinal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HombreValorInicial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HombreValorFinal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MujerValorInicial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MujerValorFinal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MedidaTiempo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Opcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescripcionTexto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescripcionParrafo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Tipo_Valor", x => x.IdTipo_Valor);
                    table.ForeignKey(
                        name: "FK_CAT_Tipo_Valor_CAT_Parametro_parametresIdParametro",
                        column: x => x.parametresIdParametro,
                        principalTable: "CAT_Parametro",
                        principalColumn: "IdParametro",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CAT_Sucursal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ColoniaId = table.Column<int>(type: "int", nullable: false),
                    Ciudad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    colony = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Codigopostal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumeroExterior = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumeroInterior = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Calle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefono = table.Column<long>(type: "bigint", nullable: true),
                    PresupuestosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacturaciónId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClinicosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServicioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    UsuarioModificoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaModifico = table.Column<DateTime>(type: "smalldatetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Sucursal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CAT_Sucursal_CAT_Colonia_ColoniaId",
                        column: x => x.ColoniaId,
                        principalTable: "CAT_Colonia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Area_DepartamentoId",
                table: "CAT_Area",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Ciudad_EstadoId",
                table: "CAT_Ciudad",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Colonia_CiudadId",
                table: "CAT_Colonia",
                column: "CiudadId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Compañia_ProcedenciaId",
                table: "CAT_Compañia",
                column: "ProcedenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_CompañiaContacto_CompañiaId",
                table: "CAT_CompañiaContacto",
                column: "CompañiaId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Estudio_AreaId",
                table: "CAT_Estudio",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_ListaP_Compañia_CompanyId",
                table: "CAT_ListaP_Compañia",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_ListaP_Promocion_PriceId",
                table: "CAT_ListaP_Promocion",
                column: "PriceId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_ListaP_Promocion_PromocionId",
                table: "CAT_ListaP_Promocion",
                column: "PromocionId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Medico_Clinica_ClinicaId",
                table: "CAT_Medico_Clinica",
                column: "ClinicaId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Parametro_AreaId",
                table: "CAT_Parametro",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Parametro_DepartmentId",
                table: "CAT_Parametro",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Parametro_FormatId",
                table: "CAT_Parametro",
                column: "FormatId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Parametro_ReagentId",
                table: "CAT_Parametro",
                column: "ReagentId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Sucursal_ColoniaId",
                table: "CAT_Sucursal",
                column: "ColoniaId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Tipo_Valor_parametresIdParametro",
                table: "CAT_Tipo_Valor",
                column: "parametresIdParametro");

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Estudio_Indicacion_IndicacionId",
                table: "Relacion_Estudio_Indicacion",
                column: "IndicacionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CAT_Banco");

            migrationBuilder.DropTable(
                name: "CAT_CFDI");

            migrationBuilder.DropTable(
                name: "CAT_CompañiaContacto");

            migrationBuilder.DropTable(
                name: "CAT_Dimension");

            migrationBuilder.DropTable(
                name: "CAT_Especialidad");

            migrationBuilder.DropTable(
                name: "CAT_FormaPago");

            migrationBuilder.DropTable(
                name: "CAT_Indicador");

            migrationBuilder.DropTable(
                name: "CAT_ListaP_Compañia");

            migrationBuilder.DropTable(
                name: "CAT_ListaP_Promocion");

            migrationBuilder.DropTable(
                name: "CAT_ListaTrabajo");

            migrationBuilder.DropTable(
                name: "CAT_Medico_Clinica");

            migrationBuilder.DropTable(
                name: "CAT_Metodo");

            migrationBuilder.DropTable(
                name: "CAT_MetodoPago");

            migrationBuilder.DropTable(
                name: "CAT_Paqueteria");

            migrationBuilder.DropTable(
                name: "CAT_Sucursal");

            migrationBuilder.DropTable(
                name: "CAT_Tipo_Valor");

            migrationBuilder.DropTable(
                name: "CAT_TipoMuestra");

            migrationBuilder.DropTable(
                name: "Relacion_Estudio_Indicacion");

            migrationBuilder.DropTable(
                name: "Relacion_Estudio_Sucursal");

            migrationBuilder.DropTable(
                name: "CAT_Compañia");

            migrationBuilder.DropTable(
                name: "CAT_ListPrecio");

            migrationBuilder.DropTable(
                name: "Promotion");

            migrationBuilder.DropTable(
                name: "CAT_Clinica");

            migrationBuilder.DropTable(
                name: "CAT_Medico");

            migrationBuilder.DropTable(
                name: "CAT_Colonia");

            migrationBuilder.DropTable(
                name: "CAT_Parametro");

            migrationBuilder.DropTable(
                name: "CAT_Estudio");

            migrationBuilder.DropTable(
                name: "CAT_Indicacion");

            migrationBuilder.DropTable(
                name: "CAT_Procedencia");

            migrationBuilder.DropTable(
                name: "CAT_Ciudad");

            migrationBuilder.DropTable(
                name: "Cat_Formato");

            migrationBuilder.DropTable(
                name: "CAT_Reactivo_Contpaq");

            migrationBuilder.DropTable(
                name: "CAT_Area");

            migrationBuilder.DropTable(
                name: "CAT_Estado");

            migrationBuilder.DropTable(
                name: "CAT_Departamento");
        }
    }
}
