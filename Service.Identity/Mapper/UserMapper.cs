using Service.Identity.Domain.Users;
using Service.Identity.Dtos;
using System;

namespace  Service.Identity.Mapper
{
    public static class UserMapper
    {
        public static UserList ToUserListDto(this UsersModel model)
        {
            if (model == null) return null;

            return new UserList
            {
                IdUsuario = model.IdUsuario,
                IdSucursal = model.IdSucursal,
                Nombre = model.Nombre,
                PrimerApellido = model.PrimerApellido,
                SegundoApellido = model.SegundoApellido,
                IdRol=model.IdRol,
                Activo = model.Activo,
            };
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

