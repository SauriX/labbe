﻿using Integration.WeeClinic.Extensions;
using Integration.WeeClinic.Models.Laboratorio_BusquedaFolioLaboratorio;
using Integration.WeeClinic.Models.Laboratorio_BusquedaFolios;
using Integration.WeeClinic.Models.Laboratorio_GetPreciosEstudios_ByidServicio;
using Integration.WeeClinic.Models.Laboratorio_ValidarCodigoPacienteLaboratorio;
using Integration.WeeClinic.Models.Laboratorio_ValidaToken;
using Integration.WeeClinic.Responses;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Shared.Error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Integration.WeeClinic.Services
{
    public class LaboratoryService : Base
    {
        private static readonly int codEstatus_Seleccionar_Estudio = 14;
        private static readonly int codEstatus_Liberar_Estudio = 1;

        //AD007044220041013DW5
        //AD007044220041014O01
        //AD007044220041015272
        //AD007044220041016TS3

        //AD00704422004140505K
        //AD00704422004140684A
        //AD0070442200414078UF

        //AD007044220043380552
        //AD007044220043381939
        //AD00704422004338225S

        // Servcio 1. Consulta de folios
        public static async Task<Laboratorio_BusquedaFolios> BusquedaFolios(string folio)
        {
            var url = "api/Laboratorio/Laboratorio_BusquedaFolios";

            var data = new Dictionary<string, string>()
            {
                ["NoFolio"] = folio
            };

            var response = await PostService<string>(url, data);

            response.ValidateNotEmpty("Datos");

            if (response["Datos"].Any(x => x.ContainsKey("Mensaje")))
            {
                var errorMessage = string.Join(" | ", response["Datos"].Where(x => x.ContainsKey("Mensaje")).SelectMany(x => x.Values));
                throw new CustomException(HttpStatusCode.FailedDependency, errorMessage);
            }

            var folios = response.Transform<Laboratorio_BusquedaFolios>();

            return folios;
        }

        // Servicio 2. Consulta de estudios solicitados
        public static async Task<Laboratorio_BusquedaFolioLaboratorio> BuscaFolioLaboratorio(string folio)
        {
            var url = "api/Servicio/BuscaFolioLaboratorio";

            var data = new Dictionary<string, string>()
            {
                ["Folio"] = folio
            };

            var response = await PostService<string>(url, data);

            response.ValidateNotEmpty("Datos", "Datos1");

            var folios = response.Transform<Laboratorio_BusquedaFolioLaboratorio>();

            return folios;
        }

        // Servicio 3. Consulta de precios por estudio
        public static async Task<Laboratorio_GetPreciosEstudios_ByidServicio> GetPreciosEstudios_ByidServicio()
        {
            var url = "api/Inventarios/GetPreciosEstudios_ByidServicio";

            var data = new Dictionary<string, string>()
            {
                ["idNodo"] = "00000000-0000-0000-0000-000000000000",
                ["idServicio"] = "139e9018-39b8-43c4-94de-0940b526d35b",
                ["ClaveSucursal"] = "",
                ["codEstatus"] = codEstatus_Seleccionar_Estudio.ToString()
            };

            var response = await PostService<string>(url, data);

            var folios = response.Transform<Laboratorio_GetPreciosEstudios_ByidServicio>();

            return folios;
        }

        // Servicio 4. Enviar token de validación de estudios
        public static async Task<Laboratorio_ValidarCodigoPacienteLaboratorio> ValidarCodigoPacienteLaboratorio()
        {
            var url = "api/Sesion/ValidarCodigoPacienteLaboratorio";

            var data = new Dictionary<string, string>()
            {
                ["idPersona"] = "88a2db0b-0e7a-4e6e-9865-c7a8d1752312",
                ["CodAccion"] = "2",
                ["CodAcceso"] = "629726",
                ["CodMotivo"] = "3",
                ["idServicio"] = "f6d7c607-6da9-4ca6-b075-1a69719d39e9"
            };

            var response = await PostService<string>(url, data);

            var folios = response.Transform<Laboratorio_ValidarCodigoPacienteLaboratorio>();

            return folios;
        }

        // Servicio 5. Validación de Token
        public static async Task<Laboratorio_ValidaToken> Laboratorio_ValidaToken()
        {
            var url = "api/Servicio/Laboratorio_ValidaToken";

            var data = new Dictionary<string, string>()
            {
                ["idPersona"] = "88a2db0b-0e7a-4e6e-9865-c7a8d1752312",
                ["idOrden"] = "b6e542dd-0386-4b94-b50b-06cd8ced0100",
                ["Token"] = "34234234",
                ["ClaveSucursal"] = ""
            };

            var response = await PostService<string>(url, data);

            var folios = response.Transform<Laboratorio_ValidaToken>();

            return folios;
        }

        // Servicio 6. Asignación de estudios
        public static async Task<string> Laboratorio_AsignaEstudio()
        {
            var url = "api/Laboratorio/Laboratorio_AsignaEstudio";

            var data = new Dictionary<string, string>()
            {
                ["Servicios"] = "35caf363-e8d4-430e-b22e-1f8d005fb4dd,00000000-0000-0000-0000-000000000000,0,0|",
                ["ClaveSucursal"] = "MT"
            };

            var response = await PostService<string>(url, data);

            return "";
        }

        // Servicio 7. Cancelación de estudio
        public static async Task<string> Laboratorio_CancelaEstudios_ByProveedor()
        {
            var url = "api/Laboratorio/Laboratorio_CancelaEstudios_ByProveedor";

            var data = new Dictionary<string, string>()
            {
                ["idServicio"] = "35caf363-e8d4-430e-b22e-1f8d005fb4dd",
                ["idNodo"] = "00000000-0000-0000-0000-000000000000",
                ["ClaveSucursal"] = "MT"
            };

            var response = await PostService<string>(url, data);

            return "";
        }

        // Servicio 8. Carga de resultados
        // Dudas: Que tipo de archivos se pueden subir?
        public static async Task<string> UploadFileAzure(IFormFile file)
        {
            try
            {
                var url = $"api/FileUpload/UploadFileAzure";

                using var multipartFormContent = new MultipartFormDataContent();
                var stream = new StreamContent(file.OpenReadStream());
                stream.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                multipartFormContent.Add(stream, name: "UploadedImage", fileName: file.FileName);

                var response = await PostService<string>(url, multipartFormContent);

                return "";
            }
            catch (Exception)
            {

                throw;
            }
        }

        // Servicio 8. Carga de resultados (Notas)
        public static async Task<string> Laboratorio_CargaURL_Resultados()
        {
            var url = "api/Laboratorio/Laboratorio_CargaURL_Resultados";

            var data = new Dictionary<string, string>()
            {
                ["idServicio"] = "35caf363-e8d4-430e-b22e-1f8d005fb4dd",
                ["idNodo"] = "00000000-0000-0000-0000-000000000000",
                ["UrlFile"] = $"{baseUrl}/API/Files/SaveTemp/1557701dac57-1551-43c8-a217-cdb1431ea9af.png",
                ["Nota"] = $"Nota de prueba"
            };

            var response = await PostService<string>(url, data);

            return "";
        }

        // Servicio 9. Relacionar archivo (PDF Resultado) con el estudio
        public static async Task<string> Laboratorio_ArchivosResultados_Update()
        {
            var url = "api/Laboratorio/Laboratorio_ArchivosResultados_Update";

            var data = new Dictionary<string, string>()
            {
                ["idServicio"] = "35caf363-e8d4-430e-b22e-1f8d005fb4dd",
                ["idNodo"] = "00000000-0000-0000-0000-000000000000",
                ["idArchivo"] = "3398544d-f3e2-4dc4-9206-f080c10d014a",
                ["Nota"] = $"Nota de prueba",
                ["isRemplazarOrnew"] = "0"
            };

            var response = await PostService<string>(url, data);

            return "";
        }

        // Servicio 10. Reemplazar archivos con el estudio
        public static async Task<string> Laboratorio_ArchivosResultados_Update_RemplazaArchivos()
        {
            var url = "api/Laboratorio/Laboratorio_ArchivosResultados_Update_RemplazaArchivos";

            var data = new Dictionary<string, string>()
            {
                ["idServicio"] = "35caf363-e8d4-430e-b22e-1f8d005fb4dd",
                ["idNodo"] = "00000000-0000-0000-0000-000000000000",
                ["idArchivo"] = "3398544d-f3e2-4dc4-9206-f080c10d017f",
            };

            var response = await PostService<string>(url, data);

            return "";
        }

        // Servicio 11. Enviar e-mail con los resultados
        public static async Task<string> SendEmailResulatdosLaboratorio()
        {
            var url = "api/Laboratorio/SendEmailResulatdosLaboratorio";

            var data = new Dictionary<string, string>()
            {
                ["Email"] = "mfarias@axsistec.com",
                ["laboratorio"] = "Laboratorio Ramos",
                ["idArchivo"] = "a2501bf9-e2e7-4922-8b35-91b2724c8d3a"
            };

            var response = await PostService<string>(url, data);

            return "";
        }

        // Servicio 18. Consultar estudios del paquete
        public static async Task<string> Laboratorio_GetDetallePaquete()
        {
            var url = "api/Laboratorio/Laboratorio_GetDetallePaquete";

            var data = new Dictionary<string, string>()
            {
                ["idServicio"] = "35caf363-e8d4-430e-b22e-1f8d005fb4dd",
                ["idNodo"] = "00000000-0000-0000-0000-000000000000"
            };

            var response = await PostService<string>(url, data);

            return "";
        }

        // Servicio 19. Consulta de información del laboratorio
        public static async Task<string> SucursalAreas_GetLaboratoriosByUser()
        {
            var url = "api/Laboratorio/SucursalAreas_GetLaboratoriosByUser";

            var data = new Dictionary<string, string>()
            {
                ["codTipoArea"] = "8"
            };

            var response = await PostService<string>(url, data);

            return "";
        }

        // Servicio 20. Consulta los estudios asignados al laboratorio
        public static async Task<string> Laboratorio_GetEstudiosbyLaboratorio()
        {
            var url = "api/Laboratorio/Laboratorio_GetEstudiosbyLaboratorio";

            var data = new Dictionary<string, string>()
            {
                ["idLaboratorio"] = "",
                ["Busqueda"] = ""
            };

            var response = await PostService<string>(url, data);

            return "";
        }

        // Servicio 21. Consulta los estudios asignados al laboratorio
        public static async Task<string> Laboratorio_GetPreciosEstudiosPerfiles()
        {
            var url = "api/Laboratorio/Laboratorio_GetPreciosEstudiosPerfiles";

            var data = new Dictionary<string, string>()
            {
                ["idServicio"] = "35caf363-e8d4-430e-b22e-1f8d005fb4dd",
                ["idNodo"] = "00000000-0000-0000-0000-000000000000",
                ["codTipoCatalogo"] = "12",
                ["ClaveSucursal"] = "MT",
            };

            var response = await PostService<string>(url, data);

            return "";
        }
    }
}