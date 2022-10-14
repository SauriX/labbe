namespace Service.MedicalRecord.Dictionary
{
    public class EmailTemplates
    {
        public class Request
        {
            public class Subjects
            {
                public const string TestMessage = "Mensaje de prueba";
                public const string PathologicalSubject = "RESULTADO LABORATORIO RAMOS";
            }

            public class Titles
            {
                public static string RequestCode(string code) => $"Solicitud: {code}";
                public const string PathologicalTitle = "Diagnóstico";
            }

            public class Messages
            {
                public const string TestMessage =
                    "Buen día,\n" +
                    "\n" +
                    "Bienvenido a Laboratorio Ramos, esta es una prueba de envío para verficación de contacto.\n" +
                    "\n" +
                    "Muchas gracias por permitirnos brindarle un servicio de calidad.";
                public const string PathologicalMessage = "Estimado paciente.+" +
                    "\n" +
                    "\nEl archivo adjunto contiene sus resultados de análisis clínicos " +
                    "\n" +
                    "\nGracias.";

            }
        }
    }
}
