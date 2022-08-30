using Integration.WeeClinic.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Integration.WeeClinic.Services
{
    public class LaboratoryService : Base
    {
        private static readonly int codEstatus_Seleccionar_Estudio = 14;
        private static readonly int codEstatus_Liberar_Estudio = 1;

        // Servcio 1. Consulta de folios
        // Dudas
        // Esta correcto que regrese IsActionPermitted: false?
        public static async Task<string> BusquedaFolios()
        {
            var url = "api/Laboratorio/Laboratorio_BusquedaFolios";

            var data = new Dictionary<string, string>()
            {
                ["NoFolio"] = folio3
            };

            var response = await PostService<string>(url, data);

            return response;
        }

        // Servicio 2. Consulta de estudios solicitados
        public static async Task<string> BuscaFolioLaboratorio()
        {
            var url = "api/Servicio/BuscaFolioLaboratorio";

            var data = new Dictionary<string, string>()
            {
                ["Folio"] = "234234234"
            };

            var response = await PostService<string>(url, data);

            return response;
        }

        #region Ejemplos respuesta BuscaFolioLaboratorio
        // Por que los 3 folios regresan el mismo resultado?
        // Por que datos viene vacio
        // Ejemplo AD00704422003227421X
        //{
        //  "IsOnline": false,
        //	"IsActionPermitted": false,
        //	"IsExists": false,
        //	"IsOk": true,
        //	"MensajeID": 0,
        //	"Mensaje": "",
        //	"TipoUsuario": null,
        //	"UserName": "",
        //	"Password": null,
        //	"ID": "00000000-0000-0000-0000-000000000000",
        //	"Fecha": "2022-08-25T16:53:11.411401+00:00",
        //	"URL": "",
        //	"NoFilas": 0,
        //	"Permiso": "SI",
        //	"dsRespuesta": {
        //      "Datos": [],
        //		"Datos1": [
        //          {
        //              "isVigente": 1,
        //				"Poliza": "1012209030845",
        //				"CodEstatus": 1,
        //				"Estatus": "Solicitada",
        //				"Cliente": "SKYWORKS SOLUTIONS DE MEXICO S DE RL DE CV",
        //				"idServicio": "35caf363-e8d4-430e-b22e-1f8d005fb4dd",
        //				"idNodo": "00000000-0000-0000-0000-000000000000",
        //				"idCliente": "b2957ff3-b4ff-46d8-ae98-bddd9fae188d",
        //				"idServicioParent": "6b1201a6-871a-4b8d-83a5-2dcbfaeb68bc",
        //				"idNodoParent": "00000000-0000-0000-0000-000000000000",
        //				"FolioSolicitud": "AD007044220032276WRT",
        //				"CODTIPOSERVICIO": 2,                                           No viene en documentacion
        //				"NoReferencia": "1125529",                                      No viene en documentacion
        //				"EdadPersona": 22,
        //				"IvaSucursal": "0"
        //          }
        //		]
        //	},
        //	"Nota": null
        //}

        // FOLIO NO VALIDO
        // Ejemplo AD00704422003227421Z
        //{
        //  "IsOnline": false,
        //	"IsActionPermitted": false,
        //	"IsExists": false,
        //	"IsOk": false,
        //	"MensajeID": 0,
        //	"Mensaje": "Cannot insert the value NULL into column 'idPersona', table 'dbHorusQP.dbo.Persona_Generales'; column does not allow nulls. INSERT fails.\r\nThe statement has been terminated.",
        //	"TipoUsuario": null,
        //	"UserName": "",
        //	"Password": null,
        //	"ID": "00000000-0000-0000-0000-000000000000",
        //	"Fecha": "2022-08-25T17:18:49.1689651+00:00",
        //	"URL": "",
        //	"NoFilas": 0,
        //	"Permiso": "SI",
        //	"dsRespuesta": null,
        //	"Nota": null
        //}

        // FOLIO NULL
        // Ejemplo null
        //{
        //  "IsOnline": false,
        //	"IsActionPermitted": false,
        //	"IsExists": false,
        //	"IsOk": false,
        //	"MensajeID": 0,
        //	"Mensaje": "",
        //	"TipoUsuario": null,
        //	"UserName": "",
        //	"Password": null,
        //	"ID": "00000000-0000-0000-0000-000000000000",
        //	"Fecha": "2022-08-25T17:20:31.7247689+00:00",
        //	"URL": "",
        //	"NoFilas": 0,
        //	"Permiso": "SI",
        //	"dsRespuesta": {
        //      "Datos": [],
        //		"Validacion": [
        //         {
        //           "Detail": "Folio is required"
        //         }
        //		]
        //	},
        //	"Nota": null
        //}

        // FOLIO CON ESTRUCTURA INVALIDA
        // Ejemplo 5dgdfgwefewfwefwef235243123123#$@#$#E@#FWFfsdfs4$%
        //{
        //  "IsOnline": false,
        //	"IsActionPermitted": false,
        //	"IsExists": false,
        //	"IsOk": false,
        //	"MensajeID": 0,
        //	"Mensaje": "",
        //	"TipoUsuario": null,
        //	"UserName": "",
        //	"Password": null,
        //	"ID": "00000000-0000-0000-0000-000000000000",
        //	"Fecha": "2022-08-25T17:23:29.4839359+00:00",
        //	"URL": "",
        //	"NoFilas": 0,
        //	"Permiso": "SI",
        //	"dsRespuesta": {
        //      "Datos": [],
        //		"Validacion": [
        //          {
        //            "Detail": "Folio Invalid type var isn't a IsString"
        //          }
        //		]
        //	},
        //	"Nota": null
        //}
        #endregion

        // Servicio 3. Consulta de precios por estudio
        public static async Task<string> GetPreciosEstudios_ByidServicio()
        {
            var url = "api/Inventarios/GetPreciosEstudios_ByidServicio";

            var data = new Dictionary<string, string>()
            {
                ["idNodo"] = "00000000-0000-0000-0000-000000000000",
                ["idServicio"] = "35caf363-e8d4-430e-b22e-1f8d005fb4dd",
                ["ClaveSucursal"] = "",
                ["codEstatus"] = codEstatus_Seleccionar_Estudio.ToString()
            };

            var response = await PostService<string>(url, data);

            return response;
        }

        // Servicio 4. Enviar token de validación de estudios
        public static async Task<string> ValidarCodigoPacienteLaboratorio()
        {
            var url = "api/Sesion/ValidarCodigoPacienteLaboratorio";

            var data = new Dictionary<string, string>()
            {
                ["idPersona"] = "",
                ["CodAccion"] = "",
                ["CodAcceso"] = "",
                ["CodMotivo"] = "",
                ["idServicio"] = ""
            };

            var response = await PostService<string>(url, data);

            return response;
        }

        // Servicio 5. Validación de Token
        public static async Task<string> Laboratorio_ValidaToken()
        {
            var url = "api/Servicio/Laboratorio_ValidaToken";

            var data = new Dictionary<string, string>()
            {
                ["idPersona"] = "",
                ["idOrden"] = "",
                ["Token"] = "",
                ["ClaveSucursal"] = ""
            };

            var response = await PostService<string>(url, data);

            return response;
        }

        // Servicio 6. Asignación de estudios
        public static async Task<string> Laboratorio_AsignaEstudio()
        {
            var url = "api/Laboratorio/Laboratorio_AsignaEstudio";

            var data = new Dictionary<string, string>()
            {
                ["Servicios"] = "",
                ["ClaveSucursal"] = ""
            };

            var response = await PostService<string>(url, data);

            return response;
        }

        // Servicio 7. Cancelación de estudio
        public static async Task<string> Laboratorio_CancelaEstudios_ByProveedor()
        {
            var url = "api/Laboratorio/Laboratorio_CancelaEstudios_ByProveedor";

            var data = new Dictionary<string, string>()
            {
                ["idServicio"] = "",
                ["idNodo"] = "",
                ["ClaveSucursal"] = ""
            };

            var response = await PostService<string>(url, data);

            return response;
        }

        // Servicio 8. Carga de resultados
        public static async Task<string> UploadFileAzure()
        {
            var url = "api/FileUpload/UploadFileAzure";

            var data = new Dictionary<string, string>()
            {
                ["UploadedImage"] = "",
            };

            var response = await PostService<string>(url, data);

            return response;
        }
    }
}