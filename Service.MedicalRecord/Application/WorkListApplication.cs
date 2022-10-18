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
                        study.ListasTrabajo = parameters.Select(x => x.Clave).ToList();
                    }
                }
            }

            var file = await _pdfClient.GenerateWorkList(data);

            return file;
        }

        private WorkListDto GetFakeData()
        {
            return new WorkListDto()
            {
                HojaTrabajo = "QUIMICAS",
                Fecha = "2022-09-23 00:00",
                Sucursal = "*",
                HoraInicio = "00:00",
                HoraFin = "23:59",
                Solicitudes = new List<WorkListRequestDto>
                {
                    new WorkListRequestDto
                    {
                        Solicitud = "2209238001",
                        HoraSolicitud = "07:27",
                        Paciente = "DAVID LEE ARPEE HINOJOSA",
                        Estatus = "Recepcion",
                        HoraEstatus = "07:27:36",
                        Estudios = new List<WorkListStudyDto>
                        {
                            new WorkListStudyDto
                            {
                                Estudio = "GLUCOSA (GLU)",
                                ListasTrabajo = new List<string>
                                {
                                    "GLU"
                                }
                            },
                             new WorkListStudyDto
                            {
                                Estudio = "PROTEINAS SERICAS (PT ALB)",
                                ListasTrabajo = new List<string>
                                {
                                    "GLU",
                                    "UREA",
                                    "CREA",
                                    "AU",
                                    "COL",
                                    "TRIG",
                                    "PT",
                                    "ALB",
                                    "GLOB",
                                    "A/G",
                                    "CA",
                                    "FOSF",
                                    "MG",
                                    "AMIL",
                                }
                            },
                        }
                    },
                    new WorkListRequestDto
                    {
                        Solicitud = "2209238002",
                        HoraSolicitud = "08:05",
                        Paciente = "VERONICA VIERA GONZALEZ",
                        Estatus = "Recepcion",
                        HoraEstatus = "08:05:18",
                        Estudios = new List<WorkListStudyDto>
                        {
                            new WorkListStudyDto
                            {
                                Estudio = "PERFIL DE QUIMICAS PRIVADO (GLU UREA CREA AU COL TRI)",
                                ListasTrabajo = new List<string>
                                {
                                    "GLU",
                                    "UREA",
                                    "CREA",
                                    "AU",
                                    "COL",
                                    "TRIG"
                                }
                            },
                            new WorkListStudyDto
                            {
                                Estudio = "PRUEBAS FUNCIONALES HEPATICAS (BILT BILD TGO TGP ALP DHL)",
                                ListasTrabajo = new List<string>
                                {
                                    "TBIL",
                                    "DBIL",
                                    "IBIL",
                                    "AST",
                                    "ALT",
                                    "FALC",
                                    "DHL"
                                }
                            },
                            new WorkListStudyDto
                            {
                                Estudio = "HIERRO SERICO",
                                ListasTrabajo = new List<string>
                                {
                                    "IRON"
                                }
                            },
                        }
                    },
                     new WorkListRequestDto
                    {
                        Solicitud = "2209238001",
                        HoraSolicitud = "07:27",
                        Paciente = "DAVID LEE ARPEE HINOJOSA",
                        Estatus = "Recepcion",
                        HoraEstatus = "07:27:36",
                        Estudios = new List<WorkListStudyDto>
                        {
                            new WorkListStudyDto
                            {
                                Estudio = "GLUCOSA (GLU)",
                                ListasTrabajo = new List<string>
                                {
                                    "GLU"
                                }
                            },
                             new WorkListStudyDto
                            {
                                Estudio = "PROTEINAS SERICAS (PT ALB)",
                                ListasTrabajo = new List<string>
                                {
                                    "GLU",
                                    "UREA",
                                    "CREA",
                                    "AU",
                                    "COL",
                                    "TRIG",
                                    "PT",
                                    "ALB",
                                    "GLOB",
                                    "A/G",
                                    "CA",
                                    "FOSF",
                                    "MG",
                                    "AMIL",
                                }
                            },
                        }
                    },
                    new WorkListRequestDto
                    {
                        Solicitud = "2209238002",
                        HoraSolicitud = "08:05",
                        Paciente = "VERONICA VIERA GONZALEZ",
                        Estatus = "Recepcion",
                        HoraEstatus = "08:05:18",
                        Estudios = new List<WorkListStudyDto>
                        {
                            new WorkListStudyDto
                            {
                                Estudio = "PERFIL DE QUIMICAS PRIVADO (GLU UREA CREA AU COL TRI)",
                                ListasTrabajo = new List<string>
                                {
                                    "GLU",
                                    "UREA",
                                    "CREA",
                                    "AU",
                                    "COL",
                                    "TRIG"
                                }
                            },
                            new WorkListStudyDto
                            {
                                Estudio = "PRUEBAS FUNCIONALES HEPATICAS (BILT BILD TGO TGP ALP DHL)",
                                ListasTrabajo = new List<string>
                                {
                                    "TBIL",
                                    "DBIL",
                                    "IBIL",
                                    "AST",
                                    "ALT",
                                    "FALC",
                                    "DHL"
                                }
                            },
                            new WorkListStudyDto
                            {
                                Estudio = "HIERRO SERICO",
                                ListasTrabajo = new List<string>
                                {
                                    "IRON"
                                }
                            },
                        }
                    },
                     new WorkListRequestDto
                    {
                        Solicitud = "2209238001",
                        HoraSolicitud = "07:27",
                        Paciente = "DAVID LEE ARPEE HINOJOSA",
                        Estatus = "Recepcion",
                        HoraEstatus = "07:27:36",
                        Estudios = new List<WorkListStudyDto>
                        {
                            new WorkListStudyDto
                            {
                                Estudio = "GLUCOSA (GLU)",
                                ListasTrabajo = new List<string>
                                {
                                    "GLU"
                                }
                            },
                             new WorkListStudyDto
                            {
                                Estudio = "PROTEINAS SERICAS (PT ALB)",
                                ListasTrabajo = new List<string>
                                {
                                    "GLU",
                                    "UREA",
                                    "CREA",
                                    "AU",
                                    "COL",
                                    "TRIG",
                                    "PT",
                                    "ALB",
                                    "GLOB",
                                    "A/G",
                                    "CA",
                                    "FOSF",
                                    "MG",
                                    "AMIL",
                                }
                            },
                        }
                    },
                    new WorkListRequestDto
                    {
                        Solicitud = "2209238002",
                        HoraSolicitud = "08:05",
                        Paciente = "VERONICA VIERA GONZALEZ",
                        Estatus = "Recepcion",
                        HoraEstatus = "08:05:18",
                        Estudios = new List<WorkListStudyDto>
                        {
                            new WorkListStudyDto
                            {
                                Estudio = "PERFIL DE QUIMICAS PRIVADO (GLU UREA CREA AU COL TRI)",
                                ListasTrabajo = new List<string>
                                {
                                    "GLU",
                                    "UREA",
                                    "CREA",
                                    "AU",
                                    "COL",
                                    "TRIG"
                                }
                            },
                            new WorkListStudyDto
                            {
                                Estudio = "PRUEBAS FUNCIONALES HEPATICAS (BILT BILD TGO TGP ALP DHL)",
                                ListasTrabajo = new List<string>
                                {
                                    "TBIL",
                                    "DBIL",
                                    "IBIL",
                                    "AST",
                                    "ALT",
                                    "FALC",
                                    "DHL"
                                }
                            },
                            new WorkListStudyDto
                            {
                                Estudio = "HIERRO SERICO",
                                ListasTrabajo = new List<string>
                                {
                                    "IRON"
                                }
                            },
                        }
                    },
                     new WorkListRequestDto
                    {
                        Solicitud = "2209238001",
                        HoraSolicitud = "07:27",
                        Paciente = "DAVID LEE ARPEE HINOJOSA",
                        Estatus = "Recepcion",
                        HoraEstatus = "07:27:36",
                        Estudios = new List<WorkListStudyDto>
                        {
                            new WorkListStudyDto
                            {
                                Estudio = "GLUCOSA (GLU)",
                                ListasTrabajo = new List<string>
                                {
                                    "GLU"
                                }
                            },
                             new WorkListStudyDto
                            {
                                Estudio = "PROTEINAS SERICAS (PT ALB)",
                                ListasTrabajo = new List<string>
                                {
                                    "GLU",
                                    "UREA",
                                    "CREA",
                                    "AU",
                                    "COL",
                                    "TRIG",
                                    "PT",
                                    "ALB",
                                    "GLOB",
                                    "A/G",
                                    "CA",
                                    "FOSF",
                                    "MG",
                                    "AMIL",
                                }
                            },
                        }
                    },
                    new WorkListRequestDto
                    {
                        Solicitud = "2209238002",
                        HoraSolicitud = "08:05",
                        Paciente = "VERONICA VIERA GONZALEZ",
                        Estatus = "Recepcion",
                        HoraEstatus = "08:05:18",
                        Estudios = new List<WorkListStudyDto>
                        {
                            new WorkListStudyDto
                            {
                                Estudio = "PERFIL DE QUIMICAS PRIVADO (GLU UREA CREA AU COL TRI)",
                                ListasTrabajo = new List<string>
                                {
                                    "GLU",
                                    "UREA",
                                    "CREA",
                                    "AU",
                                    "COL",
                                    "TRIG"
                                }
                            },
                            new WorkListStudyDto
                            {
                                Estudio = "PRUEBAS FUNCIONALES HEPATICAS (BILT BILD TGO TGP ALP DHL)",
                                ListasTrabajo = new List<string>
                                {
                                    "TBIL",
                                    "DBIL",
                                    "IBIL",
                                    "AST",
                                    "ALT",
                                    "FALC",
                                    "DHL"
                                }
                            },
                            new WorkListStudyDto
                            {
                                Estudio = "HIERRO SERICO",
                                ListasTrabajo = new List<string>
                                {
                                    "IRON"
                                }
                            },
                        }
                    },
                     new WorkListRequestDto
                    {
                        Solicitud = "2209238001",
                        HoraSolicitud = "07:27",
                        Paciente = "DAVID LEE ARPEE HINOJOSA",
                        Estatus = "Recepcion",
                        HoraEstatus = "07:27:36",
                        Estudios = new List<WorkListStudyDto>
                        {
                            new WorkListStudyDto
                            {
                                Estudio = "GLUCOSA (GLU)",
                                ListasTrabajo = new List<string>
                                {
                                    "GLU"
                                }
                            },
                             new WorkListStudyDto
                            {
                                Estudio = "PROTEINAS SERICAS (PT ALB)",
                                ListasTrabajo = new List<string>
                                {
                                    "GLU",
                                    "UREA",
                                    "CREA",
                                    "AU",
                                    "COL",
                                    "TRIG",
                                    "PT",
                                    "ALB",
                                    "GLOB",
                                    "A/G",
                                    "CA",
                                    "FOSF",
                                    "MG",
                                    "AMIL",
                                }
                            },
                        }
                    },
                    new WorkListRequestDto
                    {
                        Solicitud = "2209238002",
                        HoraSolicitud = "08:05",
                        Paciente = "VERONICA VIERA GONZALEZ",
                        Estatus = "Recepcion",
                        HoraEstatus = "08:05:18",
                        Estudios = new List<WorkListStudyDto>
                        {
                            new WorkListStudyDto
                            {
                                Estudio = "PERFIL DE QUIMICAS PRIVADO (GLU UREA CREA AU COL TRI)",
                                ListasTrabajo = new List<string>
                                {
                                    "GLU",
                                    "UREA",
                                    "CREA",
                                    "AU",
                                    "COL",
                                    "TRIG"
                                }
                            },
                            new WorkListStudyDto
                            {
                                Estudio = "PRUEBAS FUNCIONALES HEPATICAS (BILT BILD TGO TGP ALP DHL)",
                                ListasTrabajo = new List<string>
                                {
                                    "TBIL",
                                    "DBIL",
                                    "IBIL",
                                    "AST",
                                    "ALT",
                                    "FALC",
                                    "DHL"
                                }
                            },
                            new WorkListStudyDto
                            {
                                Estudio = "HIERRO SERICO",
                                ListasTrabajo = new List<string>
                                {
                                    "IRON"
                                }
                            },
                        }
                    }
                },
            };
        }
    }
}
