using Service.Billing.Controllers;
using Service.Billing.Dtos.Payment;
using System.Threading.Tasks;

namespace Service.Billing.Application.IApplication
{
    public interface INetPayApplication
    {
        Task<string> PaymentCharge(PayPalPaymentDto payment);
        Task<PayPalPaymentDto> ProcessResponse(NetPayResponse response);
    }
}
