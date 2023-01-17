using System;

namespace Service.MedicalRecord.Dtos.TrackingOrder
{
    public class TrackingOrderFormDto
    {
        public Guid Id { get; set; }
        public string SucursalDestinoId { get; set; }
        public string SucursalDestinoNombre { get; set; }
        public string SucursalOrigenId { get; set; }
        public string SucrusalOrigenNombre { get; set; }
        public int MaquiladorId { get; set; }
        public string Clave { get; set; }
        public DateTime DiaRecoleccion { get; set; }
        public string RutaId { get; set; }
        public string RutaNombre { get; set; }
        public string MuestraId { get; set; }
        public bool EscaneoCodigoBarras { get; set; }
        public double Temperatura { get; set; }
        public bool Activo { get; set; }
        public Guid UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public Guid UsuarioModId { get; set; }
        public DateTime FechaMod { get; set; }
        
        public StudiesRequestRouteDto[] Estudios { get; set; }
        public int HoraDeRecoleccion { get; set; }

        public DateTime Fecha { get; set; }
        public string SolicitudId { get; set; }
        public string ClaveEstudio { get; set; }
        public string Estudio { get; set; }
        public string PacienteId { get; set; }
        public bool Escaneado { get; set; }
        public Guid UsuarioId { get; set; }
       
    }
}
