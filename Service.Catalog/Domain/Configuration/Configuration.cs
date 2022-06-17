using System.ComponentModel.DataAnnotations.Schema;

namespace Service.Catalog.Domain.Configuration
{
    public class Configuration
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Valor { get; set; }
    }
}
