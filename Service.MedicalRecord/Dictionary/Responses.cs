namespace Service.MedicalRecord.Dictionary
{
    public class Response
    {
        public class Request
        {
            public const string PriceChanged = "Los precios seleccionados no coinciden con los precios asignados de los estudios";
            public const string PackWithoutStudies = "Alguno de los paquetes no contiene estudios";
            public const string NoStudySelected = "No se selecciono ningun estudio para actualizar";
            public static string RepeatedStudies(string studies) => $"Los siguientes estudios se encuentran repetidos: {studies}";
        }
    }
}
