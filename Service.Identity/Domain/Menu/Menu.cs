using System.Collections.Generic;

namespace Service.Identity.Domain.Menu
{
    public class Menu
    {
        public Menu() { }

        public Menu(short id, short? menuPadreId, string descripcion, string controlador, string ruta, short orden)
        {
            Id = id;
            MenuPadreId = menuPadreId;
            Descripcion = descripcion;
            Controlador = controlador;
            Icono = controlador;
            Ruta = ruta;
            Orden = orden;
            Activo = true;
        }

        public short Id { get; set; }
        public short? MenuPadreId { get; set; }
        public virtual Menu MenuPadre { get; set; }
        public string Descripcion { get; set; }
        public string Controlador { get; set; }
        public string Icono { get; set; }
        public string Ruta { get; set; }
        public short Orden { get; set; }
        public bool Activo { get; set; }

        public virtual ICollection<Menu> SubMenus { get; set; }
    }
}
