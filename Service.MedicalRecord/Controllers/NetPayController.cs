using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Controllers
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

    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class NetPayController : ControllerBase
    {
        [HttpPost("test")]
        public async Task<object> GetData(TestNetPay test)
        {
            var a = test;

            return new { Code = "00", Message = "Recibido" };
        }
    }
}

// http://nubeqa.netpay.com.mx:3334