using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.InvoiceCompany
{
    public class InvoiceCompanyDeliverDto
    {
        public List<Contact> Contactos { get; set; }
        public List<string> MediosEnvio { get; set; }
        public Guid FacturapiId { get; set; }
        public Guid UsuarioId { get; set; }
        public bool EsPrueba { get; set; }
    }
    public class Contact
    {
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
    }
}
