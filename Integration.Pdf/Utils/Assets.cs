using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Integration.Pdf.Utils
{
    public class Assets
    {
        public static byte[] GetLogoBytes()
        {
            return File.ReadAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets\\LabRamosLogo.png"));
        }
    }
}