using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Dtos.CashRegister
{
    public class CashDto
    {
        public List<CashRegisterDto> PerDay { get; set; }
        public List<CashRegisterDto> Canceled { get; set; }
        public List<CashRegisterDto> OtherDay { get; set; }
    }
}
