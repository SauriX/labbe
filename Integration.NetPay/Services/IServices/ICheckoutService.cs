using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integration.NetPay.Services.IServices
{
    public interface ISaleService
    {
        Task<string> PaymentCharge(object payment);
    }
}
