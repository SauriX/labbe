using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Client.IClient;
using Service.MedicalRecord.Dtos.WorkList;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Application
{
    public class WorkListApplication : IWorkListApplication
    {
        private readonly IPdfClient _pdfClient;

        public WorkListApplication(IPdfClient pdfClient)
        {
            _pdfClient = pdfClient;
        }

        public async Task<byte[]> PrintWorkList(WorkListFilterDto filter)
        {
            var wl = GetFakeData();

            var file = await _pdfClient.GenerateWorkList(wl);

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
