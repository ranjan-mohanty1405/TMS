using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TMS.Models
{
    public class ChallanModel
    {
        public int ChalanId { get; set; }
        public string Inv_Number { get; set; }
        public string Challan_No { get; set; }
        public Nullable<System.DateTime> Chalan_Date { get; set; }
        public string TP_Number { get; set; }
        public string Truck_Number { get; set; }
        public Nullable<decimal> Load_Quantity { get; set; }
        public Nullable<decimal> UL_Quantity { get; set; }
        public Nullable<decimal> Rate { get; set; }
        public Nullable<decimal> Off_Expenses { get; set; }
        public Nullable<decimal> Cash_Adv { get; set; }
        public Nullable<decimal> Bank_Adv { get; set; }
        public Nullable<decimal> HSD_Adv { get; set; }
        public string Slip_No { get; set; }
        public string Petrol_Pump_Name { get; set; }
        public Nullable<decimal> Driver_Welfare { get; set; }
        public string Status { get; set; }
        public string Consignor { get; set; }
        public string Consignee { get; set; }
        public string Billing_Party { get; set; }
        public string Loading_Point { get; set; }
        public string Unloading_Point { get; set; }
        public string Truck_Owner_Name { get; set; }
        public string Address { get; set; }
        public string Driver_Name { get; set; }
        public string DL_Number { get; set; }
        public string Contact_No { get; set; }
        public string Ref_Number { get; set; }
        public Nullable<System.DateTime> Receive_Date { get; set; }
        public List<SelectListItem> OwnerList { get; set; }
        public List<SelectListItem> VehicleList { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public Nullable<decimal> others { get; set; }
        public Nullable<decimal> shoratge { get; set; }
        public Nullable<decimal> tds { get; set; }
        public Nullable<decimal> freight { get; set; }
        public Nullable<decimal> totaldeduction { get; set; }
        public Nullable<decimal> Netpayble { get; set; }
        public Nullable<decimal> tollgate { get; set; }
        public Nullable<decimal> Finalpayment { get; set; }
        public DateTime PaymentDate { get; set; }
        public Nullable<decimal> PaidAmount { get; set; }

    }
}