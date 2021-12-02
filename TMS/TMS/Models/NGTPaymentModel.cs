using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TMS.Models
{
    public class NGTPaymentModel
    {
        public int Payment_Id { get; set; }
        public string Chalan_No { get; set; }
        public string TP_Number { get; set; }
        public string Loading_Point { get; set; }
        public string Unloading_Point { get; set; }
        public Nullable<System.DateTime> Chalan_Date { get; set; }
        public Nullable<int> Truck_Number { get; set; }
        public Nullable<int> Owner_Name { get; set; }
        public string PAN { get; set; }
        public Nullable<decimal> Loading_Wt { get; set; }
        public Nullable<decimal> Unloding_Wt { get; set; }
        public Nullable<System.DateTime> Unloading_Date { get; set; }
        public Nullable<decimal> Paid_Qty { get; set; }
        public Nullable<decimal> Rate { get; set; }
        public Nullable<decimal> Freight { get; set; }
        public Nullable<decimal> Cash_Adv { get; set; }
        public Nullable<decimal> Bank_Adv { get; set; }
        public Nullable<decimal> HSD_Adv { get; set; }
        public string Petrol_Pump_Name { get; set; }
        public Nullable<decimal> Shortage { get; set; }
        public Nullable<decimal> Office_Exp { get; set; }
        public Nullable<decimal> Others { get; set; }
        public Nullable<decimal> CTC_Amount { get; set; }
        public Nullable<decimal> TDS_Amount { get; set; }
        public Nullable<decimal> Total_Deduction { get; set; }
        public Nullable<decimal> Net_Payble { get; set; }
        public Nullable<decimal> Toll_Gate { get; set; }
        public Nullable<decimal> Final_Amount { get; set; }
        public Nullable<System.DateTime> Payment_Date { get; set; }
        public string Bank_Name { get; set; }
        public string Cheque_No { get; set; }
        public string Paid_To { get; set; }
        public string PAN_No { get; set; }
        public string Payment_By { get; set; }
        public Nullable<System.DateTime> Received_Date { get; set; }
        public string Reference_No { get; set; }
        public string Voucher_No { get; set; }
        public Nullable<decimal> CC_Amount { get; set; }
        public string Clllected_At { get; set; }
        public Nullable<decimal> Driver_Welfare { get; set; }
        public List<SelectListItem> OwnerList { get; set; }
        public List<SelectListItem> VehicleList { get; set; }
        public List<SelectListItem> challanlist { get; set; }
    }
}