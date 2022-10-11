using Service.Identity.Domain.User;
using Service.Identity.Dtos.User;
using Shared.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using PP = Service.Identity.Dictionary.PermissionProps;

namespace Service.Identity.Mapper
{
    public static class UserMapper
    {
        public static UserListDto ToUserListDto(this User model)
        {
            if (model == null) return null;

            return new UserListDto
            {
                Id = model.Id.ToString(),
                Clave = model.Clave,
                Nombre = model.NombreCompleto,
                TipoUsuario = model.Rol.Nombre,
                Activo = model.Activo,
            };
        }

        public static IEnumerable<UserListDto> ToUserListDto(this List<User> model)
        {
            if (model == null) return null;

            return model.Select(x => new UserListDto
            {
                Id = x.Id.ToString(),
                Clave = x.Clave,
                Nombre = x.NombreCompleto,
                TipoUsuario = x.Rol.Nombre,
                Activo = x.Activo,
                SucursalId = x.SucursalId,
            });
        }

        public static UserFormDto ToUserFormDto(this User model, string key)
        {
            if (model == null) return null;
            List<string> images = new  List<string>();
            if (model.Imagenes != null) {   
                foreach (var imagen in model.Imagenes)
                {
                    var image = imagen.Ruta.Replace("wwwroot/images/users","");
                    images.Add(image);
                }
            }

            return new UserFormDto
            {
                Id = model.Id.ToString(),
                Clave = model.Clave,
                Nombre = model.Nombre,
                PrimerApellido = model.PrimerApellido,
                SegundoApellido = model.SegundoApellido,
                SucursalId = model.SucursalId,
                RolId = model.RolId,
                Rol = model.Rol.Nombre,
                Contraseña = Crypto.DecryptString(model.Contraseña, key),
                ConfirmaContraseña = Crypto.DecryptString(model.Contraseña, key),
                Activo = model.Activo,
                Permisos = model.Permisos.ToUserPermissionDto(),
                Images = images,
            };
        }

        public static List<UserPermissionDto> ToUserPermissionDto(this IEnumerable<UserPermission> model)
        {
            if (model == null) return null;

            var dto = new List<UserPermissionDto>();

            int i = 1;
            foreach (var item in model)
            {
                dto.Add(new UserPermissionDto(i++, item.MenuId, item.Menu.Descripcion, PP.Access, item.Acceder, PP.AccessType));
                dto.Add(new UserPermissionDto(i++, item.MenuId, item.Menu.Descripcion, PP.Create, item.Crear, PP.CreateType));
                dto.Add(new UserPermissionDto(i++, item.MenuId, item.Menu.Descripcion, PP.Update, item.Modificar, PP.UpdateType));
                dto.Add(new UserPermissionDto(i++, item.MenuId, item.Menu.Descripcion, PP.Print, item.Imprimir, PP.PrintType));
                dto.Add(new UserPermissionDto(i++, item.MenuId, item.Menu.Descripcion, PP.Download, item.Descargar, PP.DownloadType));
                dto.Add(new UserPermissionDto(i++, item.MenuId, item.Menu.Descripcion, PP.Mail, item.EnviarCorreo, PP.MailType));
                dto.Add(new UserPermissionDto(i++, item.MenuId, item.Menu.Descripcion, PP.Wapp, item.EnviarWapp, PP.WappType));
            }

            return dto;
        }

        public static User ToModel(this UserFormDto dto, string key)
        {
            if (dto == null) return null;

            return new User
            {
                Clave = dto.Clave,
                Nombre = dto.Nombre,
                PrimerApellido = dto.PrimerApellido,
                SegundoApellido = dto.SegundoApellido,
                SucursalId = dto.SucursalId,
                RolId = dto.RolId,
                Contraseña = Crypto.EncryptString(dto.Contraseña, key),
                Activo = dto.Activo,
                UsuarioCreoId = dto.UsuarioId,
                FechaCreo = DateTime.Now,
                Permisos = dto.Permisos.ToModel(dto.UsuarioId)
            };
        }

        public static User ToModel(this UserFormDto dto, User model, string key)
        {
            if (dto == null || model == null) return null;

            return new User
            {
                Id = model.Id,
                Clave = model.Clave,
                Nombre = dto.Nombre,
                PrimerApellido = dto.PrimerApellido,
                SegundoApellido = dto.SegundoApellido,
                SucursalId = dto.SucursalId,
                RolId = dto.RolId,
                Contraseña = Crypto.EncryptString(dto.Contraseña, key),
                Activo = dto.Activo,
                FlagPassword = model.FlagPassword,
                UsuarioCreoId = model.UsuarioCreoId,
                FechaCreo = model.FechaCreo,
                UsuarioModificoId = dto.UsuarioId,
                FechaModifico = DateTime.Now,
                Permisos = dto.Permisos.ToModel(model.Permisos, model.Id, dto.UsuarioId)
            };
        }

        public static List<UserPermission> ToModel(this IEnumerable<UserPermissionDto> dto, Guid userId)
        {
            if (dto == null) return null;

            var permissions = dto
                .GroupBy(x => x.MenuId)
                .Select(x => new UserPermission
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

        public static List<UserPermission> ToModel(this IEnumerable<UserPermissionDto> dto, IEnumerable<UserPermission> model, Guid userId, Guid editionUserId)
        {
            if (dto == null || model == null) return null;

            var permissions = dto
                .GroupBy(x => x.MenuId)
                .Select(x =>
                {
                    var permission = model.FirstOrDefault(m => m.MenuId == x.Key);

                    return new UserPermission
                    {
                        MenuId = x.Key,
                        UsuarioId = userId,
                        Acceder = x.FirstOrDefault(p => p.Tipo == PP.AccessType).Asignado,
                        Crear = x.FirstOrDefault(p => p.Tipo == PP.CreateType).Asignado,
                        Modificar = x.FirstOrDefault(p => p.Tipo == PP.UpdateType).Asignado,
                        Imprimir = x.FirstOrDefault(p => p.Tipo == PP.PrintType).Asignado,
                        Descargar = x.FirstOrDefault(p => p.Tipo == PP.DownloadType).Asignado,
                        EnviarCorreo = x.FirstOrDefault(p => p.Tipo == PP.MailType).Asignado,
                        EnviarWapp = x.FirstOrDefault(p => p.Tipo == PP.WappType).Asignado,
                        UsuarioCreoId = permission?.UsuarioCreoId ?? editionUserId,
                        FechaCreo = permission?.FechaCreo ?? DateTime.Now,
                        UsuarioModificoId = permission == null ? null : editionUserId,
                        FechaModifico = permission == null ? null : DateTime.Now
                    };
                }).ToList();

            return permissions;
        }
    }
}
