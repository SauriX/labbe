﻿namespace Service.MedicalRecord.Dtos.RouteTracking
{
    public class RouteTrackingStudyListDto
    {
      public int Id { get; set; }
      public string Nombre { get; set; }
      public string Area { get; set; }
      public int Status { get; set; }
      public string Registro { get; set; }
      public string Entrega { get; set; }
      public bool Seleccion { get; set; }
      public string Clave { get; set; }
    }
}