namespace Service.MedicalRecord.Dtos.WeeClinic
{
    public class WeeCancellationDto
    {
        public string IdServicio { get; set; }
        public string IdNodo { get; set; }
        public int Action { get; set; }
        public bool Cancelado => Action == 1;
    }
}
