using Integration.WeeClinic.Responses;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
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
        // Dudas
        // Datos llega como un [], por que? Llegan campos de mas que en la documentacion
        public static async Task<string> BuscaFolioLaboratorio()
        {
            var url = "api/Servicio/BuscaFolioLaboratorio";

            var data = new Dictionary<string, string>()
            {
                ["Folio"] = "AD00704422003397421U"
            };

            var response = await PostService<string>(url, data);

            return response;
        }

        // Servicio 3. Consulta de precios por estudio
        // Dudas
        // Aunque se mande 1 sucursal que no existe regresa datos
        // Me regresa Servicio Descartado aunque no exista el folio
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
                ["idPersona"] = "4254656b-f760-470b-b807-25fc5d11b37d",
                ["CodAccion"] = "1",
                ["CodAcceso"] = "",
                ["CodMotivo"] = "5",
                ["idServicio"] = null
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
                ["idPersona"] = "4254656b-f760-470b-b807-25fc5d11b37f",
                ["idOrden"] = "a11c4d76-878e-4ec2-a7b9-03aeee28ddff",
                ["Token"] = "34234234",
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
                ["Servicios"] = "35caf363-e8d4-430e-b22e-1f8d005fb4dd,00000000-0000-0000-0000-000000000000,0,0|",
                ["ClaveSucursal"] = "MT"
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
                ["idServicio"] = "35caf363-e8d4-430e-b22e-1f8d005fb4dd",
                ["idNodo"] = "00000000-0000-0000-0000-000000000000",
                ["ClaveSucursal"] = "MT"
            };

            var response = await PostService<string>(url, data);

            return response;
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

                return response;
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

            return response;
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

            return response;
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

            return response;
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

            return response;
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

            return response;
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

            return response;
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

            return response;
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

            return response;
        }
    }
}