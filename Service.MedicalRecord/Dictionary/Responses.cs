namespace Service.MedicalRecord.Dictionary
{
    public class Response
    {
        public class Request
        {
            public const string PriceChanged = "Los precios seleccionados no coinciden con los precios asignados de los estudios";
            public const string PackWithoutStudies = "Alguno de los paquetes no contiene estudios";
            public const string NoStudySelected = "No se selecciono ningun estudio para actualizar";
            public const string NoPaymentSelected = "No se selecciono ningun pago para actualizar";
            public const string AlreadyCancelled = "La solicitud ya ha sido cancelada";
            public const string AlreadyCompleted = "La solicitud ya está completada";
            public const string ProcessingStudies = "La solicitud ya cuenta con estudios en estatus no pendiente";
            public static string RepeatedStudies(string studies) => $"Los siguientes estudios se encuentran repetidos: {studies}";
        }

        public class Quotation
        {
            public const string NoStudySelected = "No se selecciono ningun estudio para actualizar";
            public const string AlreadyCancelled = "La cotización ya ha sido cancelada";
            public const string PackWithoutStudies = "Alguno de los paquetes no contiene estudios";
        }
    }
}
