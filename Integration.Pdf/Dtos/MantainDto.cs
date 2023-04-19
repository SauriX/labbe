﻿using Integration.Pdf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Integration.Pdf.Dtos
{
    public class MantainDto
    {
        public Guid Id { get; set; }
        public Guid IdEquipo { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public Guid? IdUser { get; set; }
        public string Clave { get; set; }
        public string No_serie { get; set; }
        public bool Ativo { get; set; }
        public List<MantainImageDto> imagenUrl { get; set; }
        public HeaderData Header { get; set; }
    }
}