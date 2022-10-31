using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Integration.WeeClinic.Dtos
{
    public class WeeServiceDto
    {
        public string IdOrden { get; set; }
        public string IdNodo { get; set; }
        public string IdServicio { get; set; }
        public string FolioOrden { get; set; }
        public string DateInsert { get; set; }
        public string IdProducto { get; set; }
        public string CorporativoNombre { get; set; }
        public string ProductoNombre { get; set; }
        public string NoPoliza { get; set; }
        public string IdPersona { get; set; }
        public string Nombre { get; set; }
        public string Paterno { get; set; }
        public string Materno { get; set; }
        public string NombreCompleto { get; set; }
        public string CURP { get; set; }
        public string FechaNacimiento { get; set; }
        public string CodGenero { get; set; }
        public string Genero { get; set; }
        public string RFC { get; set; }
        public int Edad { get; set; }
        public string Edad_Complete { get; set; }
        public string ClaveCDP { get; set; }
        public string CDPNombre { get; set; }
        public string ServicioNota { get; set; }
        public string Especialidad { get; set; }
        public int IsAvaliableProv { get; set; }
        public int IsAvaliable { get; set; }
        public string Estatus { get; set; }
        public int CodEstatus { get; set; }
        public decimal CantidadSolicitada { get; set; }
        public int Vigencia { get; set; }
        public int CodCubierto { get; set; }
        public string Cubierto { get; set; }
        public int RestanteDays { get; set; }
        public int IsUploadEstudio { get; set; }
        public int IsCancel { get; set; }
        public string DateInsertS { get; set; }
        public string ClaveInterna { get; set; }
        public string DescripcionInterna { get; set; }
        public string Telefono_Paciente { get; set; }
        public string Correo_Paciente { get; set; }
        public int IsVigente { get; set; }
        public string Poliza { get; set; }
        public string IdServicioParent { get; set; }
        public string TPANombre { get; set; }
        public int CodTipoCatalogo { get; set; }
        public string IdTPA { get; set; }
        public string IdCliente { get; set; }
        public string Cobertura { get; set; }
        public string IdCobertura { get; set; }
        public string NoConvenio { get; set; }
        public string NombreCompleto_Medico { get; set; }
        public string Cedula_Medico { get; set; }
        public string Titulo_Medico { get; set; }
        public string Institucion_Medico { get; set; }
        public string No_Siniestro { get; set; }
        public int IsTyC { get; set; }
    }
}
