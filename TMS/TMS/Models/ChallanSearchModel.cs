using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMS.Models
{
    public class ChallanSearchModel
    {
        public string fromdate { get; set; }
        public string todate { get; set; }
        public string cash { get; set; }
        public string bank { get; set; }
        public string hsd { get; set; }
        public string ofc { get; set; }
        public string owner { get; set; }
        public string pan { get; set; }
    }
}