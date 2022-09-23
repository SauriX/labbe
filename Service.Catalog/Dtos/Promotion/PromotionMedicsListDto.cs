using System;

namespace Service.Catalog.Dtos.Promotion
{
    public class PromotionMedicsListDto
    {
        public Guid Id { get; set; }
        public string Clave { get; set; }
        public bool Activo { get; set; }
        public string Nombre { get; set; }

}
}
