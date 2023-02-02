using Service.Identity.Domain.Menu;
using Service.Identity.Domain.User;
using Service.Identity.Dtos.Menu;
using Service.Identity.Dtos.Scopes;
using System.Collections.Generic;
using System.Linq;

namespace Service.Identity.Mapper
{
    public static class ProfileMapper
    {
        public static ScopesDto ToScopesDto(this UserPermission permission, User user)
        {
            if (permission == null) return null;

            return new ScopesDto
            {
                Pantalla = permission.Menu.Descripcion,
                Acceder = permission.Acceder,
                Crear = permission.Crear,
                Modificar = permission.Modificar,
                Descargar = permission.Descargar,
                Imprimir = permission.Imprimir,
                EnviarCorreo = permission.EnviarCorreo,
                EnviarWapp = permission.EnviarWapp,
                SucursalesId = user.Sucursales.Select(x => x.BranchId).ToList(),
            };
        }

        public static IEnumerable<MenuDto> ToMenuDto(this IEnumerable<Menu> model)
        {
            if (model == null) return null;

            return model.Select(x => new MenuDto
            {
                Descripcion = x.Descripcion,
                Ruta = x.Ruta,
                Icono = x.Icono,
                SubMenus = x.SubMenus.ToMenuDto(),
            });
        }
    }
}
