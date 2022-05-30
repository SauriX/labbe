using System.Collections.Generic;

namespace Service.Identity.Dtos.Menu
{
    public class MenuDto
    {
        public int Id { get; set; }
        public string Ruta { get; set; }
        public string Icono { get; set; }
        public string Descripcion { get; set; }
        public IEnumerable<MenuDto> SubMenus { get; set; }
    }
}
