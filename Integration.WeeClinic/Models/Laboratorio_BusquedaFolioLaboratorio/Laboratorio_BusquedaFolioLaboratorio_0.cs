using DocumentFormat.OpenXml.Office2010.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Integration.WeeClinic.Models.Laboratorio_BusquedaFolioLaboratorio
{
    public class Laboratorio_BusquedaFolioLaboratorio_0
    {
        [JsonPropertyName("idOrden")]
        public string IdOrden { get; set; }
        [JsonPropertyName("idNodo")]
        public string IdNodo { get; set; }
        [JsonPropertyName("idServicio")]
        public string IdServicio { get; set; }
        public string FolioOrden { get; set; }
        [JsonPropertyName("xDateInsert")]
        public string DateInsert { get; set; }
        [JsonPropertyName("idProducto")]
        public string IdProducto { get; set; }
        public string CorporativoNombre { get; set; }
        public string ProductoNombre { get; set; }
        public string NoPoliza { get; set; }
        [JsonPropertyName("idPersona")]
        public string IdPersona { get; set; }
        public string Nombre { get; set; }
        public string Paterno { get; set; }
        public string Materno { get; set; }
        public string NombreCompleto { get; set; }
        public string CURP { get; set; }
        public string FechaNacimiento { get; set; }
        [JsonPropertyName("codGenero")]
        public string CodGenero { get; set; }
        public string Genero { get; set; }
        public string RFC { get; set; }
        public int Edad { get; set; }
        public string Edad_Complete { get; set; }
        public string ClaveCDP { get; set; }
        public string CDPNombre { get; set; }
        public string ServicioNota { get; set; }
        public string Especialidad { get; set; }
        [JsonPropertyName("isAvaliableProv")]
        public int IsAvaliableProv { get; set; }
        [JsonPropertyName("isAvaliable")]
        public int IsAvaliable { get; set; }
        public string Estatus { get; set; }
        public int CodEstatus { get; set; }
        public decimal CantidadSolicitada { get; set; }
        public int Vigencia { get; set; }
        public int CodCubierto { get; set; }
        public string Cubierto { get; set; }
        public int RestanteDays { get; set; }
        [JsonPropertyName("isUploadEstudio")]
        public int IsUploadEstudio { get; set; }
        [JsonPropertyName("isCancel")]
        public int IsCancel { get; set; }
        [JsonPropertyName("xDateInsertS")]
        public string DateInsertS { get; set; }
        public string ClaveInterna { get; set; }
        public string DescripcionInterna { get; set; }
        public string Telefono_Paciente { get; set; }
        public string Correo_Paciente { get; set; }
        [JsonPropertyName("isVigente")]
        public int IsVigente { get; set; }
        public string Poliza { get; set; }
        [JsonPropertyName("idServicioParent")]
        public string IdServicioParent { get; set; }
        public string TPANombre { get; set; }
        [JsonPropertyName("codTipoCatalogo")]
        public int CodTipoCatalogo { get; set; }
        [JsonPropertyName("idTPA")]
        public string IdTPA { get; set; }
        [JsonPropertyName("idCliente")]
        public string IdCliente { get; set; }
        public string Cobertura { get; set; }
        [JsonPropertyName("idCobertura")]
        public string IdCobertura { get; set; }
        public string NoConvenio { get; set; }
        public string NombreCompleto_Medico { get; set; }
        public string Cedula_Medico { get; set; }
        public string Titulo_Medico { get; set; }
        public string Institucion_Medico { get; set; }
        public string No_Siniestro { get; set; }
        [JsonPropertyName("isTyC")]
        public int IsTyC { get; set; }
    }
}
