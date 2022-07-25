using Integration.Contpaqi.Service.IService;
using Microsoft.Win32;
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Integration.Contpaqi.Service
{
    public class SessionService : ISessionService
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

        public bool Connected { get; set; }

        public void InitConnection()
        {
            // Tomar resultado Función Manejo Errores
            int lResultado;

            // Asignar la Ruta de DLL MGWServicios
            string szRegKeySistema = @"SOFTWARE\\Computación en Acción, SA CV\\CONTPAQ I COMERCIAL";

            // Establece la ruta donde se encuentar el archivo MGWSERVICIOS.DLL
            RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
            RegistryKey keySistema = baseKey?.OpenSubKey(szRegKeySistema);

            if (keySistema == null)
            {
                throw new Exception("No se encontró el Registry " + szRegKeySistema);
            }

            object lEntrada = keySistema.GetValue("DirectorioBase");
            lResultado = SetCurrentDirectory(lEntrada.ToString());

            if (lResultado != 1)
            {
                ThrowError(lResultado);
            }

            try
            {
                lResultado = fSetNombrePAQ("CONTPAQ I COMERCIAL");
            }
            catch (Exception e)
            {
                throw new Exception($"Error fSetNombrePAQ: 'CONTPAQ I COMERCIAL' {Environment.NewLine}{e.Message}");
            }

            if (lResultado != 0)
            {
                ThrowError(lResultado);
            }

            // Se abre empresa
            lResultado = fAbreEmpresa("C:\\Compac\\Empresas\\comTrebol");
            if (lResultado != 0)
            {
                ThrowError(lResultado);
            }
            else
            {
                Console.WriteLine("Se Abrio Empresa Correctamente...");
            }

            // Se cierra empresa
            fCierraEmpresa();

            // Se termina SDK
            fTerminaSDK();
        }

        public void EndConnection()
        {
            throw new NotImplementedException();
        }

        // Función para el manejo de errores en SDK
        public static void ThrowError(int iError)
        {
            StringBuilder sMensaje = new(512);
            if (iError != 0)
            {
                SDK.Functions.fError(iError, sMensaje, 512);
                string error = "CONTPAQi Error: " + sMensaje;
                throw new Exception(error);
            }
        }
    }
}
