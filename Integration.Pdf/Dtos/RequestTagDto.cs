﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Integration.Pdf.Dtos
{
    public class RequestTagDto
    {
        public string Clave { get; set; }
        public string Paciente { get; set; }
        public string Estudio { get; set; }
        public string ClaveEtiqueta { get; set; }
        public string Ciudad { get; set; }
        public string NombreInfo { get; set; }
        public string Hora { get; set; }
        public string Tipo { get; set; }
        public string EdadSexo { get; set; }
    }
}