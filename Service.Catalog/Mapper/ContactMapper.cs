using Service.Catalog.Domain.Company;
using Service.Catalog.Dtos.Company;
using System.Collections.Generic;
using System.Linq;

namespace Service.Catalog.Mapper
{
    public static class ContactMapper
    {
        public static IEnumerable<ContactListDto> ToContactListDto(this IEnumerable<Contact> model)
        {
            if (model == null) return null;

            return model.Select(x => new ContactListDto
            {
                IdContacto = x.Id,
                Nombre = x.Nombre + " " + x.Apellidos,
                Correo = x.Correo,
                Telefono = x.Telefono, 
                Activo = x.Activo,
            });
        }
        public static IEnumerable<ContactListDto> ToStudyListDtos(this List<Contact> model)
        {
            if (model == null) return null;

            return model.Select(x => new ContactListDto
            {
                IdContacto = x.Id,
                Nombre = x.Nombre + " " + x.Apellidos,
                Correo = x.Correo,
                Telefono = x.Telefono,
                Activo = x.Activo,
            });
        }
    }
}
