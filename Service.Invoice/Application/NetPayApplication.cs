using Integration.NetPay.Services.IServices;
using Service.Billing.Application.IApplication;
using Service.Billing.Client.IClient;
using Service.Billing.Controllers;
using Service.Billing.Dtos.Notification;
using Service.Billing.Dtos.Payment;
using Shared.Extensions;
using System.Threading.Tasks;

namespace Service.Billing.Application
{
    public class NetPayApplication : INetPayApplication
    {
        private readonly ISaleService _checkoutService;
        private readonly IMedicalRecordClient _medicalRecordClient;
        private readonly ISenderClient _senderClient;

        public NetPayApplication(ISaleService checkoutService, IMedicalRecordClient medicalRecordClient, ISenderClient senderClient)
        {
            _checkoutService = checkoutService;
            _medicalRecordClient = medicalRecordClient;
            _senderClient = senderClient;
        }

        public async Task<string> PaymentCharge(PayPalPaymentDto payment)
        {
            var res = await _checkoutService.PaymentCharge(payment);

            return res;
        }

        public async Task<PayPalPaymentDto> ProcessResponse(NetPayResponse response)
        {
            var res = await _medicalRecordClient.CreatePayment(response.Payment);

            var not = new NotificationDto
            {
                Mensaje = "Todo chido",
                Usuario = response.Payment.NotificacionId,
                Metodo = "NotifyPaymentResponse",
                Datos = res.ToDictionary(lowercaseFirstLetter: true)
            };

            await _senderClient.Notify(not);

            return res;
        }
    }
}
