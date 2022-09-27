using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Integration.Pdf.Dtos
{
    public class LabResultsDto
    {
        public string Id { get; set; }
        public string Solicitud { get; set; }
        public string Nombre { get; set; }
        public string Registro { get; set; }
        public string Entrega { get; set; }
        public string UsuarioCreo { get; set; }
        public string Sucursal { get; set; }
        public string SucursalNombre { get; set; }
        public string Edad { get; set; }
        public string Sexo { get; set; }
        public string Compañia { get; set; }
        public byte Procedencia { get; set; }
        public string Departamento { get; set; }
        public string Area { get; set; }
        public string NombreMedico { get; set; }
        public List<StudyDto> Estudios { get; set; }
        public List<StudyParamsDto> Parametros { get; set; }
    }

    public class StudyDto
    {
        public string Clave { get; set; }
        public string Estudio { get; set; }
        public string Precio { get; set; }
        public decimal Dias { get; set; }
    }

    public class StudyParamsDto
    {
        public Guid Id { get; set; }
        public string NombreParametro { get; set; }
        public string Unidades { get; set; }
        public string Resultado { get; set; }
        public int ValorInicial { get; set; }
        public int ValorFinal { get; set; }
    }
}