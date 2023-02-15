﻿using ClosedXML.Report;
using MoreLinq;
using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Client.IClient;
using Service.MedicalRecord.Dictionary;
using Service.MedicalRecord.Dtos.Request;
using Service.MedicalRecord.Dtos.RportStudy;
using Service.MedicalRecord.Mapper;
using Service.MedicalRecord.Repository.IRepository;
using Shared.Error;
using Shared.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Application
{
    public class ReportStudyApplication: IReportStudyApplication
    {

        private readonly IRequestRepository _repository;
        private readonly ICatalogClient _catalogClient;
        private readonly IPdfClient _pdfClient;


        private const byte URGENCIA_CARGO = 3;

        public ReportStudyApplication(
            IRequestRepository repository,
            ICatalogClient catalogClient,
            IPdfClient pdfClient)
        {
    
            _repository = repository;
            _catalogClient = catalogClient;
            _pdfClient = pdfClient;

        }

        public async Task<List<ReportRequestListDto>> GetByFilter(RequestFilterDto filter)
        {
            var request = await _repository.GetByFilterEntrega(filter);

            if (request == null)
            {
                throw new CustomException(HttpStatusCode.NotFound);
            }
                var test = request.toRequestList().ToList();
            return request.toRequestList().ToList();
        }


        public async Task<byte[]> ExportRequest(RequestFilterDto filter)
        {
            var request = await GetByFilter(filter);
            if (request == null)
            {
                throw new CustomException(HttpStatusCode.NotFound);
            }



            return await _pdfClient.RequestDayForm(request);
        }


        public async Task<(byte[] file, string fileName)> ExportList(RequestFilterDto search)
        {
            var studies = await GetByFilter(search);
        
            foreach (var request in studies)
            {
                if (studies.Count > 0)
                {
                    request.Estudios.Insert(0, new ReportStudyListDto { Clave = "Clave", Nombre = "Nombre Estudio",Regitro="Fecha de Registro" ,Fecha= "Fecha de entrega",  Estatus = "Estatus" });
                }
            }
            studies = studies.OrderBy(x=>x.Sucursal).ToList();
            List<ReportList> list = new List<ReportList>();
            var pivote = studies.First().Sucursal.ToString().Trim();
            var testeadi = new List<ReportRequestListDto>();
            testeadi.Add(new ReportRequestListDto
            {
                Solicitud = "Solicitud",
                Paciente = "Nombre",
                Edad = "Edad",
                Sexo = "Sexo",
                Sucursal = "Sucursal",
                Compañia = "Compañia",
                Entrega = "Entrega",
                Estudios = new List<ReportStudyListDto>()


            });
          

          
            foreach (var study in studies) {
                if (!list.Any(x => x.SucursalRaw == pivote))
                {
                    var sucursali = Regex.Replace(study.Sucursal, @"[^\w]", "");

                    if (studies.FindAll(x => x.isPatologia).Count() > 0)
                    {
                        var item = new ReportList
                        {
                            Sucursal = sucursali,
                            Tipo = "Patologia",
                            Requests = studies.FindAll(y => y.isPatologia && y.Sucursal == pivote),
                            SucursalRaw = pivote

                        };
                        list.Add(item);
                    }

                    if (studies.FindAll(x => !x.isPatologia).Count() > 0)
                    {
                        var Lab = new ReportList
                        {
                            Sucursal = sucursali,
                            Tipo = "Laboratorio",
                            Requests = studies.FindAll(y => !y.isPatologia  && y.Sucursal == pivote),
                            SucursalRaw = pivote

                        };
                        list.Add(Lab);
                    }

                    


                }
                pivote = study.Sucursal;


            }
              foreach (var request in list)
              {
                  if (request.Requests.Count > 0)
                  {
                      request.Requests.Insert(0, new ReportRequestListDto {
                          Solicitud = "Solicitud",
                          Paciente = "Nombre",
                          Edad = "Edad",
                          Sexo= "Sexo",
                          Sucursal="Sucursal",
                          Compañia = "Compañia",
                          Entrega = "Entrega",
                          Estudios = new List<ReportStudyListDto>()

                      });
                  }
              }
            
            var path = Assets.InformeDia;

            var template = new XLTemplate(path);
            var sucursalRaw = list.First().SucursalRaw;
            var sucursal = list.First().Sucursal;
            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Toma de Muestra de Estudio");
            template.AddVariable("FechaInicio", search.FechaInicial?.ToString("dd/MM/yyyy"));
            template.AddVariable("FechaFinal", search.FechaFinal?.ToString("dd/MM/yyyy"));

            

            var test = list.GroupBy(x => x.Sucursal);

            foreach (var item in test) {
           

                    template.Workbook.Worksheet("Solicitudes").CopyTo(item.Key);
                    template.Workbook.Worksheet(item.Key).Range("$A$2:$G$7").AddToNamed($"Solicitudes{item.Key}");
                    template.Workbook.Worksheet(item.Key).Range("$A$3:$G$6").AddToNamed($"Solicitudes{item.Key}_Requests");
                    template.Workbook.Worksheet(item.Key).Range("$A$4:$G$5").AddToNamed($"Solicitudes{item.Key}_Requests_Estudios");
                    template.AddVariable($"Solicitudes{item.Key}", item.ToList());


            }

            template.Workbook.Worksheet("Solicitudes").Delete();
            template.Generate();


          /* template.Workbook.Worksheets.ToList().ForEach(x =>
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
                
            });*/
        
            template.Format();

            return (template.ToByteArray(), $"Informe Del Día.xlsx");
        }
    }
}
