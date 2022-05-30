using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Identity.Domain.Role;
using Service.Identity.Dtos;
using Service.Identity.Dtos.Role;
using Service.Identity.Dtos.User;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using PP = Service.Identity.Dictionary.PermissionProps;

namespace Service.Identity.Mapper
{
    public static class RoleMapper
    {
        public static RoleListDto ToRoleListDto(this Role model)
        {
            if (model == null) return null;

            return new RoleListDto
            {
                Id = model.Id.ToString(),
                Nombre = model.Nombre,
                Activo = model.Activo,
            };
        }

        public static IEnumerable<RoleListDto> ToRoleListDto(this List<Role> model)
        {
            if (model == null) return null;

            return model.Select(x => new RoleListDto
            {
                Id = x.Id.ToString(),
                Nombre = x.Nombre,
                Activo = x.Activo,
            });
        }

        public static RoleFormDto ToRoleFormDto(this Role model)
        {
            if (model == null) return null;

            return new RoleFormDto
            {
                Id = model.Id.ToString(),
                Nombre = model.Nombre,
                Activo = model.Activo,
                Permisos = model.Permisos.ToRolePermissionDto(),
            };
        }

        public static List<RolePermissionDto> ToRolePermissionDto(this IEnumerable<RolePermission> model)
        {
            if (model == null)
                return null;

            var dto = new List<RolePermissionDto>();

            int i = 1;
            foreach (var item in model)
            {
                dto.Add(new RolePermissionDto(i++, item.MenuId, item.Menu.Descripcion, PP.Access, item.Acceder, PP.AccessType));
                dto.Add(new RolePermissionDto(i++, item.MenuId, item.Menu.Descripcion, PP.Create, item.Crear, PP.CreateType));
                dto.Add(new RolePermissionDto(i++, item.MenuId, item.Menu.Descripcion, PP.Update, item.Modificar, PP.UpdateType));
                dto.Add(new RolePermissionDto(i++, item.MenuId, item.Menu.Descripcion, PP.Print, item.Imprimir, PP.PrintType));
                dto.Add(new RolePermissionDto(i++, item.MenuId, item.Menu.Descripcion, PP.Download, item.Descargar, PP.DownloadType));
                dto.Add(new RolePermissionDto(i++, item.MenuId, item.Menu.Descripcion, PP.Mail, item.EnviarCorreo, PP.MailType));
                dto.Add(new RolePermissionDto(i++, item.MenuId, item.Menu.Descripcion, PP.Wapp, item.EnviarWapp, PP.WappType));
            }

            return dto;
        }

        public static Role ToModel(this RoleFormDto dto)
        {
            if (dto == null) return null;

            return new Role
            {
                Nombre = dto.Nombre,
                Activo = dto.Activo,
                UsuarioCreoId = dto.UsuarioId,
                FechaCreo = DateTime.Now,
                Permisos = dto.Permisos.ToModel(dto.UsuarioId)
            };
        }

        public static Role ToModel(this RoleFormDto dto, Role model)
        {
            if (dto == null || model == null) return null;

            return new Role
            {
                Id = model.Id,
                Nombre = dto.Nombre,
                Activo = dto.Activo,
                UsuarioCreoId = model.UsuarioCreoId,
                FechaCreo = model.FechaCreo,
                UsuarioModificoId = dto.UsuarioId,
                FechaModifico = DateTime.Now,
                Permisos = dto.Permisos.ToModel(model.Permisos, model.Id, dto.UsuarioId)
            };
        }

        public static List<RolePermission> ToModel(this IEnumerable<RolePermissionDto> dto, Guid userId)
        {
            if (dto == null) return null;

            var permissions = dto
                .GroupBy(x => x.MenuId)
                .Select(x => new RolePermission
                {
                    MenuId = x.Key,
                    Acceder = x.FirstOrDefault(p => p.Tipo == PP.AccessType).Asignado,
                    Crear = x.FirstOrDefault(p => p.Tipo == PP.CreateType).Asignado,
                    Modificar = x.FirstOrDefault(p => p.Tipo == PP.UpdateType).Asignado,
                    Imprimir = x.FirstOrDefault(p => p.Tipo == PP.PrintType).Asignado,
                    Descargar = x.FirstOrDefault(p => p.Tipo == PP.DownloadType).Asignado,
                    EnviarCorreo = x.FirstOrDefault(p => p.Tipo == PP.MailType).Asignado,
                    EnviarWapp = x.FirstOrDefault(p => p.Tipo == PP.WappType).Asignado,
                    UsuarioCreoId = userId,
                    FechaCreo = DateTime.Now,
                }).ToList();

            return permissions;
        }

        public static List<RolePermission> ToModel(this IEnumerable<RolePermissionDto> dto, IEnumerable<RolePermission> model, Guid roleId, Guid userId)
        {
            if (dto == null || model == null) return null;

            var permissions = dto
                .GroupBy(x => x.MenuId)
                .Select(x =>
                {
                    var permission = model.FirstOrDefault(m => m.MenuId == x.Key);

                    return new RolePermission
                    {
                        MenuId = x.Key,
                        RolId = roleId,
                        Acceder = x.FirstOrDefault(p => p.Tipo == PP.AccessType).Asignado,
                        Crear = x.FirstOrDefault(p => p.Tipo == PP.CreateType).Asignado,
                        Modificar = x.FirstOrDefault(p => p.Tipo == PP.UpdateType).Asignado,
                        Imprimir = x.FirstOrDefault(p => p.Tipo == PP.PrintType).Asignado,
                        Descargar = x.FirstOrDefault(p => p.Tipo == PP.DownloadType).Asignado,
                        EnviarCorreo = x.FirstOrDefault(p => p.Tipo == PP.MailType).Asignado,
                        EnviarWapp = x.FirstOrDefault(p => p.Tipo == PP.WappType).Asignado,
                        UsuarioCreoId = permission?.UsuarioCreoId ?? userId,
                        FechaCreo = permission?.FechaCreo ?? DateTime.Now,
                        UsuarioModificoId = permission == null ? null : userId,
                        FechaModifico = permission == null ? null : DateTime.Now
                    };
                }).ToList();

            return permissions;
        }
    }
}
