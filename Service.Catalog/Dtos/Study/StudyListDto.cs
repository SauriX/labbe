using Service.Catalog.Domain.Catalog;

namespace Service.Catalog.Dtos.Study
{
    public class StudyListDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Clave { get; set; }
        public int AreaId { get; set; }
        public string  Area { get; set; }
    }
}
