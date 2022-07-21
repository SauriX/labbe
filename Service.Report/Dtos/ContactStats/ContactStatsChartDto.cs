namespace Service.Report.Dtos.ContactStats
{
    public class ContactStatsChartDto
    {
        public int CantidadTelefono { get; set; }
        public int CantidadCorreo { get; set; }
        public int Total => CantidadTelefono + CantidadCorreo;
    }
}
