using Service.MedicalRecord.Dictionary;
using Service.MedicalRecord.Domain.Quotation;
using Service.MedicalRecord.Dtos.Quotation;
using Service.MedicalRecord.Dtos.Request;
using Shared.Dictionary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Service.MedicalRecord.Mapper
{
    public static class QuotationMapper
    {
        private const byte VIGENTE = 1;
        private const byte PARTICULAR = 2;
        private const byte URGENCIA_NORMAL = 1;
        //private const byte DESCUENTO_PORCENTAJE = 1;
        private const byte DESCUENTO_DINERO = 2;

        public static QuotationDto ToQuotationDto(this Quotation model)
        {
            if (model == null) return null;

            return new QuotationDto
            {
                CotizacionId = model.Id,
                NombreMedico = model.Medico?.Nombre,
                NombreCompania = model.Compañia == null ? "" : model.Compañia.Nombre,
                ClaveMedico = model.Medico?.Clave,
                Observaciones = model.Observaciones,
                ExpedienteId = model.ExpedienteId,
                SucursalId = model.SucursalId,
                Clave = model.Clave,
                Registro = $"{model.FechaCreo:dd/MM/yyyy}"
            };
        }

        public static IEnumerable<QuotationInfoDto> ToQuotationInfoDto(this IEnumerable<Quotation> model)
        {
            if (model == null) return new List<QuotationInfoDto>();

            return model.Select(x => new QuotationInfoDto
            {
                CotizacionId = x.Id,
                Clave = x.Clave,
                Paciente = x.Expediente?.NombreCompleto,
                Expediente = x.Expediente?.Expediente,
                Correo = x.EnvioCorreo,
                Whatsapp = x.EnvioWhatsApp,
                Fecha = x.FechaCreo,
                Activo = x.Activo,
                Estudios = x.Estudios.Select(s => new QuotationStudyInfoDto
                {
                    Id = s.Id,
                    Clave = s.Clave,
                    Nombre = s.Nombre,
                })
            });
        }

        public static QuotationGeneralDto ToQuotationGeneralDto(this Quotation model)
        {
            if (model == null) return null;

            return new QuotationGeneralDto
            {
                CotizacionId = model.Id,
                Procedencia = model.Procedencia,
                CompañiaId = model.CompañiaId,
                MedicoId = model.MedicoId,
                Correo = model.EnvioCorreo,
                WhatsApp = model.EnvioWhatsApp,
                Observaciones = model.Observaciones
            };
        }

        public static QuotationTotalDto ToQuotationTotalDto(this Quotation model)
        {
            if (model == null) return null;

            return new QuotationTotalDto
            {
                TotalEstudios = model.TotalEstudios,
                Cargo = model.Cargo,
                CargoTipo = model.CargoTipo,
                Total = model.Total
            };
        }

        //public static QuotationOrderDto ToQuotationOrderDto(this Quotation model)
        //{
        //    if (model == null) return null;

        //    var studies = model.Paquetes?.SelectMany(x => x.Estudios)?.ToList() ?? new List<QuotationStudy>();
        //    studies.AddRange(model.Estudios ?? new List<QuotationStudy>());

        //    return new QuotationOrderDto
        //    {
        //        Clave = model.Clave,
        //        Sucursal = model.Sucursal.Nombre,
        //        FechaVenta = DateTime.Now.ToString("yyyy-MM-dd"),
        //        FechaCotizacion = model.FechaCreo.ToString("dd/MM/yyyy"),
        //        Fecha = model.FechaCreo.ToString("dd/MM/yyyy"),
        //        Personal = model.UsuarioCreo,
        //        Paciente = model.Expediente.NombreCompleto,
        //        FechaNacimiento = model.Expediente.FechaDeNacimiento.ToString("dd-MM-yyyy"),
        //        Edad = model.Expediente.Edad.ToString(),
        //        Sexo = model.Expediente.Genero,
        //        TelefonoPaciente = model.EnvioWhatsApp ?? model.Expediente.Telefono,
        //        Expediente = model.Expediente.Expediente,
        //        Medico = model.Medico?.Nombre,
        //        Compañia = model.Procedencia == PARTICULAR ? "Particular" : model.Compañia?.Nombre,
        //        Correo = model.EnvioCorreo,
        //        Observaciones = model.Observaciones,
        //        Total = model.Total.ToString("C"),
        //        Descuento = model.DescuentoTipo == DESCUENTO_DINERO ? model.Descuento.ToString("C") : (model.TotalEstudios * model.Descuento).ToString("C"),
        //        Cargo = model.CargoTipo == DESCUENTO_DINERO ? model.Cargo.ToString("C") : (model.TotalEstudios * model.Cargo).ToString("C"),
        //        Estudios = studies.Select(x => new QuotationOrderStudyDto
        //        {
        //            Clave = x.Clave,
        //            Estudio = x.Nombre,
        //            Precio = x.Precio.ToString("C")
        //        }).ToList()
        //    };
        //}

        public static List<QuotationPackDto> ToQuotationPackDto(this IEnumerable<QuotationPack> model)
        {
            if (model == null) return new List<QuotationPackDto>();

            return model.Select(x => new QuotationPackDto
            {
                Id = x.Id,
                CotizacionId = x.CotizacionId,
                PaqueteId = x.PaqueteId,
                Clave = x.Clave,
                Nombre = x.Nombre,
                ListaPrecioId = x.ListaPrecioId,
                ListaPrecio = x.ListaPrecio,
                Dias = x.Dias,
                Horas = x.Horas,
                AplicaCargo = x.AplicaCargo,
                Precio = x.Precio,
                Descuento = x.Descuento,
                DescuentoPorcentaje = x.DescuentoPorcentaje,
                PrecioFinal = x.PrecioFinal,
                Estudios = x.Estudios.ToQuotationStudyDto()
            }).ToList();
        }

        public static List<QuotationStudyDto> ToQuotationStudyDto(this IEnumerable<QuotationStudy> model)
        {
            if (model == null) return new List<QuotationStudyDto>();

            return model.Select(x => new QuotationStudyDto
            {
                Id = x.Id,
                CotizacionId = x.CotizacionId,
                EstudioId = x.EstudioId,
                Clave = x.Clave,
                Nombre = x.Nombre,
                PaqueteId = x.PaqueteId,
                ListaPrecioId = x.ListaPrecioId,
                ListaPrecio = x.ListaPrecio,
                Dias = x.Dias,
                Horas = x.Horas,
                AplicaCargo = x.AplicaCargo,
                Precio = x.Precio,
                Descuento = x.Descuento,
                DescuentoPorcentaje = x.DescuentoPorcentaje,
                PrecioFinal = x.PrecioFinal
            }).ToList();
        }

        public static RequestConvertDto ToRequestConvertDto(this Quotation quotation, Guid userId, string userName)
        {
            if (quotation == null) return null;

            var requestInfo = new RequestConvertDto
            {
                ExpedienteId = (Guid)quotation.ExpedienteId,
                SucursalId = quotation.SucursalId,
                Usuario = userName,
                UsuarioId = userId,
                General = new RequestGeneralDto
                {
                    Activo = true,
                    CompañiaId = quotation.CompañiaId,
                    MedicoId = quotation.MedicoId,
                    Observaciones = quotation.Observaciones,
                    Procedencia = quotation.Procedencia,
                    UsuarioId = userId,
                    Correo = quotation.EnvioCorreo,
                    Whatsapp = quotation.EnvioWhatsApp
                },
                Estudios = quotation.Estudios.Select(x => new RequestStudyDto
                {
                    AplicaCargo = x.AplicaCargo,
                    AreaId = null,// x.AreaId,
                    Clave = x.Clave,
                    DepartamentoId = null,// x.DepartamentoId,
                    Descuento = x.Descuento,
                    DescuentoPorcentaje = x.DescuentoPorcentaje,
                    Dias = x.Dias,
                    EstatusId = Status.RequestStudy.Pendiente,
                    EstudioId = x.EstudioId,
                    FechaEntrega = DateTime.Now.AddHours(x.Horas),
                    Horas = x.Horas,
                    ListaPrecio = x.ListaPrecio,
                    ListaPrecioId = x.ListaPrecioId,
                    Nombre = x.Nombre,
                    PaqueteId = x.PaqueteId,
                    Precio = x.Precio,
                    PrecioFinal = x.PrecioFinal,
                    Promocion = x.Promocion,
                    PromocionId = x.PromocionId,
                    UsuarioActualizacion = userName
                }).ToList(),
                Paquetes = quotation.Paquetes.Select(x => new RequestPackDto
                {
                    PaqueteId = x.PaqueteId,
                    AplicaCargo = x.AplicaCargo,
                    AreaId = Catalogs.Area.PAQUETES,
                    Clave = x.Clave,
                    DepartamentoId = Catalogs.Department.PAQUETES,
                    Descuento = x.Descuento,
                    DescuentoPorcentaje = x.DescuentoPorcentaje,
                    Dias = x.Dias,
                    Horas = x.Horas,
                    ListaPrecio = x.ListaPrecio,
                    ListaPrecioId = x.ListaPrecioId,
                    Nombre = x.Nombre,
                    Precio = x.Precio,
                    PrecioFinal = x.PrecioFinal,
                    Promocion = x.Promocion,
                    PromocionId = x.PromocionId,
                }).ToList(),
            };

            return requestInfo;
        }

        public static Quotation ToModel(this QuotationDto dto)
        {
            if (dto == null) return null;

            return new Quotation
            {
                Id = Guid.NewGuid(),
                ExpedienteId = dto.ExpedienteId,
                SucursalId = dto.SucursalId,
                Clave = dto.Clave,
                Procedencia = PARTICULAR,
                EstatusId = VIGENTE,
                UsuarioCreoId = dto.UsuarioId,
                FechaCreo = DateTime.Now,
                Activo = true,
            };
        }

        public static List<QuotationPack> ToModel(this IEnumerable<QuotationPackDto> dto, Guid quotationId, IEnumerable<QuotationPack> packs, Guid userId)
        {
            if (dto == null) return new List<QuotationPack>();

            return dto.Select(x =>
            {
                var pack = packs?.FirstOrDefault(s => s.Id == x.Id);

                return new QuotationPack
                {
                    Id = x.Id,
                    CotizacionId = quotationId,
                    PaqueteId = x.PaqueteId,
                    Clave = x.Clave,
                    Nombre = x.Nombre,
                    ListaPrecioId = x.ListaPrecioId,
                    ListaPrecio = x.ListaPrecio,
                    AplicaCargo = x.AplicaCargo,
                    Dias = x.Dias,
                    Horas = x.Horas,
                    Precio = x.Precio,
                    Descuento = x.Descuento,
                    DescuentoPorcentaje = x.DescuentoPorcentaje,
                    PrecioFinal = x.PrecioFinal,
                    UsuarioCreoId = pack?.UsuarioCreoId ?? userId,
                    FechaCreo = pack?.FechaCreo ?? DateTime.Now,
                    UsuarioModificoId = pack == null ? null : userId,
                    FechaModifico = pack == null ? null : DateTime.Now
                };
            }).ToList();
        }

        public static List<QuotationStudy> ToModel(this IEnumerable<QuotationStudyDto> dto, Guid quotationId, IEnumerable<QuotationStudy> studies, Guid userId)
        {
            if (dto == null) return new List<QuotationStudy>();

            return dto.Select(x =>
            {
                var study = studies?.FirstOrDefault(s => s.Id == x.Id);

                return new QuotationStudy
                {
                    Id = x.Id,
                    CotizacionId = quotationId,
                    EstudioId = x.EstudioId,
                    Clave = x.Clave,
                    Nombre = x.Nombre,
                    PaqueteId = x.PaqueteId,
                    ListaPrecioId = x.ListaPrecioId,
                    ListaPrecio = x.ListaPrecio,
                    AplicaCargo = x.AplicaCargo,
                    Dias = x.Dias,
                    Horas = x.Horas,
                    Precio = x.Precio,
                    Descuento = x.Descuento,
                    DescuentoPorcentaje = x.DescuentoPorcentaje,
                    PrecioFinal = x.PrecioFinal,
                    UsuarioCreoId = study?.UsuarioCreoId ?? userId,
                    FechaCreo = study?.FechaCreo ?? DateTime.Now,
                    UsuarioModificoId = study == null ? null : userId,
                    FechaModifico = study == null ? null : DateTime.Now,
                };
            }).ToList();
        }
    }
}
