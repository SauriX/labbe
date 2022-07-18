using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Sender.Settings.Interfaces
{
    public interface IKeySettings
    {
        public string MailPassword { get; set; }
        public string Token { get; set; }
    }
}
