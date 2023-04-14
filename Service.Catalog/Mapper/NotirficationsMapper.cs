
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using Service.Catalog.Domain.Notifications;
using Service.Catalog.Dtos.Common;
using Service.Catalog.Dtos.Notifications;
using Service.Catalog.Dtos.Promotion;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Catalog.Mapper
{
    public static class NotirficationsMapper
    {       
        public static List<NotificationListDto> toNotificationListDto(this List<Notifications> model)
            {
            var lista = model.Select(x => new NotificationListDto
            {
                Id = x.Id,
                Clave = x.Titulo,
                Titulo = x.Titulo,
                Fecha = x.FechaCreo!=null? x.FechaCreo.Value.ToShortDateString():"",
                Activo = x.Activo,
                Contenido = x.Contenido,
                Tipo = x.Tipo,
            }).ToList();
            return lista;
        }
        public static NotificationListDto toNotificationListDto(this Notifications model)
        {
            return new NotificationListDto
            {
                Id = model.Id,
                Clave = model.Titulo,
                Titulo = model.Titulo,
                Fecha = model.FechaCreo != null ? model.FechaCreo.Value.ToShortDateString() : "",
                Activo = model.Activo,
                Contenido = model.Contenido
            };
        }
        public static NotificationFormDto toNotificationFormDto(this Notifications model)
        {
            if (model == null) return null;
            var dias = new List<DayDto>();
            if (model.Lunes)
            {
                dias.Add(new DayDto { Id = 1, Dia = "L" });
            }
            if (model.Martes)
            {
                dias.Add(new DayDto { Id = 2, Dia = "M" });
            }
            if (model.Miercoles)
            {
                dias.Add(new DayDto { Id = 3, Dia = "M" });
            }
            if (model.Jueves)
            {
                dias.Add(new DayDto { Id = 4, Dia = "J" });
            }
            if (model.Viernes)
            {
                dias.Add(new DayDto { Id = 5, Dia = "V" });
            }
            if (model.Sabado)
            {
                dias.Add(new DayDto { Id = 6, Dia = "S" });
            }
            if (model.Domingo)
            {
                dias.Add(new DayDto { Id = 7, Dia = "D" });
            }

            List<DateTime> fechas = new List<DateTime>();
            fechas.Add(model.FechaInicial);
            fechas.Add(model.FechaFinal);
            return new NotificationFormDto
            {
                Id = model.Id,
                Titulo = model.Titulo,
                Contenido = model.Contenido,
                IsNotifi = model.IsNotifi,
                Fechas = fechas,
                Activo = model.Activo,
                Sucursales = model.Sucursales.Select(x => x.BranchId).ToList(),
                Roles = model.Roles.Select(x => x.RolId).ToList(),
                Dias = dias
            };
        }
        public static List<Notification_Branch> ToNotificationBranchesModel(this IEnumerable<Guid> dto, Guid notifiId)
        {
            return dto.Select(x => new Notification_Branch
            {
                BranchId = x,
                NotificacionId = notifiId,
                FechaCreo = DateTime.Now,
            }).ToList();
        }
        public static List<Notification_Role> ToNotificationRoleModel(this IEnumerable<Guid> dto, Guid notifiId)
        {
            return dto.Select(x => new Notification_Role
            {
                RolId = x,
                NotificacionId = notifiId,
                FechaCreo = DateTime.Now,
            }).ToList();
        }
        public static Notifications toModel(this NotificationFormDto dto)
        {
            return new Notifications
            {
                Id = dto.Id.Value,
                Titulo = dto.Titulo,
                Contenido = dto.Contenido,
                IsNotifi = false,
                FechaInicial = dto.Fechas[0],
                FechaFinal = dto.Fechas[1],
                Sucursales = dto.Sucursales.ToNotificationBranchesModel(dto.Id.Value),
                Roles = dto.Roles.ToNotificationRoleModel(dto.Id.Value),
                Lunes = dto.Dias.Any(x => x.Id == 1),
                Martes = dto.Dias.Any(x => x.Id == 2),
                Miercoles = dto.Dias.Any(x => x.Id == 3),
                Jueves = dto.Dias.Any(x => x.Id == 4),
                Viernes = dto.Dias.Any(x => x.Id == 5),
                Sabado = dto.Dias.Any(x => x.Id == 6),
                Domingo = dto.Dias.Any(x => x.Id == 7),
                Activo = true,
                UsuarioCreoId = dto.UsuarioId.ToString(),
                FechaCreo = DateTime.Now,
            };

        }
        public static Notifications toModel(this NotificationFormDto dto,Notifications model)
        {
            return new Notifications
            {
                Id = dto.Id.Value,
                Titulo = dto.Titulo,
                Contenido = dto.Contenido,
                IsNotifi = model.IsNotifi,
                FechaInicial = dto.Fechas[0],
                FechaFinal = dto.Fechas[1],
                Sucursales = dto.Sucursales.ToNotificationBranchesModel(dto.Id.Value),
                Roles = dto.Roles.ToNotificationRoleModel(dto.Id.Value),
                Lunes = dto.Dias.Any(x => x.Id == 1),
                Martes = dto.Dias.Any(x => x.Id == 2),
                Miercoles = dto.Dias.Any(x => x.Id == 3),
                Jueves = dto.Dias.Any(x => x.Id == 4),
                Viernes = dto.Dias.Any(x => x.Id == 5),
                Sabado = dto.Dias.Any(x => x.Id == 6),
                Domingo = dto.Dias.Any(x => x.Id == 7),
                Activo = true,
                UsuarioCreoId = model.UsuarioCreoId,
                FechaCreo = model.FechaCreo,
                UsuarioModificoId= dto.UsuarioId.ToString(),
                FechaModifico = DateTime.Now,
            };

        }
    }
}
