using System.Collections.Generic;
using System;
using Service.Report.Dtos.StudyStats;

namespace Service.Report.Domain.MedicalRecord
{
    public class RequestInfo
    {
        public Guid Id { get; set; }
        public Guid ExpedienteId { get; set; }
        public string Expediente { get; set; }
        public string NombreCompleto { get; set; }
        public int Edad { get; set; }
        public string Sexo { get; set; }
        public string Celular { get; set; }
        public string Correo { get; set; }
        public string Solicitud { get; set; }
        public string ClavePatalogica { get; set; }
        public Guid SucursalId { get; set; }
        public string Sucursal { get; set; }
        public byte EstatusId { get; set; }
        public string NombreEstatus { get; set; }
        public byte Procedencia { get; set; }
        public Guid CompañiaId { get; set; }
        public string Compañia { get; set; }
        public Guid MedicoId { get; set; }
        public string Medico { get; set; }
        public string ClaveMedico { get; set; }
        public byte Urgencia { get; set; }
        public bool Parcialidad { get; set; }
        public decimal TotalEstudios { get; set; }
        public decimal Descuento { get; set; }
        public decimal DescuentoPorcentual { get; set; }
        public decimal Cargo { get; set; }
        public decimal CargoPorcentual { get; set; }
        public decimal Promocion { get; set; }
        public decimal Copago { get; set; }
        public decimal PrecioEstudios { get; set; }
        public decimal Total { get; set; }
        public decimal Saldo { get; set; }
        public DateTime Fecha { get; set; }
        public List<RequestStudies> Estudios { get; set; }
    }
}
