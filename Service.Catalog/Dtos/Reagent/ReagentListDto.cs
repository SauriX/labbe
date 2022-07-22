namespace Service.Catalog.Dtos.Reagent
{
    public class ReagentListDto
    {
        public string Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string ClaveSistema { get; set; }
        public string NombreSistema { get; set; }
        public bool Activo { get; set; }
    }
}
