using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace createQrcode.Models
{
public class ReqQrcode
{
        public string merchantId { set; get; }
        public string merchantGuid { set; get; } = "970488";
        public string merchantName { set; get; }
        public string merchantCateloryCode { set; get; }
        public string storeLable { set; get; }
        public string terminalLable { set; get; }
        public string countryCode { set; get; } = "VN";
        public string merchantCity { set; get; } = "HANOI";
        public string ccy { set; get; } = "704";
        public string postalCode { set; get; } = "10000";
        public string billNumber { set; get; }
        public string amount { set; get; }
}
}
