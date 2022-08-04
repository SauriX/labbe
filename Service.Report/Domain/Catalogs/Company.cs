using System;

namespace Service.Report.Domain.Catalogs
{
    public class Company
    {
        public Guid Id { get; set; }
        public string NombreEmpresa { get; set; }
        public byte Convenio { get; set; }
    }
}
