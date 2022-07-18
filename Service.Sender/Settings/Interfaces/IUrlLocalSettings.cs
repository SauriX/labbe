using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Sender.Settings.Interfaces
{
    public interface IUrlLocalSettings
    {
        public string Layout { get; set; }
        public string SensitivesImages { get; set; }
        public string UsersImages { get; set; }
    }
}
