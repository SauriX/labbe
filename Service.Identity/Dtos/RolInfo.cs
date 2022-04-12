using System;

namespace Service.Identity.Dtos
{
    public class RolInfo
    {
      public Guid  id { get; set; }
      public string nombre { get; set; }
      public bool activo { get; set; }
    }
}
