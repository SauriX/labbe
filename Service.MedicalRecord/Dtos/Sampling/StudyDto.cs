using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.Sampling
{
    public class StudyDto
    {
        public int EstudioId { get; set; }
        public string Nombre { get; set; }
        public string Area { get; set; }
        public int EstatusId { get; set; }
        public string Registro { get; set; }
        public string Entrega { get; set; }
        public bool Seleccion { get; set; }
        public string Clave { get; set; }
        public string NombreEstatus { get; set; }
        public List<StudyParamsDto> Parametros { get; set; }
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
