using System.ComponentModel.DataAnnotations.Schema;

namespace Service.Catalog.Domain.Constant
{
    public class State
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte Id { get; set; }
        public string Estado { get; set; }
    }
}
