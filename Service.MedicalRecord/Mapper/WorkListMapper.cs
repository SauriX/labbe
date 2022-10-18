using Service.MedicalRecord.Dtos.WorkList;
using Service.MedicalRecord.Domain.Request;
using System.Collections.Generic;
using System.Linq;

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
                    Solicitud = x.Clave,
                    Paciente = x.Expediente.NombreCompleto,
                    Estatus = "",
                    HoraSolicitud = x.FechaCreo.ToString("HH:mm"),
                    HoraEstatus = "",
                    Estudios = x.Estudios.Select(s => new WorkListStudyDto
                    {
                        EstudioId = s.EstudioId,
                        Estudio = s.Nombre,
                    }).ToList()
                }).ToList()
            };
        }
    }
}
