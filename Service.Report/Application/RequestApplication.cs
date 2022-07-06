using ClosedXML.Excel;
using ClosedXML.Report;
using Service.Report.Application.IApplication;
using Service.Report.Dictionary;
using Service.Report.Domain.Request;
using Service.Report.Dtos.Request;
using Service.Report.Mapper;
using Service.Report.Repository.IRepository;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Application
{
    public class RequestApplication : IRequestApplication
    {
        public readonly IRequestRepository _repository;
        public RequestApplication(IRequestRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<RequestFiltroDto>> GetBranchByCount()
        {
            var req = await _repository.GetRequestByCount();
            var results = from c in req
                          group c by new { c.Id, c.Nombre, c.Clave } into grupo
                          select new RequestFiltroDto
                          {
                              Id = grupo.Key.Id,
                              Visitas = grupo.Count(),
                              Nombre = grupo.Key.Nombre,
                              Clave = grupo.Key.Clave

                          };


            return results;
        }
        public async Task<List<RequestFiltroDto>> GetFilter(RequestSearchDto search)
        {
            var doctors = await _repository.GetFilter(search);
            return doctors.ToRequestRecordsListDto();
        }

        public async Task<(byte[] file, string fileName)> ExportTableBranch(string search = null)
        {
            var indication = await GetBranchByCount();

            var path = Assets.ReportTable;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Sucursales");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("Expediente", indication);

            template.Generate();

            var range = template.Workbook.Worksheet("Sucursales").Range("Sucursales");
            var table = template.Workbook.Worksheet("Sucursales").Range("$A$3:" + range.RangeAddress.LastAddress).CreateTable();
            table.Theme = XLTableTheme.TableStyleMedium2;

            template.Format();

            return (template.ToByteArray(), "Estadística de expedientes.xlsx");
        }

        public async Task<(byte[] file, string fileName)> ExportGraphicBranch(string search = null)
        {
            var indication = await GetBranchByCount();

            var path = Assets.ReportGraphic;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Sucursales");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("Gráfica", indication);

            template.Generate();

            var range = template.Workbook.Worksheet("Sucursales").Range("Sucursales");
            var table = template.Workbook.Worksheet("Sucursales").Range("$A$3:" + range.RangeAddress.LastAddress).CreateTable();
            table.Theme = XLTableTheme.TableStyleMedium2;

            template.Format();

            return (template.ToByteArray(), "Estadística de Expedientes.xlsx");
        }
    }
}
