using Service.MedicalRecord.Dtos.WorkList;
using Service.MedicalRecord.Domain.Request;
using System.Collections.Generic;
using System.Linq;
using Service.MedicalRecord.Dictionary;
using System;

namespace Service.MedicalRecord.Mapper
{
    public static class WorkListMapper
    {
        public static WorkListDto ToWorkListDto(this List<Request> model)
        {
            return new WorkListDto
            {
                Solicitudes = model.Select(x => new WorkListRequestDto
                {
                    Id = x.Id,
                    Solicitud = x.Clave,
                    Paciente = x.Expediente.NombreCompleto.ToUpper(),
                    HoraSolicitud = x.FechaCreo.ToString("HH:mm"),
                    Estudios = x.Estudios.Select(s => new WorkListStudyDto
                    {
                        SolicitudEstudioId = s.Id,
                        EstudioId = s.EstudioId,
                        Estudio = s.Nombre.ToUpper(),
                        Estatus = s.Estatus.Nombre.ToUpper(),
                        HoraEstatus = GetStatusDate(s),
                    }).ToList()
                }).ToList()
            };
        }

        private static string GetStatusDate(RequestStudy study)
        {
            DateTime? date = new DateTime();

            switch (study.EstatusId)
            {
                case Status.RequestStudy.Pendiente:
                    date = study.FechaCreo;
                    break;
                case Status.RequestStudy.TomaDeMuestra:
                    date = study.FechaTomaMuestra;
                    break;
                case Status.RequestStudy.Solicitado:
                    date = study.FechaSolicitado;
                    break;
                case Status.RequestStudy.Capturado:
                    date = study.FechaCaptura;
                    break;
                case Status.RequestStudy.Validado:
                    date = study.FechaValidacion;
                    break;
                case Status.RequestStudy.Liberado:
                    date = study.FechaLiberado;
                    break;
                case Status.RequestStudy.Enviado:
                    date = study.FechaEnviado;
                    break;
                case Status.RequestStudy.EnRuta:
                    date = study.FechaEnviado;
                    break;
                case Status.RequestStudy.Cancelado:
                    date = study.FechaModifico;
                    break;
                case Status.RequestStudy.Entregado:
                    date = study.FechaEntrega;
                    break;
                default:
                    break;
            }

            return date?.ToString("dd/MM/yy HH:mm");
        }
    }
}