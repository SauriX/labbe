using Service.Billing.Dtos.Payment;
using System.Threading.Tasks;

namespace Service.Billing.Client.IClient
{
    public interface IMedicalRecordClient
    {
        Task<PayPalPaymentDto> CreatePayment(PayPalPaymentDto payment);
    }
}
