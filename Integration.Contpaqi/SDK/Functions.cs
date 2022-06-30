using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Integration.Contpaqi.SDK
{
    internal class Functions
    {
        [DllImport("MGWSERVICIOS.DLL")]
        public static extern int fSetNombrePAQ(string aNombrePAQ);
        [DllImport("MGWSERVICIOS.DLL")]
        public static extern int fAbreEmpresa(string Directorio);
        [DllImport("MGWSERVICIOS.DLL")]
        public static extern void fTerminaSDK();
        [DllImport("MGWSERVICIOS.DLL")]
        public static extern void fCierraEmpresa();
        [DllImport("MGWSERVICIOS.DLL")]
        public static extern void fError(int NumeroError, StringBuilder Mensaje, int Longitud);
        [DllImport("KERNEL32")]
        public static extern int SetCurrentDirectory(string pPtrDirActual);
    }
}
