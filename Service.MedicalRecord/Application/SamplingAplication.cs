using ClosedXML.Report;
using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Dictionary;
using Service.MedicalRecord.Dtos.RequestedStudy;
using Service.MedicalRecord.Dtos.Sampling;
using Service.MedicalRecord.Mapper;
using Service.MedicalRecord.Repository.IRepository;
using Shared.Error;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using SharedResponses = Shared.Dictionary.Responses;
using RecordResponses = Service.MedicalRecord.Dictionary.Response;
using Service.MedicalRecord.Domain.Request;
using MoreLinq;
using MassTransit;
using Service.MedicalRecord.Client.IClient;
using EventBus.Messages.Common;
using ZXing;

namespace Service.MedicalRecord.Application
{
    public class SamplingAplication : ISamplingApplication
    {
        public readonly ISamplingRepository _repository;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ICatalogClient _catalogClient;
        public SamplingAplication(ISamplingRepository repository, IPublishEndpoint publishEndpoint, ICatalogClient catalogClient)
        {

            _repository = repository;
            _publishEndpoint = publishEndpoint;
            _catalogClient = catalogClient;
        }

        public async Task<(byte[] file, string fileName)> ExportList(RequestedStudySearchDto search)
        {
            var studies = await GetAll(search);

            foreach (var request in studies)
            {
                if (studies.Count > 0)
                {
                    request.Estudios.Insert(0, new StudyDto { Clave = "Clave", Nombre = "Nombre Estudio", Registro = "Fecha de Registro", Entrega = "Fecha de Entrega", NombreEstatus = "Estatus" });
                }
            }

            var path = Assets.InformeExpedientes;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Toma de Muestra de Estudio");
            template.AddVariable("FechaInicio", search.Fecha.First().ToString("dd/MM/yyyy"));
            template.AddVariable("FechaFinal", search.Fecha.Last().ToString("dd/MM/yyyy"));
            template.AddVariable("Solicitudes", studies);

            template.Generate();

            template.Workbook.Worksheets.ToList().ForEach(x =>
            {
                var cuenta = 0;
                int[] posiciones = { 0, 0 };
                x.Worksheet.Rows().ForEach(y =>
                {
                    var descripcion = template.Workbook.Worksheets.FirstOrDefault().Cell(y.RowNumber(), "B").Value.ToString();
                    var next = template.Workbook.Worksheets.FirstOrDefault().Cell(y.RowNumber() + 2, "B").Value.ToString();
                    var plusOne = template.Workbook.Worksheets.FirstOrDefault().Cell(y.RowNumber() + 1, "B").Value.ToString();
                    if (descripcion == "Clave" && cuenta == 0)
                    {
                        posiciones[0] = y.RowNumber();
                        cuenta++;
                    }
                    if ((next == "Clave" || (next == "" && plusOne == "")) && cuenta == 1)
                    {
                        posiciones[1] = y.RowNumber();
                        cuenta++;
                    }
                    if (cuenta == 2)
                    {
                        x.Rows(posiciones[0], posiciones[1]).Group();
                        x.Rows(posiciones[0], posiciones[1]).Collapse();
                        cuenta = 0;
                        posiciones.ForEach(z => z = 0);
                    }
                });
            });
            template.Format();

            return (template.ToByteArray(), $"Informe Toma de Muestra de Estudio.xlsx");
        }

        public async Task<List<SamplingListDto>> GetAll(RequestedStudySearchDto search)
        {

            var requestedStudy = await _repository.GetAll(search);





            if (requestedStudy != null)
            {

                return requestedStudy.ToSamplingListDto(search);

            }
            else
            {
                return null;
            }
        }

        public async Task<int> UpdateStatus(List<RequestedStudyUpdateDto> requestDto)
        {
            int studyCount = 0;
            foreach (var item in requestDto)
            {
                var request = await GetExistingRequest(item.SolicitudId);

                var studiesIds = item.EstudioId;
                var studies = await _repository.GetStudyById(item.SolicitudId, studiesIds);

                studies = studies.Where(x => x.EstatusId == Status.RequestStudy.Pendiente || x.EstatusId == Status.RequestStudy.TomaDeMuestra).ToList();

                if (studies == null || studies.Count == 0)
                {
                    throw new CustomException(HttpStatusCode.BadRequest, RecordResponses.Request.NoStudySelected);
                }

                foreach (var study in studies)
                {
                    if (study.EstatusId == Status.RequestStudy.Pendiente)
                    {
                        study.EstatusId = Status.RequestStudy.TomaDeMuestra;
                        study.FechaTomaMuestra = DateTime.Now;
                        study.UsuarioTomaMuestra = item.Usuario;

                    }
                    else
                    {
                        study.EstatusId = Status.RequestStudy.Pendiente;
                    }

                }
                studyCount += studies.Count;

                await _repository.BulkUpdateStudies(item.SolicitudId, studies);
                if (request.Urgencia != 1)
                {
                    var notifications = await _catalogClient.GetNotifications("Toma de muestra");
                    var createnotification = notifications.FirstOrDefault(x => x.Tipo == "Urgent");

                    if (createnotification.Activo)
                    {

                            var mensaje = createnotification.Contenido.Replace("[Nsolicitud]", request.Clave);
                            var contract = new NotificationContract(mensaje, false);
                            await _publishEndpoint.Publish(contract);
                        
                    }


                }

            }

            return studyCount;
        }

        private async Task<Request> GetExistingRequest(Guid requestId)
        {
            var request = await _repository.FindAsync(requestId);

            if (request == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, SharedResponses.NotFound);
            }

            return request;
        }
    }
}
