using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.Request
{
    public class RequestTicketDto
    {
        public string DireccionSucursal { get; set; } // "Laboratorio Alfonso Ramos, S.A. de C.V. Avenida Humberto Lobo #555 A, Col. del Valle C.P. 66220 San Pedro Garza García, Nuevo León."
        public string Contacto { get; set; } // "Tel/WhatsApp: 81 4170 0769 RFC: LAR900731TL0"
        public string Sucursal { get; set; } // "SUCURSAL MONTERREY"
        public string Folio { get; set; }
        public string Fecha { get; set; }
        public string Atiende { get; set; }
        public string Paciente { get; set; }
        public string Expediente { get; set; }
        public string FechaNacimiento { get; set; }
        public string Solicitud { get; set; }
        public string FechaEntrega { get; set; }
        public string Medico { get; set; }
        public string FormaPago { get; set; }
        public string Subtotal { get; set; }
        public string Descuento { get; set; }
        public string IVA { get; set; }
        public string Total { get; set; }
        public string Anticipo { get; set; }
        public string Saldo { get; set; }
        public string PagoLetra { get; set; }
        public string MonederoUtilizado { get; set; }
        public string MonederoGenerado { get; set; }
        public string MonederoAcumulado { get; set; }
        public string CodigoPago { get; set; }
        public string Usuario { get; set; }
        public string Contraseña { get; set; }
        public string ContactoTelefono { get; set; }

        public List<RequestTicketStudyDto> Estudios { get; set; } = new List<RequestTicketStudyDto>();
    }

    public class RequestTicketStudyDto
    {
        public string Cantidad { get; set; }
        public string Clave { get; set; }
        public string Estudio { get; set; }
        public string Precio { get; set; }
        public string Descuento { get; set; }
        public string Total { get; set; }
    }
}
