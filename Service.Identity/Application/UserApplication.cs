﻿using Service.Identity.Application.IApplication;
using Service.Identity.Repository.IRepository;
using Shared.Dictionary;
using Shared.Error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Service.Identity.Mapper;
using ClosedXML.Excel;
using Service.Identity.Dictionary;
using ClosedXML.Report;
using Shared.Extensions;
using Service.Identity.Domain.User;
using Service.Identity.Dtos.User;
using Microsoft.Extensions.Configuration;
using System.Text;
using Service.Identity.Dtos.Profile;
using System.Text.RegularExpressions;
using Service.Identity.Utils;

namespace Service.Identity.Application
{
    public class UserApplication : IUserApplication
    {
        private readonly string key;
        private readonly IUserRepository _repository;

        public UserApplication(IConfiguration configuration, IUserRepository repository)
        {
            key = configuration.GetValue<string>("PasswordKey");
            _repository = repository;
        }

        public async Task<IEnumerable<UserListDto>> GetAll(string search)
        {
            var users = await _repository.GetAll(search);

            return users.ToUserListDto();
        }

        public async Task<UserFormDto> GetById(string id)
        {
            var validGuid = Guid.TryParse(id, out Guid guid);

            if (!validGuid)
            {
                throw new CustomException(HttpStatusCode.BadRequest, Responses.NotFound);
            }

            var user = await _repository.GetById(guid);

            if (user == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            return user.ToUserFormDto(key);
        }

        public string GeneratePassword()
        {
            return GeneratePass(8);
        }

        public async Task<IEnumerable<UserPermissionDto>> GetPermission()
        {
            var permissions = await _repository.GetPermission();

            return permissions.ToUserPermissionDto();
        }

        public async Task<string> GenerateCode(UserCodeDto data, string suffix = null)
        {
            var code = data.Nombre.Substring(0, 1);
            code += data.PrimerApellido;
            code += suffix;

            var exists = await _repository.GetByCode(code);

            if (exists != null)
            {
                if (code.Length == (data.PrimerApellido.Length + 1))
                {
                    return await GenerateCode(data, "A");
                }

                var last = code[^1];
                var next = (char)(last + 1);

                return await GenerateCode(data, next.ToString());
            }

            return code;
        }

        public async Task<UserListDto> Create(UserFormDto user)
        {
            if (!string.IsNullOrWhiteSpace(user.Id))
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.NotPossible);
            }

            if (user.Contraseña != user.ConfirmaContraseña)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "Las contraseñas deben coincidir");
            }

            var newUser = user.ToModel(key);

            var isDuplicate = await _repository.IsDuplicate(newUser);

            if (isDuplicate)
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.Duplicated("El nombre"));
            }

            await _repository.Create(newUser);

            newUser = await _repository.GetById(newUser.Id);

            return newUser.ToUserListDto();
        }

        public async Task<UserListDto> Update(UserFormDto user)
        {
            var validGuid = Guid.TryParse(user.Id, out Guid guid);

            if (!validGuid)
            {
                throw new CustomException(HttpStatusCode.BadRequest, Responses.NotFound);
            }

            if (user.Contraseña != user.ConfirmaContraseña)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "Las contraseñas deben coincidir");
            }

            var existing = await _repository.GetById(guid);

            if (existing == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            var updatedUser = user.ToModel(existing, key);

            var isDuplicate = await _repository.IsDuplicate(updatedUser);

            if (isDuplicate)
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.Duplicated("El nombre"));
            }

            await _repository.Update(updatedUser);

            updatedUser = await _repository.GetById(guid);

            return updatedUser.ToUserListDto();
        }

        public async Task UpdatePassword(ChangePasswordFormDto data)
        {
            var validGuid = Guid.TryParse(data.Id, out Guid guid);

            if (!validGuid)
            {
                throw new CustomException(HttpStatusCode.BadRequest, Responses.NotFound);
            }

            var existing = await _repository.GetById(guid);

            if (existing == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            if (data.Contraseña != data.ConfirmaContraseña)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "Las contraseñas deben coincidir");
            }

            if (data.Contraseña?.Length != 8)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "La longitud debe ser de 8 caracteres");
            }

            existing.Contraseña = Crypto.EncryptString(data.Contraseña, key);
            existing.FlagPassword = true;
            existing.UsuarioModificoId = data.UsuarioId;
            existing.FechaModifico = DateTime.Now;

            await _repository.Update(existing);
        }

        public async Task<byte[]> ExportList(string search = null)
        {
            var users = await GetAll(search);

            var path = Assets.UserList;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Usuarios");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("Usuarios", users);

            template.Generate();

            var range = template.Workbook.Worksheet("Usuarios").Range("Usuarios");
            var table = template.Workbook.Worksheet("Usuarios").Range("$A$3:" + range.RangeAddress.LastAddress).CreateTable();
            table.Theme = XLTableTheme.TableStyleMedium2;

            template.Format();

            return template.ToByteArray();
        }

        public async Task<byte[]> ExportForm(string id)
        {
            var user = await GetById(id);

            var path = Assets.UserForm;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Usuarios");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("Usuario", user);
            template.AddVariable("Permisos", user.Permisos);
            template.Generate();

            return template.ToByteArray();
        }

        private static string GeneratePass(int lenght)
        {
            string password = string.Empty;
            string[] letters = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "ñ", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z",
                                        "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"};
            Random rand = new();

            for (int i = 0; i < lenght; i++)
            {
                int randomLetter = rand.Next(0, 100);
                int randomNumber = rand.Next(0, 9);

                if (randomLetter < letters.Length)
                {
                    password += letters[randomLetter];
                }
                else
                {
                    password += randomNumber.ToString();
                }
            }
            return password;
        }
    }
}