﻿using Service.Catalog.Domain.Route;
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
                SucursalOrigenId = model.SucursalOrigenId,
                SucursalDestinoId = model.SucursalDestinoId,
                Activo = model.Activo,
                //Estudios = model?.Estudios?.Select(x => new Route_StudyListDto
                //{
                //    Id = x.EstudioId,
                //    Clave = x.Estudio.Clave.Trim(),
                //    Nombre = x.Estudio.Nombre.Trim(),
                //    Area = x.Estudio.Area.Nombre.Trim(),
                //    Departamento = x.Estudio.Area.Departamento.Nombre.Trim(),
                //    Activo = true,
                //})?.ToList()
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
                SucursalOrigenId = x.SucursalOrigenId,
                SucursalDestinoId = x.SucursalDestinoId,
                Activo = x.Activo,
                //Estudios = x?.Estudios?.Select(x => new Route_StudyListDto
                //{
                //    Id = x.EstudioId,
                //    Clave = x.Estudio.Clave,
                //    Nombre = x.Estudio.Nombre,
                //    Area = x.Estudio.Area.Nombre,
                //    Departamento = x.Estudio.Area.Departamento.Nombre,
                //    Activo = true,
                //})?.ToList()
            });
        }

        public static RouteFormDto ToRouteFormDto(this Route model)
        {
            if (model == null) return null;
            var dias = new List<DiasDto>();
            if (model.Lunes)
            {
                dias.Add(new DiasDto { Id = 1, Dia = "L" });
            }
            if (model.Martes)
            {
                dias.Add(new DiasDto { Id = 2, Dia = "M" });
            }
            if (model.Miercoles)
            {
                dias.Add(new DiasDto { Id = 3, Dia = "M" });
            }
            if (model.Jueves)
            {
                dias.Add(new DiasDto { Id = 4, Dia = "J" });
            }
            if (model.Viernes)
            {
                dias.Add(new DiasDto { Id = 5, Dia = "V" });
            }
            if (model.Sabado)
            {
                dias.Add(new DiasDto { Id = 6, Dia = "S" });
            }
            if (model.Domingo)
            {
                dias.Add(new DiasDto { Id = 7, Dia = "D" });
            }

            return new RouteFormDto
            {
                Id = model.Id.ToString(),
                Clave = model.Clave,
                Nombre = model.Nombre,
                SucursalOrigenId = model.SucursalOrigenId,
                SucursalDestinoId = model.SucursalDestinoId,
                PaqueteriaId = model.PaqueteriaId,
                Activo = model.Activo,
                Comentarios = model.Comentarios.ToString(),
                HoraDeRecoleccion = model.HoraDeRecoleccion,
                TiempoDeEntrega = model.TiempoDeEntrega,
                FormatoDeTiempoId = model.FormatoDeTiempoId,
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
                SucursalOrigenId = dto.SucursalOrigenId,
                SucursalDestinoId = dto.SucursalDestinoId,
                PaqueteriaId = dto.PaqueteriaId,
                Activo = dto.Activo,
                Comentarios = dto.Comentarios.ToString(),
                HoraDeRecoleccion = dto.HoraDeRecoleccion,
                TiempoDeEntrega = dto.TiempoDeEntrega,
                FormatoDeTiempoId = dto.FormatoDeTiempoId,
                Estudios = dto?.Estudio?.Select(x => new Route_Study
                {
                    EstudioId = x.Id,
                    FechaCreo = DateTime.Now,
                    FechaMod = DateTime.Now,
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
                SucursalOrigenId = dto.SucursalOrigenId,
                SucursalDestinoId = dto.SucursalDestinoId,
                PaqueteriaId = dto.PaqueteriaId,
                Activo = dto.Activo,
                Comentarios = dto.Comentarios.ToString(),
                HoraDeRecoleccion = model.HoraDeRecoleccion,
                TiempoDeEntrega = dto.TiempoDeEntrega,
                FormatoDeTiempoId = dto.FormatoDeTiempoId,
                UsuarioCreoId = dto.UsuarioId.ToString(),
                FechaCreo = DateTime.Now,
                Estudios = dto?.Estudio?.Select(x => new Route_Study
                {
                    EstudioId = x.Id,
                    FechaCreo = DateTime.Now,
                    FechaMod = DateTime.Now,
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
