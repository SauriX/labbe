using FluentValidation;
using Service.Catalog.Dtos.PriceList;
using System;
using System.Collections.Generic;

namespace Service.Catalog.Dtos.Promotion
{
    public class PromotionFormDto
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string TipoDescuento { get; set; }
        public decimal Cantidad { get; set; }
        public bool AplicaMedicos { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public bool Activo { get; set; }
        public Guid ListaPrecioId { get; set; }
        public PromotionDayDto Dias { get; set; }
        public IEnumerable<Guid> Sucursales { get; set; }
        public IEnumerable<Guid> Medicos { get; set; }
        public List<PromotionStudyPackDto> Estudios { get; set; }
        public Guid UsuarioId { get; set; }
    }
}
