namespace Service.MedicalRecord.Domain.Status
{
    public class StatusRequestPayment
    {
        public StatusRequestPayment()
        {
        }

        public StatusRequestPayment(byte id, string clave, string nombre)
        {
            Id = id;
            Clave = clave;
            Nombre = nombre;
        }

        public byte Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
    }
}
