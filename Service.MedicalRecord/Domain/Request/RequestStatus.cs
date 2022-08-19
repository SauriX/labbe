namespace Service.MedicalRecord.Domain.Request
{
    public class RequestStatus
    {
        public RequestStatus()
        {
        }

        public RequestStatus(byte id, string clave, string nombre, string color)
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
