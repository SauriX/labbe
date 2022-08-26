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


        public static async Task<string> BusquedaFolios()
        {
            try
            {
                var url = $"{baseUrl}/API/api/Laboratorio/Laboratorio_BusquedaFolios";

                var loginData = new Dictionary<string, string>()
                {
                    ["NoFolio"] = folio3
                };

                using HttpClient client = new();

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                client.DefaultRequestHeaders.Add("TokenClienteCert", certKey);

                var response = await client.PostAsync(url, new FormUrlEncodedContent(loginData));

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    return data;
                }

                if ((int)response.StatusCode == 400)
                {
                    var error = await response.Content.ReadFromJsonAsync<ErrorResponse>();
                    throw new Exception(string.Concat(error.Error, Environment.NewLine, "Descripción: ", error.ErrorDescription));
                }

                if ((int)response.StatusCode == 401 || (int)response.StatusCode == 403)
                {
                    throw new Exception("Error de autenticación, token invalido o sin permisos con WeeClinic");
                }

                throw new Exception("Ha ocurrido un error al buscar folio con WeeClinic");
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region Ejemplos respuesta BusquedaFolios
        // Esta bien que regrese IsActionPermitted: false?
        // Ejemplo AD00704422003227421X
        //{
        //  "IsOnline": false,
        //	"IsActionPermitted": false,
        //	"IsExists": false,
        //	"IsOk": true,
        //	"MensajeID": 0,
        //	"Mensaje": "",
        //	"TipoUsuario": null,
        //	"UserName": "mfarias@axsistec.com :: a4b419d2-18ce-45d1-ae28-f51c2cc822f7 :: a4b419d2-18ce-45d1-ae28-f51c2cc822f7",
        //	"Password": null,
        //	"ID": "00000000-0000-0000-0000-000000000000",
        //	"Fecha": "2022-08-25T15:53:35.1724695+00:00",
        //	"URL": "",
        //	"NoFilas": 1,
        //	"Permiso": "SI",
        //	"dsRespuesta": {
        //      "Datos": [
        //          {
        //              "idOrden": "a11c4d76-878e-4ec2-a7b9-03aeee28ddf6",
        //				"FolioOrden": "AD00704422003227421X",
        //				"xDateInsert": "2022-08-17T16:33:46.497",
        //				"FechaFolio": "17/08/2022",
        //				"idProducto": "930a0ea6-2620-4dd9-a144-25a1e9bea7f8",
        //				"CorporativoNombre": "SKYWORKS SOLUTIONS DE MEXICO S DE RL DE CV",
        //				"ProductoNombre": "MULTISALUD COL ANGIE",
        //				"NoPoliza": "1012209030845",
        //				"idPersona": "4254656b-f760-470b-b807-25fc5d11b37d",
        //				"Nombre": "Karla Maria",
        //				"Paterno": "sanchez",
        //				"Materno": "rodriguez",
        //				"NombreCompleto": "Karla Maria sanchez rodriguez",
        //				"CURP": "SARK000525MBCNDRA4",
        //				"codGenero": "F",
        //				"Genero": "Mujer",
        //				"RFC": "SARK0005251H9",
        //				"Edad": 22,
        //				"Busqueda": "AD00704422003227421X",
        //				"TPA": "General de Salud / General de Seguros",
        //				"isEstatus": 1,
        //				"Copagos": "25",
        //				"isTyC": 1,
        //				"NombreCompleto_Medico": "Gerardo Lopez Martinez",
        //				"EstatusVigencia": 1
        //          }
        //		]
        //	},
        //	"Nota": null
        //}

        // FOLIO QUE NO EXISTE
        // Ejemplo AD00704422003227421Xtest
        //{
        //  "IsOnline": false,
        //	"IsActionPermitted": false,
        //	"IsExists": false,
        //	"IsOk": true,
        //	"MensajeID": 0,
        //	"Mensaje": "",
        //	"TipoUsuario": null,
        //	"UserName": "mfarias@axsistec.com :: a4b419d2-18ce-45d1-ae28-f51c2cc822f7 :: a4b419d2-18ce-45d1-ae28-f51c2cc822f7",
        //	"Password": null,
        //	"ID": "00000000-0000-0000-0000-000000000000",
        //	"Fecha": "2022-08-25T16:00:57.6783022+00:00",
        //	"URL": "",
        //	"NoFilas": 0,
        //	"Permiso": "SI",
        //	"dsRespuesta": {
        //      "Datos": []
        //  },
        //	"Nota": null
        //}

        // FOLIO NULL
        // Ejemplo null
        //{
        //  "IsOnline": false,
        //	"IsActionPermitted": false,
        //	"IsExists": false,
        //	"IsOk": true,
        //	"MensajeID": 0,
        //	"Mensaje": "",
        //	"TipoUsuario": null,
        //	"UserName": "mfarias@axsistec.com :: a4b419d2-18ce-45d1-ae28-f51c2cc822f7 :: a4b419d2-18ce-45d1-ae28-f51c2cc822f7",
        //	"Password": null,
        //	"ID": "00000000-0000-0000-0000-000000000000",
        //	"Fecha": "2022-08-25T16:14:11.1763488+00:00",
        //	"URL": "",
        //	"NoFilas": 1,
        //	"Permiso": "SI",
        //	"dsRespuesta": {
        //        "Datos": [
        //            {
        //            "Mensaje": "El folio no pertenece a su red."
        //            }
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
        //	"UserName": "mfarias@axsistec.com :: a4b419d2-18ce-45d1-ae28-f51c2cc822f7 :: a4b419d2-18ce-45d1-ae28-f51c2cc822f7",
        //	"Password": null,
        //	"ID": "00000000-0000-0000-0000-000000000000",
        //	"Fecha": "2022-08-25T16:23:19.2828288+00:00",
        //	"URL": "",
        //	"NoFilas": 0,
        //	"Permiso": "SI",
        //	"dsRespuesta": {
        //      "Datos": [],
        //		"Validacion": [
        //          {
        //            "Detail": "NoFolio Invalid type var isn't a IsString"
        //          }
        //		]
        //	},
        //	"Nota": null
        //}
        #endregion

        public static async Task<string> BuscaFolioLaboratorio()
        {
            try
            {
                var url = $"{baseUrl}/API/api/Servicio/BuscaFolioLaboratorio";

                var loginData = new Dictionary<string, string>()
                {
                    ["Folio"] = "234234234"
                };

                using HttpClient client = new();

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                client.DefaultRequestHeaders.Add("TokenClienteCert", certKey);

                var response = await client.PostAsync(url, new FormUrlEncodedContent(loginData));

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    return data;
                }

                if ((int)response.StatusCode == 400)
                {
                    var error = await response.Content.ReadFromJsonAsync<ErrorResponse>();
                    throw new Exception(string.Concat(error.Error, Environment.NewLine, "Descripción: ", error.ErrorDescription));
                }

                if ((int)response.StatusCode == 401 || (int)response.StatusCode == 403)
                {
                    throw new Exception("Error de autenticación, token invalido o sin permisos con WeeClinic");
                }

                throw new Exception("Ha ocurrido un error al buscar folio con WeeClinic");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<string> GetPreciosEstudios_ByidServicio()
        {
            try
            {
                var url = $"{baseUrl}/API/api/Inventarios/GetPreciosEstudios_ByidServicio";

                var loginData = new Dictionary<string, string>()
                {
                    ["idNodo"] = "00000000-0000-0000-0000-000000000000",
                    ["idServicio"] = "35caf363-e8d4-430e-b22e-1f8d005fb4dd",
                    ["ClaveSucursal"] = "",
                    ["codEstatus"] = codEstatus_Seleccionar_Estudio.ToString(),
                };

                using HttpClient client = new();

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                client.DefaultRequestHeaders.Add("TokenClienteCert", certKey);

                var response = await client.PostAsync(url, new FormUrlEncodedContent(loginData));

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    return data;
                }

                if ((int)response.StatusCode == 400)
                {
                    var error = await response.Content.ReadFromJsonAsync<ErrorResponse>();
                    throw new Exception(string.Concat(error.Error, Environment.NewLine, "Descripción: ", error.ErrorDescription));
                }

                if ((int)response.StatusCode == 401 || (int)response.StatusCode == 403)
                {
                    throw new Exception("Error de autenticación, token invalido o sin permisos con WeeClinic");
                }

                throw new Exception("Ha ocurrido un error al buscar folio con WeeClinic");
            }
            catch (Exception)
            {
                throw;
            }
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
    }
}