using Service.MedicalRecord.Dictionary;
using Service.MedicalRecord.Dtos.RportStudy;
using Shared.Dictionary;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.MedicalRecord.Mapper
{
    public static class ReportStudyMapper
    {
        public static List<ReportStudyListDto> toStudyList(this List<Service.MedicalRecord.Domain.Request.RequestStudy>  studys) {

            return studys.Select(x => new ReportStudyListDto
            {

                IdStudio = x.EstudioId.ToString(),
                Clave = x.Clave,
                Nombre = x.Nombre,
                Estatus = x.Estatus.Nombre,
                Fecha = x.FechaEntrega.ToShortDateString(),
                Regitro = x.FechaCreo.ToShortDateString(),
                Color = x.Estatus.Color,        
            
            }).ToList();
        }

        public static List<ReportRequestListDto> toRequestList(this List<Service.MedicalRecord.Domain.Request.Request> request ) { 
            return request.Select(x => new ReportRequestListDto {

            ExpedienteId = x.ExpedienteId.ToString(),
            SolicitudId = x.Id.ToString(),
            Solicitud = x.Clave,
            Paciente = x.Expediente.NombreCompleto,
            Edad = x.Expediente.Edad.ToString(),
            Sexo= x.Expediente.Genero,
            Sucursal = x.Sucursal.Nombre.ToString(),
            Medico = x.Medico.Nombre,
            Tipo = x.Urgencia ==1 ? "Normal":"Urgente",
            Compañia = x.Compañia.Nombre,
            Entrega = x.Estudios.Count() >1 ? $"{x.Estudios.Select(y=>y.FechaEntrega).Min().ToShortDateString()}-{x.Estudios.Select(y => y.FechaEntrega).Max().ToShortDateString()}" : x.Estudios.First().FechaEntrega.ToShortDateString(),
            Estudios= x.Estudios.ToList().toStudyList(),
            Estatus = x.Urgencia == 1 ? x.Estudios.Any(y => y.EstatusId == Status.RequestStudy.EnRuta) ? "Ruta": x.Estudios.Any(y => y.DepartamentoId == Catalogs.Department.PATOLOGIA) ? x.ClavePatologica:  "Normal" : "Urgente", 
            isPatologia = x.Estudios.Any(y=>y.DepartamentoId == Catalogs.Department.PATOLOGIA )
            }).ToList();
        
        
        }
    }
}
