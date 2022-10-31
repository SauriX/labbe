﻿using System;

namespace Service.MedicalRecord.Domain
{
    public class ClinicResults
    {
        public Guid Id { get; set; }
        public Guid SolicitudId { get; set; }
        public virtual Request.Request Solicitud{ get; set; }
        public int SolicitudEstudioId { get; set; }
        public int EstudioId { get; set; }
        public virtual Request.RequestStudy SolicitudEstudio { get; set; }
        public Guid ParametroId { get; set; }
        public string TipoValorId { get; set; }
        public string Unidades { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string NombreCorto { get; set; }
        public string ValorInicial { get; set; }
        public string ValorFinal { get; set; }
        public decimal? CriticoMinimo { get; set; }
        public decimal? CriticoMaximo { get; set; }
        public string Formula { get; set; }
        public string Resultado { get; set; }
    }
}
