using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mail;
using TMS.Abstract;
using TMS.Models;
using System.Text;
using System.Net;
using System.IO;
using System.Web.Mvc;

namespace TMS.Concrete
{
    public class ApplicationRepository : IApplicationRepository
    {
        TMSEntities con = new TMSEntities();

        public User CheckLogin(string username, string password)
        {
            var data = con.Users.Where(x => x.Username == username && x.Password == password).FirstOrDefault();

            return data;
        }
        public List<vw_vehicle_master> vw_Vehicle_Masters()
        {
            var data = con.vw_vehicle_master.ToList();

            return data;
        }
        public List<vw_vehicle_owner> vw_Vehicle_Owners()
        {
            var data = con.vw_vehicle_owner.ToList();

            return data;
        }
        public int SaveOwner(VehicleOwner vo)
        {
            if(vo.OwnerId!=null&&vo.OwnerId!=0)
            {
                var data = con.VehicleOwners.Where(x => x.OwnerId == vo.OwnerId&&x.IsActive==true&&x.IsDeleted==false).FirstOrDefault();
                data.OwnerName = vo.OwnerName;
                data.Adress = vo.Adress;
                data.PAN = vo.PAN;
                data.ContactNo = vo.ContactNo;
                data.Email = vo.Email;
                con.SaveChanges();
                return 2;
            }
            else
            {
                var data1 = con.VehicleOwners.Where(x => x.PAN == vo.PAN && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
                if(data1!=null)
                {
                    return 3;
                }
                else{
                    VehicleOwner data = new VehicleOwner();
                    data.OwnerName = vo.OwnerName;
                    data.Adress = vo.Adress;
                    data.PAN = vo.PAN;
                    data.ContactNo = vo.ContactNo;
                    data.Email = vo.Email;
                    data.IsActive = true;
                    data.IsDeleted = false;
                    data.CreatedBy = 1;
                    data.CreatedDate = DateTime.Now;
                    con.VehicleOwners.Add(data);
                    con.SaveChanges();
                    string ownerid = data.OwnerId.ToString();
                    data.OwnerEncrypt = Base64StringEncode(ownerid);
                    con.SaveChanges();
                    return 1;
                }
            }
        }
        public bool DeleteOwner(int id)
        {
            var data = con.VehicleOwners.Where(x => x.OwnerId == id && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
            data.IsActive = false;
            data.IsDeleted = true;
            con.SaveChanges();
            return true;
        }
        public VehicleOwner VehicleOwner(string id)
        {
            int oid = Convert.ToInt32(Base64StringDecode(id));
            var data = con.VehicleOwners.Where(x => x.OwnerId == oid).FirstOrDefault();
            return data;
        }
        public int SaveVehicle(VehicleMasterModel vm)
        {
            if (vm.VehicleId != null && vm.VehicleId != 0)
            {
                var data = con.VehicleMasters.Where(x => x.VehicleId == vm.VehicleId && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
                data.OwnerId = vm.OwnerId;
                data.VehicleNo = vm.VehicleNo;
                con.SaveChanges();
                return 2;
            }
            else
            {
                var data1 = con.VehicleMasters.Where(x => x.VehicleNo == vm.VehicleNo && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
                if (data1 != null)
                {
                    return 3;
                }
                else
                {
                    VehicleMaster data = new VehicleMaster();
                    data.OwnerId = vm.OwnerId;
                    data.VehicleNo = vm.VehicleNo;

                    data.IsActive = true;
                    data.IsDeleted = false;
                    data.CreatedBy = 1;
                    data.CreatedDate = DateTime.Now;
                    con.VehicleMasters.Add(data);
                    con.SaveChanges();
                    string vehicleid = data.VehicleId.ToString();
                    data.VehicleEncrypt = Base64StringEncode(vehicleid);
                    con.SaveChanges();
                    return 1;
                }
            }
        }
        public bool DeleteVehicle(int id)
        {
            var data = con.VehicleMasters.Where(x => x.VehicleId == id).FirstOrDefault();
            data.IsActive = false;
            data.IsDeleted = true;
            con.SaveChanges();
            return true;
        }
        public List<SelectListItem> ownerlist()
        {
            var data = con.VehicleOwners.Where(x=>x.IsActive==true&&x.IsDeleted==false).ToList();
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "Select", Value = "0" });

            foreach (var item in data)
            {
                li.Add(new SelectListItem
                {
                    Text = item.OwnerName,
                    Value = item.OwnerId.ToString(),
                });
            }
            return li;
        }
        public List<SelectListItem> vehiclelist(int id)
        {
            var data = con.VehicleMasters.Where(x=>x.OwnerId== id&&x.IsActive == true && x.IsDeleted == false).ToList();
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem{Text = "Select",Value = "0"});
            if (data.Count > 0)
            {
                foreach (var item in data)
                {
                    li.Add(new SelectListItem
                    {
                        Text = item.VehicleNo,
                        Value = item.VehicleId.ToString(),
                    });
                }
            }
            return li;
        }
        public List<SelectListItem> challanlist()
        {
            var data = con.Chalan_Detail.Where(x => x.IsActive == true && x.IsDeleted == false&&(x.IsPaid==null||x.IsPaid==false)).ToList();
            List<SelectListItem> li = new List<SelectListItem>();
            if (data.Count > 0)
            {
                foreach (var item in data)
                {
                    li.Add(new SelectListItem
                    {
                        Text = item.TP_Number,
                        Value = item.TP_Number.ToString(),
                    });
                }
            }
            return li;
        }
        public List<SelectListItem> GetPAN()
        {
            var data = con.VehicleOwners.Where(x => x.IsActive == true && x.IsDeleted == false).ToList();
            List<SelectListItem> li = new List<SelectListItem>();
            if (data.Count > 0)
            {
                foreach (var item in data)
                {
                    li.Add(new SelectListItem
                    {
                        Text = item.PAN,
                        Value = item.OwnerName.ToString(),
                    });
                }
            }
            return li;
        }
        public List<SelectListItem> GetOwner()
        {
            var data = con.VehicleOwners.Where(x => x.IsActive == true && x.IsDeleted == false).ToList();
            List<SelectListItem> li = new List<SelectListItem>();
            if (data.Count > 0)
            {
                foreach (var item in data)
                {
                    li.Add(new SelectListItem
                    {
                        Text = item.OwnerName,
                        Value = item.PAN!=null?item.PAN.ToString():"",
                    });
                }
            }
            return li;
        }
        public VehicleMaster VehicleMaster(string id)
        {
            int vid = Convert.ToInt32(Base64StringDecode(id));
            var data = con.VehicleMasters.Where(x => x.VehicleId == vid).FirstOrDefault();
            return data;
        }
        public string Base64StringEncode(string originalString)
        {
            var bytes = Encoding.UTF8.GetBytes(originalString);

            var encodedString = Convert.ToBase64String(bytes);

            return encodedString;
        }
        public string Base64StringDecode(string encodedString)
        {
            var bytes = Convert.FromBase64String(encodedString);

            var decodedString = Encoding.UTF8.GetString(bytes);

            return decodedString;
        }
        public int SaveChallan(ChallanModel cm)
        {
            int result = 0;
            if (cm.TP_Number!=null&&cm.TP_Number!="")
            {
                Chalan_Detail cd = con.Chalan_Detail.Where(x=>x.TP_Number==cm.TP_Number).FirstOrDefault();
                if (cd != null)
                {
                    cd.Inv_Number = cm.Inv_Number;
                    cd.Challan_No = cm.Challan_No;
                    cd.Chalan_Date = cm.Chalan_Date;
                    cd.TP_Number = cm.TP_Number;
                    cd.Truck_Number = cm.Truck_Number;
                    cd.Load_Quantity = cm.Load_Quantity;
                    cd.UL_Quantity = cm.UL_Quantity;
                    cd.Rate = cm.Rate;
                    //cd.Off_Expenses = cm.Off_Expenses != null ? cm.Off_Expenses : 0;
                    //cd.Cash_Adv = cm.Cash_Adv != null ? cm.Cash_Adv : 0;
                    //cd.Bank_Adv = cm.Bank_Adv != null ? cm.Bank_Adv : 0;
                    //cd.HSD_Adv = cm.HSD_Adv != null ? cm.HSD_Adv : 0;
                    cd.Slip_No = cm.Slip_No;
                    if(cd.UL_Quantity!=null&&cd.UL_Quantity!=0)
                    {
                        cd.Freight = Convert.ToDecimal(cd.UL_Quantity * cd.Rate);
                    }
                    if(cm.Address!=null)
                    {
                        cd.TDS = (cd.Freight * 20) / 100;
                        string Address = cm.Address.Substring(3, 1);
                        if(Address=="P")
                        {
                            cd.TDS = (cd.Freight * 1) / 100;
                        }
                        else
                        {
                            cd.TDS = (cd.Freight * 2) / 100;
                        }
                    }
                    else
                    {
                        cd.TDS = (cd.Freight * 20) / 100;
                    }
                    //cd.Petrol_Pump_Name = cm.Petrol_Pump_Name;
                    cd.Driver_Welfare = cm.Driver_Welfare != null ? cm.Driver_Welfare : 0;
                    cd.Status = cm.Status;
                    cd.Consignor = cm.Consignor;
                    cd.Consignee = cm.Consignee;
                    cd.Billing_Party = cm.Billing_Party;
                    cd.Loading_Point = cm.Loading_Point;
                    cd.Unloading_Point = cm.Unloading_Point;
                    cd.Truck_Owner_Name = cm.Truck_Owner_Name;
                    cd.Address = cm.Address;
                    cd.Driver_Name = cm.Driver_Name;
                    cd.DL_Number = cm.DL_Number;
                    cd.Contact_No = cm.Contact_No;
                    cd.Ref_Number = cm.Ref_Number;
                    //cd.Receive_Date = cm.Receive_Date;
                    con.SaveChanges();
                    result= 2;
                }
                else
                {
                    result= 3;
                }
            }
            return result;
        }
        public int SaveUploadedChallan(List<ChallanModel> challanModels)
        {
            if(challanModels.ToList().Count>0)
            {
                foreach(var cm in challanModels)
                {
                    string tpnumber = cm.TP_Number.Trim();
                    string[] arr = tpnumber.Split('/');
                    string input = arr[1];
                    string output = input.TrimStart(new char[] { '0' });

                    tpnumber = arr[0]+"/" + output;
                    var data = con.Chalan_Detail.Where(x => x.TP_Number == tpnumber).FirstOrDefault();
                    if (data == null)
                    {
                        Chalan_Detail cd = new Chalan_Detail();
                        cd.Inv_Number = InvNumber();
                        cd.TP_Number = cm.TP_Number.Trim();
                        cd.Truck_Number = cm.Truck_Number;
                        cd.Load_Quantity = cm.Load_Quantity;
                        cd.UL_Quantity = cm.UL_Quantity;
                        cd.Rate = cm.Rate;
                        cd.Off_Expenses = cm.Off_Expenses != null ? cm.Off_Expenses : 0;
                        cd.Cash_Adv = cm.Cash_Adv != null ? cm.Cash_Adv : 0;
                        cd.Bank_Adv = cm.Bank_Adv != null ? cm.Bank_Adv : 0;
                        cd.HSD_Adv = cm.HSD_Adv != null ? cm.HSD_Adv : 0;
                        //cd.TDS = 0;
                        //cd.Freight = Convert.ToDecimal(cd.UL_Quantity * cd.Rate);
                        if (cd.UL_Quantity != null && cd.UL_Quantity != 0)
                        {
                            cd.Freight = Convert.ToDecimal(cd.UL_Quantity * cd.Rate);
                        }
                        if (cm.Address != null)
                        {
                            cd.TDS = (cd.Freight * 20) / 100;
                            string Address = cm.Address.Substring(3, 1);
                            if (Address == "P")
                            {
                                cd.TDS = (cd.Freight * 1) / 100;
                            }
                            else
                            {
                                cd.TDS = (cd.Freight * 2) / 100;
                            }
                        }
                        else
                        {
                            cd.TDS = (cd.Freight * 20) / 100;
                        }
                        cd.Driver_Welfare = 0;
                        cd.IsActive = true;
                        cd.IsDeleted = false;
                        con.Chalan_Detail.Add(cd);
                        con.SaveChanges();

                        NGT_Payment ng = new NGT_Payment();
                        ng.TP_Number = cm.TP_Number.Trim();
                        //ng.Truck_Number = cm.Truck_Number;
                        //ng.Loading_Wt = cm.Load_Quantity;
                        //ng.Unloding_Wt = cm.UL_Quantity;
                        //ng.Rate = cm.Rate;
                        //ng.Freight = Convert.ToDecimal(cm.UL_Quantity * cm.Rate);
                        con.NGT_Payment.Add(ng);
                        con.SaveChanges();
                    }
                }
            }
            return 1;
        }
        public Chalan_Detail Chalan_Detail(int id)
        {
            var data = con.Chalan_Detail.Where(x => x.ChalanId == id).FirstOrDefault();
            return data;
        }
        public List<vw_challan_detail> vw_Challan_Details()
        {
            var data = con.vw_challan_detail.ToList();
            return data;
        }
        public bool DeleteChallan(int id)
        {
            var data = con.Chalan_Detail.Where(x => x.ChalanId == id).FirstOrDefault();
            data.IsActive = false;
            data.IsDeleted = true;
            con.SaveChanges();
            return true;
        }
        public string InvNumber()
        {
            string data = "";
            int chalan = con.Chalan_Detail.ToList().Count;
            if(chalan==0)
            {
                data = "INV/" + DateTime.Now.Year + "/0" + 1;
            }
            else
            {
                chalan = chalan + 1;
                data = "INV/" + DateTime.Now.Year + "/0" + chalan;
            }
            return data;
        }
        public string ChalanNumber()
        {
            string data = "";
            int chalan = con.Chalan_Detail.ToList().Count;
            if (chalan == 0)
            {
                data = "CHLN/" + DateTime.Now.Year + "/0" + 1;
            }
            else
            {
                chalan = chalan + 1;
                data = "CHLN/" + DateTime.Now.Year + "/0" + chalan;
            }
            return data;
        }
        public Chalan_Detail BindChalanDetail(string id)
        {
            var data = con.Chalan_Detail.Where(x => x.IsActive == true && x.IsDeleted == false && x.TP_Number == id).FirstOrDefault();

            return data;
        }
        public bool UpdateChallanDetail(List<ChallanUpdateModel> challanUpdateModels)
        {
            if(challanUpdateModels.ToList().Count>0)
            {
                foreach(var item in challanUpdateModels)
                {
                    var data = con.Chalan_Detail.Where(x => x.ChalanId == item.chalanid).FirstOrDefault();
                    if (data != null)
                    {                    
                        data.Off_Expenses = item.ofcx;
                        data.Cash_Adv = item.cash;
                        data.Bank_Adv = item.bank;
                        data.HSD_Adv = item.hsd;
                        if(item.rcv!=null)
                        {
                            data.Receive_Date = Convert.ToDateTime(item.rcv);
                        }
                        if(item.petrol!=null)
                        {
                            data.Petrol_Pump_Name = item.petrol;
                        }
                        con.SaveChanges();                    
                    }
                }
            }
            return true;
        }
        public bool makepayment(List<PaymentModel> data)
        {
            if(data.ToList().Count>0)
            {
                foreach(var item in data)
                {
                    var chln = con.Chalan_Detail.Where(x => x.TP_Number == item.Tp_Number).FirstOrDefault();

                    if(chln != null)
                    {
                        chln.TP_Number = item.Tp_Number;
                        chln.FromAccount = item.faccount;
                        chln.ChequeNo = item.cheque;
                        chln.BankCash = item.bank;
                        chln.ToAccount = item.taccount;
                        chln.OtherPayment = item.other;
                        chln.PaymentDate = item.pdate;
                        chln.IsPaid = true;
                        con.SaveChanges();
                    }
                }
            }
            return true;
        }
    }
}