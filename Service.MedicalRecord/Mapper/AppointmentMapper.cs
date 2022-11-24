﻿using Service.MedicalRecord.Domain.Appointments;
using Service.MedicalRecord.Domain.Quotation;
using Service.MedicalRecord.Dtos.Appointment;
using Service.MedicalRecord.Dtos.Quotation;
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
                Expediente = x.Expediente.Expediente,
                //  Estudios = 
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
                Expediente = x.Expediente.Expediente,
                Estudios = x.Estudios.Count()>0?x.Estudios.First().Clave:"",
                Type = "laboratorio",
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
                Expediente = x.Expediente.Expediente,
                Type = "domicilio"
            }).ToList();
        }

        public static AppointmentForm ToDtoAppointmentLab(this AppointmentLab model)
        {
            if (model == null) return null;

            return new AppointmentForm
            {
                Id = model.Id.ToString(),
                expediente = model.Expediente.Expediente,
                expedienteid = model.ExpedienteId.ToString(),
                nomprePaciente = model.NombrePaciente,
                edad = model.Edad,
                //cargo = model.Expediente.ca,
                //typo =model.t,
                fechaNacimiento =model.Expediente.FechaDeNacimiento,
                estudy = model.Estudios.ToList(),
                generales = new AppointmentGeneral
                {
                    procedencia = model.Procedencia,
                    compañia = model.CompaniaID.ToString(),
                    medico = model.MedicoID.ToString(),
                    nomprePaciente = model.NombrePaciente,
                    //observaciones=model.,
                    //tipoEnvio=model.en,
                    email = model.Email,
                    whatssap = model.WhatsApp,
                    activo = model.Activo,
                },
                Genero = model.Genero,
                SucursalId = model.SucursalID,
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
                fechaNacimiento =model.Expediente.FechaDeNacimiento,
                estudy = model.Estudios.ToList(),

                Genero = model.Genero,
                //SucursalId = model.SucursalID,
                tipo = "domicilio",
                generalesDom = new GeneralDomicilio
                {
                    //Recoleccion =,
                    Direccion = model.Direccion,
                    //General =,
                    NomprePaciente = model.NombrePaciente,
                    //Observaciones =model.on,
                    //TipoEnvio =,
                    Email = model.Email,
                    Whatssap = model.WhatsApp,
                    Activo = model.Activo,
                },
                status = model.Status,
                fecha = model.FechaCita
            };


        }

        public static AppointmentLab toModel(this AppointmentForm dto)
        {
            if (dto == null) return null;

            return new AppointmentLab
            {
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
                FechaCita = DateTime.Now,
                HoraCita = DateTime.Now,
                Activo = dto.generales.activo,
                UsuarioCreoId = dto.UserId,
                FechaCreo = DateTime.Now,
                Estudios = dto.estudy,
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
                Estudios = dto.estudy,
                Status = 1
            };


        }
        public static AppointmentLab toModel(this AppointmentForm dto, AppointmentLab model)
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
                UsuarioModId = dto.UserId,
                FechaMod = DateTime.Now,
                Estudios = dto.estudy,
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
                Estudios = dto.estudy,
                Status = dto.status,
                Cita = model.Cita
            };


        }
    }
}
