using System;

namespace Service.MedicalRecord.Dtos.Sampling
{
    public class SamplingSearchDto
    {
       public DateTime[] Fecha { get; set; }
       public string Buscar { get; set; }
       public string? Procedencia { get; set; }
       public string Departamento { get; set; }
       public string Ciudad { get; set; }
       public string TipoSolicitud { get; set; }
       public string Area { get; set; }
       public string Sucursal { get; set; }
       public string Status { get; set; }
       public string Medico { get; set; }
       public string Compañia { get; set; }
    }
}
