using Service.MedicalRecord.Domain.TrackingOrder;
using Service.MedicalRecord.Dtos.TrackingOrder;

namespace Service.MedicalRecord.Mapper
{
    public static class TrackingOrderMapper
    {
        public static TrackingOrderDto ToTrackingOrderFormDto(this TrackingOrder model)
        {
            if (model == null) return null;

            return new TrackingOrderDto
            {
                Id = model.Id,
                ClaveEstudio = model.ClaveEstudio,
                Escaneado = model.Escaneado,
                EscaneoCodigoBarras = model.EscaneoCodigoBarras,
                Estudio = model.Estudio,
                MuestraId = model.MuestraId,
                PacienteId = model.PacienteId,
                SolicitudId = model.SolicitudId,
                SucursalDestinoId = model.SucursalDestinoId,
                SucursalOrigenId = model.SucursalOrigenId,
                Temperatura = model.Temperatura

            };
        }
    }
}
