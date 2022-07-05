using Shared.Error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Helpers
{
    public class Log
    {
        public static void Write(ServerException ex)
        {
            //File.AppendAllText(Path.Combine(Directory.GetCurrentDirectory(), "log.txt"), Environment.NewLine + Environment.NewLine + DateTime.Now.ToString() + " => " + e.Message);
            //if (e.InnerException != null)
            //{
            //    File.AppendAllText(Path.Combine(Directory.GetCurrentDirectory(), "log.txt"), Environment.NewLine + Environment.NewLine + DateTime.Now.ToString() + " => InnerException: " + e.InnerException.Message);
            //}
        }
    }
}
