using System.Collections.Generic;

namespace Service.Identity.Dtos
{
    public class Menu {

        public int id { get; set; }
        public string ruta {get; set;}
        public string icono { get; set; }
        public string descripcion { get; set; }
        public List<Menu> subMenus { get; set; }

    }
}
