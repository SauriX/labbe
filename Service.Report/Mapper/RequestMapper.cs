﻿using Service.Report.Domain.Request;
using Service.Report.Dtos.Request;
using Service.Report.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Report.Mapper
{
    public static class RequestMapper
    {
        public static IEnumerable<RequestFiltroDto> ToRequestListDto(this List<Request> model)
        {
            if (model == null) return null;

            var results = from c in model
                          group c by new { c.Id, c.Sucursal, c.ExpedienteNombre} into grupo
                          select new RequestFiltroDto
                          {
                              Id = grupo.Key.Id,
                              Visitas = grupo.Count(),
                              Nombre = grupo.Key.Sucursal,
                              Clave = grupo.Key.ExpedienteNombre,
                          };
            return results;
        }


        public static RequestFiltroDto ToRequestFormDto(this Request model)
        {
            if (model == null) return null;

            return new RequestFiltroDto
            {
               Id = model.Id,
               Clave = model.Expediente.Expediente,
               Nombre = model.Sucursal,

            };
        }

        public static List<RequestFiltroDto> ToRequestRecordsListDto(this List<Report.Domain.Request.Request> model)
        {
            if (model == null) return null;

            return model.Select(x => new RequestFiltroDto
            {
                Id = x.Id,
                Nombre = x.Sucursal,
                Clave = x?.Expediente?.Expediente,
            }).ToList();
        }
    }
}
