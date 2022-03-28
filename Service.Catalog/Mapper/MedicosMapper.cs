using Identidad.Api.ViewModels.Medicos;
using Identidad.Api.ViewModels.Menu;
using System.Collections.Generic;
using System.Linq;

namespace Identidad.Api.mapper
{
    public static class MedicosMapper
    {
        public static MedicsFormDto ToMedicsFormDto(this Medics model)
        {
            if (model == null) return null;
            return new MedicsFormDto
            {
                Clave = model.Clave,    
                NombreCompleto = model.Nombre,
                Correo = model.Correo,
                Telefono = model.Telefono,
                Celular = model.Celular,
                Direccion = $"{model.Calle} {model.NumeroExterior}",
                Clinica = string.Join(", ", model.Clinicas.Select(x => x.ClinicaId)),
                Observaciones = model.Observaciones,
                EspecialidadId = model.EspecialidadId,
                Activo = model.Activo

            };
        }
        public static IEnumerable<MedicsFormDto> ToMedicsFormDto(this List<Medics> model)
        {
            if (model == null) return null;
            return model.Select(x => new MedicsFormDto
            {
                Clave = x.Clave,
                NombreCompleto = x.Nombre + " " + x.PrimerApellido + " " + x.SegundoApellido,
                EspecialidadId = x.EspecialidadId,
                Observaciones = x.Observaciones,
                Direccion = x.Calle + " " + x.NumeroExterior + " " + x.ColoniaId,
                Correo = x.Correo,
                Telefono = x.Telefono,
                Celular = x.Celular,
                Activo = x.Activo

            });
        }

    }
}
