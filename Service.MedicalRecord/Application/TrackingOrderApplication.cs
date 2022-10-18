﻿using ClosedXML.Excel;
using ClosedXML.Report;
using Service.MedicalRecord.Dictionary;
using Service.MedicalRecord.Domain.TrackingOrder;
using Service.MedicalRecord.Dtos.TrackingOrder;
using Service.MedicalRecord.Mapper;
using Service.MedicalRecord.Repository.IRepository;
using Shared.Error;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using SharedResponses = Shared.Dictionary.Responses;
using System.Linq;

namespace Service.MedicalRecord.Application.IApplication
{
    public class TrackingOrderApplication : ITrackingOrderApplication
    {
        private readonly ITrackingOrderRepository _repository;

        public TrackingOrderApplication(ITrackingOrderRepository repository)
        {
            _repository = repository;
        }
        public async Task<TrackingOrderDto> Create(TrackingOrderFormDto order)
        {
            var newOrder = order.ToModel();
            await _repository.Create(newOrder);
            return newOrder.ToTrackingOrderFormDto();
        }
        public async Task<TrackingOrder> GetExistingOrder(Guid orderId)
        {
            var order = await _repository.FindAsync(orderId);

            if (order == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, SharedResponses.NotFound);
            }

            return order;
        }
        public async Task<TrackingOrderCurrentDto> GetOrderById(Guid orderId)
        {
            var existingOrder = await GetExistingOrder(orderId);

            var estudiosByOrder = existingOrder.Estudios.ToList().Select(x => x.EstudioId).ToList();

            var findStudies = await FindEstudios(estudiosByOrder);

            return existingOrder.toCurrentOrderDto(findStudies);

        }
        
        public async Task Update(TrackingOrderFormDto order)
        {
            var existingOrder = await GetExistingOrder(order.Id);

            var updatedOrder = order.toUpdateModel(existingOrder);

            await _repository.Update(updatedOrder);
            
        }

        public async Task<IEnumerable<EstudiosListDto>> FindEstudios(List<int> estudios)
        {
            var estudiosEncontrados = await _repository.FindEstudios(estudios);

            return estudiosEncontrados.ToStudiesRequestRouteDto(); 
        }

        

        
        public async Task<(byte[] file, string fileName)> ExportForm(TrackingOrderFormDto order)
        {
            
            try
            {

                //var orden = await GetAll(search);
                //var newOrder = order.ToModel();

                var path = Assets.TrackingOrderForm;

                var template = new XLTemplate(path);

                template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
                template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
                template.AddVariable("Titulo", "Orden de Seguimiento");
                template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
                template.AddVariable("Orden", order);
                template.AddVariable("Estudios", order.Estudios);

                template.Generate();

                var range = template.Workbook.Worksheet("OrdenSeguimiento").Range("Estudios");
                var table = template.Workbook.Worksheet("OrdenSeguimiento").Range("$A$10:" + range.RangeAddress.LastAddress).CreateTable();
                table.Theme = XLTableTheme.TableStyleMedium2;

                template.Format();

                return (template.ToByteArray(), "Creación de Orden de Seguimiento.xlsx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> ConfirmarRecoleccion(Guid seguimientoId)
        {
            return await _repository.ConfirmarRecoleccion(seguimientoId);

        }

        public async Task<bool> CancelarRecoleccion(Guid seguimientoId)
        {
            return await _repository.CancelarRecoleccion(seguimientoId);

        }

        
    }
}
