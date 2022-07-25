using Service.Sender.Domain.EmailConfiguration;
using Service.Sender.Dtos;

namespace Service.Sender.Mapper
{
    public static class EmailConfigurationMapper
    {
        public static EmailConfigurationDto ToEmailconfigurationDto(this EmailConfiguration model)
        {
            if (model == null) return null;

            return new EmailConfigurationDto
            {
                Remitente = model.Remitente,
                Correo = model.Correo,
                Smtp = model.Smtp,
                RequiereContraseña = model.RequiereContraseña,
                Contraseña = model.Contraseña
            };
        }

        public static EmailConfiguration ToModel(this EmailConfigurationDto dto)
        {
            if (dto == null) return null;

            return new EmailConfiguration
            {
                Remitente = dto.Remitente,
                Correo = dto.Correo,
                Smtp = dto.Smtp,
                RequiereContraseña = dto.RequiereContraseña,
                Contraseña = dto.Contraseña
            };
        }
    }
}
