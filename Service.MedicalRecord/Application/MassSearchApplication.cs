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

namespace Service.MedicalRecord.Application
{
    public class MassSearchApplication : IMassSearchApplication
    {
        private readonly IMassSearchRepository _repository;

        public MassSearchApplication(IMassSearchRepository repository)
        {
            _repository = repository;
        }

        public async Task<(byte[] file, string fileName)> ExportList(DeliverResultsFilterDto search)
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
            template.AddVariable("FechaInicio", search.FechaInicial.ToString("dd/MM/yyyy"));
            template.AddVariable("FechaFinal", search.FechaFinal.ToString("dd/MM/yyyy"));
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

        public async Task<List<RequestsInfoDto>> GetAllCaptureResults(DeliverResultsFilterDto search)
        {

            var requests = await _repository.GetAllCaptureResults(search);

            return requests.ToDeliverResultInfoDto();
        }

        public async Task<MassSearchInfoDto> GetByFilter(MassSearchFilterDto filter)
        {
            var request = await _repository.GetByFilter(filter);

            return request.ToMassSearchInfoDto();
        }
    }
}
