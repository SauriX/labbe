using Service.Catalog.Domain.Catalog;

namespace Service.Catalog.Dtos.Study
{
    public class StudyListDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int AreaId { get; set; }
        public virtual Area? Area { get; set; }
    }
}
