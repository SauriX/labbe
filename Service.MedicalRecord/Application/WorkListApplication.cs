using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Client.IClient;
using Service.MedicalRecord.Domain.Catalogs;
using Service.MedicalRecord.Dtos.WorkList;
using Service.MedicalRecord.Mapper;
using Service.MedicalRecord.Repository.IRepository;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Application
{
    public class WorkListApplication : IWorkListApplication
    {
        private readonly IWorkListRepository _repository;
        private readonly ICatalogClient _catalogClient;
        private readonly IPdfClient _pdfClient;
        private readonly IRepository<Branch> _branchRepository;

        public WorkListApplication(IWorkListRepository repository, ICatalogClient catalogClient, IPdfClient pdfClient, IRepository<Branch> branchRepository)
        {
            _repository = repository;
            _catalogClient = catalogClient;
            _pdfClient = pdfClient;
            _branchRepository = branchRepository;
        }

        public async Task<byte[]> PrintWorkList(WorkListFilterDto filter)
        {
            var requests = await _repository.GetWorkList(filter.AreaId, filter.Sucursales, filter.Fecha, filter.HoraInicio, filter.HoraFin);

            var studiesIds = requests.SelectMany(x => x.Estudios).Select(x => x.EstudioId).ToList();
            var studyParams = await _catalogClient.GetStudies(studiesIds);

            var data = requests.ToWorkListDto();

            var branches = await _branchRepository.Get(x => filter.Sucursales.Contains(x.Id));

            data.HojaTrabajo = filter.Area;
            data.Sucursal = string.Join(", ", branches.Select(x => x.Nombre));
            data.Fecha = filter.Fecha.ToString("dd/MM/yyyy");
            data.HoraInicio = filter.HoraInicio.ToString("HH:mm");
            data.HoraFin = filter.HoraFin.ToString("HH:mm");

            foreach (var request in data.Solicitudes)
            {
                foreach (var study in request.Estudios)
                {
                    var st = studyParams.FirstOrDefault(x => x.Id == study.EstudioId);
                    if (st != null)
                    {
                        var parameters = st.Parametros;
                        study.ListasTrabajo = parameters.Select(x => x.Clave.ToUpper()).ToList();
                    }
                }
            }

            var file = await _pdfClient.GenerateWorkList(data);

            return file;
        }
    }
}
