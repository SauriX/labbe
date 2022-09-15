using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Integration.WeeClinic.Services
{
    public class FinancesService : Base
    {
        // Servcio 1. Agregar Factura
        public static async Task<string> Factura_AddNewFactura()
        {
            var url = "api/Factura/Factura_AddNewFactura";

            var data = new Dictionary<string, string>()
            {
                ["EmisorRFC"] = "LAR900731TL0",
                ["ReceptorRFC"] = "GGCI6120193U6",
                ["Folio"] = "12435",
                ["FechaFactura"] = "11/03/2022"
            };

            var response = await PostService<string>(url, data);

            return "";
        }

        // Servcio 2. Obtener Clientes
        public static async Task<string> Finanzas_GetClienteByFactura()
        {
            var url = "api/Factura/Finanzas_GetClienteByFactura";

            var data = new Dictionary<string, string>()
            {
                ["idFactura"] = ""
            };

            var response = await PostService<string>(url, data);

            return "";
        }

        // Servcio 3. Obtener Coberturas de Acuerdo a Cliente Seleccionado
        public static async Task<string> Facturas_GetSeparacionbyCliente()
        {
            var url = "api/Factura/Facturas_GetSeparacionbyCliente";

            var data = new Dictionary<string, string>()
            {
                ["idFactura"] = ""
            };

            var response = await PostService<string>(url, data);

            return "";
        }

        // Servcio 4. Obtener Coberturas de Acuerdo a Cliente Seleccionado
        public static async Task<string> Sucursal_GetSucursalByProveedor()
        {
            var url = "api/Factura/Sucursal_GetSucursalByProveedor";

            var data = new Dictionary<string, string>()
            {
                ["idFactura"] = ""
            };

            var response = await PostService<string>(url, data);

            return "";
        }

        // Servcio 5. Búsqueda de Servicios
        public static async Task<string> Finanzas_GetServiciosByFactura()
        {
            var url = "api/Factura/Finanzas_GetServiciosByFactura";

            var data = new Dictionary<string, string>()
            {
                ["idFactura"] = "",
                ["FechaInicio"] = "",
                ["FechaFin"] = "",
                ["idCliente"] = "",
                ["idSucursal"] = "",
                ["Busqueda"] = "",
                ["idCobertura"] = ""
            };

            var response = await PostService<string>(url, data);

            return "";
        }

        // Servcio 6. Agregar Servicios
        public static async Task<string> Finanzas_AddFacturaServicios()
        {
            var url = "api/Factura/Finanzas_AddFacturaServicios";

            var data = new Dictionary<string, string>()
            {
                ["idFactura"] = "",
                ["idServicio"] = "",
                ["idNodo"] = "",
                ["idCosto"] = ""
            };

            var response = await PostService<string>(url, data);

            return "";
        }

        // Servcio 7. Cancelar Factura o Eliminar Servicios
        public static async Task<string> Finanzas_DeleteFacturaServicio()
        {
            var url = "api/Factura/Finanzas_DeleteFacturaServicio";

            var data = new Dictionary<string, string>()
            {
                ["idFactura"] = "",
                ["idServicio"] = "",
                ["idNodo"] = "",
                ["CodEstatus"] = ""
            };

            var response = await PostService<string>(url, data);

            return "";
        }

        // Servicio 8. Subir Archivos en Azure
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

        // Servcio 9. Validar XML, Ligar a Factura y PDF
        public static async Task<string> Factura_AddFileFactura()
        {
            var url = "api/Factura/Factura_AddFileFactura";

            var data = new Dictionary<string, string>()
            {
                ["idFactura"] = "",
                ["RutaFile"] = "",
                ["idArchivo"] = "",
                ["Descripcion"] = ""
            };

            var response = await PostService<string>(url, data);

            return "";
        }
    }
}
