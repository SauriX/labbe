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
        // Dudas
        // Datos llega como un [], por que? Llegan campos de mas que en la documentacion
        public static async Task<string> BuscaFolioLaboratorio()
        {
            var url = "api/Servicio/BuscaFolioLaboratorio";

            var data = new Dictionary<string, string>()
            {
                ["Folio"] = folio3
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