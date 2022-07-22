using Microsoft.Win32; //SetCurrentDiectory
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Test
{
    internal class Program
    {
        [DllImport("MGWServicios.dll")]
        public static extern int fSetNombrePAQ(string aNombrePAQ);
        [DllImport("MGWServicios.dll")]
        public static extern int fAbreEmpresa(string Directorio);
        [DllImport("MGWServicios.dll")]
        public static extern void fTerminaSDK();
        [DllImport("MGWServicios.dll")]
        public static extern void fCierraEmpresa();
        [DllImport("MGWServicios.dll")]
        public static extern void fError(int NumeroError, StringBuilder Mensaje, int Longitud);
        [DllImport("KERNEL32")]
        public static extern int SetCurrentDirectory(string pPtrDirActual);

        static void Main(string[] args)
        {
            //Tomar resultado Función Manejo Errores
            int lResultado;
            //asignar la Ruta de DLL MGWServicios
            string szRegKeySistema = @"SOFTWARE\\Computación en Acción, SA CV\\CONTPAQ I COMERCIAL";
            //string szRegKeySistema = @"C:\\Program Files (x86)\\Compac\\COMERCIAL";
            //Establece la ruta donde se encuentar el archivo MGWSERVICIOS.DLL
            //RegistryKey keySistema = Registry.LocalMachine.OpenSubKey(szRegKeySistema);
            RegistryKey keySistema = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(szRegKeySistema);
            object lEntrada = keySistema.GetValue("DirectorioBase");
            lResultado = SetCurrentDirectory(lEntrada.ToString());
            //Sistemas : CONTPAQi® Comercial
            lResultado = fSetNombrePAQ("CONTPAQ I COMERCIAL");
            if (lResultado != 0)
            {
                rError(lResultado);
            }
            else
            {
                //Se abre empresa
                fAbreEmpresa("C:\\Compac\\Empresas\\comTrebol");
                if (lResultado != 0)
                {
                    rError(lResultado);
                }
                else
                {
                    Console.WriteLine("Se Abrio Empresa Correctamente...");
                }
                //Se verifica Opción
                Console.WriteLine("¿Deseas Cerrar Empresa y Terminar SDK ? 1 = Si || 2 = No");
                if (Console.ReadLine() == "1")
                {
                    fCierraEmpresa(); // se Cierra Empresa
                    fTerminaSDK(); //Se termina SDK
                }
            }//fin else fSetNombrePAQ
        }//fin Main

        // Función para el manejo de errores en SDK
        public static void rError(int iError)
        {
            StringBuilder sMensaje = new StringBuilder(512);
            if (iError != 0)
            {
                fError(iError, sMensaje, 512);
                Console.WriteLine("Error: " + sMensaje);
                Console.ReadKey();
            }
        }
    }
}
