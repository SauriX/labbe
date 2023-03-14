﻿using Service.Catalog.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Catalog.Domain.Route
{
    public class Route
    {
        public Route()
        {
        }

        public Route(string clave, string nombre, Guid sucursalId, int maquiladorId, IEnumerable<int> estudioIds)
        {
            var id = Guid.NewGuid();
            var date = DateTime.Now;

            Id = id;
            Clave = clave;
            Nombre = nombre;
            OrigenId = sucursalId;
            MaquiladorId = maquiladorId;
            Activo = true;
            Lunes = true;
            Martes = true;
            Miercoles = true;
            Jueves = true;
            Viernes = true;
            Sabado = true;
            Domingo = true;
            FechaCreo = date;
            Estudios = estudioIds.Select(x => new Route_Study
            {
                RouteId = id,
                EstudioId = x,
                FechaCreo = date
            }).ToList();
        }

        public Guid Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public Guid? OrigenId { get; set; }
        public virtual Branch.Branch Origen { get; set; }
        public Guid? DestinoId { get; set; }
        public virtual Branch.Branch Destino { get; set; }
        public int? MaquiladorId { get; set; }
        public virtual Maquila.Maquila Maquilador { get; set; }
        public int? PaqueteriaId { get; set; }
        public virtual Delivery Paqueteria { get; set; }
        public string Comentarios { get; set; }
        public DateTime HoraDeRecoleccion { get; set; }
        public int TiempoDeEntrega { get; set; }
        public byte TipoTiempo { get; set; }
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
