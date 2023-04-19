using Service.MedicalRecord.Dtos.TrackingOrder;
using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.RouteTracking
{
    public class RouteTrackingFormDto
    {
        public Guid Id { get; set; }
        public string Origen { get; set; }
		public string Clave { get; set; }
		public decimal Temperatura { get; set; }
		public DateTime Recoleccion { get; set; }
		public string Solicitud { get; set; }
		public string Destino { get; set; }
        public bool Activo { get; set; }
        public string Muestra { get; set; }
        public bool Escaneo { get; set; }
        public Guid RutaId { get; set; }
        public List<StudyRouteDto> Estudios { get; set; }
        public IEnumerable<TagRouteDto> Etiquetas { get; set; }
    }
}
