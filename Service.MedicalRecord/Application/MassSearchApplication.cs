using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Dtos.MassSearch;
using Service.MedicalRecord.Repository.IRepository;
using System.Collections.Generic;
using System.Threading.Tasks;
using Service.MedicalRecord.Mapper;
using System;
using Service.MedicalRecord.Dictionary;
using Service.MedicalRecord.Domain.Request;
using ClosedXML.Report;
using System.Linq;
using MoreLinq;
using Shared.Extensions;
using Service.MedicalRecord.Client.IClient;
using Service.MedicalRecord.Domain.Catalogs;
using Service.MedicalRecord.Dtos.WorkList;
using Shared.Dictionary;
using Shared.Error;
using System.Net;
using Service.MedicalRecord.Dtos.General;

namespace Service.MedicalRecord.Application
{
    public class MassSearchApplication : IMassSearchApplication
    {
        private readonly IMassSearchRepository _repository;
        private readonly IWorkListRepository _workListRepository;
        private readonly ICatalogClient _catalogClient;
        private readonly IRepository<Branch> _branchRepository;
        private readonly IPdfClient _pdfClient;

        public MassSearchApplication(IMassSearchRepository repository, IPdfClient pdfClient, IWorkListRepository workListRepository, ICatalogClient catalogClient, IRepository<Branch> branchRepository)
        {
            _repository = repository;
            _pdfClient = pdfClient;
            _catalogClient = catalogClient;
            _workListRepository = workListRepository;
            _branchRepository = branchRepository;
        }

        public async Task<(byte[] file, string fileName)> ExportList(GeneralFilterDto search)
        {
            var studies = await GetAllCaptureResults(search);

            foreach (var request in studies)
            {
                if (studies.Count > 0)
                {
                    request.Estudios.Insert(0, new RequestsStudiesInfoDto { 
                        Estudio = "Clave", 
                        MedioSolicitado = "Medio Solicitado", 
                        FechaEntrega = "Fecha de Entrega", 
                        Estatus = "Estatus", 
                        Registro = "Registro" 
                    });
                }
            }

            var path = Assets.EnvioResultados;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Captura de resultados (Clínicos)");
            template.AddVariable("FechaInicio", search.Fecha.First().ToString("dd/MM/yyyy"));
            template.AddVariable("FechaFinal", search.Fecha.Last().ToString("dd/MM/yyyy"));
            template.AddVariable("Expedientes", studies);

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

            return (template.ToByteArray(), $"Busqueda y envío de captura de resultados.xlsx");
        }

        public async Task<byte[]> DownloadResultsPdf(GeneralFilterDto filter)
        {
            var studies = await GetByFilter(filter);
            var results = studies.Results.SelectMany(x => x.Parameters).ToList();
            var requestResults = studies.Results.Select(x => x.Id).ToList();

            if (filter.Area[0] == 0 && filter.SucursalId == null)
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.MissingFilters("El parámetro área y sucursales"));
            }
            else if (filter.Area[0] == 0 && filter.SucursalId.Count == 0)
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.MissingFilters("El parámetro área y sucursales"));
            }

            var requests = await _workListRepository.GetMassiveWorkList(filter.Area[0], filter.SucursalId, filter.Fecha);

            var studiesIds = requests.SelectMany(x => x.Estudios).Select(x => x.EstudioId).ToList();
            var studyParams = await _catalogClient.GetStudies(studiesIds);

            var data = requests.ToWorkListDto();
            var branches = await _branchRepository.Get(x => filter.SucursalId.Contains(x.Id));

            data.Solicitudes = data.Solicitudes.Where(x => requestResults.Contains(x.Id)).ToList();
            data.HojaTrabajo = filter.NombreArea;
            data.Sucursal = string.Join(", ", branches.Select(x => x.Nombre));
            data.Fechas = new List<string> { filter.Fecha.First().ToString("dd/MM/yyyy"), filter.Fecha.Last().ToString("dd/MM/yyyy") };
            data.MostrarResultado = true;

            foreach (var request in data.Solicitudes)
            {
                foreach (var study in request.Estudios)
                {
                    var st = studyParams.FirstOrDefault(x => x.Id == study.EstudioId);
                    if (st != null)
                    {
                        var parameters = st.Parametros;
                        foreach (var param in parameters.Where(x => x.TipoValor != "9"))
                        {
                            param.SolicitudEstudioId = study.SolicitudEstudioId;
                            param.EstudioId = study.EstudioId;
                            var result = results.Find(x => x.SolicitudEstudioId == study.SolicitudEstudioId && x.Id.ToString() == param.Id);

                            study.ResultadoListasTrabajo.Add(new WorkListStudyResultsDto
                            {
                                Clave = result.Clave,
                                Resultado = result.Valor,
                                Unidades = result.Unidades,

                            });
                        }
                    }
                }
            }

            var file = await _pdfClient.GenerateWorkList(data);


            return file;
        }

        public async Task<List<RequestsInfoDto>> GetAllCaptureResults(GeneralFilterDto search)
        {

            var requests = await _repository.GetAllCaptureResults(search);

            return requests.ToDeliverResultInfoDto();
        }

        public async Task<MassSearchInfoDto> GetByFilter(GeneralFilterDto filter)
        {
            var request = await _repository.GetByFilter(filter);

            return request.ToMassSearchInfoDto(filter);
        }
    }
}
