﻿using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.Sampling
{
    public class SamplingSearchDto
    {
       public DateTime[] Fecha { get; set; }
       public string Buscar { get; set; }
       public List<int> Procedencia { get; set; }
       public List<string> Departamento { get; set; }
       public List<string> Ciudad { get; set; }
       public List <int> TipoSolicitud { get; set; }
       public List <string> Area { get; set; }
       public List <string> Sucursal { get; set; }
       public List <int> Status { get; set; }
       public List <string> Medico { get; set; }
       public List <string> Compañia { get; set; }
    }
}
