using Integration.WeeClinic.Dtos;
using Integration.WeeClinic.Models.Laboratorio_BusquedaFolioLaboratorio;
using Integration.WeeClinic.Models.Laboratorio_BusquedaFolios;
using Integration.WeeClinic.Models.Laboratorio_GetPreciosEstudios_ByidServicio;
using MassTransit;
using Newtonsoft.Json;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Integration.WeeClinic.Mapper
{
    public static class LaboratoryMapper
    {
        public static WeePatientInfoDto ToWeePatientInfoDto(this Laboratorio_BusquedaFolios_0 weeData)
        {
            return JsonConvert.DeserializeObject<WeePatientInfoDto>(JsonConvert.SerializeObject(weeData));
        }

        public static List<WeeServiceDto> ToWeeServiceDto(this List<Laboratorio_BusquedaFolioLaboratorio_0> weeData)
        {
            return JsonConvert.DeserializeObject<List<WeeServiceDto>>(JsonConvert.SerializeObject(weeData));
        }

        public static WeeServicePricesDto ToWeeServicePricesDto(this Laboratorio_GetPreciosEstudios_ByidServicio weeData)
        {
            var total = weeData.Datos[0];
            var patient = weeData.Datos1[0];
            var insurance = weeData.Datos2[0];

            return new WeeServicePricesDto
            {
                Total = new WeeServicePricesTotalDto
                {
                    PrecioUnitario = total.PrecioUnitario,
                    Subtotal = total.Subtotal,
                    RetencionIVA = total.RetencionIVA,
                    RetencionISR = total.RetencionISR,
                    ImpuestoEstatal = total.ImpuestoEstatal,
                    CopagoIsPorcentaje = total.CopagoIsPorcentaje,
                    IEPS = total.IEPS,
                    Descuento = total.Descuento,
                    ISR = total.ISR,
                    IsCubierto = total.IsCubierto,
                    TipoIVA = total.TipoIVA,
                    Cantidad = total.Cantidad,
                    TipoCopago = total.TipoCopago,
                    Copago = total.Copago,
                    TipoDeducible = total.TipoDeducible,
                    Deducible = total.Deducible,
                    TipoCoaseguro = total.TipoCoaseguro,
                    Coaseguro = total.Coaseguro,
                    DescuentoPorcentaje = total.DescuentoPorcentaje,
                    MontoExcedente = total.MontoExcedente
                },
                Paciente = new WeeServicePricesSingleDto
                {
                    SubTotal = patient.SubTotalPaciente,
                    Descuento = patient.Descuento,
                    SubTotalDescuento = patient.SubTotalDescuento,
                    IVA = patient.IVAPaciente,
                    RIVA = patient.RIVAPaciente,
                    RISR = patient.RISRPaciente,
                    IEPS = patient.IEPSPaciente,
                    ImpuestoEstatal = patient.ImpuestoEstatalPaciente,
                    ISR = patient.ISRPaciente,
                    TipoCosto = patient.TipoCosto,
                    IVAAplicado = patient.IVAAplicadoPaciente,
                    MontoExcedente = patient.MontoExcedente,
                    Total = patient.TotalPaciente
                },
                Aseguradora = new WeeServicePricesSingleDto
                {
                    SubTotal = insurance.SubTotalAseguradora,
                    Descuento = insurance.Descuento,
                    SubTotalDescuento = insurance.SubTotalDescuento,
                    IVA = insurance.IVAAseguradora,
                    RIVA = insurance.RIVAAseguradora,
                    RISR = insurance.RISRAseguradora,
                    IEPS = insurance.IEPSAseguradora,
                    ImpuestoEstatal = insurance.ImpuestoEstatalAseguradora,
                    ISR = insurance.ISRAseguradora,
                    TipoCosto = insurance.TipoCosto,
                    IVAAplicado = insurance.IVAAplicadoAseguradora,
                    MontoExcedente = insurance.MontoExcedente,
                    Total = insurance.TotalAseguradora
                }
            };
        }
    }
}
