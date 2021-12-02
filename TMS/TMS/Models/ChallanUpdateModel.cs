using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMS.Models
{
    public class ChallanUpdateModel
    {
        public int chalanid { get; set; }
        public decimal cash { get; set; }
        public decimal bank { get; set; }
        public decimal hsd { get; set; }
        public decimal ofcx { get; set; }
        public string petrol { get; set; }
        public string rcv { get; set; }
    }
}