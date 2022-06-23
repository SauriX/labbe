using Service.Catalog.Domain.Catalog;
using System;
using System.Collections.Generic;

namespace Service.Catalog.Domain.Route
{
    public class Route
    {
        public Guid Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public Guid? SucursalOrigenId { get; set; }
        public virtual Branch.Branch SucursalOrigen { get; set; }
        public Guid? SucursalDestinoId { get; set; }
        public virtual Branch.Branch SucursalDestino { get; set; }
        public int? MaquiladorId { get; set; }
        public virtual Maquila.Maquila Maquilador { get; set; }
        public bool? RequierePaqueteria { get; set; }
        public int? SeguimientoPaqueteria { get; set; }
        public int? PaqueteriaId { get; set; }
        public virtual Delivery Paqueteria { get; set; }
        public string Comentarios { get; set; }
        public int DiasDeEntrega { get; set; }
        public DateTime? HoraDeEntregaEstimada { get; set; }
        public DateTime? HoraDeEntrega { get; set; }
        public int? HoraDeRecoleccion { get; set; }
        public int TiempoDeEntrega { get; set; }
        public Decimal FormatoDeTiempoId { get; set; }
        public string EstudioId { get; set; }
        public DateTime? FechaInicial { get; set; }
        public DateTime? FechaFinal { get; set; }
        public Guid? IdResponsableEnvio { get; set; }
        public Guid? IdResponsableRecepcion { get; set; }
        public bool Activo { get; set; }
        public string UsuarioCreoId { get; set; }
        public DateTime? FechaCreo { get; set; }
        public string UsuarioModificoId { get; set; }
        public DateTime? FechaModifico { get; set; }
        public bool Lunes { get; set; }
        public bool Martes { get; set; }
        public bool Miercoles { get; set; }
        public bool Jueves { get; set; }
        public bool Viernes { get; set; }
        public bool Sabado { get; set; }
        public bool Domingo { get; set; }
        public virtual ICollection<Route_Study> Estudios { get; set; }
    }
}
