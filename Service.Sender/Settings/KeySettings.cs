using Service.Sender.Settings.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Sender.Settings
{
    public class KeySettings : IKeySettings
    {
        public string MailPassword { get; set; }
        public string Token { get; set; }
    }
}
