using ClosedXML.Excel;
using ClosedXML.Report;
using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Client.IClient;
using Service.MedicalRecord.Dictionary;
using Service.MedicalRecord.Domain.RouteTracking;
using Service.MedicalRecord.Domain.TrackingOrder;
using Service.MedicalRecord.Dtos.PendingRecive;
using Service.MedicalRecord.Dtos.RequestedStudy;
using Service.MedicalRecord.Dtos.RouteTracking;
using Service.MedicalRecord.Mapper;
using Service.MedicalRecord.Repository.IRepository;
using Shared.Error;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Application
{
    public class RouteTrackingApplication: IRouteTrackingApplication
    {
        public readonly IRouteTrackingRepository _repository;
        private readonly ICatalogClient _catalogClient;
        private readonly IPdfClient _pdfClient;
        public RouteTrackingApplication(IRouteTrackingRepository repository,ICatalogClient catalog, IPdfClient pdfClient)
        {
            _repository = repository;
            _catalogClient = catalog;
            _pdfClient = pdfClient;
        }

        public async Task<List<RouteTrackingListDto>> GetAll(RouteTrackingSearchDto search)
        {
            var routeTrackingList = await _repository.GetAll( search);

                return  routeTrackingList.ToList().ToRouteTrackingDto();
        }
        public async Task <RouteTrackingFormDto> GetByid(Guid id)
        {
            var routeTrackingList = await _repository.getById(id);

            return routeTrackingList.ToRouteTrackingDto();
        }

        public async Task<int> UpdateStatus(List<RequestedStudyUpdateDto> requestDto)
        {
            int studyCount = 0;
            foreach (var item in requestDto)
            {
                var ruteOrder = await _repository.getById(item.SolicitudId);
                var route = new RouteTracking
                {
                         Id = Guid.NewGuid(),
                        SegumientoId = Guid.Parse(ruteOrder.Estudios.FirstOrDefault().SeguimientoId.ToString()),
                        RutaId = Guid.Parse(ruteOrder.RutaId),
                        SucursalId = Guid.Parse(ruteOrder.SucursalDestinoId),
                        FechaDeEntregaEstimada = ruteOrder.FechaCreo,
                        SolicitudId = ruteOrder.Estudios.FirstOrDefault().SolicitudId,
                        HoraDeRecoleccion = ruteOrder.FechaCreo,
                        UsuarioCreoId= ruteOrder.UsuarioCreoId,
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
                template.AddVariable("Estudios", order);

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

        public async Task<List<PendingReciveDto>> GetAllRecive(PendingSearchDto search) {
            List<PendingReciveDto> revefinal = new List<PendingReciveDto>();
            var tracking = await _repository.GetAllRecive(search);
            var recive = tracking.ToPendingReciveDto();

            foreach (var item in recive) {
                var register = item;
                List<ReciveStudyDto> estudios = new List<ReciveStudyDto>();
               var routeTra = await _repository.GetTracking(Guid.Parse(item.Id));
                var route = await _catalogClient.GetRuta(Guid.Parse(item.Claveroute));
                var sucursal = await _catalogClient.GetBranch(Guid.Parse(item.Sucursal));
                if (routeTra != null) {
                    register.Status.Route = true;
                    foreach (var study in item.Study)
                    {
                        study.Horarecoleccion = routeTra.HoraDeRecoleccion;
                        estudios.Add(study);
                    }
                    register.Horaen = routeTra.FechaDeEntregaEstimada;
                    register.Fechaen = routeTra.FechaDeEntregaEstimada;

                }
                if (item.Fechareal != DateTime.MinValue) {
                    register.Status.Entregado = true;
                }
                register.Claveroute = route.Clave;
                register.Sucursal = sucursal.nombre;
                revefinal.Add(register);

            }
            return revefinal;
        }

        public async Task<byte[]> Print(PendingSearchDto search)
        {
            var request = await GetAllRecive(search);

            if (request == null )
            {
                throw new CustomException(HttpStatusCode.NotFound);
            }



            return await _pdfClient.PendigForm(request);
        }
    }
}
