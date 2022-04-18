using Service.Identity.Domain.Users;
using Service.Identity.Dtos;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

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
                    IdUsuario = user.Id,
                    IdSucursal = user.IdSucursal,
                    Nombre = user.Nombre,
                    PrimerApellido = user.PrimerApellido,
                    SegundoApellido = user.SegundoApellido,
                    IdRol = user.IdRol,
                    Activo = user.Activo,
                    clave = user.Clave,
                    TipoUsuario = "test",
                    confirmaContraseña = DecodeFrom64(user.Contraseña),
                    contraseña = DecodeFrom64(user.Contraseña)
                }); ;
            }
            return users;
        }

        public static UserList ToUserInfoDto(UsersModel model, List<UserPermission> permisos=null)
        {

            if (model == null) return null;
            return new UserList
            {
                IdUsuario = model.Id,
                IdSucursal = model.IdSucursal,
                Nombre = model.Nombre,
                PrimerApellido = model.PrimerApellido,
                SegundoApellido = model.SegundoApellido,
                IdRol = model.IdRol,
                Activo = model.Activo,
                clave = model.Clave,
                contraseña = DecodeFrom64(model.Contraseña),
                confirmaContraseña = DecodeFrom64(model.Contraseña),
                TipoUsuario = model.IdRol.ToString(),
                permisos = permisos,
            };
            
            
        }
        public static UsersModel ToregisterUSerDto(RegisterUserDTO model,string token)  
        {
            string jwt = token;
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = (JwtSecurityToken)tokenHandler.ReadToken(jwt);
            var claimValue = securityToken.Claims.FirstOrDefault(c => c.Type == "nameid")?.Value;
          
            if (model == null) return null;

            return new UsersModel
            {

                Activo = model.activo,
                Clave = model.Clave,
                Contraseña = EncodeTo64(model.Contraseña),
                FechaCreo = DateTime.Now,
                flagpassword = false,
                Id = Guid.NewGuid(),
                IdSucursal = model.IdSucursal,
                Nombre = model.Nombre,
                PrimerApellido = model.PrimerApellido,
                SegundoApellido = model.SegundoApellido,
                IdRol = Guid.Parse(model.tipoUsuario),
                UserName = model.Clave,
                UsuarioCreoId = Guid.NewGuid(),
                UsuarioModId = Guid.NewGuid(),
                FechaMod = DateTime.Now,

                
            };
        }
        public static UsersModel ToupdateUSerDto(RegisterUserDTO model, string token)
        {
            string jwt = token;
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = (JwtSecurityToken)tokenHandler.ReadToken(jwt);
            var claimValue = securityToken.Claims.FirstOrDefault(c => c.Type == "nameid")?.Value;

            if (model == null) return null;

            return new UsersModel
            {

                Activo = model.activo,
                Clave = model.Clave,
                Contraseña = model.Contraseña,
                FechaCreo = DateTime.Now,   
                flagpassword = false,
                Id = Guid.Parse(model.idUsuario),
                IdSucursal = model.IdSucursal,
                Nombre = model.Nombre,
                PrimerApellido = model.PrimerApellido,
                SegundoApellido = model.SegundoApellido,
                IdRol = Guid.Parse(model.tipoUsuario),
                UserName = model.Clave,
                UsuarioCreoId = Guid.Parse(claimValue),
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
                Activo = true,
                UsuarioCreoId = model.UsuarioCreoId,
                FechaCreo = model.FechaCreo,
                FechaMod = DateTime.Now,
            };
        }
        static public string EncodeTo64(string toEncode)

        {

            byte[] toEncodeAsBytes

                  = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);

            string returnValue

                  = System.Convert.ToBase64String(toEncodeAsBytes);

            
            return returnValue;

        }

        static public string DecodeFrom64(string encodedData)

        {
            string returnValue = "";
            if (!string.IsNullOrWhiteSpace(encodedData))
            {

                byte[] encodedDataAsBytes

                    = System.Convert.FromBase64String(encodedData);

                 returnValue =

                   System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);
            }
            return returnValue;

        }
    }
}

