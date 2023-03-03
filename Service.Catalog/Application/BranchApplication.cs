using ClosedXML.Excel;
using ClosedXML.Report;
using EventBus.Messages.Catalog;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Service.Catalog.Application.IApplication;
using Service.Catalog.Dictionary;
using Service.Catalog.Domain.Branch;
using Service.Catalog.Domain.Constant;
using Service.Catalog.Domain.Series;
using Service.Catalog.Dtos.Branch;
using Service.Catalog.Dtos.Series;
using Service.Catalog.Mapper;
using Service.Catalog.Repository.IRepository;
using Shared.Dictionary;
using Shared.Error;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Service.Catalog.Application
{
    public class BranchApplication : IBranchApplication
    {
        private readonly string key;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IBranchRepository _repository;
        private readonly ILocationRepository _locationRepository;
        private readonly ISeriesRepository _seriesRepository;

        public BranchApplication(IPublishEndpoint publishEndpoint, IBranchRepository repository, ILocationRepository locationRepository, ISeriesRepository seriesRepository, IConfiguration configuration)
        {
            key = configuration.GetValue<string>("PasswordKey");
            _publishEndpoint = publishEndpoint;
            _repository = repository;
            _locationRepository = locationRepository;
            _seriesRepository = seriesRepository;
        }

        public async Task<bool> Create(BranchFormDto branch)
        {
            if (!string.IsNullOrEmpty(branch.IdSucursal))
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.NotPossible);
            }

            var seriesIds = branch.Series.Select(x => x.Id).ToList();

            var series = await _seriesRepository.GetByIds(seriesIds);

            var newBranch = branch.ToModel(key, series);

            var (isDuplicate, code) = await _repository.IsDuplicate(newBranch);

            if (isDuplicate)
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.Duplicated(Responses.Duplicated($"El {code}")));
            }

            var location = await _locationRepository.GetColoniesByZipCode(newBranch.Codigopostal);

            if (location == null || location.Count == 0)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "Código postal no válido");
            }

            var city = await _locationRepository.GetCityByName(newBranch.Ciudad);

            if (city == null)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "Ciudad no válida");
            }

            var hasMatriz = await _repository.HasMatriz(newBranch);

            if (!hasMatriz && !newBranch.Matriz)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "Configure la matriz para la ciudad");
            }

            if (hasMatriz && newBranch.Matriz)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "Ya existe una matriz para la ciudad");
            }

            var duplicateSeries = await CheckSerieDuplicate(newBranch);

            if(duplicateSeries.Count() > 0)
            {
                var seriesName = string.Join(", ", duplicateSeries.Select(x => x.Clave));

                throw new CustomException(HttpStatusCode.BadRequest, $"La(s) serie(s) {seriesName} ya pertenecen a una sucursal");
            }

            string finalCode = await GetBranchFolio(newBranch, city);

            string lastConsecutive = await _repository.GetLastConsecutive();
            string nextConsecutive = (Convert.ToInt32(lastConsecutive) + 1).ToString("D2");


            newBranch.Clinicos = finalCode;
            newBranch.Codigo = nextConsecutive;
            await _repository.Create(newBranch);

            var contract = new BranchContract(newBranch.Id, newBranch.Codigo, newBranch.Clave, newBranch.Nombre, newBranch.Clinicos, newBranch.Codigopostal, location.First().CiudadId, newBranch.Ciudad);

            await _publishEndpoint.Publish(contract);

            return true;
        }

        private async Task<IEnumerable<SeriesListDto>> CheckSerieDuplicate(Branch branch)
        {
            var listSeries = branch.Series.Select(x => x.Id).ToList();

            var duplicateSeries = await _seriesRepository.IsSerieDuplicate(branch.Id, listSeries);

            return duplicateSeries.ToSeriesListDto();
        }

        public async Task<BranchFormDto> GetById(string Id)
        {
            var branch = await _repository.GetById(Id);

            if (branch == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            var branchForm = branch.ToBranchFormDto(key);

            return branchForm;
        }

        public async Task<BranchFormDto> GetByName(string name)
        {
            var branch = await _repository.GetByName(name);

            if (branch == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            return branch.ToBranchFormDto(key);
        }

        public async Task<string> GetCodeRange(Guid id)
        {
            var codeRange = await _repository.GetCodeRange(id);

            return codeRange;
        }

        public async Task<bool> Update(BranchFormDto branch)
        {
            var existing = await _repository.GetById(branch.IdSucursal);

            if (existing == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            var series = await _seriesRepository.GetByBranch(Guid.Parse(branch.IdSucursal));

            var updatedBranch = branch.ToModel(existing, key, series);

            var (isDuplicate, code) = await _repository.IsDuplicate(updatedBranch);

            if (isDuplicate)
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.Duplicated($"El {code}"));
            }

            var location = await _locationRepository.GetColoniesByZipCode(updatedBranch.Codigopostal);

            if (location == null || location.Count == 0)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "Código postal no válido");
            }

            var city = await _locationRepository.GetCityByName(updatedBranch.Ciudad);

            if (city == null)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "Ciudad no válida");
            }

            var isMAtrisActive = await _repository.HasMatriz(updatedBranch);

            if (isMAtrisActive && updatedBranch.Matriz)
            {
                throw new CustomException(HttpStatusCode.Conflict, "Ya exsite una matriz activa");
            }

            var duplicateSeries = await CheckSerieDuplicate(updatedBranch);

            if (duplicateSeries.Count() > 0)
            {
                var seriesName = string.Join(", ", duplicateSeries.Select(x => x.Clave));

                throw new CustomException(HttpStatusCode.BadRequest, $"La(s) serie(s) {seriesName} ya pertenecen a una sucursal");
            }

            if (existing.Matriz != updatedBranch.Matriz)
            {
                string finalCode = await GetBranchFolio(updatedBranch, city);
                updatedBranch.Clinicos = finalCode;
            }

            await _repository.Update(updatedBranch);

            var contract = new BranchContract(updatedBranch.Id, updatedBranch.Codigo, updatedBranch.Clave, updatedBranch.Nombre, updatedBranch.Clinicos, updatedBranch.Codigopostal, location.First().CiudadId, updatedBranch.Ciudad);

            await _publishEndpoint.Publish(contract);

            return true;
        }

        public async Task<IEnumerable<BranchInfoDto>> GetAll(string search = null)
        {
            var branch = await _repository.GetAll(search);
            return branch.ToBranchListDto();
        }

        public async Task<(byte[] file, string fileName)> ExportListBranch(string search = null)
        {
            var indication = await GetAll(search);

            var path = Assets.BranchList;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Sucursales");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("Sucursales", indication);

            template.Generate();

            var range = template.Workbook.Worksheet("Sucursales").Range("Sucursales");
            var table = template.Workbook.Worksheet("Sucursales").Range("$A$3:" + range.RangeAddress.LastAddress).CreateTable();
            table.Theme = XLTableTheme.TableStyleMedium2;

            template.Format();

            return (template.ToByteArray(), "Catálogo de Sucursales.xlsx");
        }

        public async Task<(byte[] file, string fileName)> ExportFormBranch(string id)
        {
            var indication = await GetById(id);

            var path = Assets.BranchtForm;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Sucursales");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("Sucursales", indication);

            template.Generate();

            template.Format();

            return (template.ToByteArray(), $"Catálogo de Sucursales ({indication.Clave}).xlsx");
        }

        public async Task<IEnumerable<BranchCityDto>> GetBranchByCity()
        {
            var branch = await _repository.GetBranchByCity();
            var results = from c in branch
                          group c by c.Ciudad into grupo
                          select new BranchCityDto
                          {
                              Ciudad = grupo.Key,
                              Sucursales = grupo.ToList().ToBranchListDto(),
                          };

            return results;
        }

        private async Task<string> GetBranchFolio(Branch branch, City city)
        {
            var config = await _repository.GetConfigByState(city.EstadoId);

            if (config == null || !config.Any())
            {
                var last = await _repository.GetLastConfig();

                var newConfig = new BranchFolioConfig(city.EstadoId, city.Id,
                    last == null ? (byte)1 : (byte)(last.ConsecutivoEstado + 1),
                    1);

                await _repository.CreateConfig(newConfig);

                config = await _repository.GetConfigByState(city.EstadoId);
            }

            var cityConfig = config.Where(x => x.CiudadId == city.Id).FirstOrDefault();

            if (cityConfig == null)
            {
                var last = config.OrderByDescending(x => x.ConsecutivoCiudad).FirstOrDefault();

                var newConfig = new BranchFolioConfig(city.EstadoId, city.Id, last.ConsecutivoEstado, (byte)(last.ConsecutivoCiudad + 1));

                await _repository.CreateConfig(newConfig);

                cityConfig = newConfig;
            }

            var lastFolio = await _repository.GetLastFolio(branch.Ciudad);
            lastFolio ??= "0-0";

            var currentLastOk = int.TryParse(lastFolio?.Split("-")[1], out int currentLast);
            currentLast = currentLast == 0 || branch.Matriz ? -1 : currentLast;

            if (!currentLastOk)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "Folio anterior no configurado correctamente");
            }

            var gap = branch.Matriz ? 1000 : 300;
            var consecutive = $"{cityConfig.ConsecutivoEstado}{cityConfig.ConsecutivoCiudad}";
            var start = $"{consecutive}{(currentLast + 1).ToString("D4")[^4..]}";
            var end = $"{consecutive}{(currentLast + gap).ToString("D4")[^4..]}";
            var finalCode = $"{start}-{end}";
            return finalCode;
        }
    }
}
