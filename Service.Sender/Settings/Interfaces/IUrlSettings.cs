using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Sender.Settings.Interfaces
{
    public interface IUrlSettings
    {
        public string Home { get; set; }
        public string Web { get; set; }
        public string Images { get; set; }
    }
}
