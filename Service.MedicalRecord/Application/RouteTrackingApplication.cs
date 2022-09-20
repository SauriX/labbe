using ClosedXML.Excel;
using ClosedXML.Report;
using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Dictionary;
using Service.MedicalRecord.Domain.RouteTracking;
using Service.MedicalRecord.Dtos.RequestedStudy;
using Service.MedicalRecord.Dtos.RouteTracking;
using Service.MedicalRecord.Mapper;
using Service.MedicalRecord.Repository.IRepository;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Application
{
    public class RouteTrackingApplication: IRouteTrackingApplication
    {
        public readonly IRouteTrackingRepository _repository;
        public RouteTrackingApplication(IRouteTrackingRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<RouteTrackingListDto>> GetAll(RouteTrackingSearchDto search)
        {
            var routeTrackingList = await _repository.GetAll( search);

            return  routeTrackingList.ToList().ToRouteTrackingDto();
        }
        public async Task <RouteTrackingListDto> GetByid(Guid id)
        {
            var routeTrackingList = await _repository.getById(id);

            return routeTrackingList.ToRouteTrackingDto();
        }
        public async Task<int> UpdateStatus(List<RequestedStudyUpdateDto> requestDto)
        {
            int studyCount = 0;
            foreach (var item in requestDto)
            {
                var ruteOrder =await GetByid(item.SolicitudId);
                var route = new RouteTracking
                {
                         Id = Guid.NewGuid(),
                        SegumientoId = Guid.Parse(ruteOrder.Seguimiento),
                        RutaId = Guid.Parse(ruteOrder.Seguimiento),
                        SucursalId = Guid.Parse(ruteOrder.Seguimiento),
                        FechaDeEntregaEstimada = ruteOrder.Fecha,
                        SolicitudId = ruteOrder.Solicitud,
                        HoraDeRecoleccion = ruteOrder.Fecha,
                        UsuarioCreoId= Guid.NewGuid(),
                        FechaCreo = DateTime.Now,

                };

                await _repository.Create(route);

            }

            return studyCount;
        }
        public async Task<(byte[] file, string fileName)> ExportForm(Guid id)
        {

            try
            {

                var order = await GetByid(id);
                //var newOrder = order.ToModel();

                var path = Assets.TrackingForm;

                var template = new XLTemplate(path);

                template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
                template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
                template.AddVariable("Titulo", "Orden de Seguimiento");
                template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
                template.AddVariable("Orden", order);
                template.AddVariable("Estudios", order.Estudios);

                template.Generate();

                //var range = template.Workbook.Worksheet("Orden").Range("Estudios");
                //var table = template.Workbook.Worksheet("Orden").Range("$A$10:" + range.RangeAddress.LastAddress).CreateTable();
                //table.Theme = XLTableTheme.TableStyleMedium2;

                template.Format();

                return (template.ToByteArray(), "Creación de Orden de Seguimiento.xlsx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
