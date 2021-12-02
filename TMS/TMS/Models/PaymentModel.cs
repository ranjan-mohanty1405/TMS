using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMS.Models
{
    public class PaymentModel
    {
        public string faccount { get; set; }
        public string cheque { get; set; }
        public string bank { get; set; }
        public string taccount { get; set; }
        public string other { get; set; }
        public DateTime pdate { get; set; }
        public string Tp_Number { get; set; }
    }  
}