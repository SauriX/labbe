using System.Collections.Generic;
using System;
using Service.Catalog.Dtos.Promotion;
using Service.Catalog.Domain.Notifications;

namespace Service.Catalog.Dtos.Notifications
{
    public class NotificationFormDto
    {
        public Guid?  Id { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public bool IsNotifi { get; set; }
        public List<DateTime> Fechas { get; set; }
        public bool Activo { get; set; }
        public Guid UsuarioId { get; set; }
        public List<Guid> Sucursales { get; set; }
        public List<Guid> Roles { get; set; }
        public IEnumerable<DiasDto> Dias { get; set; }
    }
}
