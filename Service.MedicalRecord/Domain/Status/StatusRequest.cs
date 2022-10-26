namespace Service.MedicalRecord.Domain.Status
{
    public class StatusRequest
    {
        public StatusRequest()
        {
        }

        public StatusRequest(byte id, string clave, string nombre, string color)
        {
            Id = id;
            Clave = clave;
            Nombre = nombre;
            Color = color;
        }

        public byte Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Color { get; set; }
    }
}
