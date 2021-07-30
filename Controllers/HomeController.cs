using createQrcode.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace createQrcode.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("/create-qr")]
        public IActionResult createQrcode([FromBody] JObject data)
        {
            var obData = new ReqQrcode()
            {
                merchantId = "BIDVCARD011",
                merchantGuid = "970488",
                merchantName = "BIDV CARD 011",
                merchantCateloryCode = "8062",
                storeLable = "BV DA LIEU",
                terminalLable = "T2127777",
                countryCode = "VN",
                merchantCity = "HANOI",
                ccy = "704",
                postalCode = "10000",
                billNumber = (string) data["ma"],
                amount = (string) data["tien"]
            };
            var dataQR = obData;
            var qrcode = new Qrcode(dataQR);
            return Json(new
            {
                data = qrcode.create()
            }); ;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
