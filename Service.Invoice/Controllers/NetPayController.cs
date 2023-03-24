using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Integration.NetPay.Services;
using Integration.NetPay.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Billing.Application.IApplication;
using Service.Billing.Dtos.Payment;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Service.Billing.Controllers
{
    public class TestNetPay
    {
        public string FolioNumber { get; set; }
        public string InternalNumber { get; set; }
        public string TableId { get; set; }
        public List<Dictionary<object, object>> ListOfPays { get; set; }
        public string TipTotalAmount { get; set; }
        public string TotalAmount { get; set; }
    }

    public class NetPayResponse
    {
        [JsonPropertyName("affiliation")]
        public string Affiliation { get; set; }
        [JsonPropertyName("applicationLabel")]
        public string ApplicationLabel { get; set; }
        [JsonPropertyName("arqc")]
        public string ARQC { get; set; }
        [JsonPropertyName("aid")]
        public string AID { get; set; }
        [JsonPropertyName("amount")]
        public string Amount { get; set; }
        [JsonPropertyName("authCode")]
        public string AuthCode { get; set; }
        [JsonPropertyName("bin")]
        public string Bin { get; set; }
        [JsonPropertyName("bankName")]
        public string BankName { get; set; }
        [JsonPropertyName("cardExpDate")]
        public string CardExpDate { get; set; }
        [JsonPropertyName("cardType")]
        public string CardType { get; set; }
        [JsonPropertyName("cardTypeName")]
        public string CardTypeName { get; set; }
        [JsonPropertyName("cityName")]
        public string CityName { get; set; }
        [JsonPropertyName("responseCode")]
        public string ResponseCode { get; set; }
        [JsonPropertyName("folioNumber")]
        public string FolioNumber { get; set; }
        [JsonPropertyName("hasPin")]
        public bool HasPin { get; set; }
        [JsonPropertyName("hexSign")]
        public string HexSign { get; set; }
        [JsonPropertyName("isQps")]
        public decimal IsQps { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; }
        [JsonPropertyName("isRePrint")]
        public bool IsRePrint { get; set; }
        [JsonPropertyName("moduleCharge")]
        public string ModuleCharge { get; set; }
        [JsonPropertyName("moduleLote")]
        public string ModuleLote { get; set; }
        [JsonPropertyName("customerName")]
        public string CustomerName { get; set; }
        [JsonPropertyName("terminalId")]
        public string TerminalId { get; set; }
        [JsonPropertyName("orderId")]
        public string OrderId { get; set; }
        [JsonPropertyName("preAuth")]
        public string PreAuth { get; set; }
        [JsonPropertyName("preStatus")]
        public decimal PreStatus { get; set; }
        [JsonPropertyName("promotion")]
        public string Promotion { get; set; }
        [JsonPropertyName("rePrintDate")]
        public string RePrintDate { get; set; }
        [JsonPropertyName("rePrintMark")]
        public string RePrintMark { get; set; }
        [JsonPropertyName("reprintModule")]
        public string RePrintModule { get; set; }
        [JsonPropertyName("cardNumber")]
        public string CardNumber { get; set; }
        [JsonPropertyName("storeName")]
        public string StoreName { get; set; }
        [JsonPropertyName("streetName")]
        public string StreetName { get; set; }
        [JsonPropertyName("ticketDate")]
        public string TicketDate { get; set; }
        [JsonPropertyName("tipAmount")]
        public string TipAmount { get; set; }
        [JsonPropertyName("tipLessAmount")]
        public string TipLessAmount { get; set; }
        [JsonPropertyName("transDate")]
        public string TransDate { get; set; }
        [JsonPropertyName("transType")]
        public string TransType { get; set; }
        [JsonPropertyName("transactionCertificate")]
        public string TransactionCertificate { get; set; }
        [JsonPropertyName("transactionId")]
        public string TransactionId { get; set; }   
        [JsonPropertyName("traceability")]
        public PayPalPaymentDto Payment { get; set; }
    }
    
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class NetPayController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly INetPayApplication _netPayService;

        public NetPayController(IAuthService authService, INetPayApplication service)
        {
            _authService = authService;
            _netPayService = service;
        }

        [HttpPost("login")]
        //public async Task<object> GetData(NetPayResponse test)
        public async Task<string> Login(Dictionary<string, object> test)
        {
            var a = await _authService.Login();

            return a;
        }

        [HttpPost("refresh")]
        //public async Task<object> GetData(NetPayResponse test)
        public async Task<string> Refresh(Dictionary<string, object> test)
        {
            var a = await _authService.Refresh();

            return a;
        }

        [HttpPost("check")]
        //public async Task<object> GetData(NetPayResponse test)
        public async Task<string> Check(Dictionary<string, object> test)
        {
            //var a = await _authService.Check();

            return "";
        }

        [HttpPost("payment/charge")]
        public async Task<string> PaymentCharge(PayPalPaymentDto payment)
        {
            var res = await _netPayService.PaymentCharge(payment);

            return res;
        }

        [HttpPost("terminal/response")]
        //public async Task<object> GetData(NetPayResponse test)
        public async Task<object> TerminalResponse(NetPayResponse response)
        {
            //var res = await _netPayService.ProcessResponse(response);

            return new { Code = "00", Message = "Recibido" };
        }
    }
}

// SANDBOX
// http://nubeqa.netpay.com.mx:3334