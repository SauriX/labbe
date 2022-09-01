using ClosedXML.Excel;
using ClosedXML.Report;
using Service.MedicalRecord.Dictionary;
using Service.MedicalRecord.Dtos.TrackingOrder;
using Service.MedicalRecord.Mapper;
using Service.MedicalRecord.Repository.IRepository;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Application.IApplication
{
    public class TrackingOrderApplication : ITrackingOrderApplication
    {
        private readonly ITrackingOrderRepository _repository; 

        public TrackingOrderApplication(ITrackingOrderRepository repository)
        {
            _repository = repository;
        }
        public Task<TrackingOrderDto> Create(TrackingOrderFormDto order)
        {
            //var newOrder =
                throw new NotImplementedException();
        }


        public async Task<IEnumerable<EstudiosListDto>> FindEstudios(List<int> estudios)
        {
            var estudiosEncontrados = await _repository.FindEstudios(estudios);
            return estudiosEncontrados.ToStudiesRequestRouteDto(); //agregar un to para el mapper

        }

        public async Task<TrackingOrderDto> GetById(Guid Id)
        {
                var order = await _repository.GetById(Id);
                return order.ToTrackingOrderFormDto();
        }

        public Task<TrackingOrderDto> GetTrackingOrder(TrackingOrderFormDto order)
        {
            throw new NotImplementedException();
        }

        public Task<TrackingOrderDto> Update(TrackingOrderFormDto order)
        {
            throw new System.NotImplementedException();
        }
        public Task<(byte[] file, string fileName)> ExportList(string search)
        {
            throw new NotImplementedException();
            //try
            //{

            //    //var orden = await GetAll(search);

            //    var path = Assets.OrdenSeguimientoList;

            //    var template = new XLTemplate(path);

            //    template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            //    template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            //    template.AddVariable("Titulo", "Equipos");
            //    template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            //    template.AddVariable("Orden", search);

            //    template.Generate();

            //    var range = template.Workbook.Worksheet("Equipos").Range("Sucursales");
            //    var table = template.Workbook.Worksheet("Equipos").Range("$A$3:" + range.RangeAddress.LastAddress).CreateTable();
            //    table.Theme = XLTableTheme.TableStyleMedium2;

            //    template.Format();

            //    return (template.ToByteArray(), "Creación de Orden de Seguimiento.xlsx");
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

    }
}
