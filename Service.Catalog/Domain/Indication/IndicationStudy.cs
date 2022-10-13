﻿using System;

namespace Service.Catalog.Domain.Indication
{
    public class IndicationStudy
    {
        public IndicationStudy()
        {
        }

        public IndicationStudy(int indicacionId, int estudioId)
        {
            IndicacionId = indicacionId;
            EstudioId = estudioId;
            Activo = true;
            FechaCreo = DateTime.Now;
        }

        public int IndicacionId { get; set; }
        public virtual Indication Indicacion { get; set; }
        public int EstudioId { get; set; }
        public virtual Domain.Study.Study Estudio { get; set; }
        public bool Activo { get; set; }
        public Guid? UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public Guid? UsuarioModificoId { get; set; }
        public DateTime? FechaModifico { get; set; }
    }
}