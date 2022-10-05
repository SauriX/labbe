using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.Route
{
    public class RouteFormDto { 

                public string Id { get; set; }
    public string Clave { get; set; }
    public string Nombre { get; set; }
    public Guid? SucursalOrigenId { get; set; }
    public Guid? SucursalDestinoId { get; set; }
    public string SucursalOrigen { get; set; }
    public string SucursalDestino { get; set; }
    public int? MaquiladorId { get; set; }
    public int? PaqueteriaId { get; set; }
    public string Comentarios { get; set; }
    public int? HoraDeRecoleccion { get; set; }
    public int DiasDeEntrega { get; set; }
    public int TiempoDeEntrega { get; set; }
    public Decimal FormatoDeTiempoId { get; set; }
    public string EstudioId { get; set; }
    public bool Activo { get; set; }
    public Guid UsuarioId { get; set; }
    public IEnumerable<DiasDto> Dias { get; set; }
    public IEnumerable<Route_StudyListDto> Estudio { get; set; }
    
    }
}
