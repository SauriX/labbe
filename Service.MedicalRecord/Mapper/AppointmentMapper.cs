using Service.MedicalRecord.Domain.Appointments;
using Service.MedicalRecord.Domain.PriceQuote;
using Service.MedicalRecord.Dtos.Appointment;
using Service.MedicalRecord.Dtos.PriceQuote;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.MedicalRecord.Mapper
{
    public static class AppointmentMapper
    {
        public static AppointmentList ToApointmentListDtoLab(this AppointmentLab x)
        {
            if (x == null) return null;

            return new AppointmentList
            {
                Id = x.Id.ToString(),
                NoSolicitud = x.Cita,
                Fecha = x.FechaCita,
                Direccion = x.Direccion,
                Nombre = x.NombrePaciente,
                Edad = x.Edad.ToString(),
                Sexo = x.Genero,
                Tipo = x.Status,
                Expediente= x.Expediente.Expediente
            };
        }
        public static AppointmentList ToApointmentListDtoDom(this AppointmentDom x)
        {
            if (x == null) return null;

            return new AppointmentList
            {
                Id = x.Id.ToString(),
                NoSolicitud = x.Cita,
                Fecha = x.FechaCita,
                Direccion = x.Direccion,
                Nombre = x.NombrePaciente,
                Tipo = x.Status,
                Sexo = x.Genero,
                Expediente = x.Expediente.Expediente
            };
        }
        public static List<AppointmentList> ToApointmentListDtoLab(this List<AppointmentLab> model)
        {
            if (model == null) return null;

            return model.Select(x => new AppointmentList
            {
                Id = x.Id.ToString(),
                NoSolicitud = x.Cita,
                Fecha = x.FechaCita,
                Direccion = x.Direccion,
                Nombre = x.NombrePaciente,
                Edad = x.Edad.ToString(),
                Sexo = x.Genero,
                Tipo = x.Status,
                Expediente = x.Expediente.Expediente
            }).ToList();
        }

        public static List<AppointmentList> ToApointmentListDtoDom(this List<AppointmentDom> model)
        {
            if (model == null) return null;

            return model.Select(x => new AppointmentList
            {
                Id = x.Id.ToString(),
                NoSolicitud = x.Cita,
                Fecha = x.FechaCita,
                Direccion = x.Direccion,
                Nombre = x.NombrePaciente,
                Tipo = x.Status,
                Sexo = x.Genero,
                Expediente = x.Expediente.Expediente
            }).ToList();
        }

        public static AppointmentForm ToDtoAppointmentLab(this AppointmentLab model) {
            if (model == null) return null;

            return new AppointmentForm
            {
                Id = model.Id.ToString(),
                expediente = model.Expediente.Expediente,
                expedienteid = model.ExpedienteId.ToString(),
                nomprePaciente = model.NombrePaciente,
                edad = model.Edad,
                //cargo = model.,
                //typo =model.t,
                //fechaNacimiento =model.fe,
                estudy = model.Estudios.Select(x => new QuotetPrice
                {
                    PrecioListaId = x.CotizacionId,
                    ListaPrecioId = x.ListaPrecioId,
                    PromocionId = x.PromocionId??0,
                    EstudioId = x.EstudioId ?? 0,
                    PaqueteId = x.PaqueteId ?? 0 ,
                    EstatusId = x.EstatusId ,
                    AplicaDescuento = x.Descuento ,
                    AplicaCargo = x.Cargo,
                    AplicaCopago = x.Copago,
                    Precio = x.Precio,
                    PrecioFinal = x.PrecioFinal,
                    Nombre = x.Clave,

                }).ToList(),
                generales =new AppointmentGeneral
                {
                    procedencia=model.Procedencia,
                    compañia=model.CompaniaID.ToString(),
                    medico=model.MedicoID.ToString(),
                    nomprePaciente=model.NombrePaciente,
                    //observaciones=model.,
                    //tipoEnvio=model.en,
                    email=model.Email,
                    whatssap=model.WhatsApp,
                    activo=model.Activo,
                  },
                Genero =model.Genero,
                SucursalId =model.SucursalID,
                tipo = "laboratorio",
                status = model.Status,
                fecha = model.FechaCita
            };


        }

        public static AppointmentForm ToDtoAppointmentDom(this AppointmentDom model)
        {
            if (model == null) return null;

            return new AppointmentForm
            {
                Id = model.Id.ToString(),
                expediente = model.Expediente.Expediente,
                expedienteid = model.ExpedienteId.ToString(),
                nomprePaciente = model.NombrePaciente,
                //edad = model.Edad,
                //cargo = model.,
                //typo =model.t,
                //fechaNacimiento =model.fe,
                estudy = model.Estudios.Select(x => new QuotetPrice
                {
                    PrecioListaId = x.CotizacionId,
                    ListaPrecioId = x.ListaPrecioId,
                    PromocionId = x.PromocionId,
                    EstudioId = x.EstudioId,
                    PaqueteId = x.PaqueteId,
                    EstatusId = x.EstatusId,
                    AplicaDescuento = x.Descuento,
                    AplicaCargo = x.Cargo,
                    AplicaCopago = x.Copago,
                    Precio = x.Precio,
                    PrecioFinal = x.PrecioFinal,
                    Nombre = x.Clave,

                }).ToList(),

                Genero = model.Genero,
                //SucursalId = model.SucursalID,
                tipo = "domicilio",
                generalesDom = new GeneralDomicilio
                {
                    //Recoleccion =,
                    Direccion =model.Direccion,
                    //General =,
                    NomprePaciente =model.NombrePaciente,
                    //Observaciones =model.on,
                    //TipoEnvio =,
                    Email =  model.Email,
                    Whatssap =model.WhatsApp,
                    Activo =model.Activo,
                },
                status=model.Status,
                fecha=model.FechaCita
            };


        }

        public static  AppointmentLab toModel(this AppointmentForm dto) {
            if (dto == null) return null;

            return new AppointmentLab {
                ExpedienteId = Guid.Parse(dto.expedienteid),
                NombrePaciente = dto.nomprePaciente,
                Edad = dto.edad,
                Direccion = "",
                Procedencia = dto.generales.procedencia,
                CompaniaID = Guid.Parse(dto.generales.compañia),
                MedicoID = Guid.Parse(dto.generales.medico),
                //  public string AfilacionID
                Genero = dto.Genero,
                //Estatus_CitaId
                Email = dto.generales.email,
                WhatsApp =dto.generales.whatssap,
                SucursalID = dto.SucursalId,
                FechaCita = DateTime.Now,
                HoraCita =DateTime.Now,
                Activo =dto.generales.activo,
                UsuarioCreoId =dto.UserId,
                FechaCreo =DateTime.Now,
                Estudios = dto.estudy.Select(x => new CotizacionStudy
                {
                    CotizacionId = x.PrecioListaId,
                    ListaPrecioId = x.ListaPrecioId,
                    PromocionId = x.PromocionId,
                    EstudioId = x.EstudioId,
                    PaqueteId = x.PaqueteId,
                    EstatusId = x.EstatusId,
                    Descuento = x.AplicaDescuento,
                    Cargo = x.AplicaCargo,
                    Copago = x.AplicaCopago,
                    Precio = x.Precio,
                    PrecioFinal = x.PrecioFinal,
                    Clave = x.Nombre
                }),
                Status = 1
            };

           
        }
        public static AppointmentDom toModelDom(this AppointmentForm dto)
        {
            if (dto == null) return null;

            return new AppointmentDom
            {
                ExpedienteId = Guid.Parse(dto.expedienteid),
                NombrePaciente = dto.nomprePaciente,
                //Edad = dto.edad,
                Direccion = dto.generalesDom.Direccion,
                //Procedencia = dto.generales.procedencia,
                //CompaniaID = Guid.Parse(dto.generales.compañia),
                //MedicoID = Guid.Parse(dto.generales.medico),
                //  public string AfilacionID
                Genero = dto.Genero,
                //Estatus_CitaId
                Email = dto.generalesDom.Email,
                WhatsApp = dto.generalesDom.Whatssap,
                //SucursalID = dto.SucursalId,
                FechaCita = DateTime.Now,
                HoraCita = DateTime.Now,
                Activo = dto.generalesDom.Activo,
                UsuarioCreoId = dto.UserId,
                FechaCreo = DateTime.Now,
                Estudios = dto.estudy.Select(x => new CotizacionStudy
                {
                    CotizacionId = x.PrecioListaId,
                    ListaPrecioId = x.ListaPrecioId,
                    PromocionId = x.PromocionId,
                    EstudioId = x.EstudioId,
                    PaqueteId = x.PaqueteId,
                    EstatusId = x.EstatusId,
                    Descuento = x.AplicaDescuento,
                    Cargo = x.AplicaCargo,
                    Copago = x.AplicaCopago,
                    Precio = x.Precio,
                    PrecioFinal = x.PrecioFinal,
                    Clave = x.Nombre
                }),
                Status = 1
        };
          

        }
        public static AppointmentLab toModel(this AppointmentForm dto,AppointmentLab model)
        {
            if (dto == null) return null;

            return new AppointmentLab
            {
                Id = model.Id,
                ExpedienteId = Guid.Parse(dto.expedienteid),
                NombrePaciente = dto.nomprePaciente,
                Edad = dto.edad,
                Direccion = "",
                Procedencia = dto.generales.procedencia,
                CompaniaID = Guid.Parse(dto.generales.compañia),
                MedicoID = Guid.Parse(dto.generales.medico),
                //  public string AfilacionID
                Genero = dto.Genero,
                //Estatus_CitaId
                Email = dto.generales.email,
                WhatsApp = dto.generales.whatssap,
                SucursalID = dto.SucursalId,
                FechaCita = dto.fecha,
                HoraCita = dto.fecha,
                Activo = dto.generales.activo,
                UsuarioCreoId = model.UsuarioCreoId,
                FechaCreo = model.FechaCreo,
                UsuarioModId= dto.UserId,
                FechaMod = DateTime.Now,
                Estudios = dto.estudy.Select(x => new CotizacionStudy
                {
                    CotizacionId = x.PrecioListaId,
                    ListaPrecioId = x.ListaPrecioId,
                    PromocionId = x.PromocionId,
                    EstudioId = x.EstudioId,
                    PaqueteId = x.PaqueteId,
                    EstatusId = x.EstatusId,
                    Descuento = x.AplicaDescuento,
                    Cargo = x.AplicaCargo,
                    Copago = x.AplicaCopago,
                    Precio = x.Precio,
                    PrecioFinal = x.PrecioFinal,
                    Clave = x.Nombre
                }),
                Status = dto.status,
                Cita = model.Cita
        };

           
        }
        public static AppointmentDom toModelDom(this AppointmentForm dto, AppointmentDom model)
        {
            if (dto == null) return null;

            return new AppointmentDom
            {
                ExpedienteId = Guid.Parse(dto.expedienteid),
                NombrePaciente = dto.nomprePaciente,
                //Edad = dto.edad,
                Direccion = dto.generalesDom.Direccion,
                //Procedencia = dto.generales.procedencia,
                //CompaniaID = Guid.Parse(dto.generales.compañia),
                //MedicoID = Guid.Parse(dto.generales.medico),
                //  public string AfilacionID
                Genero = dto.Genero,
                //Estatus_CitaId
                Email = dto.generalesDom.Email,
                WhatsApp = dto.generalesDom.Whatssap,
                //SucursalID = dto.SucursalId,
                FechaCita = dto.fecha,
                HoraCita = dto.fecha,
                Activo = dto.generalesDom.Activo,
                UsuarioCreoId = dto.UserId,
                FechaCreo = DateTime.Now,
                Estudios = dto.estudy.Select(x => new CotizacionStudy
                {
                    CotizacionId = x.PrecioListaId,
                    ListaPrecioId = x.ListaPrecioId,
                    PromocionId = x.PromocionId,
                    EstudioId = x.EstudioId,
                    PaqueteId = x.PaqueteId,
                    EstatusId = x.EstatusId,
                    Descuento = x.AplicaDescuento,
                    Cargo = x.AplicaCargo,
                    Copago = x.AplicaCopago,
                    Precio = x.Precio,
                    PrecioFinal = x.PrecioFinal,
                    Clave = x.Nombre
                }),
                Status = dto.status,
                Cita = model.Cita
            };


        }
    }
}
