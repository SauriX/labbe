using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class InicialRestaurada : Migration
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
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioModificoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioModificoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaModifico = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_CFDI", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CAT_ciudadBranch",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_ciudadBranch", x => x.Id);
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
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioModificoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaModifico = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Clinica", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CAT_Configuracion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    Valor = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Configuracion", x => x.Id);
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
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioModificoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioModificoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaModifico = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioModificoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioModificoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioModificoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    UsuarioModificoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaModifico = table.Column<DateTime>(type: "smalldatetime", nullable: true),
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
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioModificoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaModifico = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Indicador", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CAT_ListaPrecio",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Visibilidad = table.Column<bool>(type: "bit", maxLength: 100, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModificoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaModifico = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_ListaPrecio", x => x.Id);
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
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioModificoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaModifico = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_ListaTrabajo", x => x.Id);
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
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioModificoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioModificoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioModificoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioModificoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaModifico = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Procedencia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CAT_Promocion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clave = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TipoDeDescuento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CantidadDescuento = table.Column<decimal>(type: "decimal(18,2)", maxLength: 100, nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFinal = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Visibilidad = table.Column<bool>(type: "bit", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModificoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModifico = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PrecioListaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Lunes = table.Column<bool>(type: "bit", nullable: false),
                    Martes = table.Column<bool>(type: "bit", nullable: false),
                    Miercoles = table.Column<bool>(type: "bit", nullable: false),
                    Jueves = table.Column<bool>(type: "bit", nullable: false),
                    Viernes = table.Column<bool>(type: "bit", nullable: false),
                    Sabado = table.Column<bool>(type: "bit", nullable: false),
                    Domingo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Promocion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CAT_Reactivo_Contpaq",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ClaveSistema = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NombreSistema = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    UsuarioModificoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaModifico = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Reactivo_Contpaq", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CAT_Tipo_Tapon",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Tipo_Tapon", x => x.Id);
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
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioModificoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaModifico = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_TipoMuestra", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CAT_Units",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioModificoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaModifico = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Units", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Relacion_Estudio_Sucursal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EstudioId = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioModId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioModificoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                name: "CAT_Lealtad",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TipoDescuento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CantidadDescuento = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaInicial = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFinal = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "date", nullable: false),
                    UsuarioModId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaMod = table.Column<DateTime>(type: "date", nullable: true),
                    PrecioListaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Lealtad", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CAT_Lealtad_CAT_ListaPrecio_PrecioListaId",
                        column: x => x.PrecioListaId,
                        principalTable: "CAT_ListaPrecio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CAT_Compañia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Contrasena = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EmailEmpresarial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NombreComercial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProcedenciaId = table.Column<int>(type: "int", nullable: false),
                    PrecioListaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PromocionesId = table.Column<int>(type: "int", nullable: true),
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
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Compañia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CAT_Compañia_CAT_ListaPrecio_PrecioListaId",
                        column: x => x.PrecioListaId,
                        principalTable: "CAT_ListaPrecio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CAT_Compañia_CAT_Procedencia_ProcedenciaId",
                        column: x => x.ProcedenciaId,
                        principalTable: "CAT_Procedencia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CAT_Compañia_CAT_Promocion_PromocionesId",
                        column: x => x.PromocionesId,
                        principalTable: "CAT_Promocion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CAT_ListaP_Promocion",
                columns: table => new
                {
                    PrecioListaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PromocionId = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UsuarioCreoId = table.Column<long>(type: "bigint", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_ListaP_Promocion", x => new { x.PrecioListaId, x.PromocionId });
                    table.ForeignKey(
                        name: "FK_CAT_ListaP_Promocion_CAT_ListaPrecio_PrecioListaId",
                        column: x => x.PrecioListaId,
                        principalTable: "CAT_ListaPrecio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CAT_ListaP_Promocion_CAT_Promocion_PromocionId",
                        column: x => x.PromocionId,
                        principalTable: "CAT_Promocion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CAT_Paquete",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AreaId = table.Column<int>(type: "int", nullable: false),
                    DepartamentoId = table.Column<int>(type: "int", nullable: false),
                    NombreLargo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Visibilidad = table.Column<bool>(type: "bit", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioModificoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaModifico = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Paquete", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CAT_Paquete_CAT_Area_AreaId",
                        column: x => x.AreaId,
                        principalTable: "CAT_Area",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CAT_Parametro",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ValorInicial = table.Column<decimal>(type: "decimal(18,2)", maxLength: 100, nullable: false),
                    TipoValor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: "0"),
                    NombreCorto = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Unidades = table.Column<int>(type: "int", nullable: false),
                    Formula = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DepartamentoId = table.Column<int>(type: "int", nullable: false),
                    DepartmentoId = table.Column<int>(type: "int", nullable: true),
                    AreaId = table.Column<int>(type: "int", nullable: false),
                    FormatoImpresionId = table.Column<int>(type: "int", nullable: false),
                    ReactivoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnidadSi = table.Column<int>(type: "int", nullable: false),
                    FCSI = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModificoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaModifico = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Parametro", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CAT_Parametro_CAT_Area_AreaId",
                        column: x => x.AreaId,
                        principalTable: "CAT_Area",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CAT_Parametro_CAT_Departamento_DepartmentoId",
                        column: x => x.DepartmentoId,
                        principalTable: "CAT_Departamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CAT_Parametro_Cat_Formato_FormatoImpresionId",
                        column: x => x.FormatoImpresionId,
                        principalTable: "Cat_Formato",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CAT_Parametro_CAT_Reactivo_Contpaq_ReactivoId",
                        column: x => x.ReactivoId,
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
                name: "Relacion_Promocion_Lealtad",
                columns: table => new
                {
                    PromotionId = table.Column<int>(type: "int", nullable: false),
                    LoyaltyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relacion_Promocion_Lealtad", x => new { x.PromotionId, x.LoyaltyId });
                    table.ForeignKey(
                        name: "FK_Relacion_Promocion_Lealtad_CAT_Lealtad_LoyaltyId",
                        column: x => x.LoyaltyId,
                        principalTable: "CAT_Lealtad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Relacion_Promocion_Lealtad_CAT_Promocion_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "CAT_Promocion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CAT_ListaP_Compañia",
                columns: table => new
                {
                    PrecioListaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompañiaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<long>(type: "bigint", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_ListaP_Compañia", x => new { x.PrecioListaId, x.CompañiaId });
                    table.ForeignKey(
                        name: "FK_CAT_ListaP_Compañia_CAT_Compañia_CompañiaId",
                        column: x => x.CompañiaId,
                        principalTable: "CAT_Compañia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CAT_ListaP_Compañia_CAT_ListaPrecio_PrecioListaId",
                        column: x => x.PrecioListaId,
                        principalTable: "CAT_ListaPrecio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompañiaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Apellidos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefono = table.Column<long>(type: "bigint", nullable: true),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioModId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contact_CAT_Compañia_CompañiaId",
                        column: x => x.CompañiaId,
                        principalTable: "CAT_Compañia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Relacion_ListaP_Paquete",
                columns: table => new
                {
                    PrecioListaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaqueteId = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UsuarioCreoId = table.Column<long>(type: "bigint", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relacion_ListaP_Paquete", x => new { x.PrecioListaId, x.PaqueteId });
                    table.ForeignKey(
                        name: "FK_Relacion_ListaP_Paquete_CAT_ListaPrecio_PrecioListaId",
                        column: x => x.PrecioListaId,
                        principalTable: "CAT_ListaPrecio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Relacion_ListaP_Paquete_CAT_Paquete_PaqueteId",
                        column: x => x.PaqueteId,
                        principalTable: "CAT_Paquete",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Relacion_Promocion_Paquete",
                columns: table => new
                {
                    PromotionId = table.Column<int>(type: "int", nullable: false),
                    PackId = table.Column<int>(type: "int", nullable: false),
                    Discountporcent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountNumeric = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FinalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Loyality = table.Column<bool>(type: "bit", nullable: false),
                    Lunes = table.Column<bool>(type: "bit", nullable: false),
                    Martes = table.Column<bool>(type: "bit", nullable: false),
                    Miercoles = table.Column<bool>(type: "bit", nullable: false),
                    Jueves = table.Column<bool>(type: "bit", nullable: false),
                    Viernes = table.Column<bool>(type: "bit", nullable: false),
                    Sabado = table.Column<bool>(type: "bit", nullable: false),
                    Domingo = table.Column<bool>(type: "bit", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFinal = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relacion_Promocion_Paquete", x => new { x.PromotionId, x.PackId });
                    table.ForeignKey(
                        name: "FK_Relacion_Promocion_Paquete_CAT_Paquete_PackId",
                        column: x => x.PackId,
                        principalTable: "CAT_Paquete",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Relacion_Promocion_Paquete_CAT_Promocion_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "CAT_Promocion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CAT_Tipo_Valor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParametroId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValorInicial = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorFinal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorInicialNumerico = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorFinalNumerico = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RangoEdadInicial = table.Column<int>(type: "int", nullable: false),
                    RangoEdadFinal = table.Column<int>(type: "int", nullable: false),
                    HombreValorInicial = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HombreValorFinal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MujerValorInicial = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MujerValorFinal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MedidaTiempoId = table.Column<byte>(type: "tinyint", nullable: false),
                    Opcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescripcionTexto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescripcionParrafo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModificoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaModifico = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Tipo_Valor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CAT_Tipo_Valor_CAT_Parametro_ParametroId",
                        column: x => x.ParametroId,
                        principalTable: "CAT_Parametro",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CAT_Maquilador",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clave = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: true),
                    PaginaWeb = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NumeroExterior = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    NumeroInterior = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Calle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ColoniaId = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    UsuarioModificoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaModifico = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Maquilador", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CAT_Maquilador_CAT_Colonia_ColoniaId",
                        column: x => x.ColoniaId,
                        principalTable: "CAT_Colonia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CAT_Medico",
                columns: table => new
                {
                    IdMedico = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PrimerApellido = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SegundoApellido = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EspecialidadId = table.Column<int>(type: "int", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CodigoPostal = table.Column<int>(type: "int", maxLength: 15, nullable: false),
                    EstadoId = table.Column<long>(type: "bigint", maxLength: 15, nullable: true),
                    CiudadId = table.Column<long>(type: "bigint", maxLength: 15, nullable: true),
                    NumeroExterior = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NumeroInterior = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Calle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ColoniaId = table.Column<int>(type: "int", nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Celular = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioModId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Medico", x => x.IdMedico);
                    table.ForeignKey(
                        name: "FK_CAT_Medico_CAT_Colonia_ColoniaId",
                        column: x => x.ColoniaId,
                        principalTable: "CAT_Colonia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CAT_Medico_CAT_Especialidad_EspecialidadId",
                        column: x => x.EspecialidadId,
                        principalTable: "CAT_Especialidad",
                        principalColumn: "Id",
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
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Codigopostal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumeroExterior = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumeroInterior = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Calle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PresupuestosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacturaciónId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Clinicos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServicioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    UsuarioModificoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaModifico = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    Matriz = table.Column<bool>(type: "bit", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "CAT_Estudio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clave = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Orden = table.Column<int>(type: "int", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NombreCorto = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Visible = table.Column<bool>(type: "bit", nullable: false),
                    DiasResultado = table.Column<int>(type: "int", nullable: false),
                    Dias = table.Column<int>(type: "int", nullable: false),
                    TiempoResultado = table.Column<int>(type: "int", nullable: false),
                    AreaId = table.Column<int>(type: "int", nullable: false),
                    DepartamentoId = table.Column<int>(type: "int", nullable: false),
                    FormatoId = table.Column<int>(type: "int", nullable: false),
                    MaquiladorId = table.Column<int>(type: "int", nullable: false),
                    MetodoId = table.Column<int>(type: "int", nullable: false),
                    SampleTypeId = table.Column<int>(type: "int", nullable: false),
                    TaponId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Prioridad = table.Column<bool>(type: "bit", nullable: false),
                    Urgencia = table.Column<bool>(type: "bit", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_CAT_Estudio_Cat_Formato_FormatoId",
                        column: x => x.FormatoId,
                        principalTable: "Cat_Formato",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CAT_Estudio_CAT_Maquilador_MaquiladorId",
                        column: x => x.MaquiladorId,
                        principalTable: "CAT_Maquilador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CAT_Estudio_CAT_Metodo_MetodoId",
                        column: x => x.MetodoId,
                        principalTable: "CAT_Metodo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CAT_Estudio_CAT_Tipo_Tapon_TaponId",
                        column: x => x.TaponId,
                        principalTable: "CAT_Tipo_Tapon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CAT_Estudio_CAT_TipoMuestra_SampleTypeId",
                        column: x => x.SampleTypeId,
                        principalTable: "CAT_TipoMuestra",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CAT_ListaP_Medicos",
                columns: table => new
                {
                    PrecioListaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<long>(type: "bigint", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_ListaP_Medicos", x => new { x.PrecioListaId, x.MedicoId });
                    table.ForeignKey(
                        name: "FK_CAT_ListaP_Medicos_CAT_ListaPrecio_PrecioListaId",
                        column: x => x.PrecioListaId,
                        principalTable: "CAT_ListaPrecio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CAT_ListaP_Medicos_CAT_Medico_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "CAT_Medico",
                        principalColumn: "IdMedico",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CAT_Medico_Clinica",
                columns: table => new
                {
                    MedicoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClinicaId = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                name: "CAT_ListaP_Sucursal",
                columns: table => new
                {
                    PrecioListaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SucursalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<long>(type: "bigint", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_ListaP_Sucursal", x => new { x.PrecioListaId, x.SucursalId });
                    table.ForeignKey(
                        name: "FK_CAT_ListaP_Sucursal_CAT_ListaPrecio_PrecioListaId",
                        column: x => x.PrecioListaId,
                        principalTable: "CAT_ListaPrecio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CAT_ListaP_Sucursal_CAT_Sucursal_SucursalId",
                        column: x => x.SucursalId,
                        principalTable: "CAT_Sucursal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CAT_Rutas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SucursalOrigenId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SucursalDestinoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MaquiladorId = table.Column<int>(type: "int", nullable: true),
                    RequierePaqueteria = table.Column<bool>(type: "bit", nullable: true),
                    SeguimientoPaqueteria = table.Column<int>(type: "int", nullable: true),
                    PaqueteriaId = table.Column<int>(type: "int", nullable: true),
                    Comentarios = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiasDeEntrega = table.Column<int>(type: "int", nullable: false),
                    HoraDeEntregaEstimada = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HoraDeEntrega = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HoraDeRecoleccion = table.Column<int>(type: "int", nullable: true),
                    TiempoDeEntrega = table.Column<int>(type: "int", nullable: false),
                    FormatoDeTiempoId = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EstudioId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaInicial = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaFinal = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdResponsableEnvio = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdResponsableRecepcion = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    UsuarioModificoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModifico = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    Lunes = table.Column<bool>(type: "bit", nullable: false),
                    Martes = table.Column<bool>(type: "bit", nullable: false),
                    Miercoles = table.Column<bool>(type: "bit", nullable: false),
                    Jueves = table.Column<bool>(type: "bit", nullable: false),
                    Viernes = table.Column<bool>(type: "bit", nullable: false),
                    Sabado = table.Column<bool>(type: "bit", nullable: false),
                    Domingo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Rutas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CAT_Rutas_CAT_Maquilador_MaquiladorId",
                        column: x => x.MaquiladorId,
                        principalTable: "CAT_Maquilador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CAT_Rutas_CAT_Paqueteria_PaqueteriaId",
                        column: x => x.PaqueteriaId,
                        principalTable: "CAT_Paqueteria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CAT_Rutas_CAT_Sucursal_SucursalDestinoId",
                        column: x => x.SucursalDestinoId,
                        principalTable: "CAT_Sucursal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CAT_Rutas_CAT_Sucursal_SucursalOrigenId",
                        column: x => x.SucursalOrigenId,
                        principalTable: "CAT_Sucursal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CAT_Sucursal_Departamento",
                columns: table => new
                {
                    SucursalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepartamentoId = table.Column<int>(type: "int", nullable: false),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Sucursal_Departamento", x => new { x.SucursalId, x.DepartamentoId });
                    table.ForeignKey(
                        name: "FK_CAT_Sucursal_Departamento_CAT_Departamento_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "CAT_Departamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CAT_Sucursal_Departamento_CAT_Sucursal_SucursalId",
                        column: x => x.SucursalId,
                        principalTable: "CAT_Sucursal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Relacion_Promocion_Sucursal",
                columns: table => new
                {
                    PromotionId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relacion_Promocion_Sucursal", x => new { x.PromotionId, x.BranchId });
                    table.ForeignKey(
                        name: "FK_Relacion_Promocion_Sucursal_CAT_Promocion_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "CAT_Promocion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Relacion_Promocion_Sucursal_CAT_Sucursal_BranchId",
                        column: x => x.BranchId,
                        principalTable: "CAT_Sucursal",
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
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    UsuarioModId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaMod = table.Column<DateTime>(type: "smalldatetime", nullable: true)
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
                name: "Relacion_Estudio_Paquete",
                columns: table => new
                {
                    PacketId = table.Column<int>(type: "int", nullable: false),
                    EstudioId = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModId = table.Column<int>(type: "int", nullable: true),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relacion_Estudio_Paquete", x => new { x.EstudioId, x.PacketId });
                    table.ForeignKey(
                        name: "FK_Relacion_Estudio_Paquete_CAT_Estudio_EstudioId",
                        column: x => x.EstudioId,
                        principalTable: "CAT_Estudio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Relacion_Estudio_Paquete_CAT_Paquete_PacketId",
                        column: x => x.PacketId,
                        principalTable: "CAT_Paquete",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Relacion_Estudio_Parametro",
                columns: table => new
                {
                    ParametroId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EstudioId = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModId = table.Column<int>(type: "int", nullable: true),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relacion_Estudio_Parametro", x => new { x.EstudioId, x.ParametroId });
                    table.ForeignKey(
                        name: "FK_Relacion_Estudio_Parametro_CAT_Estudio_EstudioId",
                        column: x => x.EstudioId,
                        principalTable: "CAT_Estudio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Relacion_Estudio_Parametro_CAT_Parametro_ParametroId",
                        column: x => x.ParametroId,
                        principalTable: "CAT_Parametro",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Relacion_Estudio_Reactivo",
                columns: table => new
                {
                    ReagentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EstudioId = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModId = table.Column<int>(type: "int", nullable: true),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relacion_Estudio_Reactivo", x => new { x.EstudioId, x.ReagentId });
                    table.ForeignKey(
                        name: "FK_Relacion_Estudio_Reactivo_CAT_Estudio_EstudioId",
                        column: x => x.EstudioId,
                        principalTable: "CAT_Estudio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Relacion_Estudio_Reactivo_CAT_Reactivo_Contpaq_ReagentId",
                        column: x => x.ReagentId,
                        principalTable: "CAT_Reactivo_Contpaq",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Relacion_Estudio_WorkList",
                columns: table => new
                {
                    WorkListId = table.Column<int>(type: "int", nullable: false),
                    EstudioId = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModId = table.Column<int>(type: "int", nullable: true),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relacion_Estudio_WorkList", x => new { x.EstudioId, x.WorkListId });
                    table.ForeignKey(
                        name: "FK_Relacion_Estudio_WorkList_CAT_Estudio_EstudioId",
                        column: x => x.EstudioId,
                        principalTable: "CAT_Estudio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Relacion_Estudio_WorkList_CAT_ListaTrabajo_WorkListId",
                        column: x => x.WorkListId,
                        principalTable: "CAT_ListaTrabajo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Relacion_ListaP_Estudio",
                columns: table => new
                {
                    PrecioListaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EstudioId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<long>(type: "bigint", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relacion_ListaP_Estudio", x => new { x.PrecioListaId, x.EstudioId });
                    table.ForeignKey(
                        name: "FK_Relacion_ListaP_Estudio_CAT_Estudio_EstudioId",
                        column: x => x.EstudioId,
                        principalTable: "CAT_Estudio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Relacion_ListaP_Estudio_CAT_ListaPrecio_PrecioListaId",
                        column: x => x.PrecioListaId,
                        principalTable: "CAT_ListaPrecio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Relacion_Promocion_Estudio",
                columns: table => new
                {
                    PromotionId = table.Column<int>(type: "int", nullable: false),
                    StudyId = table.Column<int>(type: "int", nullable: false),
                    Discountporcent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountNumeric = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FinalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Loyality = table.Column<bool>(type: "bit", nullable: false),
                    Lunes = table.Column<bool>(type: "bit", nullable: false),
                    Martes = table.Column<bool>(type: "bit", nullable: false),
                    Miercoles = table.Column<bool>(type: "bit", nullable: false),
                    Jueves = table.Column<bool>(type: "bit", nullable: false),
                    Viernes = table.Column<bool>(type: "bit", nullable: false),
                    Sabado = table.Column<bool>(type: "bit", nullable: false),
                    Domingo = table.Column<bool>(type: "bit", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFinal = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relacion_Promocion_Estudio", x => new { x.PromotionId, x.StudyId });
                    table.ForeignKey(
                        name: "FK_Relacion_Promocion_Estudio_CAT_Estudio_StudyId",
                        column: x => x.StudyId,
                        principalTable: "CAT_Estudio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Relacion_Promocion_Estudio_CAT_Promocion_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "CAT_Promocion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Relacion_Ruta_Estudio",
                columns: table => new
                {
                    RouteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EstudioId = table.Column<int>(type: "int", nullable: false),
                    UsuarioCreoId = table.Column<long>(type: "bigint", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relacion_Ruta_Estudio", x => new { x.RouteId, x.EstudioId });
                    table.ForeignKey(
                        name: "FK_Relacion_Ruta_Estudio_CAT_Estudio_EstudioId",
                        column: x => x.EstudioId,
                        principalTable: "CAT_Estudio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Relacion_Ruta_Estudio_CAT_Rutas_RouteId",
                        column: x => x.RouteId,
                        principalTable: "CAT_Rutas",
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
                name: "IX_CAT_Compañia_PrecioListaId",
                table: "CAT_Compañia",
                column: "PrecioListaId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Compañia_ProcedenciaId",
                table: "CAT_Compañia",
                column: "ProcedenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Compañia_PromocionesId",
                table: "CAT_Compañia",
                column: "PromocionesId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Estudio_AreaId",
                table: "CAT_Estudio",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Estudio_FormatoId",
                table: "CAT_Estudio",
                column: "FormatoId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Estudio_MaquiladorId",
                table: "CAT_Estudio",
                column: "MaquiladorId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Estudio_MetodoId",
                table: "CAT_Estudio",
                column: "MetodoId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Estudio_SampleTypeId",
                table: "CAT_Estudio",
                column: "SampleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Estudio_TaponId",
                table: "CAT_Estudio",
                column: "TaponId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Lealtad_PrecioListaId",
                table: "CAT_Lealtad",
                column: "PrecioListaId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_ListaP_Compañia_CompañiaId",
                table: "CAT_ListaP_Compañia",
                column: "CompañiaId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_ListaP_Medicos_MedicoId",
                table: "CAT_ListaP_Medicos",
                column: "MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_ListaP_Promocion_PromocionId",
                table: "CAT_ListaP_Promocion",
                column: "PromocionId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_ListaP_Sucursal_SucursalId",
                table: "CAT_ListaP_Sucursal",
                column: "SucursalId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Maquilador_ColoniaId",
                table: "CAT_Maquilador",
                column: "ColoniaId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Medico_ColoniaId",
                table: "CAT_Medico",
                column: "ColoniaId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Medico_EspecialidadId",
                table: "CAT_Medico",
                column: "EspecialidadId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Medico_Clinica_ClinicaId",
                table: "CAT_Medico_Clinica",
                column: "ClinicaId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Paquete_AreaId",
                table: "CAT_Paquete",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Parametro_AreaId",
                table: "CAT_Parametro",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Parametro_DepartmentoId",
                table: "CAT_Parametro",
                column: "DepartmentoId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Parametro_FormatoImpresionId",
                table: "CAT_Parametro",
                column: "FormatoImpresionId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Parametro_ReactivoId",
                table: "CAT_Parametro",
                column: "ReactivoId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Rutas_MaquiladorId",
                table: "CAT_Rutas",
                column: "MaquiladorId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Rutas_PaqueteriaId",
                table: "CAT_Rutas",
                column: "PaqueteriaId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Rutas_SucursalDestinoId",
                table: "CAT_Rutas",
                column: "SucursalDestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Rutas_SucursalOrigenId",
                table: "CAT_Rutas",
                column: "SucursalOrigenId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Sucursal_ColoniaId",
                table: "CAT_Sucursal",
                column: "ColoniaId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Sucursal_Departamento_DepartamentoId",
                table: "CAT_Sucursal_Departamento",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Tipo_Valor_ParametroId",
                table: "CAT_Tipo_Valor",
                column: "ParametroId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_CompañiaId",
                table: "Contact",
                column: "CompañiaId");

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Estudio_Indicacion_IndicacionId",
                table: "Relacion_Estudio_Indicacion",
                column: "IndicacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Estudio_Paquete_PacketId",
                table: "Relacion_Estudio_Paquete",
                column: "PacketId");

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Estudio_Parametro_ParametroId",
                table: "Relacion_Estudio_Parametro",
                column: "ParametroId");

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Estudio_Reactivo_ReagentId",
                table: "Relacion_Estudio_Reactivo",
                column: "ReagentId");

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Estudio_WorkList_WorkListId",
                table: "Relacion_Estudio_WorkList",
                column: "WorkListId");

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_ListaP_Estudio_EstudioId",
                table: "Relacion_ListaP_Estudio",
                column: "EstudioId");

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_ListaP_Paquete_PaqueteId",
                table: "Relacion_ListaP_Paquete",
                column: "PaqueteId");

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Promocion_Estudio_StudyId",
                table: "Relacion_Promocion_Estudio",
                column: "StudyId");

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Promocion_Lealtad_LoyaltyId",
                table: "Relacion_Promocion_Lealtad",
                column: "LoyaltyId");

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Promocion_Paquete_PackId",
                table: "Relacion_Promocion_Paquete",
                column: "PackId");

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Promocion_Sucursal_BranchId",
                table: "Relacion_Promocion_Sucursal",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Ruta_Estudio_EstudioId",
                table: "Relacion_Ruta_Estudio",
                column: "EstudioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CAT_Banco");

            migrationBuilder.DropTable(
                name: "CAT_CFDI");

            migrationBuilder.DropTable(
                name: "CAT_ciudadBranch");

            migrationBuilder.DropTable(
                name: "CAT_Configuracion");

            migrationBuilder.DropTable(
                name: "CAT_Dimension");

            migrationBuilder.DropTable(
                name: "CAT_FormaPago");

            migrationBuilder.DropTable(
                name: "CAT_Indicador");

            migrationBuilder.DropTable(
                name: "CAT_ListaP_Compañia");

            migrationBuilder.DropTable(
                name: "CAT_ListaP_Medicos");

            migrationBuilder.DropTable(
                name: "CAT_ListaP_Promocion");

            migrationBuilder.DropTable(
                name: "CAT_ListaP_Sucursal");

            migrationBuilder.DropTable(
                name: "CAT_Medico_Clinica");

            migrationBuilder.DropTable(
                name: "CAT_MetodoPago");

            migrationBuilder.DropTable(
                name: "CAT_Sucursal_Departamento");

            migrationBuilder.DropTable(
                name: "CAT_Tipo_Valor");

            migrationBuilder.DropTable(
                name: "CAT_Units");

            migrationBuilder.DropTable(
                name: "Contact");

            migrationBuilder.DropTable(
                name: "Relacion_Estudio_Indicacion");

            migrationBuilder.DropTable(
                name: "Relacion_Estudio_Paquete");

            migrationBuilder.DropTable(
                name: "Relacion_Estudio_Parametro");

            migrationBuilder.DropTable(
                name: "Relacion_Estudio_Reactivo");

            migrationBuilder.DropTable(
                name: "Relacion_Estudio_Sucursal");

            migrationBuilder.DropTable(
                name: "Relacion_Estudio_WorkList");

            migrationBuilder.DropTable(
                name: "Relacion_ListaP_Estudio");

            migrationBuilder.DropTable(
                name: "Relacion_ListaP_Paquete");

            migrationBuilder.DropTable(
                name: "Relacion_Promocion_Estudio");

            migrationBuilder.DropTable(
                name: "Relacion_Promocion_Lealtad");

            migrationBuilder.DropTable(
                name: "Relacion_Promocion_Paquete");

            migrationBuilder.DropTable(
                name: "Relacion_Promocion_Sucursal");

            migrationBuilder.DropTable(
                name: "Relacion_Ruta_Estudio");

            migrationBuilder.DropTable(
                name: "CAT_Clinica");

            migrationBuilder.DropTable(
                name: "CAT_Medico");

            migrationBuilder.DropTable(
                name: "CAT_Compañia");

            migrationBuilder.DropTable(
                name: "CAT_Indicacion");

            migrationBuilder.DropTable(
                name: "CAT_Parametro");

            migrationBuilder.DropTable(
                name: "CAT_ListaTrabajo");

            migrationBuilder.DropTable(
                name: "CAT_Lealtad");

            migrationBuilder.DropTable(
                name: "CAT_Paquete");

            migrationBuilder.DropTable(
                name: "CAT_Estudio");

            migrationBuilder.DropTable(
                name: "CAT_Rutas");

            migrationBuilder.DropTable(
                name: "CAT_Especialidad");

            migrationBuilder.DropTable(
                name: "CAT_Procedencia");

            migrationBuilder.DropTable(
                name: "CAT_Promocion");

            migrationBuilder.DropTable(
                name: "CAT_Reactivo_Contpaq");

            migrationBuilder.DropTable(
                name: "CAT_ListaPrecio");

            migrationBuilder.DropTable(
                name: "CAT_Area");

            migrationBuilder.DropTable(
                name: "Cat_Formato");

            migrationBuilder.DropTable(
                name: "CAT_Metodo");

            migrationBuilder.DropTable(
                name: "CAT_Tipo_Tapon");

            migrationBuilder.DropTable(
                name: "CAT_TipoMuestra");

            migrationBuilder.DropTable(
                name: "CAT_Maquilador");

            migrationBuilder.DropTable(
                name: "CAT_Paqueteria");

            migrationBuilder.DropTable(
                name: "CAT_Sucursal");

            migrationBuilder.DropTable(
                name: "CAT_Departamento");

            migrationBuilder.DropTable(
                name: "CAT_Colonia");

            migrationBuilder.DropTable(
                name: "CAT_Ciudad");

            migrationBuilder.DropTable(
                name: "CAT_Estado");
        }
    }
}
