using Service.Sender.Settings.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Sender.Settings
{
    public class UrlLocalSettings : IUrlLocalSettings
    {
        public string Layout { get; set; }
        public string SensitivesImages { get; set; }
        public string UsersImages { get; set; }
    }
}
