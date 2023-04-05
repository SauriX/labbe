using Service.Catalog.Domain.Route;
using Service.Catalog.Dtos.Common;
using Service.Catalog.Dtos.Promotion;
using Service.Catalog.Dtos.Route;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Catalog.Mapper
{
    public static class RouteMapper
    {
        public static RouteListDto ToRouteListDto(this Route model)
        {
            if (model == null) return null;

            return new RouteListDto
            {
                Id = model.Id,
                Clave = model.Clave,
                Nombre = model.Nombre,
                OrigenId = model.OrigenId,
                Origen = model?.Origen?.Nombre,
                DestinoId = model?.DestinoId,
                Destino = model?.Destino?.Nombre + " " + model?.Maquilador?.Nombre,
                Activo = model.Activo,
            };
        }
        public static IEnumerable<RouteListDto> ToRouteListDto(this List<Route> model)
        {
            if (model == null) return null;

            return model.Select(x => new RouteListDto
            {
                Id = x.Id,
                Clave = x.Clave,
                Nombre = x.Nombre,
                OrigenId = x?.OrigenId,
                Origen = x?.Origen.Nombre,
                DestinoId = x?.DestinoId,
                Destino = x?.Destino?.Nombre + " " + x?.Maquilador?.Nombre,
                MaquiladorId = x?.MaquiladorId,
                Activo = x.Activo,
            });
        }

        public static List<RouteFormDto> ToRouteFoundDto(this List<Route> model)
        {
            if (model == null) return null;

            return model.Select(x =>
            {
                var dias = new List<DayDto>();
                if (x.Lunes) dias.Add(new DayDto { Id = 1, Dia = "L" });
                if (x.Martes) dias.Add(new DayDto { Id = 2, Dia = "M" });
                if (x.Miercoles) dias.Add(new DayDto { Id = 3, Dia = "M" });
                if (x.Jueves) dias.Add(new DayDto { Id = 4, Dia = "J" });
                if (x.Viernes) dias.Add(new DayDto { Id = 5, Dia = "V" });
                if (x.Sabado) dias.Add(new DayDto { Id = 6, Dia = "S" });
                if (x.Domingo) dias.Add(new DayDto { Id = 7, Dia = "D" });

                return new RouteFormDto
                {

                    Id = x.Id.ToString(),
                    Clave = x.Clave,
                    Nombre = x.Nombre,
                    OrigenId = x?.OrigenId,
                    DestinoId = x?.DestinoId,
                    MaquiladorId = x?.MaquiladorId,
                    Origen = x?.Origen?.Nombre,
                    Destino = x?.Destino?.Nombre + " " + x?.Maquilador?.Nombre,
                    PaqueteriaId = x.PaqueteriaId,
                    Activo = x.Activo,
                    Comentarios = x.Comentarios,
                    HoraDeRecoleccion = x.HoraDeRecoleccion,
                    TiempoDeEntrega = x.TiempoDeEntrega,
                    Estudio = x?.Estudios?.Select(y => new Route_StudyListDto
                    {
                        Id = y.EstudioId,
                        Clave = y.Estudio.Clave,
                        Nombre = y.Estudio.Nombre,
                        Area = y.Estudio.Area.Nombre,
                        Departamento = y.Estudio.Area.Departamento.Nombre,
                    })?.ToList(),
                    Dias = dias
                };
            }).ToList();
        }

        public static RouteFormDto ToRouteFormDto(this Route model)
        {
            if (model == null) return null;

            var dias = new List<DayDto>();
            if (model.Lunes) dias.Add(new DayDto { Id = 1, Dia = "L" });
            if (model.Martes) dias.Add(new DayDto { Id = 2, Dia = "M" });
            if (model.Miercoles) dias.Add(new DayDto { Id = 3, Dia = "M" });
            if (model.Jueves) dias.Add(new DayDto { Id = 4, Dia = "J" });
            if (model.Viernes) dias.Add(new DayDto { Id = 5, Dia = "V" });
            if (model.Sabado) dias.Add(new DayDto { Id = 6, Dia = "S" });
            if (model.Domingo) dias.Add(new DayDto { Id = 7, Dia = "D" });

            return new RouteFormDto
            {
                Id = model.Id.ToString(),
                Clave = model.Clave,
                Nombre = model.Nombre,
                OrigenId = model?.OrigenId,
                DestinoId = model?.DestinoId,
                MaquiladorId = model?.MaquiladorId,
                Origen = model?.Origen?.Nombre,
                Destino = model?.Destino?.Nombre + " " + model?.Maquilador?.Nombre,
                PaqueteriaId = model.PaqueteriaId,
                Activo = model.Activo,
                Comentarios = model?.Comentarios,
                HoraDeRecoleccion = model.HoraDeRecoleccion,
                TiempoDeEntrega = model.TiempoDeEntrega,
                TipoTiempo = model.TipoTiempo,
                Estudio = model?.Estudios?.Select(x => new Route_StudyListDto
                {
                    Id = x.EstudioId,
                    Clave = x.Estudio.Clave,
                    Nombre = x.Estudio.Nombre,
                    Area = x.Estudio.Area.Nombre,
                    Departamento = x.Estudio.Area.Departamento.Nombre,
                })?.ToList(),
                Dias = dias
            };
        }

        public static Route ToModel(this RouteFormDto dto)
        {
            if (dto == null) return null;

            return new Route
            {
                Clave = dto.Clave,
                Nombre = dto.Nombre,
                OrigenId = dto?.OrigenId,
                DestinoId = dto?.DestinoId,
                MaquiladorId = dto?.MaquiladorId,
                PaqueteriaId = dto.PaqueteriaId,
                Activo = dto.Activo,
                Comentarios = dto.Comentarios.ToString(),
                HoraDeRecoleccion = dto.HoraDeRecoleccion,
                TiempoDeEntrega = dto.TiempoDeEntrega,
                TipoTiempo = dto.TipoTiempo,
                UsuarioCreoId = dto.UsuarioId.ToString(),
                FechaCreo = DateTime.Now,
                Estudios = dto?.Estudio?.Select(x => new Route_Study
                {
                    EstudioId = x.Id,
                    FechaCreo = DateTime.Now,
                    UsuarioCreoId = dto.UsuarioId.ToString()
                })?.ToList(),
                Lunes = dto.Dias.Any(x => x.Id == 1),
                Martes = dto.Dias.Any(x => x.Id == 2),
                Miercoles = dto.Dias.Any(x => x.Id == 3),
                Jueves = dto.Dias.Any(x => x.Id == 4),
                Viernes = dto.Dias.Any(x => x.Id == 5),
                Sabado = dto.Dias.Any(x => x.Id == 6),
                Domingo = dto.Dias.Any(x => x.Id == 7),
            };
        }

        public static Route ToModel(this RouteFormDto dto, Route model)
        {
            if (dto == null) return null;

            return new Route
            {
                Id = model.Id,
                Clave = dto.Clave,
                Nombre = dto.Nombre,
                OrigenId = dto?.OrigenId,
                DestinoId = dto?.DestinoId,
                MaquiladorId = dto?.MaquiladorId,
                PaqueteriaId = dto?.PaqueteriaId,
                Activo = dto.Activo,
                Comentarios = dto?.Comentarios,
                HoraDeRecoleccion = dto.HoraDeRecoleccion,
                TiempoDeEntrega = dto.TiempoDeEntrega,
                TipoTiempo = dto.TipoTiempo,
                UsuarioCreoId = model.UsuarioCreoId,
                FechaCreo = model.FechaCreo,
                UsuarioModificoId = dto.UsuarioId.ToString(),
                FechaModifico = DateTime.Now,
                Estudios = dto?.Estudio?.Select(x => new Route_Study
                {
                    RouteId = model.Id,
                    EstudioId = x.Id,
                    FechaCreo = (DateTime)model.FechaCreo,
                    UsuarioCreoId = model.UsuarioCreoId,
                    FechaMod = DateTime.Now,
                    UsuarioModId = dto.UsuarioId.ToString()
                })?.ToList(),
                Lunes = dto.Dias.Any(x => x.Id == 1),
                Martes = dto.Dias.Any(x => x.Id == 2),
                Miercoles = dto.Dias.Any(x => x.Id == 3),
                Jueves = dto.Dias.Any(x => x.Id == 4),
                Viernes = dto.Dias.Any(x => x.Id == 5),
                Sabado = dto.Dias.Any(x => x.Id == 6),
                Domingo = dto.Dias.Any(x => x.Id == 7),
            };
        }
    }
}
