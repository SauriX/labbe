﻿using ClosedXML.Excel;
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
using Service.MedicalRecord.Domain;
using Service.MedicalRecord.Domain.Request;
using Service.MedicalRecord.PdfModels;
using Service.MedicalRecord.Dtos.Route;

namespace Service.MedicalRecord.Application
{
    public class RouteTrackingApplication : IRouteTrackingApplication
    {
        public readonly IRouteTrackingRepository _repository;
        private readonly ICatalogClient _catalogClient;
        private readonly IPdfClient _pdfClient;
        private readonly IIdentityClient _identityClient;
        public object SharedResponses { get; private set; }

        public RouteTrackingApplication(IRouteTrackingRepository repository, ICatalogClient catalog, IPdfClient pdfClient, IIdentityClient identityClient)
        {
            _repository = repository;
            _catalogClient = catalog;
            _pdfClient = pdfClient;
            _identityClient = identityClient;
        }

        public async Task<List<RouteTrackingListDto>> GetAll(RouteTrackingSearchDto search)
        {
            var routeTrackingList = await _repository.GetAll(search);
            var studyTags = await _repository.GetTagsByOrigin();

            List<RouteFormDto> tagRoutes = new();

            var tagDestination = studyTags.Where(x => x.DestinoId != null).Select(y => y.DestinoId).ToList();
            tagRoutes = await _catalogClient.GetRutas(tagDestination);

            var trackingTags = routeTrackingList.ToRouteTrackingDto(studyTags, tagRoutes);

            return trackingTags;
        }

        public async Task<RouteTrackingFormDto> GetById(Guid id)
        {
            var routeTracking = await _repository.GetById(id);

            return routeTracking.ToRouteTrackingDto();
        }

        public async Task<List<TagTrackingOrderDto>> GetAllTags(string search)
        {
            var tags = await _repository.GetAllTags(search);

            return tags.ToTagTrackingOrderDto();
        }

        public async Task<List<TagTrackingOrderDto>> FindTags(string routeId)
        {
            var trackingTags = await _repository.FindTags(routeId);

            return trackingTags.ToTagTrackingOrderDto();
        }

        public async Task<RouteTrackingDto> CreateTrackingOrder(RouteTrackingFormDto order)
        {

        }

        public async Task<int> UpdateStatus(List<RequestedStudyUpdateDto> requestDto)
        {
            try
            {

                foreach (var item in requestDto)
                {
                    var ruteOrder = await _repository.GetById(item.RuteOrder);
                    var list = ruteOrder.ToRouteTrackingDtoList();
                    List<string> IdRoutes = new();
                    IdRoutes.Add(list.rutaId.ToString());
                    var routes = await _catalogClient.GetRutas(IdRoutes);
                    var route = routes.FirstOrDefault(x => Guid.Parse(x.Id) == list.rutaId);
                    DateTime oDate = Convert.ToDateTime(list.Fecha);
                    list.Fecha = oDate.AddDays(route.TiempoDeEntrega).ToString();
                    var routeT = new RouteTracking
                    {
                        Id = Guid.NewGuid(),
                        SegumientoId = Guid.Parse(ruteOrder.Estudios.FirstOrDefault().SeguimientoId.ToString()),
                        //RutaId = Guid.Parse(ruteOrder.RutaId),
                        SucursalId = Guid.Parse(ruteOrder.DestinoId),
                        FechaDeEntregaEstimada = DateTime.Parse(list.Fecha),
                        SolicitudId = ruteOrder.Estudios.FirstOrDefault().SolicitudId,
                        HoraDeRecoleccion = ruteOrder.FechaCreo,
                        UsuarioCreoId = ruteOrder.UsuarioCreoId,
                        FechaCreo = DateTime.Now,

                    };
                    await _repository.Create(routeT);
                }
                int studyCount = 0;
                foreach (var item in requestDto)
                {
                    var ruteOrder = await _repository.GetById(item.RuteOrder);
                    var solicitudId = ruteOrder.Estudios.FirstOrDefault().SolicitudId;

                    var request = await GetExistingRequest(solicitudId);

                    var studiesIds = item.EstudioId;
                    var studies = await _repository.GetStudyById(solicitudId, studiesIds);

                    studies = studies.Where(x => x.EstatusId == Status.RequestStudy.TomaDeMuestra || x.EstatusId == Status.RequestStudy.EnRuta).ToList();

                    if (studies == null || studies.Count == 0)
                    {
                        throw new CustomException(HttpStatusCode.BadRequest);
                    }

                    foreach (var study in studies)
                    {
                        if (study.EstatusId == Status.RequestStudy.TomaDeMuestra)
                        {
                            study.EstatusId = Status.RequestStudy.EnRuta;
                        }
                        else
                        {
                            study.EstatusId = Status.RequestStudy.TomaDeMuestra;
                        }
                    }
                    studyCount += studies.Count;
                    await _repository.BulkUpdateStudies(solicitudId, studies);

                }
                return studyCount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private async Task<Request> GetExistingRequest(Guid requestId)
        {
            var request = await _repository.FindAsync(requestId);
            if (request == null)
            {
                throw new CustomException(HttpStatusCode.NotFound);
            }
            return request;
        }
        public async Task<(byte[] file, string fileName)> ExportForm(Guid id)
        {

            try
            {
                var order = await GetById(id);
                var path = Assets.TrackingForm;
                var template = new XLTemplate(path);
                template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
                template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
                template.AddVariable("Titulo", "Orden de Seguimiento");
                template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
                template.AddVariable("Orden", order);
                template.AddVariable("Estudios", order);
                template.Generate();
                template.Format();
                return (template.ToByteArray(), "Creación de Orden de Seguimiento.xlsx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<PendingReciveDto>> GetAllRecive(PendingSearchDto search)
        {
            List<PendingReciveDto> revefinal = new List<PendingReciveDto>();
            var tracking = await _repository.GetAllRecive(search);
            var recive = tracking.ToPendingReciveDto();

            foreach (var item in recive)
            {
                var register = item;
                var routeTra = await _repository.GetTracking(Guid.Parse(item.Id));

                var route = await _catalogClient.GetRuta(Guid.Parse(item.Claveroute));
                var sucursal = await _catalogClient.GetBranch(Guid.Parse(item.Sucursal));
                if (routeTra != null)
                {
                    register.Status.Route = true;
                    register.Fechaen = routeTra.FechaDeEntregaEstimada;
                }
                if (item.Fechareal != DateTime.MinValue)
                {
                    register.Status.Entregado = true;
                }
                register.Claveroute = route.Clave;
                register.Sucursal = sucursal.nombre;
                revefinal.Add(register);

            }
            if (search.Fecha != null)
            {
                revefinal = revefinal.AsQueryable().
                    Where(x => x.Fechaen.Date == search.Fecha.Value.Date).ToList();
            }
            return revefinal;
        }
        public async Task<byte[]> Print(PendingSearchDto search)
        {
            var request = await GetAllRecive(search);

            if (request == null)
            {
                throw new CustomException(HttpStatusCode.NotFound);
            }
            return await _pdfClient.PendigForm(request);
        }
        public async Task<byte[]> ExportDeliver(Guid id)
        {
            var trakingorder = await _repository.GetById(id);
            if (trakingorder == null)
            {
                throw new CustomException(HttpStatusCode.NotFound);
            }


            var order = trakingorder.ToRouteTrackingDtoList();

            List<RouteTrackingListDto> routefinal = new List<RouteTrackingListDto>();
            List<string> IdRoutes = new();

            IdRoutes.Add(order.rutaId.ToString());

            var routes = await _catalogClient.GetRutas(IdRoutes);

            var route = routes.FirstOrDefault(x => Guid.Parse(x.Id) == order.rutaId);
            DateTime oDate = Convert.ToDateTime(order.Fecha);
            order.Fecha = oDate.AddDays(route.TiempoDeEntrega).ToString();


            var user = await _identityClient.GetByid(trakingorder.Estudios.FirstOrDefault().Solicitud.UsuarioModificoId.ToString());

            var orderForm = order.toDeliverOrder($"{user.Nombre} {user.PrimerApellido} {user.SegundoApellido}");
            List<Col> columns = new()
            {
                new Col("Clave de estudio", ParagraphAlignment.Left),
                new Col("Estudio", ParagraphAlignment.Left),
                new Col("Temperatura", ParagraphAlignment.Left),
                new Col("Solicitud", ParagraphAlignment.Left),
                new Col("Paciente", ParagraphAlignment.Left),
                new Col("Confirmación muestra origen", ParagraphAlignment.Left),
                new Col("Confirmación muestra destino", ParagraphAlignment.Left),
            };
            var data = order.Estudios.Select(x => new Dictionary<string, object>
            {
                { "Clave de estudio", x.Clave },
                { "Estudio", x.Nombre },
                { "Temperatura", Convert.ToDecimal(trakingorder.Temperatura) },
                { "Solicitud", trakingorder.Estudios.FirstOrDefault(y=>y.SolicitudId == Guid.Parse(x.Solicitudid) && y.EtiquetaId == x.Id).Solicitud.Clave },
                { "Paciente", trakingorder.Estudios.FirstOrDefault(y=>y.SolicitudId == Guid.Parse(x.Solicitudid) && y.EtiquetaId == x.Id).Solicitud.Expediente.NombreCompleto},
                { "Confirmación muestra origen",  trakingorder.Estudios.FirstOrDefault(y => y.SolicitudId == Guid.Parse(x.Solicitudid) && y.EtiquetaId == x.Id).Solicitud.Estudios.FirstOrDefault(w=>w.EstudioId==x.Id).EstatusId== Status.RequestStudy.TomaDeMuestra?"si":"no"},
                { "Confirmación muestra destino", ""}
            }).ToList();
            orderForm.Columnas = columns;
            orderForm.Datos = data;
            return await _pdfClient.DeliverForm(orderForm);
        }
    }
}
