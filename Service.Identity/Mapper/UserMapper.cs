using Service.Identity.Domain.Users;
using Service.Identity.Dtos;
using System;
using System.Collections.Generic;

namespace  Service.Identity.Mapper
{
    public static class UserMapper
    {
        public static IEnumerable<UserList> ToUserListDto(IEnumerable<UsersModel> modelList)
        {
            List<UserList> users = new List<UserList>();
            if (modelList == null) return null;
            foreach (UsersModel user in modelList) {
                users.Add(new UserList
                {
                    IdUsuario = user.IdUsuario,
                    IdSucursal = user.IdSucursal,
                    Nombre = user.Nombre,
                    PrimerApellido = user.PrimerApellido,
                    SegundoApellido = user.SegundoApellido,
                    IdRol = user.IdRol,
                    Activo = user.Activo,
                    clave = user.Clave,
                });
            }
            return users;
        }
        public static RegisterUserDTO ToregisterUSerDto<T>(this T model) where T : UsersModel
        {
            if (model == null) return null;

            return new RegisterUserDTO
            {
                IdUsuario = model.Id,
                Clave = model.Clave,
                Nombre = model.Nombre,
                Activo = model.Activo,
            };
        }

        public static T ToModel<T>(this RegisterUserDTO dto) where T : UsersModel, new()
        {
            if (dto == null) return null;

            return new T
            {
                Id = new Guid(),
                Clave = dto.Clave,
                Nombre = dto.Nombre,
                Activo = dto.Activo,
                UsuarioCreoId = dto.IdUsuario,
                FechaCreo = DateTime.Now,
            };
        }

        public static T ToModel<T>(this RegisterUserDTO dto, T model) where T : UsersModel, new()
        {
            if (dto == null || model == null) return null;

            return new T
            {
                Id = model.Id,
                Clave = dto.Clave,
                Nombre = dto.Nombre,
                Activo = dto.Activo,
                UsuarioCreoId = model.UsuarioCreoId,
                FechaCreo = model.FechaCreo,
                UsuarioModId = dto.IdUsuario,
                FechaMod = DateTime.Now,
            };
        }
    }
}

